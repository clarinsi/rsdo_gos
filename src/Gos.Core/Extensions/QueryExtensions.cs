using Gos.Core.Search.Queries;
using Gos.ServiceModel.Interfaces;

namespace Gos.Core.Extensions
{
    public static class QueryExtensions
    {
        public static TQuery WithFilters<TQuery>(this TQuery query, ServiceModel.Requests.Search request)
            where TQuery : Query
        {
            query.DiscourseTypeIds = request.DiscourseTypeIds;
            query.DiscourseChannelIds = request.DiscourseChannelIds;
            query.DiscourseEventIds = request.DiscourseEventIds;
            query.DiscourseRegionIds = request.DiscourseRegionIds;
            query.DiscourseYears = request.DiscourseYears;
            query.SpeakerAgeIds = request.SpeakerAgeIds;
            query.SpeakerEducationIds = request.SpeakerEducationIds;
            query.SpeakerLanguageIds = request.SpeakerLanguageIds;
            query.SpeakerRegionIds = request.SpeakerRegionIds;
            query.SpeakerSexIds = request.SpeakerSexIds;
            return query;
        }

        public static TQuery WithPageInfo<TQuery>(this TQuery query, IPagedSearch request)
            where TQuery : Query
        {
            query.From = request.From;
            query.Size = request.Size;
            return query;
        }
    }
}