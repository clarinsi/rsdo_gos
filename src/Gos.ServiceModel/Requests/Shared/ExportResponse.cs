using System.IO;

namespace Gos.ServiceModel.Requests.Shared
{
    public class ExportResponse
    {
        public string ContentType { get; set; }

        public string FileName { get; set; }

        public Stream Stream { get; set; }
    }
}