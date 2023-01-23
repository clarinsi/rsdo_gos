using System.Collections.Generic;

namespace Gos.Core.Search.Queries.Concordance
{
    public class ConcordanceQuery : Query
    {
        public ConcordanceQueryMainWord MainWord { get; set; }

        public bool ReturnRandomRows { get; set; }

        public List<ConcordanceQueryWordInContext> WordsInContext { get; set; }
    }
}