using System.Collections.Generic;
using Gos.ServiceModel.Enums;

namespace Gos.Core.Search.Queries.Concordance
{
    public class ConcordanceQueryWord
    {
        public string ConversationalForm { get; set; }

        public string LeftMark { get; set; }

        public List<string> Lemmas { get; set; }

        public List<string> Msds { get; set; }

        public ConditionType PartOfSpeechCondition { get; set; }

        public int? PartOfSpeechId { get; set; }

        public string RightMark { get; set; }

        public string StandardForm { get; set; }
    }
}