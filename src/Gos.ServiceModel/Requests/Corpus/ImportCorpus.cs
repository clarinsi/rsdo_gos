using MediatR;

namespace Gos.ServiceModel.Requests.Corpus
{
    public class ImportCorpus : IRequest<Unit>
    {
        public string SourcePath { get; set; }
    }
}