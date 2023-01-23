using Gos.Core.Search.Queries;

namespace Gos.Infrastructure.Search.QueryHandlers
{
    public interface IQueryHandlerFactory
    {
        IQueryHandler<TQuery, TResult> Get<TQuery, TResult>()
            where TQuery : Query
            where TResult : QueryResult;
    }
}