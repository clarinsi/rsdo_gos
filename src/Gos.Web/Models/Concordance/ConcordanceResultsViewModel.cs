using System;
using Gos.Core;
using Gos.ServiceModel.Enums;
using Gos.ServiceModel.Requests.Concordance;

namespace Gos.Web.Models.Concordance
{
    public class ConcordanceResultsViewModel : ConcordanceInputViewModel, IResultsViewModel
    {
        public override string BodyCssClass => "concordance results";

        public string Query => Request.Query;

        public ConcordanceSearchResponse Result { get; set; }

        public string ResultsInfo
        {
            get
            {
                var start = (Result.Offset + 1).ToString(Constants.Formats.CountsFormat);
                var end = Math.Min(Result.Total, Result.Offset + Constants.Search.DefaultPageSize).ToString(Constants.Formats.CountsFormat);
                var total = Result.Total.ToString(Constants.Formats.CountsFormat);
                return string.Format(Resources.ConcordanceResource.ResultsInfo, start, end, total);
            }
        }

        public TranscriptionType TranscriptionType => Request.TranscriptionType;
    }
}