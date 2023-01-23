using System.Collections.Generic;
using Gos.ServiceModel.Enums;

namespace Gos.Core.Search.Queries.List
{
    public class ListQuery : Query
    {
        public ConditionType Condition { get; set; }

        public string ConversationalForm { get; set; }

        public string Query { get; set; }

        public bool GroupByMsd { get; set; }

        public List<string> Lemmas { get; set; }

        public List<string> Msds { get; set; }

        public List<int> PartOfSpeechIds { get; set; }

        public string StandardForm { get; set; }

        public TranscriptionType TranscriptionType { get; set; }
    }
}