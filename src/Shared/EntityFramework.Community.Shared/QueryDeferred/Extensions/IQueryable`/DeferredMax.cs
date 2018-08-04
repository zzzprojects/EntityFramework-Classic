using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Returns the maximum value in a generic <see cref="T:System.Linq.IQueryable`1"></see>.</summary>
    /// <param name="source">A sequence of values to determine the maximum of.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The maximum value in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<TSource> DeferredMax<TSource>(this IQueryable<TSource> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<TSource>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Max, source),
                source.Expression));
    }
    /// <summary>QueryDeferred extension method. Invokes a projection function on each element of a generic <see cref="T:System.Linq.IQueryable`1"></see> and returns the maximum resulting value.</summary>
    /// <param name="source">A sequence of values to determine the maximum of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TResult">The type of the value returned by the function represented by selector.</typeparam>
    /// <returns>QueryDeferred extension method. The maximum value in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<TResult> DeferredMax<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<TResult>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Max, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
}