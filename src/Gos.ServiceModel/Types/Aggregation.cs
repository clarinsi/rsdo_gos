using System.Collections.Generic;
using Gos.ServiceModel.Enums;

namespace Gos.ServiceModel.Types
{
    public class Aggregation
    {
        public List<AggregationItem> Items { get; set; } = new();

        public AggregationType Type { get; set; }
    }
}