using Gos.ServiceModel.Interfaces;

namespace Gos.ServiceModel.Requests.List
{
    public class ListSearch : BaseListSearch<ListSearchResponse>, IPagedSearch
    {
        public int From { get; set; } = 0;

        public int Size { get; set; } = 20;
    }
}