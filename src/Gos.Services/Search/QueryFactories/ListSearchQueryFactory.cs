using System.Threading.Tasks;
using Gos.Core.Search.Queries.List;
using Gos.ServiceModel.Requests.List;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Services.Search.QueryFactories
{
    public class ListSearchQueryFactory : BaseListQueryFactory, IQueryFactory<ListSearch, ListQuery>
    {
        public ListSearchQueryFactory(IPartOfSpeechService partOfSpeechService)
            : base(partOfSpeechService)
        {
        }

        public async Task<ListQuery> GetQuery(ListSearch request)
        {
            var query = await base.GetQuery<ListSearch, ListSearchResponse>(request);
            query.From = request.From;
            query.Size = request.Size;
            return query;
        }
    }
}