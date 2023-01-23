using MediatR;

namespace Gos.ServiceModel.Requests.Corpus
{
    public class ImportCounters : IRequest<Unit>
    {
        public string SourcePath { get; set; }
    }
}