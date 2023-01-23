using System.Collections.Generic;
using Gos.Core.Search.Aggregations;
using Gos.Core.Search.Queries;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Types;

namespace Gos.Services.Search.Aggregations
{
    public abstract class BaseAggregationProvider : IAggregationProvider
    {
        private readonly IAggregatorFactory aggregatorFactory;

        protected BaseAggregationProvider(IAggregatorFactory aggregatorFactory)
        {
            this.aggregatorFactory = aggregatorFactory;
        }

        public abstract AggregationType Type { get; }

        public Aggregation Get<TQuery>(TQuery query)
            where TQuery : Query
        {
            // Run aggregator
            var aggregator = aggregatorFactory.GetAggregator(Type);
            var items = aggregator.Get(query);

            return new Aggregation()
            {
                Items = GetItems(items),
                Type = Type,
            };
        }

        protected abstract List<AggregationItem> GetItems(IDictionary<string, int> items);
    }
}