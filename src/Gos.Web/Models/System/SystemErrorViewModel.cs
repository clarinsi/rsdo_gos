namespace Gos.Web.Models.System
{
    public class SystemErrorViewModel : BaseViewModel
    {
        public string RequestId { get; set; }

        public int StatusCode { get; set; }
    }
}