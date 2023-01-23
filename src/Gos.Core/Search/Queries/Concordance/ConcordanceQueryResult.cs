using System.Collections.Generic;

namespace Gos.Core.Search.Queries.Concordance
{
    public class ConcordanceQueryResult : QueryResult
    {
        public List<ConcordanceQueryResultItem> Items { get; set; }
    }
}