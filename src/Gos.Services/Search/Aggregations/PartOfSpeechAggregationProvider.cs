using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Aggregations;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Types;
using Gos.Services.Framework;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.Search.Aggregations
{
    public class PartOfSpeechAggregationProvider : BaseAggregationProvider
    {
        private readonly GosDbContext dbContext;

        public PartOfSpeechAggregationProvider(IAggregatorFactory aggregatorFactory, GosDbContext dbContext)
            : base(aggregatorFactory)
        {
            this.dbContext = dbContext;
        }

        public override AggregationType Type => AggregationType.PartOfSpeech;

        protected override List<AggregationItem> GetItems(IDictionary<string, int> items)
        {
            var ids = items.Select(b => int.Parse(b.Key)).ToList();
            var partOfSpeeches = dbContext.PartOfSpeeches.Include(p => p.Translations).Where(pp => ids.Contains(pp.Id)).ToList();

            return (from item in items
                let id = int.Parse(item.Key)
                join partOfSpeech in partOfSpeeches on id equals partOfSpeech.Id
                orderby item.Value descending, partOfSpeech.Title
                select new AggregationItem
                {
                    Count = item.Value,
                    Key = partOfSpeech.Id,
                    Title = partOfSpeech.Title,
                }).ToList();
        }
    }
}