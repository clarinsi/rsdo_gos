using System.Collections.Generic;
using System.Linq;
using Gos.Core.Extensions;
using Gos.Core.Search.Queries;
using OpenSearch.Client;

namespace Gos.Infrastructure.Search.QueryBuilders
{
    public abstract class BaseQueryBuilder
    {
        protected static List<QueryContainer> GetFilterQueries(Query query)
        {
            var queries = new List<QueryContainer>();

            if (!query.DiscourseTypeIds.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "discourseTypeId",
                        Terms = query.DiscourseTypeIds.Cast<object>(),
                    });
            }

            if (!query.DiscourseChannelIds.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "discourseChannelId",
                        Terms = query.DiscourseChannelIds.Cast<object>(),
                    });
            }

            if (!query.DiscourseEventIds.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "discourseEventId",
                        Terms = query.DiscourseEventIds.Cast<object>(),
                    });
            }

            if (!query.DiscourseYears.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "discourseYear",
                        Terms = query.DiscourseYears.Cast<object>(),
                    });
            }

            if (!query.SpeakerAgeIds.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "speakerAgeId",
                        Terms = query.SpeakerAgeIds.Cast<object>(),
                    });
            }

            if (!query.SpeakerEducationIds.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "speakerEducationId",
                        Terms = query.SpeakerEducationIds.Cast<object>(),
                    });
            }

            if (!query.SpeakerLanguageIds.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "speakerLanguageId",
                        Terms = query.SpeakerLanguageIds.Cast<object>(),
                    });
            }

            if (!query.SpeakerRegionIds.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "speakerRegionId",
                        Terms = query.SpeakerRegionIds.Cast<object>(),
                    });
            }

            if (!query.SpeakerSexIds.IsNullOrEmpty())
            {
                queries.Add(
                    new TermsQuery()
                    {
                        Field = "speakerSexId",
                        Terms = query.SpeakerSexIds.Cast<object>(),
                    });
            }

            return queries;
        }
    }
}