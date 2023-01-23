using Gos.ServiceModel.Requests.Concordance;

namespace Gos.Web.Models.Concordance
{
    public abstract class ConcordanceInputViewModel : BaseViewModel
    {
        public ConcordanceSearch Request { get; set; }
    }
}