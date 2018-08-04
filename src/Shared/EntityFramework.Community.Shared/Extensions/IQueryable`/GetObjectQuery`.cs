using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>Returns the queryable typed as DbQuery&lt;T&gt;.</summary>
    /// <typeparam name="T">The type of entity being queried.</typeparam>
    /// <param name="this">The IQueryable to act on.</param>
    /// <returns>A DbQuery&lt;T&gt;</returns>
    public static ObjectQuery<T> GetObjectQuery<T>(this IQueryable<T> @this)
    {
        return (ObjectQuery<T>)@this.TryGetObjectQuery();
    }
}