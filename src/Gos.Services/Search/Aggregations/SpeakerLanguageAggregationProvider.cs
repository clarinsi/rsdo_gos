using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Aggregations;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Types;
using Gos.Services.Framework;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.Search.Aggregations
{
    public class SpeakerLanguageAggregationProvider : BaseAggregationProvider
    {
        private readonly GosDbContext dbContext;

        public SpeakerLanguageAggregationProvider(IAggregatorFactory aggregatorFactory, GosDbContext dbContext)
            : base(aggregatorFactory)
        {
            this.dbContext = dbContext;
        }

        public override AggregationType Type => AggregationType.SpeakerLanguage;

        protected override List<AggregationItem> GetItems(IDictionary<string, int> items)
        {
            var ids = items.Select(b => int.Parse(b.Key)).ToList();
            var types = dbContext.SpeakerLanguages.Include(e => e.Translations).Where(tt => ids.Contains(tt.Id)).ToList();

            return (from item in items
                let id = int.Parse(item.Key)
                join language in types on id equals language.Id
                orderby item.Value descending, language.Title
                select new AggregationItem
                {
                    Count = item.Value,
                    Key = language.Id,
                    Title = language.Title,
                }).ToList();
        }
    }
}