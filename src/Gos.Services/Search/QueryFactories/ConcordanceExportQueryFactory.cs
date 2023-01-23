using System.Threading.Tasks;
using Gos.Core.Search.Queries.Concordance;
using Gos.ServiceModel.Requests.Concordance;
using Gos.ServiceModel.Requests.Shared;
using Gos.Services.Services.LemmatizationService;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Services.Search.QueryFactories
{
    public class ConcordanceExportQueryFactory : BaseConcordanceQueryFactory, IQueryFactory<ConcordanceExport, ConcordanceQuery>
    {
        public ConcordanceExportQueryFactory(ILemmatizationService lemmatizationService, IPartOfSpeechService partOfSpeechService)
            : base(lemmatizationService, partOfSpeechService)
        {
        }

        public async Task<ConcordanceQuery> GetQuery(ConcordanceExport request)
        {
            var query = await base.GetQuery<ConcordanceExport, ExportResponse>(request);
            query.From = 0;
            query.Size = request.Rows;
            query.ReturnRandomRows = request.Type == ConcordanceExportType.RandomRows;
            return query;
        }
    }
}