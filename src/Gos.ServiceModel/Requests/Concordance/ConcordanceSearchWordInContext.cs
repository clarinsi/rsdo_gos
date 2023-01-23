using Gos.ServiceModel.Enums;

namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceSearchWordInContext : ConcordanceSearchWord
    {
        public ConditionType Condition { get; set; } = ConditionType.Is;

        public DistanceType DistanceType { get; set; } = DistanceType.Distance;

        public int LeftPosition { get; set; } = 3;

        public int RightPosition { get; set; } = 3;
    }
}