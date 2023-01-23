using Gos.ServiceModel.Enums;

namespace Gos.Web.Models.Concordance
{
    public class ConcordanceIndexViewModel : ConcordanceInputViewModel, IIndexViewModel
    {
        public override string BodyCssClass => "concordance index index-page";

        public int Discourses { get; set; }

        public int Statements { get; set; }

        public TranscriptionType TranscriptionType => Request.TranscriptionType;

        public int Words { get; set; }
    }
}