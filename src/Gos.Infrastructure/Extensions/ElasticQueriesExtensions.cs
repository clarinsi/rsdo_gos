using System.Collections.Generic;
using OpenSearch.Client;

namespace Gos.Infrastructure.Extensions
{
    public static class ElasticQueriesExtensions
    {
        public static QueryContainer ToBooleanAndQuery(this List<QueryContainer> queries)
        {
            return queries.Count switch
            {
                0 => null,
                1 => queries[0],
                _ => new BoolQuery()
                {
                    Must = queries,
                },
            };
        }

        public static QueryContainer ToBooleanOrQuery(this List<QueryContainer> queries)
        {
            return queries.Count switch
            {
                0 => null,
                1 => queries[0],
                _ => new BoolQuery()
                {
                    Should = queries,
                },
            };
        }
    }
}