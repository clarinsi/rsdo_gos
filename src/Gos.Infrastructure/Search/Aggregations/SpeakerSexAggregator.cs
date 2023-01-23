using Gos.Infrastructure.Search.Indexes;
using Gos.Infrastructure.Search.QueryBuilders;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.Aggregations
{
    public class SpeakerSexAggregator : BaseAggregator
    {
        public SpeakerSexAggregator(IOpenSearchClient client, IIndexProviderFactory indexProviderFactory, IQueryBuilderFactory queryBuilderFactory)
            : base(client, indexProviderFactory, queryBuilderFactory)
        {
        }

        protected override string FieldName => "speakerSexId";
    }
}