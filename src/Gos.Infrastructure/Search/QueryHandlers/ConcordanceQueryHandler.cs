using System;
using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Queries.Concordance;
using Gos.Infrastructure.Search.Dtos;
using Gos.Infrastructure.Search.Indexes;
using Gos.Infrastructure.Search.QueryBuilders;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.QueryHandlers
{
    public class ConcordanceQueryHandler : IQueryHandler<ConcordanceQuery, ConcordanceQueryResult>
    {
        private readonly IOpenSearchClient client;
        private readonly IIndexProviderFactory indexProviderFactory;
        private readonly IQueryBuilderFactory queryBuilderFactory;

        public ConcordanceQueryHandler(IOpenSearchClient client, IIndexProviderFactory indexProviderFactory, IQueryBuilderFactory queryBuilderFactory)
        {
            this.client = client;
            this.indexProviderFactory = indexProviderFactory;
            this.queryBuilderFactory = queryBuilderFactory;
        }

        public ConcordanceQueryResult Handle(ConcordanceQuery query)
        {
            // Get request
            var request = GetRequest(query);

            // Execute search
            var response = client.Search<EsConcordanceDto>(request);
            if (!response.IsValid)
            {
                throw new Exception($"Invalid response from Elastic: {response.DebugInformation}!");
            }

            return new ConcordanceQueryResult()
            {
                Total = response.Total,
                Items = GetItems(response),
            };
        }

        private SearchRequest GetRequest(ConcordanceQuery query)
        {
            // Get criteria query
            var queryBuilder = queryBuilderFactory.GetBuilder<ConcordanceQuery>();
            var criteriaQuery = queryBuilder.Build(query);

            var indexName = indexProviderFactory.GetProvider<EsConcordanceDto>().IndexName;
            return new SearchRequest(indexName)
            {
                From = query.From,
                Size = query.Size,
                TrackTotalHits = true,
                Query = criteriaQuery,
                StoredFields = new[]
                {
                    "discourseId",
                    "statementOrder",
                    "tokenOrder"
                }
            };
        }

        private List<ConcordanceQueryResultItem> GetItems(ISearchResponse<EsConcordanceDto> response)
        {
            return response.Hits.Select(
                    h => new ConcordanceQueryResultItem
                    {
                        DiscourseId = h.Fields.Value<int>("discourseId"),
                        StatementOrder = h.Fields.Value<int>("statementOrder"),
                        TokenOrder = h.Fields.Value<int>("tokenOrder")
                    })
                .ToList();
        }
    }
}