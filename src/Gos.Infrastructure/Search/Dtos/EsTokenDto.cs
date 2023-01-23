using OpenSearch.Client;

namespace Gos.Infrastructure.Search.Dtos
{
    public class EsTokenDto
    {
        [Keyword]
        public string Conversational { get; set; }

        [Keyword]
        public string ConversationalLower { get; set; }

        [Keyword]
        public string LeftMark { get; set; }

        [Keyword]
        public string Lemma { get; set; }

        [Keyword]
        public string LemmaLower { get; set; }

        [Keyword]
        public string Msd { get; set; }

        public int? PartOfSpeechId { get; set; }

        [Keyword]
        public string RightMark { get; set; }

        [Keyword]
        public string Standard { get; set; }

        [Keyword]
        public string StandardLower { get; set; }
    }
}