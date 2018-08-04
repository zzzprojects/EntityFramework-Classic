using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>QueryDeferred extension method. Determines whether two sequences are equal by using the default equality comparer to compare elements.</summary>
    /// <param name="source1">An <see cref="T:System.Linq.IQueryable`1"></see> whose elements to compare to those of source2.</param>
    /// <param name="source2">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> whose elements to compare to those of the first sequence.</param>
    /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
    /// <returns>QueryDeferred extension method. true if the two source sequences are of equal length and their corresponding elements compare equal; otherwise, false.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source1">source1</paramref> or <paramref name="source2">source2</paramref> is null.</exception>
    public static QueryDeferred<bool> DeferredSequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2)
    {
        Check.NotNull(source1, nameof(source1));
        Check.NotNull(source2, nameof(source2));

        return new QueryDeferred<bool>(
            source1,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.SequenceEqual, source1, source2),
                new[] { source1.Expression, GetSourceExpression(source2) }
            ));
    }
    /// <summary>QueryDeferred extension method. Determines whether two sequences are equal by using a specified <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> to compare elements.</summary>
    /// <param name="source1">An <see cref="T:System.Linq.IQueryable`1"></see> whose elements to compare to those of source2.</param>
    /// <param name="source2">An <see cref="T:System.Collections.Generic.IEnumerable`1"></see> whose elements to compare to those of the first sequence.</param>
    /// <param name="comparer">An <see cref="T:System.Collections.Generic.IEqualityComparer`1"></see> to use to compare elements.</param>
    /// <typeparam name="TSource">The type of the elements of the input sequences.</typeparam>
    /// <returns>QueryDeferred extension method. true if the two source sequences are of equal length and their corresponding elements compare equal; otherwise, false.</returns>
    /// <exception cref="T:System.ArgumentNullException"><paramref name="source1">source1</paramref> or <paramref name="source2">source2</paramref> is null.</exception>
    public static QueryDeferred<bool> DeferredSequenceEqual<TSource>(this IQueryable<TSource> source1, IEnumerable<TSource> source2, IEqualityComparer<TSource> comparer)
    {
        Check.NotNull(source1, nameof(source1));
        Check.NotNull(source2, nameof(source2));

        return new QueryDeferred<bool>(
            source1,
            Expression.Call(
                null,
                GetMethodInfo(Queryable.SequenceEqual, source1, source2, comparer),
                new[]
                {
                    source1.Expression,
                    GetSourceExpression(source2),
                    Expression.Constant(comparer, typeof(IEqualityComparer<TSource>))
                }
            ));
    }
}