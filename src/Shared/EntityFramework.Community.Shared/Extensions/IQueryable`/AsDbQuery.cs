using System.Data.Entity.Infrastructure;
using System.Linq;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>
    ///     Returns the queryable typed as DbQuery&lt;T&gt;.
    /// </summary>
    /// <typeparam name="TQuery">The type of entity being queried.</typeparam>
    /// <param name="this">The IQueryable to act on.</param>
    /// <returns>A DbQuery&lt;T&gt;</returns>
    public static DbQuery<TQuery> AsDbQuery<TQuery>(this IQueryable<TQuery> @this)
    {
        return (DbQuery<TQuery>) @this;
    }
}