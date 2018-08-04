using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of <see cref="T:System.Int32"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Int32"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Int32.MaxValue"></see>.</exception>
    public static QueryDeferred<int> DeferredSum(this IQueryable<int> source)
    {
        Check.NotNull(source, nameof(source));


        return new QueryDeferred<int>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of nullable <see cref="T:System.Int32"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Int32"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Int32.MaxValue"></see>.</exception>
    public static QueryDeferred<int?> DeferredSum(this IQueryable<int?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<int?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of <see cref="T:System.Int64"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Int64"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Int64.MaxValue"></see>.</exception>
    public static QueryDeferred<long> DeferredSum(this IQueryable<long> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<long>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of nullable <see cref="T:System.Int64"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Int64"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Int64.MaxValue"></see>.</exception>
    public static QueryDeferred<long?> DeferredSum(this IQueryable<long?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<long?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of <see cref="T:System.Single"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Single"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<float> DeferredSum(this IQueryable<float> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<float>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of nullable <see cref="T:System.Single"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Single"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<float?> DeferredSum(this IQueryable<float?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<float?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of <see cref="T:System.Double"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Double"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<double> DeferredSum(this IQueryable<double> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<double>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of nullable <see cref="T:System.Double"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Double"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<double?> DeferredSum(this IQueryable<double?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<double?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of <see cref="T:System.Decimal"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Decimal"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Decimal.MaxValue"></see>.</exception>
    public static QueryDeferred<decimal> DeferredSum(this IQueryable<decimal> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<decimal>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of a sequence of nullable <see cref="T:System.Decimal"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Decimal"></see> values to calculate the sum of.</param>
    /// <returns>QueryDeferred extension method. The sum of the values in the sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Decimal.MaxValue"></see>.</exception>
    public static QueryDeferred<decimal?> DeferredSum(this IQueryable<decimal?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<decimal?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of <see cref="T:System.Int32"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Int32.MaxValue"></see>.</exception>
    public static QueryDeferred<int> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<int>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of nullable <see cref="T:System.Int32"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Int32.MaxValue"></see>.</exception>
    public static QueryDeferred<int?> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<int?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of <see cref="T:System.Int64"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Int64.MaxValue"></see>.</exception>
    public static QueryDeferred<long> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<long>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of nullable <see cref="T:System.Int64"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Int64.MaxValue"></see>.</exception>
    public static QueryDeferred<long?> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<long?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of <see cref="T:System.Single"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<float> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<float>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of nullable <see cref="T:System.Single"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<float?> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<float?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of <see cref="T:System.Double"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<double> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<double>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of nullable <see cref="T:System.Double"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<double?> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<double?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of <see cref="T:System.Decimal"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Decimal.MaxValue"></see>.</exception>
    public static QueryDeferred<decimal> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<decimal>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the sum of the sequence of nullable <see cref="T:System.Decimal"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values of type TSource.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The sum of the projected values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.OverflowException">The sum is larger than <see cref="System.Decimal.MaxValue"></see>.</exception>
    public static QueryDeferred<decimal?> DeferredSum<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<decimal?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Sum, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
}