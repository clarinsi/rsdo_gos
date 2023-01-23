using Gos.Infrastructure.Search.Dtos;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.Indexes
{
    public class ConcordanceIndexProvider : BaseIndexProvider<EsConcordanceDto>
    {
        public ConcordanceIndexProvider(IOpenSearchClient client)
            : base(client)
        {
        }
    }
}