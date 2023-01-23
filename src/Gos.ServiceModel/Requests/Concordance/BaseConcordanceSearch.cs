using System.Collections.Generic;

namespace Gos.ServiceModel.Requests.Concordance
{
    public abstract class BaseConcordanceSearch<TResponse> : Search<TResponse>
    {
        public ConcordanceSearchMainWord MainWord { get; set; }

        public List<ConcordanceSearchWordInContext> WordsInContext { get; set; }
    }
}