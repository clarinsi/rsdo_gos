using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Encodings.Web;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;

namespace Gos.Web.HtmlHelpers
{
    public static class HtmlHelperExtensions
    {
        public static IHtmlContent DropDown(this IHtmlHelper htmlHelper, string name, IEnumerable<SelectListItem> selectList, object htmlAttributes = null)
        {
            // Order items so that selected will be first (dropdown takes first item as selected)
            var items = selectList.ToList();
            var selectedIndex = items.FindIndex(x => x.Selected);
            if (selectedIndex > 0)
            {
                var element = items[selectedIndex];
                items.RemoveAt(selectedIndex);
                items.Insert(0, element);
            }

            var div = new TagBuilder("div");
            div.MergeAttributes(GetHtmlAttributeDictionaryOrNull(htmlAttributes));
            div.AddCssClass("dropdown");

            div.InnerHtml.AppendHtml(htmlHelper.Hidden(name));

            // p
            var p = new TagBuilder("p");
            p.AddCssClass("dropdown-field");

            var b = new TagBuilder("b");
            b.AddCssClass("dropdown-value");
            b.Attributes.Add("data-value", "");
            p.InnerHtml.AppendHtml(b);
            div.InnerHtml.AppendHtml(p);

            // ul
            var ul = new TagBuilder("ul");
            ul.AddCssClass("dropdown-list");
            foreach (var item in items)
            {
                var li = new TagBuilder("li");
                li.Attributes.Add("data-value", item.Value);
                li.Attributes.Add("data-text", item.Text);
                li.InnerHtml.Append(item.Text);
                ul.InnerHtml.AppendHtml(li);
            }

            div.InnerHtml.AppendHtml(ul);
            return div;
        }

        public static IHtmlContent DistanceSelector(this IHtmlHelper htmlHelper, string name, bool isLeft, object htmlAttributes = null)
        {
            var div = new TagBuilder("div");
            div.MergeAttributes(GetHtmlAttributeDictionaryOrNull(htmlAttributes));

            div.InnerHtml.AppendHtml(htmlHelper.Hidden(name, 3));

            var range = Enumerable.Range(0, 10);
            if (isLeft)
            {
                range = range.Reverse();
            }

            foreach (var position in range)
            {
                var span = new TagBuilder("span");
                span.InnerHtml.Append(position.ToString());
                span.AddCssClass("number");

                switch (position)
                {
                    case 0:
                        span.AddCssClass("zero");
                        break;
                    case 1:
                        span.AddCssClass($"selected range {(isLeft ? "last" : "first")}");
                        break;
                    case 2:
                        span.AddCssClass("selected range");
                        break;
                    case 3:
                        span.AddCssClass($"selected range {(isLeft ? "first" : "last")}");
                        break;
                }

                div.InnerHtml.AppendHtml(span);
            }

            return div;
        }

        public static IHtmlContent CreateHiddenInputsFromQueryString(this IHtmlHelper htmlHelper, IEnumerable<string> keysToSkip = null)
        {
            var hashedKeys = keysToSkip != null ? new HashSet<string>(keysToSkip) : new HashSet<string>();
            var uri = new Uri(htmlHelper.ViewContext.HttpContext.Request.GetDisplayUrl());
            var query = QueryHelpers.ParseQuery(uri.Query);
            using (var sw = new StringWriter())
            {
                foreach (var item in query)
                {
                    if (hashedKeys.Contains(item.Key))
                    {
                        continue;
                    }

                    foreach (var value in item.Value)
                    {
                        var hiddenBuilder = new TagBuilder("input");
                        hiddenBuilder.TagRenderMode = TagRenderMode.SelfClosing;
                        hiddenBuilder.Attributes.Add("type", "hidden");
                        hiddenBuilder.Attributes.Add("name", item.Key);
                        hiddenBuilder.Attributes.Add("value", value);
                        hiddenBuilder.WriteTo(sw, HtmlEncoder.Default);
                    }
                }

                return htmlHelper.Raw(sw.ToString());
            }
        }

        private static IDictionary<string, object> GetHtmlAttributeDictionaryOrNull(object htmlAttributes)
        {
            IDictionary<string, object> htmlAttributeDictionary = null;
            if (htmlAttributes != null)
            {
                htmlAttributeDictionary = htmlAttributes as IDictionary<string, object>;
                if (htmlAttributeDictionary == null)
                {
                    htmlAttributeDictionary = Microsoft.AspNetCore.Mvc.ViewFeatures.HtmlHelper.AnonymousObjectToHtmlAttributes(htmlAttributes);
                }
            }

            return htmlAttributeDictionary;
        }
    }
}