using Gos.ServiceModel.Enums;

namespace Gos.Core.Search.Aggregations
{
    public interface IAggregatorFactory
    {
        IAggregator GetAggregator(AggregationType aggregationType);
    }
}