using Autofac.Features.Indexed;
using Gos.Core.Search.Aggregations;
using Gos.ServiceModel.Enums;

namespace Gos.Infrastructure.Search.Aggregations
{
    public class AggregatorFactory : IAggregatorFactory
    {
        private readonly IIndex<AggregationType, IAggregator> aggregators;

        public AggregatorFactory(IIndex<AggregationType, IAggregator> aggregators)
        {
            this.aggregators = aggregators;
        }

        public IAggregator GetAggregator(AggregationType aggregationType) => aggregators[aggregationType];
    }
}