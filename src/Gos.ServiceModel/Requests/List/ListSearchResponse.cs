using System.Collections.Generic;
using Gos.ServiceModel.Interfaces;

namespace Gos.ServiceModel.Requests.List
{
    public class ListSearchResponse : SearchResponse, IPagedResponse
    {
        public List<ListSearchResponseItem> Items { get; set; } = new();

        public int Offset { get; set; }

        public long Total { get; set; }
    }
}