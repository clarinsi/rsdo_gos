using System.Collections.Generic;
using Autofac;

namespace Gos.Infrastructure.Search.Indexes
{
    public class IndexProviderFactory : IIndexProviderFactory
    {
        private readonly ILifetimeScope lifetimeScope;

        public IndexProviderFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IEnumerable<IIndexProvider> GetAllProviders()
        {
            return lifetimeScope.Resolve<IEnumerable<IIndexProvider>>();
        }

        public IIndexProvider<TDocument> GetProvider<TDocument>()
        {
            return lifetimeScope.Resolve<IIndexProvider<TDocument>>();
        }
    }
}