using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gos.Web.Models.Concordance
{
    public class ConcordanceAdvancedViewModel : BaseViewModel
    {
        public override string BodyCssClass => "concordance advanced";

        public SelectList ConditionsList { get; set; }

        public SelectList LeftMarkList { get; set; }

        public SelectList PartOfSpeechesList { get; set; }

        public SelectList RightMarkList { get; set; }

        public SelectList WordList { get; set; }
    }
}