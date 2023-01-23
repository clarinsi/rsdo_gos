using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Gos.Core.Search.Queries.List;
using Gos.ServiceModel.Requests.List;
using Gos.Services.Services.PartOfSpeechService;

namespace Gos.Services.RequestHandlers.List
{
    public abstract class BaseListHandler
    {
        private readonly IPartOfSpeechService partOfSpeechService;

        protected BaseListHandler(IPartOfSpeechService partOfSpeechService)
        {
            this.partOfSpeechService = partOfSpeechService;
        }

        protected async Task<List<ListSearchResponseItem>> GetResponseItems(List<ListQueryResultItem> items)
        {
            var convertedItems = new List<ListSearchResponseItem>();
            foreach (var item in items)
            {
                var convertedItem = await GetResponseItem(item);
                convertedItems.Add(convertedItem);
            }

            return convertedItems;
        }

        private async Task<ListSearchResponseItem> GetResponseItem(ListQueryResultItem item)
        {
            return new ListSearchResponseItem()
            {
                Frequency = item.Frequency,
                ConversationalForm = item.ConversationalForm,
                StandardForm = item.StandardForm,
                Msd = item.Msd,
                MsdDescription = await partOfSpeechService.GetMsdDescriptionByCode(item.Msd),
            };
        }
    }
}