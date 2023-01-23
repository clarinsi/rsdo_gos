using Gos.ServiceModel.Requests.Shared;

namespace Gos.ServiceModel.Requests.Concordance
{
    public class ConcordanceExport : BaseConcordanceSearch<ExportResponse>
    {
        public int Rows { get; set; } = 100;

        public ConcordanceExportType Type { get; set; } = ConcordanceExportType.FirstRows;
    }
}