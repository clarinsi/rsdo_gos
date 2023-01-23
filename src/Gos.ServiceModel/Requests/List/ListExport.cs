using Gos.ServiceModel.Requests.Shared;

namespace Gos.ServiceModel.Requests.List
{
    public class ListExport : BaseListSearch<ExportResponse>
    {
        public int Rows { get; set; } = 100;
    }
}