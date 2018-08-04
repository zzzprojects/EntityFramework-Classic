using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Returns the only element of a sequence, and throws an exception if there is not exactly one element in the sequence.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> to return the single element of.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The single element of the input sequence.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException"><paramref name="source">source</paramref> has more than one element.</exception>
    public static QueryDeferred<TSource> DeferredSingle<TSource>(this IQueryable<TSource> source)
    {
        Check.NotNull(source, nameof(source));


        return new QueryDeferred<TSource>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Single, source),
                source.Expression
            ));
    }
    /// <summary>QueryDeferred extension method. Returns the only element of a sequence that satisfies a specified condition, and throws an exception if more than one such element exists.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> to return a single element from.</param>
    /// <param name="predicate">A function to test an element for a condition.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The single element of the input sequence that satisfies the condition in <paramref name="predicate">predicate</paramref>.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="predicate">predicate</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate">predicate</paramref>.
    /// -or-
    /// More than one element satisfies the condition in <paramref name="predicate">predicate</paramref>.
    /// -or-
    /// The source sequence is empty.</exception>
    public static QueryDeferred<TSource> DeferredSingle<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) where TSource : class
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(predicate, nameof(predicate));

        return new QueryDeferred<TSource>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Single, source, predicate),
                new[] { source.Expression, Expression.Quote(predicate) }
            ));
    }
}