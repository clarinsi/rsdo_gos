using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Gos.Core.Entities;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Extensions;
using Gos.Services.Services.StatementService;

namespace Gos.Services.RequestHandlers.Concordance
{
    public abstract class BaseConcordanceHandler
    {
        protected readonly IStatementService StatementService;

        protected BaseConcordanceHandler(IStatementService statementService)
        {
            this.StatementService = statementService;
        }

        protected async Task<List<ConcordanceSearchResponseItem>> GetResponseItems(
            ConcordanceQuery query,
            List<ConcordanceQueryResultItem> items,
            bool loadSoundFiles = false)
        {
            var result = new List<ConcordanceSearchResponseItem>();
            foreach (var item in items)
            {
                result.Add(await GetResponseItem(query, item, loadSoundFiles));
            }

            return result;
        }

        private async Task<ConcordanceSearchResponseItem> GetResponseItem(ConcordanceQuery query, ConcordanceQueryResultItem item, bool loadSoundFiles)
        {
            // Get discourse tokens
            var orderStart = item.TokenOrder - 10;
            var orderEnd = item.TokenOrder + 10;

            // Get predicate for token context limitation
            Expression<Func<Token, bool>> predicate = t =>
                t.Segment.Statement.Discourse.Id == item.DiscourseId && t.DiscourseOrder >= orderStart && t.DiscourseOrder <= orderEnd;

            // Get tokens
            var tokens = await StatementService.GetTokens(predicate);

            // Get center token
            var centerTokenIndex = tokens.FindIndex(t => t.TokenOrder == item.TokenOrder);

            // Highlight tokens
            tokens = tokens.Highlight(query, centerTokenIndex);

            // Get sound files
            var soundFiles = loadSoundFiles ? await StatementService.GetSoundFiles(predicate) : null;

            // Get contexts
            return new ConcordanceSearchResponseItem
            {
                LeftContext = tokens.GetRange(0, centerTokenIndex),
                CenterContext = tokens[centerTokenIndex],
                RightContext = tokens.GetRange(centerTokenIndex + 1, tokens.Count - centerTokenIndex - 1),
                SoundFiles = soundFiles,
            };
        }
    }
}