using System;
using System.Collections.Generic;
using System.Linq;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;

namespace Gos.Web.UrlHelpers
{
    public static class UrlHelperExtensions
    {
        public static (string baseUri, List<KeyValuePair<string, string>> items) GetParsedQuery(this IUrlHelper urlHelper)
        {
            var uri = new Uri(urlHelper.ActionContext.HttpContext.Request.GetDisplayUrl());

            // Get base uri depending on action and controller
            var action = urlHelper.ActionContext.RouteData.Values["action"].ToString();
            var controller = urlHelper.ActionContext.RouteData.Values["controller"].ToString();
            var baseUri = urlHelper.Action(action, controller);

            var query = QueryHelpers.ParseQuery(uri.Query);

            var items = query.SelectMany(x => x.Value, (col, value) => new KeyValuePair<string, string>(col.Key, value)).ToList();

            return (baseUri, items);
        }

        public static string GetReturnUrl(this IUrlHelper urlHelper)
        {
            var request = urlHelper.ActionContext.HttpContext.Request;

            // Path
            var returnUrl = string.IsNullOrEmpty(request.Path) ? "~/" : $"~{request.Path.Value}";

            // Add query string
            if (!string.IsNullOrEmpty(request.QueryString.Value))
            {
                returnUrl += request.QueryString.Value;
            }

            return returnUrl;
        }

        public static string GetSortUrl(this IUrlHelper urlHelper, string sortField, SortDirection sortDirection = SortDirection.Ascending)
        {
            var parsedQuery = urlHelper.GetParsedQuery();
            parsedQuery.items.RemoveAll(x => x.Key.Equals(nameof(Search.SortField), StringComparison.OrdinalIgnoreCase));
            parsedQuery.items.RemoveAll(x => x.Key.Equals(nameof(Search.SortDirection), StringComparison.OrdinalIgnoreCase));

            var qb = new QueryBuilder(parsedQuery.items)
            {
                { nameof(Search.SortField), sortField },
                { nameof(Search.SortDirection), sortDirection.ToString() }
            };

            return parsedQuery.baseUri + qb.ToQueryString();
        }
    }
}