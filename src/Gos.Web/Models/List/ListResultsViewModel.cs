using System;
using Gos.Core;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.List;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gos.Web.Models.List
{
    public class ListResultsViewModel : ListInputViewModel, IResultsViewModel
    {
        public override string BodyCssClass => "list results";

        public string GroupByMsdIcon => Request.GroupByMsd ? "radio_button_checked" : "radio_button_unchecked";

        public string Query => Request.Query;

        public ListSearchResponse Result { get; set; }

        public string ResultsInfo
        {
            get
            {
                var start = (Result.Offset + 1).ToString(Constants.Formats.CountsFormat);
                var end = Math.Min(Result.Total, Result.Offset + Constants.Search.DefaultPageSize).ToString(Constants.Formats.CountsFormat);
                var total = Result.Total.ToString(Constants.Formats.CountsFormat);
                return string.Format(Resources.ListResource.ResultsInfo, start, end, total);
            }
        }

        public SelectList SortList { get; set; }

        public TranscriptionType TranscriptionType => Request.TranscriptionType;
    }
}