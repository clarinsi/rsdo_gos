using Gos.Core.Search.Queries;
using Gos.ServiceModel.Types;

namespace Gos.Services.Search.Aggregations
{
    public interface IAggregationProvider
    {
        Aggregation Get<TQuery>(TQuery query)
            where TQuery : Query;
    }
}