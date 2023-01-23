using System;
using Gos.Core;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gos.Web.UrlHelpers
{
    public static class PagerUrlHelperExtensions
    {
        public static string GetPageLink(this IUrlHelper urlHelper, int page)
        {
            var from = (page - 1) * 20;
            return GetPageLinkInternal(urlHelper, from.ToString());
        }

        public static string GetPagerTemplateLink(this IUrlHelper urlHelper)
        {
            return GetPageLinkInternal(urlHelper, "@@FROM@@");
        }

        private static string GetPageLinkInternal(IUrlHelper urlHelper, string from)
        {
            var parsedQuery = urlHelper.GetParsedQuery();
            parsedQuery.items.RemoveAll(x => x.Key.Equals("from", StringComparison.OrdinalIgnoreCase));
            parsedQuery.items.RemoveAll(x => x.Key.Equals("size", StringComparison.OrdinalIgnoreCase));

            var qb = new QueryBuilder(parsedQuery.items)
            {
                { "from", from },
                { "size", Constants.Search.DefaultPageSize.ToString() }
            };

            return parsedQuery.baseUri + qb.ToQueryString();
        }
    }
}