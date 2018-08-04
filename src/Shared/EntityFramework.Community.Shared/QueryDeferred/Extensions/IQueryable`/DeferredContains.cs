using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Determines whether a sequence contains a specified element by using the default equality comparer.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> in which to locate item.</param>
    /// <param name="item">The object to locate in the sequence.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. true if the input sequence contains an element that has the specified value; otherwise, false.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<bool> DeferredContains<TSource>(this IQueryable<TSource> source, TSource item)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<bool>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Contains, source, item),
                new[] { source.Expression, Expression.Constant(item, typeof(TSource)) }
            ));
    }
    /// <summary>QueryDeferred extension method. Determines whether a sequence contains a specified element by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see>.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> in which to locate item.</param>
    /// <param name="item">The object to locate in the sequence.</param>
    /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> to compare values.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. true if the input sequence contains an element that has the specified value; otherwise, false.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<bool> DeferredContains<TSource>(this IQueryable<TSource> source, TSource item, IEqualityComparer<TSource> comparer)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<bool>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.Contains, source, item, comparer),
                new[] { source.Expression, Expression.Constant(item, typeof(TSource)), Expression.Constant(comparer, typeof(IEqualityComparer<TSource>)) }
            ));
    }
}