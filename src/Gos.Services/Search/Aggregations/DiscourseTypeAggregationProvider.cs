using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Aggregations;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Types;
using Gos.Services.Framework;
using Microsoft.EntityFrameworkCore;

namespace Gos.Services.Search.Aggregations
{
    public class DiscourseTypeAggregationProvider : BaseAggregationProvider
    {
        private readonly GosDbContext dbContext;

        public DiscourseTypeAggregationProvider(IAggregatorFactory aggregatorFactory, GosDbContext dbContext)
            : base(aggregatorFactory)
        {
            this.dbContext = dbContext;
        }

        public override AggregationType Type => AggregationType.DiscourseType;

        protected override List<AggregationItem> GetItems(IDictionary<string, int> items)
        {
            var ids = items.Select(b => int.Parse(b.Key)).ToList();
            var types = dbContext.DiscourseTypes.Include(e => e.Translations).Where(tt => ids.Contains(tt.Id)).ToList();

            return (from item in items
                let id = int.Parse(item.Key)
                join type in types on id equals type.Id
                orderby item.Value descending, type.Title
                select new AggregationItem
                {
                    Count = item.Value,
                    Key = type.Id,
                    Title = type.Title,
                }).ToList();
        }
    }
}