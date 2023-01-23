using System.Collections.Generic;

namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceSearchResponseItem
    {
        public ConcordanceToken CenterContext { get; set; }

        public List<ConcordanceToken> LeftContext { get; set; } = new();

        public List<ConcordanceToken> RightContext { get; set; } = new();

        public List<string> SoundFiles { get; set; }
    }
}