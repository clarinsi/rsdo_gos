using System.Threading.Tasks;
using Gos.Core.Search.Queries;

namespace Gos.Services.Search.QueryFactories
{
    public interface IQueryFactory<TRequest, TQuery>
        where TQuery : Query
    {
        Task<TQuery> GetQuery(TRequest request);
    }
}