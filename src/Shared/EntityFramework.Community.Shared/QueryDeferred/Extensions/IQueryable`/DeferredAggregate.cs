using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Applies an accumulator function over a sequence.</summary>
    /// <param name="source">A sequence to aggregate over.</param>
    /// <param name="func">An accumulator function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The final accumulator value.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="func">func</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<TSource> DeferredAggregate<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, TSource, TSource>> func)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(func, nameof(func));

        return new QueryDeferred<TSource>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Aggregate, source, func),
                new[] { source.Expression, Expression.Quote(func) }
            ));
    }

    /// <summary>QueryDeferred extension method. Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value.</summary>
    /// <param name="source">A sequence to aggregate over.</param>
    /// <param name="seed">The initial accumulator value.</param>
    /// <param name="func">An accumulator function to invoke on each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
    /// <returns>QueryDeferred extension method. The final accumulator value.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="func">func</paramref> is null.</exception>
    public static QueryDeferred<TAccumulate> DeferredAggregate<TSource, TAccumulate>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(func, nameof(func));

        return new QueryDeferred<TAccumulate>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Aggregate, source, seed, func),
                new[] { source.Expression, Expression.Constant(seed), Expression.Quote(func) }
            ));
    }

    /// <summary>QueryDeferred extension method. Applies an accumulator function over a sequence. The specified seed value is used as the initial accumulator value, and the specified function is used to select the result value.</summary>
    /// <param name="source">A sequence to aggregate over.</param>
    /// <param name="seed">The initial accumulator value.</param>
    /// <param name="func">An accumulator function to invoke on each element.</param>
    /// <param name="selector">A function to transform the final accumulator value into the result value.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <typeparam name="TAccumulate">The type of the accumulator value.</typeparam>
    /// <typeparam name="TResult">The type of the resulting value.</typeparam>
    /// <returns>QueryDeferred extension method. The transformed final accumulator value.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="func">func</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<TResult> DeferredAggregate<TSource, TAccumulate, TResult>(this IQueryable<TSource> source, TAccumulate seed, Expression<Func<TAccumulate, TSource, TAccumulate>> func, Expression<Func<TAccumulate, TResult>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(func, nameof(func));
        Check.NotNull(selector, nameof(selector));


        return new QueryDeferred<TResult>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Aggregate, source, seed, func, selector),
                source.Expression,
                Expression.Constant(seed),
                Expression.Quote(func),
                Expression.Quote(selector)
            ));
    }
}