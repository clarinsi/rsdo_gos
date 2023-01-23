using System;
using Gos.Core;
using Microsoft.Extensions.Configuration;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search
{
    public class ElasticClientFactory
    {
        private readonly IConfiguration configuration;

        public ElasticClientFactory(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IOpenSearchClient CreateClient()
        {
            var connectionString = configuration[ConfigurationKey.Elastic.ConnectionString];
            var connectionSettings = new ConnectionSettings(new Uri(connectionString)).SniffOnStartup(false).RequestTimeout(TimeSpan.FromMinutes(5));

#if DEBUG
            connectionSettings.EnableDebugMode().IncludeServerStackTraceOnError(false);
#endif

            return new OpenSearchClient(connectionSettings);
        }
    }
}