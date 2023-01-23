using System.Collections.Generic;
using System.Linq;

namespace Gos.Core.Extensions
{
    public static class EnumerableExtensions
    {
        public static IEnumerable<IEnumerable<T>> CartesianProduct<T>(this IEnumerable<IEnumerable<T>> sequences)
        {
            IEnumerable<IEnumerable<T>> emptyProduct = new[]
            {
                Enumerable.Empty<T>()
            };
            return sequences.Aggregate(
                emptyProduct,
                (accumulator, sequence) => from accseq in accumulator
                    from item in sequence
                    select accseq.Concat(
                        new[]
                        {
                            item
                        }));
        }

        public static bool IsNullOrEmpty<TSource>(this IEnumerable<TSource> collection)
        {
            return collection == null || !collection.Any();
        }
    }
}