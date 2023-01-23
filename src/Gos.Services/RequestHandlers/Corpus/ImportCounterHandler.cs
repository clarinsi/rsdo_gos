using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using EFCore.BulkExtensions;
using Gos.Core;
using Gos.Core.Entities;
using Gos.ServiceModel.Requests.Corpus;
using Gos.Services.Framework;
using Gos.Services.Framework.Fragments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.RequestHandlers.Corpus
{
    public class ImportCounterHandler : IRequestHandler<ImportCounters, Unit>
    {
        private readonly GosDbContext dbContext;
        private readonly IFragmentParser segmentParser;

        public ImportCounterHandler(GosDbContext dbContext, IFragmentParserFactory fragmentParserFactory)
        {
            this.dbContext = dbContext;
            this.segmentParser = fragmentParserFactory.GetParser(FragmentType.Segment);
        }

        public async Task<Unit> Handle(ImportCounters request, CancellationToken cancellationToken)
        {
            // Create object for statistics
            var stats = new Stats();

            // Import data
            await Import(request.SourcePath, stats, cancellationToken);

            // Save statistics into database
            await SaveStatistics(stats);

            return new Unit();
        }

        private async Task Import(string sourcePath, Stats stats, CancellationToken cancellationToken)
        {
            using (var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                var xmlDocument = await XDocument.LoadAsync(stream, LoadOptions.None, cancellationToken);
                var corpusEl = xmlDocument.Element(Constants.TeiNs + "teiCorpus");

                // Import texts
                var sourceFolder = Path.GetDirectoryName(sourcePath);
                foreach (var includeEl in corpusEl.Elements(Constants.IncludeNs + "include"))
                {
                    var textFilePath = Path.Combine(sourceFolder, includeEl.Attribute("href")?.Value);
                    await ImportTextFile(textFilePath, stats);
                }
            }
        }

        private async Task ImportTextFile(string sourcePath, Stats stats)
        {
            using (var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                using (var xmlReader = XmlReader.Create(stream))
                {
                    while (xmlReader.Read())
                    {
                        if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.Name == "TEI" && xmlReader.NamespaceURI == Constants.TeiNamespace)
                        {
                            using (var discourseReader = xmlReader.ReadSubtree())
                            {
                                var discourseEl = XElement.Load(discourseReader);
                                await ImportDiscourse(discourseEl, stats);
                            }
                        }
                    }
                }
            }
        }

        private async Task ImportDiscourse(XElement discourseEl, Stats stats)
        {
            stats.Discourses++;

            // Text
            var textEl = discourseEl.Element(Constants.TeiNs + "text");
            await ImportText(textEl, stats);
        }

        private async Task ImportText(XElement textEl, Stats stats)
        {
            var bodyEl = textEl.Element(Constants.TeiNs + "body");

            // Loop through statements
            foreach (var statementEl in bodyEl.Elements(Constants.TeiNs + "u"))
            {
                await ImportStatement(statementEl, stats);
            }
        }

        private async Task ImportStatement(XElement statementEl, Stats stats)
        {
            stats.Statements++;

            foreach (var segEl in statementEl.Elements(Constants.TeiNs + "seg"))
            {
                // Get tokens for segment
                var tokens = segmentParser.GetTokens(segEl);
                foreach (var token in tokens)
                {
                    stats.Words++;

                    if (!string.IsNullOrEmpty(token.StandardForm) && !string.IsNullOrEmpty(token.Lemma))
                    {
                        var standardForm = token.StandardForm.ToLower();
                        if (!stats.CorpusForms.ContainsKey(standardForm))
                        {
                            stats.CorpusForms.Add(standardForm, new HashSet<string>());
                        }

                        if (!stats.CorpusForms[standardForm].Contains(token.Lemma))
                        {
                            stats.CorpusForms[standardForm].Add(token.Lemma);
                        }
                    }
                }
            }
        }

        private async Task SaveStatistics(Stats stats)
        {
            // Save corpus forms
            var forms = stats.CorpusForms.SelectMany(
                    x => x.Value.Select(
                        l => new CorpusForm
                        {
                            StandardForm = x.Key,
                            Lemma = l,
                        }))
                .ToList();
            await dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE \"{nameof(dbContext.CorpusForms)}\"");
            await dbContext.BulkInsertAsync(forms);

            // Save global statistics
            var counters = new CorpusCounter
            {
                Discourses = stats.Discourses,
                Statements = stats.Statements,
                Words = stats.Words,
            };
            await dbContext.Database.ExecuteSqlRawAsync($"TRUNCATE TABLE \"{nameof(dbContext.CorpusCounters)}\"");
            await dbContext.CorpusCounters.AddAsync(counters);

            // Save changes
            await dbContext.SaveChangesAsync();
        }

        private class Stats
        {
            public IDictionary<string, HashSet<string>> CorpusForms = new Dictionary<string, HashSet<string>>();

            public int Discourses { get; set; }

            public int Statements { get; set; }

            public int Words { get; set; }
        }
    }
}