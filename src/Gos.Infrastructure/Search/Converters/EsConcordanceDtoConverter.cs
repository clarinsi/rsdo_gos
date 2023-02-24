using System.Linq;
using System.Threading.Tasks;
using Gos.Core.Entities;
using Gos.Core.Model;
using Gos.Infrastructure.Search.Dtos;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Infrastructure.Search.Converters
{
    public class EsConcordanceDtoConverter : IEsDtoConverter<Concordance, EsConcordanceDto>
    {
        private readonly IPartOfSpeechService partOfSpeechService;

        public EsConcordanceDtoConverter(IPartOfSpeechService partOfSpeechService)
        {
            this.partOfSpeechService = partOfSpeechService;
        }

        public async Task<EsConcordanceDto> Convert(Concordance entity)
        {
            var discourse = entity.Statement.Discourse;
            var speaker = entity.Statement.Speaker;
            return new EsConcordanceDto
            {
                DiscourseId = discourse.Id,
                DiscourseChannelId = discourse.Channel?.Id,
                DiscourseEventId = discourse.Event?.Id,
                DiscourseTypeId = discourse.Type?.Id,
                DiscourseYear = discourse.Date.Year,
                SpeakerAgeId = speaker?.Age?.Id,
                SpeakerEducationId = speaker?.Education?.Id,
                SpeakerLanguageId = speaker?.Language?.Id,
                SpeakerRegionId = speaker?.Region1?.Id,
                SpeakerSexId = speaker?.Sex?.Id,
                StatementOrder = entity.Statement.Order,
                Token = await ConvertToken(entity.Token),
                TokenLeft1 = await ConvertToken(entity.TokenLeft1),
                TokenLeft2 = await ConvertToken(entity.TokenLeft2),
                TokenLeft3 = await ConvertToken(entity.TokenLeft3),
                TokenLeft4 = await ConvertToken(entity.TokenLeft4),
                TokenLeft5 = await ConvertToken(entity.TokenLeft5),
                TokenLeft6 = await ConvertToken(entity.TokenLeft6),
                TokenLeft7 = await ConvertToken(entity.TokenLeft7),
                TokenLeft8 = await ConvertToken(entity.TokenLeft8),
                TokenLeft9 = await ConvertToken(entity.TokenLeft9),
                TokenLeft10 = await ConvertToken(entity.TokenLeft10),
                TokenOrder = entity.Token.DiscourseOrder,
                TokenRight1 = await ConvertToken(entity.TokenRight1),
                TokenRight2 = await ConvertToken(entity.TokenRight2),
                TokenRight3 = await ConvertToken(entity.TokenRight3),
                TokenRight4 = await ConvertToken(entity.TokenRight4),
                TokenRight5 = await ConvertToken(entity.TokenRight5),
                TokenRight6 = await ConvertToken(entity.TokenRight6),
                TokenRight7 = await ConvertToken(entity.TokenRight7),
                TokenRight8 = await ConvertToken(entity.TokenRight8),
                TokenRight9 = await ConvertToken(entity.TokenRight9),
                TokenRight10 = await ConvertToken(entity.TokenRight10),
            };
        }

        private async Task<EsTokenDto> ConvertToken(Token token)
        {
            if (token != null)
            {
                var partOfSpeech = await partOfSpeechService.GetPartOfSpeechByMsdCode(token.Msd);
                return new EsTokenDto
                {
                    Conversational = token.ConversationalForm,
                    ConversationalLower = token.ConversationalForm?.ToLower(),
                    LeftMark = token.LeftMark,
                    Lemma = token.Lemma,
                    LemmaLower = token.Lemma?.ToLower(),
                    Msd = token.Msd,
                    PartOfSpeechId = partOfSpeech?.Id,
                    RightMark = token.RightMark,
                    Standard = token.StandardForm,
                    StandardLower = token.StandardForm?.ToLower(),
                };
            }

            return null;
        }
    }
}