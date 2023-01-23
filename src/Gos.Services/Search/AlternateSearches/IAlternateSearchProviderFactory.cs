using Gos.ServiceModel.Enums;
using MediatR;

namespace Gos.Services.Search.AlternateSearches
{
    public interface IAlternateSearchProviderFactory
    {
        IAlternateSearchProvider<TRequest, TResponse> GetProvider<TRequest, TResponse>(AlternateSearchType type)
            where TRequest : IRequest<TResponse>;
    }
}