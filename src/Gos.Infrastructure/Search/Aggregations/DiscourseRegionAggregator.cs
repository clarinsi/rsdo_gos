using Gos.Infrastructure.Search.Indexes;
using Gos.Infrastructure.Search.QueryBuilders;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.Aggregations
{
    public class DiscourseRegionAggregator : BaseAggregator

    {
        public DiscourseRegionAggregator(IOpenSearchClient client, IIndexProviderFactory indexProviderFactory, IQueryBuilderFactory queryBuilderFactory)
            : base(client, indexProviderFactory, queryBuilderFactory)
        {
        }

        protected override string FieldName => "discourseRegionId";
    }
}