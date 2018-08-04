using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Returns the number of elements in a sequence.</summary>
    /// <param name="source">The <see cref="T:System.Linq.IQueryable`1"></see> that contains the elements to be counted.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The number of elements in the input sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source">source</paramref> is larger than <see cref="System.Int32.MaxValue"></see>.</exception>
    public static QueryDeferred<int> DeferredCount<TSource>(this IQueryable<TSource> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<int>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Count, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Returns the number of elements in the specified sequence that satisfies a condition.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> that contains the elements to be counted.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The number of elements in the sequence that satisfies the condition in the predicate function.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="predicate">predicate</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The number of elements in <paramref name="source">source</paramref> is larger than <see cref="System.Int32.MaxValue"></see>.</exception>
    public static QueryDeferred<int> DeferredCount<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(predicate, nameof(predicate));

        return new QueryDeferred<int>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Count, source, predicate),
                new[] { source.Expression, Expression.Quote(predicate) }
            ));
    }
}