using Gos.ServiceModel.Enums;

namespace Gos.Core.Search.Queries.Concordance
{
    public class ConcordanceQueryWordInContext : ConcordanceQueryWord
    {
        public ConditionType Condition { get; set; }

        public DistanceType DistanceType { get; set; }

        public int LeftPosition { get; set; }

        public int RightPosition { get; set; }
    }
}