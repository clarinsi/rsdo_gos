using System.Threading.Tasks;
using Gos.Core.Search.Queries.List;
using Gos.ServiceModel.Requests.List;
using Gos.ServiceModel.Requests.Shared;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Services.Search.QueryFactories
{
    public class ListExportQueryFactory : BaseListQueryFactory, IQueryFactory<ListExport, ListQuery>
    {
        public ListExportQueryFactory(IPartOfSpeechService partOfSpeechService)
            : base(partOfSpeechService)
        {
        }

        public async Task<ListQuery> GetQuery(ListExport request)
        {
            var query = await base.GetQuery<ListExport, ExportResponse>(request);
            query.From = 0;
            query.Size = request.Rows;
            return query;
        }
    }
}