using Gos.ServiceModel.Enums;

namespace Gos.ServiceModel.Requests.Concordance
{
    public abstract class ConcordanceSearchWord
    {
        public string Form { get; set; }

        public string ConversationalFormOverride { get; set; }

        public string StandardFormOverride { get; set; }

        public FormSearchType FormSearchType { get; set; }

        public string Lemma { get; set; }

        public string LeftMark { get; set; }

        public string Msds { get; set; }

        public ConditionType PartOfSpeechCondition { get; set; }

        public int? PartOfSpeechId { get; set; }

        public string RightMark { get; set; }

        public TranscriptionType TranscriptionType { get; set; }
    }
}