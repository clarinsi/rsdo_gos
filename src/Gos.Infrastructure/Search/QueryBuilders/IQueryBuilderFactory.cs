using Gos.Core.Search.Queries;

namespace Gos.Infrastructure.Search.QueryBuilders
{
    public interface IQueryBuilderFactory
    {
        IQueryBuilder<TQuery> GetBuilder<TQuery>()
            where TQuery : Query;
    }
}