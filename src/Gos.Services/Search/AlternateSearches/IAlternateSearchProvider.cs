using System.Threading.Tasks;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Types;
using MediatR;

namespace Gos.Services.Search.AlternateSearches
{
    public interface IAlternateSearchProvider<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        AlternateSearchType Type { get; }

        Task<AlternateSearch<TRequest>> Get(TRequest request);
    }
}