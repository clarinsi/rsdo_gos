using Gos.ServiceModel.Requests.List;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Gos.Web.Models.List
{
    public abstract class ListInputViewModel : BaseViewModel
    {
        public SelectList ConditionsList { get; set; }

        public SelectList PartOfSpeechesList { get; set; }

        public ListSearch Request { get; set; }
    }
}