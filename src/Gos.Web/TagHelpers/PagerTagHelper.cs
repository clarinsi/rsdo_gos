using System;
using System.IO;
using System.Text.Encodings.Web;
using Gos.Core;
using Gos.Web.UrlHelpers;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Mvc.TagHelpers;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace Gos.Web.TagHelpers
{
    [HtmlTargetElement("pager")]
    public class PagerTagHelper : TagHelper
    {
        public static readonly int PageSize = Constants.Search.DefaultPageSize;

        private readonly IUrlHelperFactory urlHelperFactory;

        public PagerTagHelper(IUrlHelperFactory urlHelperFactory)
        {
            this.urlHelperFactory = urlHelperFactory;
        }

        private enum PageState
        {
            None,
            Active,
            Disabled,
            First,
            Last,
            Previous,
            Next,
            Current,
        }

        public int CurrentPage => (int)Math.Floor(Offset / (double)PageSize) + 1;

        [HtmlAttributeName("offset")]
        public int Offset { get; set; }

        [HtmlAttributeName("Total")]
        public long Total { get; set; }

        public int TotalPages => (int)Math.Ceiling(Total / (double)PageSize);

        [ViewContext]
        public ViewContext ViewContext { get; set; }

        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            base.Process(context, output);

            output.TagName = "div";
            using (var writer = new StringWriter())
            {
                // Previous
                RenderPreviousPage(writer);

                // First
                if (CurrentPage > 1)
                {
                    RenderFirstPage(writer);
                }

                // Current
                RenderCurrentPage(writer);

                // Render next three pages
                for (var i = CurrentPage + 1; i < CurrentPage + 5 && i < TotalPages - 1; i++)
                {
                    RenderPage(writer, i, i.ToString(), PageState.None);
                }

                // Last
                if (CurrentPage < TotalPages)
                {
                    RenderLastPage(writer);
                }

                // Next
                RenderNextPage(writer);

                output.Content.AppendHtml(writer.ToString());
            }
        }

        private void RenderCurrentPage(StringWriter writer)
        {
            var itemBuilder = new TagBuilder("div");
            itemBuilder.AddCssClass("active");

            var linkBuilder = new TagBuilder("a");
            linkBuilder.Attributes.Add("href", "#");
            linkBuilder.InnerHtml.AppendHtml(CurrentPage.ToString(Constants.Formats.CountsFormat));
            itemBuilder.InnerHtml.AppendHtml(linkBuilder);

            var templateUrl = urlHelperFactory.GetUrlHelper(ViewContext).GetPagerTemplateLink();
            var inputBuilder = new TagBuilder("div");
            inputBuilder.AddCssClass("page-input");
            inputBuilder.Attributes.Add("style", "display:none;");
            inputBuilder.InnerHtml.AppendHtml(
                $"<input type=\"number\" value=\"{CurrentPage}\" /><a href=\"{templateUrl}\"><i class=\"material-icons\">check</i></a>");
            itemBuilder.InnerHtml.AppendHtml(inputBuilder);
            itemBuilder.WriteTo(writer, HtmlEncoder.Default);
        }

        private void RenderFirstPage(StringWriter writer)
        {
            var state = CurrentPage == 1 ? PageState.Active : PageState.None;
            RenderPage(writer, 1, "1", state, PageState.First);
        }

        private void RenderLastPage(StringWriter writer)
        {
            var state = CurrentPage == TotalPages ? PageState.Active : PageState.None;
            RenderPage(writer, TotalPages, TotalPages.ToString(Constants.Formats.CountsFormat), state, PageState.Last);
        }

        private void RenderNextPage(StringWriter writer)
        {
            var state = CurrentPage >= TotalPages ? PageState.Disabled : PageState.None;
            RenderPage(writer, CurrentPage + 1, "&raquo;", state, PageState.Next);
        }

        private void RenderPage(StringWriter writer, int page, string html, params PageState[] states)
        {
            var renderedPage = RenderPage(page, html, states);
            renderedPage.WriteTo(writer, HtmlEncoder.Default);
        }

        private TagBuilder RenderPage(int page, string html, params PageState[] states)
        {
            var itemBuilder = new TagBuilder("div");
            foreach (var state in states)
            {
                if (state != PageState.None)
                {
                    itemBuilder.AddCssClass(state.ToString().ToLower());
                }
            }

            var link = urlHelperFactory.GetUrlHelper(ViewContext).GetPageLink(page);
            var linkBuilder = new TagBuilder("a");
            linkBuilder.Attributes.Add("href", link);
            linkBuilder.InnerHtml.AppendHtml(html);

            itemBuilder.InnerHtml.AppendHtml(linkBuilder);
            return itemBuilder;
        }

        private void RenderPreviousPage(StringWriter writer)
        {
            var state = CurrentPage <= 1 ? PageState.Disabled : PageState.None;
            RenderPage(writer, CurrentPage - 1, "&laquo;", state, PageState.Previous);
        }
    }
}