using System.Threading.Tasks;

namespace Gos.Infrastructure.Search.Indexes
{
    public interface IIndexProvider<TDocument> : IIndexProvider
    {
    }

    public interface IIndexProvider
    {
        string IndexName { get; }

        Task CreateIndex();

        Task DeleteIndex();

        Task<bool> IndexExists();

        Task RefreshIndex();
    }
}