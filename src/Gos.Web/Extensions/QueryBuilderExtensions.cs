using System.Collections.Generic;
using Gos.Core.Extensions;
using Microsoft.AspNetCore.Http.Extensions;

namespace Gos.Web.Extensions
{
    public static class QueryBuilderExtensions
    {
        public static void AddWithNullCheck(this QueryBuilder queryBuilder, string key, string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                queryBuilder.Add(key, value);
            }
        }

        public static void AddWithNullCheck(this QueryBuilder queryBuilder, string key, IEnumerable<string> values)
        {
            if (!values.IsNullOrEmpty())
            {
                queryBuilder.Add(key, values);
            }
        }
    }
}