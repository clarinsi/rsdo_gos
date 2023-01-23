using Autofac;
using Gos.Core.Search.Queries;

namespace Gos.Infrastructure.Search.QueryHandlers
{
    public class QueryHandlerFactory : IQueryHandlerFactory
    {
        private readonly ILifetimeScope lifetimeScope;

        public QueryHandlerFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IQueryHandler<TQuery, TResult> Get<TQuery, TResult>()
            where TQuery : Query
            where TResult : QueryResult
        {
            return lifetimeScope.Resolve<IQueryHandler<TQuery, TResult>>();
        }
    }
}