using Gos.Core.Search.Queries;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.QueryBuilders
{
    public interface IQueryBuilder<TQuery>
        where TQuery : Query
    {
        QueryContainer Build(TQuery query);
    }
}