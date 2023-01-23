using System.Threading.Tasks;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Requests.Concordance;
using Gos.Services.Services.LemmatizationService;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Services.Search.QueryFactories
{
    public class ConcordanceSearchQueryFactory : BaseConcordanceQueryFactory, IQueryFactory<ConcordanceSearch, ConcordanceQuery>
    {
        public ConcordanceSearchQueryFactory(ILemmatizationService lemmatizationService, IPartOfSpeechService partOfSpeechService)
            : base(lemmatizationService, partOfSpeechService)
        {
        }

        public async Task<ConcordanceQuery> GetQuery(ConcordanceSearch request)
        {
            var query = await base.GetQuery<ConcordanceSearch, ConcordanceSearchResponse>(request);
            query.From = request.From;
            query.Size = request.Size;
            return query;
        }
    }
}