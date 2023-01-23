using Autofac.Features.Indexed;
using Gos.ServiceModel.Enums;

namespace Gos.Services.Search.Aggregations
{
    public class AggregationProviderFactory : IAggregationProviderFactory
    {
        private readonly IIndex<AggregationType, IAggregationProvider> providers;

        public AggregationProviderFactory(IIndex<AggregationType, IAggregationProvider> providers)
        {
            this.providers = providers;
        }

        public IAggregationProvider GetProvider(AggregationType aggregationType) => providers[aggregationType];
    }
}