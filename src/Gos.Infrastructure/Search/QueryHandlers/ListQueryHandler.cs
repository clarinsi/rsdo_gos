using System;
using System.Collections.Generic;
using System.Linq;
using Gos.Core.Search.Queries.List;
using Gos.Infrastructure.Search.Dtos;
using Gos.Infrastructure.Search.Indexes;
using Gos.Infrastructure.Search.QueryBuilders;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.QueryHandlers
{
    public class ListQueryHandler : IQueryHandler<ListQuery, ListQueryResult>
    {
        private readonly IOpenSearchClient client;
        private readonly IIndexProviderFactory indexProviderFactory;
        private readonly IQueryBuilderFactory queryBuilderFactory;

        public ListQueryHandler(IOpenSearchClient client, IIndexProviderFactory indexProviderFactory, IQueryBuilderFactory queryBuilderFactory)
        {
            this.client = client;
            this.indexProviderFactory = indexProviderFactory;
            this.queryBuilderFactory = queryBuilderFactory;
        }

        public ListQueryResult Handle(ListQuery query)
        {
            // Get criteria query
            var queryBuilder = queryBuilderFactory.GetBuilder<ListQuery>();
            var criteriaQuery = queryBuilder.Build(query);

            var items = new List<ListQueryResultItem>();
            CompositeKey afterKey = null;
            do
            {
                // Get request and execute search
                var request = GetRequest(criteriaQuery, query.GroupByMsd, afterKey);
                var response = client.Search<EsConcordanceDto>(request);
                if (!response.IsValid)
                {
                    throw new Exception($"Invalid response from Elastic: {response.DebugInformation}!");
                }

                if (response.Aggregations.ContainsKey("composite"))
                {
                    var composite = response.Aggregations.Composite("composite");
                    foreach (var bucket in composite.Buckets)
                    {
                        var values = bucket.Key.Values.ToArray();
                        var resultItem = new ListQueryResultItem
                        {
                            ConversationalForm = values[0].ToString(),
                            StandardForm = values[1].ToString(),
                            Frequency = (int)bucket.DocCount.Value,
                        };

                        if (query.GroupByMsd)
                        {
                            resultItem.Msd = values[2].ToString();
                        }

                        items.Add(resultItem);
                    }

                    // Exit if there is nothing more to read
                    if (composite.Buckets.Count < 1000)
                    {
                        break;
                    }

                    afterKey = composite.AfterKey;
                }
            }
            while (true);

            return new ListQueryResult()
            {
                Items = items,
                Total = items.Count,
            };
        }

        private SearchRequest GetRequest(QueryContainer criteriaQuery, bool groupByMsd, CompositeKey afterKey)
        {
            // Get sources for aggregations
            var sources = new List<ICompositeAggregationSource>
            {
                new TermsCompositeAggregationSource("conversational")
                {
                    Field = "token.conversational",
                },
                new TermsCompositeAggregationSource("standard")
                {
                    Field = "token.standard",
                }
            };

            if (groupByMsd)
            {
                sources.Add(
                    new TermsCompositeAggregationSource("msd")
                    {
                        Field = "token.msd",
                    });
            }

            // Get index name
            var indexName = indexProviderFactory.GetProvider<EsConcordanceDto>().IndexName;

            // Compose request
            return new SearchRequest(indexName)
            {
                From = 0,
                Size = 0,
                TrackTotalHits = true,
                Query = criteriaQuery,
                Aggregations = new AggregationDictionary()
                {
                    {
                        "composite", new CompositeAggregation("composite")
                        {
                            Sources = sources,
                            Size = 1000,
                            After = afterKey,
                        }
                    }
                }
            };
        }
    }
}