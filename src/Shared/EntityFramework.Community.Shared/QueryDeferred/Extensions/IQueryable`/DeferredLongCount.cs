using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Returns an <see cref="T:System.Int64"></see> that represents the total number of elements in a sequence.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> that contains the elements to be counted.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The number of elements in <paramref name="source">source</paramref>.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The number of elements exceeds <see cref="System.Int64.MaxValue"></see>.</exception>
    public static QueryDeferred<long> DeferredLongCount<TSource>(this IQueryable<TSource> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<long>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.LongCount, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Returns an <see cref="T:System.Int64"></see> that represents the number of elements in a sequence that satisfy a condition.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> that contains the elements to be counted.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The number of elements in <paramref name="source">source</paramref> that satisfy the condition in the predicate function.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="predicate">predicate</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The number of matching elements exceeds <see cref="System.Int64.MaxValue"></see>.</exception>
    public static QueryDeferred<long> DeferredLongCount<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(predicate, nameof(predicate));

        return new QueryDeferred<long>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.LongCount, source, predicate),
                new[] { source.Expression, Expression.Quote(predicate) }
            ));
    }
}