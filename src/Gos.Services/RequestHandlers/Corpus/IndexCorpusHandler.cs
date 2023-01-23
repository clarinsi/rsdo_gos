using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Xml.Linq;
using Gos.Core;
using Gos.Core.Entities;
using Gos.Core.Extensions;
using Gos.Core.Model;
using Gos.Core.Search;
using Gos.ServiceModel.Requests.Corpus;
using Gos.Services.Framework;
using Gos.Services.Framework.ConcordanceBuilder;
using Gos.Services.Framework.Fragments;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.RequestHandlers.Corpus
{
    public class IndexCorpusHandler : IRequestHandler<IndexCorpus>
    {
        private readonly GosDbContext dbContext;
        private readonly ISearchEngine searchEngine;
        private readonly IConcordanceBuilder concordanceBuilder;

        public IndexCorpusHandler(GosDbContext dbContext, ISearchEngine searchEngine, IConcordanceBuilder concordanceBuilder)
        {
            this.dbContext = dbContext;
            this.searchEngine = searchEngine;
            this.concordanceBuilder = concordanceBuilder;
        }

        public async Task<Unit> Handle(IndexCorpus request, CancellationToken cancellationToken)
        {
            // Recreate schema
            await searchEngine.DeleteSchema();
            await searchEngine.CreateSchema();

            // Index
            await Index();

            // Commit changes
            await searchEngine.Commit();

            return new Unit();
        }

        private async Task Index()
        {
            // Loop through statements
            var lastId = 0;
            List<Statement> statements = null;
            while ((statements = dbContext.Statements.AsNoTracking()
                .Include(s => s.Discourse)
                .Include(s => s.Discourse.Type)
                .Include(s => s.Discourse.Channel)
                .Include(s => s.Discourse.Event)
                .Include(s => s.Discourse.Region)
                .Include(s => s.Speaker)
                .Include(s => s.Speaker.Age)
                .Include(s => s.Speaker.Education)
                .Include(s => s.Speaker.Language)
                .Include(s => s.Speaker.Region1)
                .Include(s => s.Speaker.Sex)
                .Where(s => s.Id > lastId)
                .OrderBy(s => s.Id)
                .Take(5000)
                .ToList()).Any())
            {
                await IndexStatements(statements);
                lastId = statements[statements.Count - 1].Id;
            }
        }

        private async Task IndexStatements(List<Statement> statements)
        {
            var batch = new List<Core.Model.Concordance>();
            foreach (var statement in statements)
            {
                var tokens = await dbContext.Tokens.Include(t => t.Segment)
                    .Where(t => t.Segment.Statement.Id == statement.Id)
                    .OrderBy(t => t.Segment.Order)
                    .ThenBy(t => t.Order)
                    .ToListAsync();
                var concordances = concordanceBuilder.GetConcordances(statement, tokens);
                batch.AddRange(concordances);
                if (batch.Count > 5000)
                {
                    await searchEngine.Index(batch);
                    batch.Clear();
                }
            }

            if (batch.Count > 0)
            {
                await searchEngine.Index(batch);
            }
        }
    }
}