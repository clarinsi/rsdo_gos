using System.Collections.Generic;
using Gos.ServiceModel.Enums;

namespace Gos.ServiceModel.Types
{
    public class AlternateSearch<TRequest>
    {
        public List<AlternateSearchItem<TRequest>> Items { get; set; }

        public TRequest OriginalSearch { get; set; }

        public AlternateSearchType Type { get; set; }
    }
}