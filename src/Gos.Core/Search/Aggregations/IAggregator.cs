using System.Collections.Generic;
using Gos.Core.Search.Queries;

namespace Gos.Core.Search.Aggregations
{
    public interface IAggregator
    {
        IDictionary<string, int> Get<TQuery>(TQuery query)
            where TQuery : Query;
    }
}