using Gos.ServiceModel.Enums;

namespace Gos.Services.Search.Aggregations
{
    public interface IAggregationProviderFactory
    {
        IAggregationProvider GetProvider(AggregationType aggregationType);
    }
}