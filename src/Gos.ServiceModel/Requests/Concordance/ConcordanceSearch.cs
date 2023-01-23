using Gos.ServiceModel.Interfaces;

namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceSearch : BaseConcordanceSearch<ConcordanceSearchResponse>, IPagedSearch
    {
        public int From { get; set; } = 0;

        public int Size { get; set; } = 20;
    }
}