using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Aggregations;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Types;

namespace Gos.Services.Search.Aggregations
{
    public class DiscourseYearAggregationProvider : BaseAggregationProvider
    {
        public DiscourseYearAggregationProvider(IAggregatorFactory aggregatorFactory)
            : base(aggregatorFactory)
        {
        }

        public override AggregationType Type => AggregationType.DiscourseYear;

        protected override List<AggregationItem> GetItems(IDictionary<string, int> items)
        {
            return (from item in items
                let year = int.Parse(item.Key)
                orderby item.Value descending, year
                select new AggregationItem()
                {
                    Count = item.Value,
                    Key = year,
                    Title = year.ToString(),
                }).ToList();
        }
    }
}