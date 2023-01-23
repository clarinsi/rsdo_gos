using System.Threading;
using System.Threading.Tasks;
using Gos.Core.Search;
using Gos.ServiceModel.Requests.Corpus;
using MediatR;

namespace Gos.Services.RequestHandlers.Corpus
{
    public class DeleteCorpusIndexHandler : IRequestHandler<DeleteCorpusIndex>
    {
        private readonly ISearchEngine searchEngine;

        public DeleteCorpusIndexHandler(ISearchEngine searchEngine)
        {
            this.searchEngine = searchEngine;
        }

        public async Task<Unit> Handle(DeleteCorpusIndex request, CancellationToken cancellationToken)
        {
            await searchEngine.DeleteSchema();

            return new Unit();
        }
    }
}