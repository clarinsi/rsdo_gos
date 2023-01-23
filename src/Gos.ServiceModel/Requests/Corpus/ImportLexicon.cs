using MediatR;

namespace Gos.ServiceModel.Requests.Corpus
{
    public class ImportLexicon : IRequest<Unit>
    {
        public string SourcePath { get; set; }
    }
}