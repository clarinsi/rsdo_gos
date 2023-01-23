using System.Collections.Generic;
using Gos.ServiceModel.Enums;

namespace Gos.ServiceModel.Requests.List
{
    public abstract class BaseListSearch<TResponse> : Search<TResponse>
    {
        protected BaseListSearch()
        {
            SortField = nameof(ListSearchResponseItem.Frequency);
            SortDirection = SortDirection.Descending;
        }

        public ConditionType Condition { get; set; }

        public string ConversationalForm { get; set; }

        public bool GroupByMsd { get; set; }

        public List<string> Lemmas { get; set; }

        public string Msds { get; set; }

        public List<int> PartOfSpeechIds { get; set; }

        public string StandardForm { get; set; }
    }
}