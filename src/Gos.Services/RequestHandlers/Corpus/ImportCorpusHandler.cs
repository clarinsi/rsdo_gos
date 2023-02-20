using System;
using System.IO;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gos.Core;
using Gos.ServiceModel.Requests.Corpus;
using Gos.Services.Framework;
using Gos.Services.Framework.Fragments;
using MediatR;

namespace Gos.Services.RequestHandlers.Corpus
{
    public partial class ImportCorpusHandler : IRequestHandler<ImportCorpus>
    {
        private readonly GosDbContext dbContext;
        private readonly IFragmentParser segmentParser;

        public ImportCorpusHandler(GosDbContext dbContext, IFragmentParserFactory fragmentParserFactory)
        {
            this.dbContext = dbContext;
            segmentParser = fragmentParserFactory.GetParser(FragmentType.Segment);
        }

        public async Task<Unit> Handle(ImportCorpus request, CancellationToken cancellationToken)
        {
            // Initialize database
            await InitializeDatabase();

            // Import data
            await Import(request.SourcePath, cancellationToken);

            return new Unit();
        }

        private async Task Import(string sourcePath, CancellationToken cancellationToken)
        {
            using (var stream = new FileStream(sourcePath, FileMode.Open, FileAccess.Read))
            {
                var xmlDocument = await XDocument.LoadAsync(stream, LoadOptions.None, cancellationToken);
                var corpusEl = xmlDocument.Element(Constants.TeiNs + "teiCorpus");

                // teiHeader
                var headerEl = corpusEl.Element(Constants.TeiNs + "teiHeader");

                // profileDesc
                var profileDescEl = headerEl.Element(Constants.TeiNs + "profileDesc");

                // particDesc
                var particDescEl = profileDescEl.Element(Constants.TeiNs + "particDesc");

                // listPerson
                foreach (var listPersonEl in particDescEl.Elements(Constants.TeiNs + "listPerson"))
                {
                    // Import speakers
                    await ImportSpeakers(listPersonEl);
                }

                // Import texts
                var sourceFolder = Path.GetDirectoryName(sourcePath);
                foreach (var includeEl in corpusEl.Elements(Constants.IncludeNs + "include"))
                {
                    var textFilePath = Path.Combine(sourceFolder, includeEl.Attribute("href")?.Value);
                    await ImportTextFile(textFilePath);
                }
            }
        }

        private async Task InitializeDatabase()
        {
            // Recreate database
            await dbContext.Database.EnsureDeletedAsync();
            await dbContext.Database.EnsureCreatedAsync();

            //  Seed data
            await dbContext.SeedDataAsync();
        }
    }
}