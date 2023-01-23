using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.Core.Search.Queries;

namespace Gos.Core.Search
{
    public interface ISearchEngine
    {
        Task Commit();

        Task CreateSchema();

        Task DeleteSchema();

        Task Index<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class;

        TResult Search<TQuery, TResult>(TQuery query)
            where TQuery : Query
            where TResult : QueryResult;
    }
}