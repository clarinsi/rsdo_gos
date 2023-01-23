using System.Collections.Generic;
using Gos.ServiceModel.Types;

namespace Gos.ServiceModel.Requests
{
    public class SearchResponse
    {
        public List<Aggregation> Aggregations { get; set; } = new();
    }
}