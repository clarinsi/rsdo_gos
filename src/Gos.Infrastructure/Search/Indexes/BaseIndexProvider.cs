using System;
using System.Threading.Tasks;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.Indexes
{
    public abstract class BaseIndexProvider<TEntity> : IIndexProvider<TEntity>
        where TEntity : class
    {
        private readonly IOpenSearchClient client;

        protected BaseIndexProvider(IOpenSearchClient client)
        {
            this.client = client;
        }

        public virtual string IndexName => $"gos_{typeof(TEntity).Name.ToLower()}";

        public async Task CreateIndex()
        {
            var response = await client.Indices.CreateAsync(
                IndexName,
                c => c.Settings(
                        s => s.Setting("max_result_window", int.MaxValue)
                            .NumberOfShards(5)
                            .NumberOfReplicas(0)
                            .RefreshInterval(-1)
                            .Merge(ms => ms.Scheduler(ss => ss.MaxThreadCount(1))))
                    .Map(ms => ms.AutoMap<TEntity>().SourceField(s => s.Enabled(false))));

            if (!response.IsValid)
            {
                throw new Exception($"Invalid Elastic response: {response.DebugInformation}!");
            }
        }

        public async Task DeleteIndex()
        {
            var response = await client.Indices.DeleteAsync(IndexName);

            if (!response.IsValid)
            {
                throw new Exception($"Invalid Elastic response: {response.DebugInformation}!");
            }
        }

        public async Task<bool> IndexExists()
        {
            return (await client.Indices.ExistsAsync(IndexName)).Exists;
        }

        public async Task RefreshIndex()
        {
            var response = await client.Indices.RefreshAsync(IndexName);

            if (!response.IsValid)
            {
                throw new Exception($"Invalid Elastic response: {response.DebugInformation}!");
            }
        }
    }
}