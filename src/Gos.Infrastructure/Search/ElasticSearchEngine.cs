using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.Core.Search;
using Gos.Core.Search.Queries;
using Gos.Infrastructure.Search.Converters;
using Gos.Infrastructure.Search.Dtos;
using Gos.Infrastructure.Search.Indexes;
using Gos.Infrastructure.Search.QueryHandlers;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search
{
    public class ElasticSearchEngine : ISearchEngine
    {
        private readonly IOpenSearchClient client;
        private readonly IEsDtoConverterFactory esDtoConverterFactory;
        private readonly IIndexProviderFactory indexProviderFactory;
        private readonly IQueryHandlerFactory queryHandlerFactory;

        public ElasticSearchEngine(
            IOpenSearchClient client,
            IEsDtoConverterFactory esDtoConverterFactory,
            IIndexProviderFactory indexProviderFactory,
            IQueryHandlerFactory queryHandlerFactory)
        {
            this.client = client;
            this.esDtoConverterFactory = esDtoConverterFactory;
            this.indexProviderFactory = indexProviderFactory;
            this.queryHandlerFactory = queryHandlerFactory;
        }

        public async Task Commit()
        {
            foreach (var indexProvider in indexProviderFactory.GetAllProviders())
            {
                if (await indexProvider.IndexExists())
                {
                    await indexProvider.RefreshIndex();
                }
            }
        }

        public async Task CreateSchema()
        {
            foreach (var indexProvider in indexProviderFactory.GetAllProviders())
            {
                if (!await indexProvider.IndexExists())
                {
                    await indexProvider.CreateIndex();
                }
            }
        }

        public async Task DeleteSchema()
        {
            foreach (var indexProvider in indexProviderFactory.GetAllProviders())
            {
                if (await indexProvider.IndexExists())
                {
                    await indexProvider.DeleteIndex();
                }
            }
        }

        public async Task Index<TEntity>(IEnumerable<TEntity> entities)
            where TEntity : class
        {
            var indexProvider = indexProviderFactory.GetProvider<EsConcordanceDto>();
            var indexName = indexProvider.IndexName;

            var request = new BulkRequest(indexName)
            {
                Operations = new List<IBulkOperation>(),
                Timeout = TimeSpan.FromMinutes(5)
            };

            // Get converter and convert entities to dtos
            var converter = esDtoConverterFactory.GetConverter<TEntity, EsConcordanceDto>();
            foreach (var entity in entities)
            {
                var dto = await converter.Convert(entity);
                request.Operations.Add(new BulkIndexOperation<EsConcordanceDto>(dto));
            }

            var response = await client.BulkAsync(request);
            if (!response.IsValid)
            {
                throw new Exception($"Invalid response from Elastic: {response.DebugInformation}!");
            }
        }

        public TResult Search<TQuery, TResult>(TQuery query)
            where TQuery : Query
            where TResult : QueryResult
        {
            // Get query handler
            var queryHandler = queryHandlerFactory.Get<TQuery, TResult>();
            return queryHandler.Handle(query);
        }
    }
}