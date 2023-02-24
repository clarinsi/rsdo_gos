using OpenSearch.Client;

namespace Gos.Infrastructure.Search.Dtos
{
    public class EsConcordanceDto
    {
        public int? DiscourseChannelId { get; set; }

        public int? DiscourseEventId { get; set; }

        [Number(Store = true)]
        public int DiscourseId { get; set; }

        public int? DiscourseTypeId { get; set; }

        public int? DiscourseYear { get; set; }

        public int? SpeakerAgeId { get; set; }

        public int? SpeakerEducationId { get; set; }

        public int? SpeakerLanguageId { get; set; }

        public int? SpeakerRegionId { get; set; }

        public int? SpeakerSexId { get; set; }

        [Number(Store = true)]
        public int StatementOrder { get; set; }

        public EsTokenDto Token { get; set; }

        public EsTokenDto TokenLeft1 { get; set; }

        public EsTokenDto TokenLeft10 { get; set; }

        public EsTokenDto TokenLeft2 { get; set; }

        public EsTokenDto TokenLeft3 { get; set; }

        public EsTokenDto TokenLeft4 { get; set; }

        public EsTokenDto TokenLeft5 { get; set; }

        public EsTokenDto TokenLeft6 { get; set; }

        public EsTokenDto TokenLeft7 { get; set; }

        public EsTokenDto TokenLeft8 { get; set; }

        public EsTokenDto TokenLeft9 { get; set; }

        [Number(Store = true)]
        public int TokenOrder { get; set; }

        public EsTokenDto TokenRight1 { get; set; }

        public EsTokenDto TokenRight10 { get; set; }

        public EsTokenDto TokenRight2 { get; set; }

        public EsTokenDto TokenRight3 { get; set; }

        public EsTokenDto TokenRight4 { get; set; }

        public EsTokenDto TokenRight5 { get; set; }

        public EsTokenDto TokenRight6 { get; set; }

        public EsTokenDto TokenRight7 { get; set; }

        public EsTokenDto TokenRight8 { get; set; }

        public EsTokenDto TokenRight9 { get; set; }
    }
}