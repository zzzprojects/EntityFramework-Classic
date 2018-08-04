using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Int32"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Int32"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<double> DeferredAverage(this IQueryable<int> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<double>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Int32"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Int32"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<double?> DeferredAverage(this IQueryable<int?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<double?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Int64"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Int64"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<double> DeferredAverage(this IQueryable<long> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<double>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Int64"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Int64"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<double?> DeferredAverage(this IQueryable<long?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<double?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Single"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Single"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<float> DeferredAverage(this IQueryable<float> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<float>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Single"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Single"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<float?> DeferredAverage(this IQueryable<float?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<float?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Double"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Double"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<double> DeferredAverage(this IQueryable<double> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<double>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Double"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Double"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<double?> DeferredAverage(this IQueryable<double?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<double?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Decimal"></see> values.</summary>
    /// <param name="source">A sequence of <see cref="T:System.Decimal"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<decimal> DeferredAverage(this IQueryable<decimal> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<decimal>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Decimal"></see> values.</summary>
    /// <param name="source">A sequence of nullable <see cref="T:System.Decimal"></see> values to calculate the average of.</param>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the source sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<decimal?> DeferredAverage(this IQueryable<decimal?> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<decimal?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source), source.Expression));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Int32"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<double> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<double>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Int32"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the <paramref name="source">source</paramref> sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<double?> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, int?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<double?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Single"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<float> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<float>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Single"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the <paramref name="source">source</paramref> sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<float?> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, float?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<float?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Int64"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<double> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<double>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Int64"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the <paramref name="source">source</paramref> sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<double?> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, long?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<double?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Double"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<double> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<double>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Double"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the <paramref name="source">source</paramref> sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<double?> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, double?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<double?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of <see cref="T:System.Decimal"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values that are used to calculate an average.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> contains no elements.</exception>
    public static QueryDeferred<decimal> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<decimal>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
    /// <summary>QueryDeferred extension method. Computes the average of a sequence of nullable <see cref="T:System.Decimal"></see> values that is obtained by invoking a projection function on each element of the input sequence.</summary>
    /// <param name="source">A sequence of values to calculate the average of.</param>
    /// <param name="selector">A projection function to apply to each element.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The average of the sequence of values, or null if the <paramref name="source">source</paramref> sequence is empty or contains only null values.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="selector">selector</paramref> is null.</exception>
    public static QueryDeferred<decimal?> DeferredAverage<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, decimal?>> selector)
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(selector, nameof(selector));

        return new QueryDeferred<decimal?>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Average, source, selector),
                new[] { source.Expression, Expression.Quote(selector) }
            ));
    }
}