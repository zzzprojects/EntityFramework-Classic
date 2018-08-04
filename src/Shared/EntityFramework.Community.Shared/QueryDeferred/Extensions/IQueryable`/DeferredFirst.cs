using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Returns the first element of a sequence.</summary>
    /// <param name="source">The <see cref="T:System.Linq.IQueryable`1"></see> to return the first element of.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The first element in <paramref name="source">source</paramref>.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">The source sequence is empty.</exception>
    public static QueryDeferred<TSource> DeferredFirst<TSource>(this IQueryable<TSource> source)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<TSource>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.First, source),
                source.Expression
            ));
    }
    /// <summary>QueryDeferred extension method. Returns the first element of a sequence that satisfies a specified condition.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> to return an element from.</param>
    /// <param name="predicate">A function to test each element for a condition.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The first element in <paramref name="source">source</paramref> that passes the test in <paramref name="predicate">predicate</paramref>.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> or <paramref name="predicate">predicate</paramref> is null.</exception>
    /// <exception cref="T:System.InvalidOperationException">No element satisfies the condition in <paramref name="predicate">predicate</paramref>.
    /// -or-
    /// The source sequence is empty.</exception>
    public static QueryDeferred<TSource> DeferredFirst<TSource>(this IQueryable<TSource> source, Expression<Func<TSource, bool>> predicate) where TSource : class
    {
        Check.NotNull(source, nameof(source));
        Check.NotNull(predicate, nameof(predicate));

        return new QueryDeferred<TSource>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.First, source, predicate),
                new[] { source.Expression, Expression.Quote(predicate) }
            ));
    }
}