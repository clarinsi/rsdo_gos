using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.Core.Extensions;
using Gos.Core.Search.Queries.List;
using Gos.ServiceModel.Requests.List;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Services.Search.QueryFactories
{
    public abstract class BaseListQueryFactory
    {
        private readonly IPartOfSpeechService partOfSpeechService;

        protected BaseListQueryFactory(IPartOfSpeechService partOfSpeechService)
        {
            this.partOfSpeechService = partOfSpeechService;
        }

        protected async Task<ListQuery> GetQuery<TRequest, TResponse>(TRequest request)
            where TRequest : BaseListSearch<TResponse>
        {
            List<string> msds = null;
            if (!request.PartOfSpeechIds.IsNullOrEmpty() && !string.IsNullOrEmpty(request.Msds))
            {
                msds = await partOfSpeechService.BuildMsds(request.PartOfSpeechIds[0], request.Msds);
            }

            return new ListQuery()
            {
                Condition = request.Condition,
                ConversationalForm = request.ConversationalForm,
                GroupByMsd = request.GroupByMsd,
                Lemmas = request.Lemmas,
                Msds = msds,
                PartOfSpeechIds = request.PartOfSpeechIds,
                Query = request.Query,
                StandardForm = request.StandardForm,
                TranscriptionType = request.TranscriptionType,
            }.WithFilters(request);
        }
    }
}