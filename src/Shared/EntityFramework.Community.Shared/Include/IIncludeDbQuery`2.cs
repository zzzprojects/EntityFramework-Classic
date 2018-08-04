using System.Data.Entity.Infrastructure;
using System.Linq;

namespace Z.EntityFramework.Classic
{
    /// <summary>Interface for include database query.</summary>
    /// <typeparam name="TQuery">Type of the query.</typeparam>
    /// <typeparam name="TPropertyCurrent">Type of the property current.</typeparam>
    public interface IIncludeDbQuery<TQuery, out TPropertyCurrent> : IQueryable<TQuery>
    {
        /// <summary>Gets or sets the include path used to chain include.</summary>
        /// <value>The include path used to chain include.</value>
        string IncludePath { get; set; }

        /// <summary>
        ///     Specifies the related objects to include in the query results and move in the Include chain to the TProperty.
        /// </summary>
        /// <param name="path"> A lambda expression representing the path to include. </param>
        /// <returns>
        ///     A new DbQuery&lt;TResult&gt; with the defined query path.
        /// </returns>
        DbQuery<TQuery> Include(string path);
    }
}