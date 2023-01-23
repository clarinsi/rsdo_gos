using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using EFCore.BulkExtensions;
using Gos.Core.Entities;
using Gos.ServiceModel.Requests.Corpus;
using Gos.Services.Framework;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.RequestHandlers.Corpus
{
    public class ImportLexiconHandler : IRequestHandler<ImportLexicon, Unit>
    {
        private readonly GosDbContext dbContext;

        public ImportLexiconHandler(GosDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<Unit> Handle(ImportLexicon request, CancellationToken cancellationToken)
        {
            var sourcePath = request.SourcePath;
            if (!File.Exists(sourcePath))
            {
                throw new FileNotFoundException($"File {sourcePath} not found!", sourcePath);
            }

            var data = ImportLexicon(sourcePath);
            await SaveData(data);
            return new Unit();
        }

        private string GetFeature(XElement element, string featName)
        {
            return element?.Elements("feat").SingleOrDefault(e => e.Attribute("att")?.Value == featName)?.Attribute("val")?.Value;
        }

        private void ImportEntry(XmlReader entryReader, Dictionary<string, HashSet<string>> data)
        {
            var entry = XDocument.Load(entryReader);
            var entryEl = entry.Root;

            // Skip if multiword
            var partOfSpeech = GetFeature(entryEl, "besedna_vrsta");
            if (!string.IsNullOrEmpty(partOfSpeech) && partOfSpeech == "večbesedna_enota")
            {
                return;
            }

            // Get lemma
            var lemmaEl = entryEl.Element("Lemma");
            var lemma = GetFeature(lemmaEl, "zapis_oblike");

            // Add lemma
            if (!data.ContainsKey(lemma))
            {
                data.Add(lemma, new HashSet<string>(StringComparer.Ordinal));
            }

            // Get forms
            foreach (var wordFormEl in entryEl.Elements("WordForm"))
            {
                foreach (var representationEl in wordFormEl.Elements("FormRepresentation"))
                {
                    var form = GetFeature(representationEl, "zapis_oblike");
                    if (!data[lemma].Contains(form))
                    {
                        data[lemma].Add(form);
                    }
                }
            }
        }

        private Dictionary<string, HashSet<string>> ImportLexicon(string sourcePath)
        {
            var items = new Dictionary<string, HashSet<string>>(StringComparer.Ordinal);
            using var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read);
            using var xmlReader = XmlReader.Create(stream);
            while (xmlReader.Read())
            {
                if (xmlReader.NodeType == XmlNodeType.Element && xmlReader.LocalName == "LexicalEntry")
                {
                    using var entryReader = xmlReader.ReadSubtree();
                    ImportEntry(entryReader, items);
                }
            }

            return items;
        }

        private async Task SaveData(Dictionary<string, HashSet<string>> data)
        {
            // Get existing lemmas and forms
            var existing = (await dbContext.CorpusForms.ToListAsync()).GroupBy(x => x.Lemma)
                .ToDictionary(g => g.Key, g => g.Select(x => x.StandardForm).ToHashSet(StringComparer.Ordinal), StringComparer.Ordinal);

            // Get lemmas and forms to add
            var missing = new List<CorpusForm>();
            foreach (var item in data)
            {
                var lemma = item.Key;
                foreach (var form in item.Value)
                {
                    if (!existing.ContainsKey(lemma) || !existing[lemma].Contains(form))
                    {
                        missing.Add(
                            new CorpusForm()
                            {
                                Lemma = lemma,
                                StandardForm = form
                            });
                    }
                }
            }

            // Save to database in batches
            await dbContext.BulkInsertAsync(missing);
        }
    }
}