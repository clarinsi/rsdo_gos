using System.Collections.Generic;
using Gos.ServiceModel.Requests.Concordance;
using Gos.ServiceModel.Requests.List;
using Gos.Web.Extensions;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gos.Web.UrlHelpers
{
    public static class ListUrlHelper
    {
        public static string ListFrequencyLink(this IUrlHelper urlHelper, ListSearch search, ListSearchResponseItem item)
        {
            var concordanceSearch = new ConcordanceSearch()
            {
                MainWord = new ConcordanceSearchMainWord()
                {
                    ConversationalFormOverride = item.ConversationalForm,
                    StandardFormOverride = item.StandardForm,
                },
            };

            if (search.GroupByMsd)
            {
                concordanceSearch.MainWord.Msds = item.Msd;
            }

            return urlHelper.ConcordanceSearchLink(concordanceSearch);
        }

        public static string ListSearchLink<TResponse>(this IUrlHelper urlHelper, BaseListSearch<TResponse> search, bool includeScheme = false)
        {
            var qb = new QueryBuilder();
            qb.AddWithNullCheck($"{nameof(ListSearch.Query)}", search.Query);
            qb.Add($"{nameof(ListSearch.TranscriptionType)}", search.TranscriptionType.ToString());

            var scheme = includeScheme ? urlHelper.ActionContext.HttpContext.Request.Scheme : null;
            return urlHelper.Action("Search", "List", null, scheme) + qb.ToQueryString();
        }
    }
}