using System.Threading.Tasks;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Services.LemmatizationService;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Services.Search.QueryFactories
{
    public class ConcordanceDetailsQueryFactory : BaseConcordanceQueryFactory, IQueryFactory<ConcordanceDetails, ConcordanceQuery>
    {
        public ConcordanceDetailsQueryFactory(ILemmatizationService lemmatizationService, IPartOfSpeechService partOfSpeechService)
            : base(lemmatizationService, partOfSpeechService)
        {
        }

        public async Task<ConcordanceQuery> GetQuery(ConcordanceDetails request)
        {
            var query = await base.GetQuery<ConcordanceDetails, ConcordanceDetailsResponse>(request);
            query.From = 0;
            query.Size = 0;
            return query;
        }
    }
}