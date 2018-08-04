using System;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Returns the element at a specified index in a sequence.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> to return an element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. The element at the specified position in <paramref name="source">source</paramref>.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    /// <exception cref="T:System.ArgumentOutOfRangeException"><paramref name="index">index</paramref> is less than zero.</exception>
    public static QueryDeferred<TSource> DeferredElementAt<TSource>(this IQueryable<TSource> source, int index)
    {
        Check.NotNull(source, nameof(source));
        if (index < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(index));
        }

        return new QueryDeferred<TSource>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.ElementAt, source, index),
                new[] { source.Expression, Expression.Constant(index) }
            ));
    }
}