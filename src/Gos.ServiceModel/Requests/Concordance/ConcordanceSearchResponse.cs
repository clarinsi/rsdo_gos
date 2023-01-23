using System.Collections.Generic;
using Gos.ServiceModel.Interfaces;
using Gos.ServiceModel.Types;

namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceSearchResponse : SearchResponse, IPagedResponse
    {
        public AlternateSearch<ConcordanceSearch> LemmasAlternateSearch { get; set; }

        public List<ConcordanceSearchResponseItem> Items { get; set; } = new();

        public int Offset { get; set; }

        public long Total { get; set; }
    }
}