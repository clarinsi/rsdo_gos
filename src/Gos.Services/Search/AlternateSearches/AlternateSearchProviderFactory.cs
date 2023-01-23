using Autofac;
using Gos.ServiceModel.Enums;
using MediatR;

namespace Gos.Services.Search.AlternateSearches
{
    public class AlternateSearchProviderFactory : IAlternateSearchProviderFactory
    {
        private readonly ILifetimeScope lifetimeScope;

        public AlternateSearchProviderFactory(ILifetimeScope lifetimeScope)
        {
            this.lifetimeScope = lifetimeScope;
        }

        public IAlternateSearchProvider<TRequest, TResponse> GetProvider<TRequest, TResponse>(AlternateSearchType type)
            where TRequest : IRequest<TResponse>
        {
            return lifetimeScope.ResolveKeyed<IAlternateSearchProvider<TRequest, TResponse>>(type);
        }
    }
}