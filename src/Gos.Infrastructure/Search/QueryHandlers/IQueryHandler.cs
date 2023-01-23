using Gos.Core.Search.Queries;

namespace Gos.Infrastructure.Search.QueryHandlers
{
    public interface IQueryHandler<TQuery, TResult>
        where TQuery : Query
        where TResult : QueryResult
    {
        TResult Handle(TQuery query);
    }
}