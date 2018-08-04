using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Determines whether a sequence contains any elements.</summary>
    /// <param name="source">A sequence to check for being empty.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. true if the source sequence contains any elements; otherwise, false.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<bool> DeferredAny<TSource>(this IQueryable<TSource> source)
    {
        Check.NotNull(source, nameof(source));


        return new QueryDeferred<bool>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Any, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Determines whether any element of a sequence satisfies a condition.</summary>
    /// <param name="source">A sequence whose elements to test for a condition.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. true if any elements in the source sequence pass the test in the specified predicate; otherwise, false.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="predicate">predicate</paramref> is null.</exception>
    public static QueryDeferred<bool> DeferredAny<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(predicate, nameof(predicate));

        return new QueryDeferred<bool>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Any, source, predicate),
                new[] { source.Expression, Expression.Quote(predicate) }
            ));
    }
}