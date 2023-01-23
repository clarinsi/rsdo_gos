using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.List;
using Gos.Web.Resources;
using Gos.Web.UrlHelpers;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.Extensions.Localization;

namespace Gos.Web.SelectLists
{
    public class ListSortSelectListProvider : ISelectListProvider
    {
        private readonly IActionContextAccessor actionContextAccessor;
        private readonly IStringLocalizer<ListResource> listResource;
        private readonly IUrlHelperFactory urlHelperFactory;

        public ListSortSelectListProvider(
            IActionContextAccessor actionContextAccessor,
            IStringLocalizer<ListResource> listResource,
            IUrlHelperFactory urlHelperFactory)
        {
            this.actionContextAccessor = actionContextAccessor;
            this.listResource = listResource;
            this.urlHelperFactory = urlHelperFactory;
        }

        public Task<SelectList> Get(object selectedValue)
        {
            var actionContext = actionContextAccessor.ActionContext;
            var urlHelper = urlHelperFactory.GetUrlHelper(actionContext);
            var request = (ListSearch)selectedValue;

            // Sort order
            var currentField = request.SortField;
            var currentDirection = request.SortDirection;

            var items = new List<SelectListItem>
            {
                GetItem(
                    urlHelper,
                    nameof(ListSearchResponseItem.ConversationalForm),
                    SortDirection.Ascending,
                    listResource["SortConversationalAsc"].Value,
                    currentField,
                    currentDirection),
                GetItem(
                    urlHelper,
                    nameof(ListSearchResponseItem.ConversationalForm),
                    SortDirection.Descending,
                    listResource["SortConversationalDesc"].Value,
                    currentField,
                    currentDirection),
                GetItem(
                    urlHelper,
                    nameof(ListSearchResponseItem.StandardForm),
                    SortDirection.Ascending,
                    listResource["SortStandardAsc"].Value,
                    currentField,
                    currentDirection),
                GetItem(
                    urlHelper,
                    nameof(ListSearchResponseItem.StandardForm),
                    SortDirection.Descending,
                    listResource["SortStandardDesc"].Value,
                    currentField,
                    currentDirection)
            };

            if (request.GroupByMsd)
            {
                items.Add(
                    GetItem(
                        urlHelper,
                        nameof(ListSearchResponseItem.Msd),
                        SortDirection.Ascending,
                        listResource["SortMsdAsc"].Value,
                        currentField,
                        currentDirection));
                items.Add(
                    GetItem(
                        urlHelper,
                        nameof(ListSearchResponseItem.Msd),
                        SortDirection.Descending,
                        listResource["SortMsdDesc"].Value,
                        currentField,
                        currentDirection));
            }

            items.Add(
                GetItem(
                    urlHelper,
                    nameof(ListSearchResponseItem.Frequency),
                    SortDirection.Ascending,
                    listResource["SortFrequencyAsc"].Value,
                    currentField,
                    currentDirection));
            items.Add(
                GetItem(
                    urlHelper,
                    nameof(ListSearchResponseItem.Frequency),
                    SortDirection.Descending,
                    listResource["SortFrequencyDesc"].Value,
                    currentField,
                    currentDirection));

            var selectList = new SelectList(items);
            return Task.FromResult(selectList);
        }

        private SelectListItem GetItem(
            IUrlHelper urlHelper,
            string field,
            SortDirection sortDirection,
            string text,
            string currentField,
            SortDirection currentDirection)
        {
            // determine if field is already sorted
            var isSorted = !string.IsNullOrEmpty(currentField) &&
                currentField.Equals(field, StringComparison.OrdinalIgnoreCase) &&
                currentDirection == sortDirection;

            // generate sort link
            var sortUrl = urlHelper.GetSortUrl(field, sortDirection);

            return new SelectListItem
            {
                Value = sortUrl,
                Text = text,
                Selected = isSorted
            };
        }
    }
}