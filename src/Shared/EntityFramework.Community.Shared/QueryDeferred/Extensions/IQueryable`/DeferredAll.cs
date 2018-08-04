using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Determines whether all the elements of a sequence satisfy a condition.</summary>
    /// <param name="source">A sequence whose elements to test for a condition.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. true if every element of the source sequence passes the test in the specified predicate, or if the sequence is empty; otherwise, false.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="predicate">predicate</paramref> is null.</exception>
    public static QueryDeferred<bool> DeferredAll<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(predicate, nameof(predicate));

        return new QueryDeferred<bool>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.All, source, predicate),
                new[] { source.Expression, Expression.Quote(predicate) }
            ));
    }
}