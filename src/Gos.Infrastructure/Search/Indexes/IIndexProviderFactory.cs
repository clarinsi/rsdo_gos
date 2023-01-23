using System.Collections.Generic;

namespace Gos.Infrastructure.Search.Indexes
{
    public interface IIndexProviderFactory
    {
        IEnumerable<IIndexProvider> GetAllProviders();

        IIndexProvider<TDocument> GetProvider<TDocument>();
    }
}