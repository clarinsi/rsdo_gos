using Autofac;
using Gos.Core.Search.Queries;

namespace Gos.Infrastructure.Search.QueryBuilders
{
    public class QueryBuilderFactory : IQueryBuilderFactory
    {
        private readonly ILifetimeScope lifetimeScope;

        public QueryBuilderFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IQueryBuilder<TQuery> GetBuilder<TQuery>()
            where TQuery : Query
        {
            return lifetimeScope.Resolve<IQueryBuilder<TQuery>>();
        }
    }
}