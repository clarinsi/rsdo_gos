using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Aggregations;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Types;

namespace Gos.Services.Search.Aggregations
{
    public class LemmaAggregationProvider : BaseAggregationProvider
    {
        public LemmaAggregationProvider(IAggregatorFactory aggregatorFactory)
            : base(aggregatorFactory)
        {
        }

        public override AggregationType Type => AggregationType.Lemma;

        protected override List<AggregationItem> GetItems(IDictionary<string, int> items)
        {
            return (from item in items
                let lemma = item.Key
                orderby item.Value descending, lemma
                select new AggregationItem()
                {
                    Count = item.Value,
                    Key = lemma,
                    Title = lemma,
                }).ToList();
        }
    }
}