using System;
using System.Collections.Generic;
using System.Linq;
using Gos.ServiceModel.Interfaces;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;

namespace Gos.Web.UrlHelpers
{
    public static class FiltersUrlHelper
    {
        public static string GetFilterUrl(this IUrlHelper urlHelper, string key, string value)
        {
            var (baseUri, items) = urlHelper.GetParsedQuery();

            if (!IsFiltered(urlHelper, key, value))
            {
                items.Add(new KeyValuePair<string, string>(key, value));
            }

            return BuildUrl(baseUri, items);
        }

        public static string GetRemoveFilterUrl(this IUrlHelper urlHelper, string key, string value)
        {
            var (baseUri, items) = urlHelper.GetParsedQuery();

            if (IsFiltered(urlHelper, key, value))
            {
                items.RemoveAll(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
            }

            return BuildUrl(baseUri, items);
        }

        public static bool IsFiltered(this IUrlHelper urlHelper, string key, string value)
        {
            var (_, items) = urlHelper.GetParsedQuery();

            return items.Any(x => x.Key.Equals(key, StringComparison.OrdinalIgnoreCase) && x.Value.Equals(value, StringComparison.OrdinalIgnoreCase));
        }

        private static string BuildUrl(string baseUri, List<KeyValuePair<string, string>> items)
        {
            // Remove parameters from and size
            items.RemoveAll(x => x.Key.Equals(nameof(IPagedSearch.From), StringComparison.OrdinalIgnoreCase));
            items.RemoveAll(x => x.Key.Equals(nameof(IPagedSearch.Size), StringComparison.OrdinalIgnoreCase));
            var qb = new QueryBuilder(items);
            return baseUri + qb.ToQueryString();
        }
    }
}