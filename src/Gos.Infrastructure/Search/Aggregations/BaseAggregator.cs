using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Aggregations;
using Gos.Core.Search.Queries;
using Gos.Infrastructure.Search.Dtos;
using Gos.Infrastructure.Search.Indexes;
using Gos.Infrastructure.Search.QueryBuilders;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.Aggregations
{
    public abstract class BaseAggregator : IAggregator
    {
        private readonly IOpenSearchClient client;
        private readonly IIndexProviderFactory indexProviderFactory;
        private readonly IQueryBuilderFactory queryBuilderFactory;

        protected BaseAggregator(IOpenSearchClient client, IIndexProviderFactory indexProviderFactory, IQueryBuilderFactory queryBuilderFactory)
        {
            this.client = client;
            this.indexProviderFactory = indexProviderFactory;
            this.queryBuilderFactory = queryBuilderFactory;
        }

        protected abstract string FieldName { get; }

        public IDictionary<string, int> Get<TQuery>(TQuery query)
            where TQuery : Query
        {
            // Build query for elastic
            var queryBuilder = queryBuilderFactory.GetBuilder<TQuery>();
            var elasticQuery = queryBuilder.Build(query);

            // Get and execute search request
            var request = GetSearchRequest(elasticQuery);
            var response = client.Search<EsConcordanceDto>(request);

            // Read response
            return ReadAggregation(response);
        }

        private SearchRequest GetSearchRequest(QueryContainer query)
        {
            var indexProvider = indexProviderFactory.GetProvider<EsConcordanceDto>();
            var indexName = indexProvider.IndexName;

            return new SearchRequest(indexName)
            {
                From = 0,
                Size = 0,
                Query = query,
                Aggregations = new AggregationDictionary()
                {
                    {
                        "gos_agg", new TermsAggregation("terms")
                        {
                            Field = FieldName,
                            Size = 100,
                        }
                    },
                },
            };
        }

        private static IDictionary<string, int> ReadAggregation(ISearchResponse<EsConcordanceDto> response)
        {
            var terms = response.Aggregations.Terms("gos_agg");
            return terms?.Buckets?.ToDictionary(x => x.Key, x => x.DocCount.HasValue ? (int)x.DocCount.Value : 0);
        }
    }
}