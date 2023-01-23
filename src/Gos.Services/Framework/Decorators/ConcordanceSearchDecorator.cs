using System.Threading;
using System.Threading.Tasks;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Services.QueryParser;
using MediatR;

namespace Gos.Services.Framework.Decorators
{
    public class ConcordanceSearchDecorator<TRequest, TResponse> : IRequestHandler<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        private readonly IRequestHandler<TRequest, TResponse> decorated;
        private readonly IQueryParser queryParser;

        public ConcordanceSearchDecorator(IRequestHandler<TRequest, TResponse> decorated, IQueryParser queryParser)
        {
            this.decorated = decorated;
            this.queryParser = queryParser;
        }

        public Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken)
        {
            if (request is BaseConcordanceSearch<TResponse> search)
            {
                if (!string.IsNullOrEmpty(search.Query))
                {
                    var parsed = queryParser.Parse(search.Query, search.TranscriptionType);
                    search.MainWord = parsed.mainWord;
                    search.WordsInContext = parsed.wordsInContext;
                }
            }

            return decorated.Handle(request, cancellationToken);
        }
    }
}