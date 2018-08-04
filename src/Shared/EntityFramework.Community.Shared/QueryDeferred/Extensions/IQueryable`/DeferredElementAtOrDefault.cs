using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Returns the element at a specified index in a sequence or a default value if the index is out of range.</summary>
    /// <param name="source">An <see cref="T:System.Linq.IQueryable`1"></see> to return an element from.</param>
    /// <param name="index">The zero-based index of the element to retrieve.</param>
    /// <typeparam name="TSource">The type of the elements of source.</typeparam>
    /// <returns>QueryDeferred extension method. default(<typeparamref name="TSource">TSource</typeparamref>) if <paramref name="index">index</paramref> is outside the bounds of <paramref name="source">source</paramref>; otherwise, the element at the specified position in <paramref name="source">source</paramref>.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source">source</paramref> is null.</exception>
    public static QueryDeferred<TSource> DeferredElementAtOrDefault<TSource>(this IQueryable<TSource> source, int index)
    {
        Check.NotNull(source, nameof(source));

        return new QueryDeferred<TSource>(
            source,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.ElementAtOrDefault, source, index),
                new[] { source.Expression, Expression.Constant(index) }
            ));
    }
}