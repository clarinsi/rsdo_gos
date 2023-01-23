using System.Collections.Generic;

namespace Gos.Core.Search.Queries.List
{
    public class ListQueryResult : QueryResult
    {
        public List<ListQueryResultItem> Items { get; set; }
    }
}