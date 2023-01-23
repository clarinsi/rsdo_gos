using Gos.ServiceModel.Enums;

namespace Gos.Web.Models.List
{
    public class ListIndexViewModel : ListInputViewModel, IIndexViewModel
    {
        public override string BodyCssClass => "list index index-page";

        public int Discourses { get; set; }

        public int Statements { get; set; }

        public TranscriptionType TranscriptionType => Request.TranscriptionType;

        public int Words { get; set; }
    }
}