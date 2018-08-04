using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Linq.Expressions;

namespace Z.EntityFramework.Classic
{
    /// <summary>
    ///     Represents a LINQ to Entities query against a DbContext that allow to to chain include ("ThenInclude,
    ///     "AlsoInclude").
    /// </summary>
    /// <typeparam name="TQuery">The type of entity being queried.</typeparam>
    /// <typeparam name="TPropertyCurrent">The type of the current property in the IncludeChain.</typeparam>
    public class IncludeDbQuery<TQuery, TPropertyCurrent> : DbQuery<TQuery>, IIncludeDbQuery<TQuery, TPropertyCurrent>
    {
        /// <summary>Constructor.</summary>
        /// <param name="query">A DbContext LINQ to Entities query.</param>
        /// <param name="includePath">The include path for the chain include.</param>
        internal IncludeDbQuery(DbQuery<TQuery> query, string includePath) : base(query.InternalQuery)
        {
            IncludePath = includePath;
        }

        /// <summary>Gets or sets the include path used to chain include.</summary>
        /// <value>The include path used to chain include.</value>
        public string IncludePath { get; set; }

        /// <summary>
        ///     Specifies the related objects to include in the query results and move in the Include chain to the TProperty.
        /// </summary>
        /// <exception cref="ArgumentException">
        ///     Thrown when one or more arguments have unsupported or illegal values.
        /// </exception>
        /// <typeparam name="TProperty">The type of navigation property being included.</typeparam>
        /// <param name="path">A lambda expression representing the path to include.</param>
        /// <returns>A new IncludeDbQuery&lt;TQuery, TProperty&gt; with the defined query path.</returns>
        public IIncludeDbQuery<TQuery, TProperty> Include<TProperty>(Expression<Func<TPropertyCurrent, TProperty>> path)
        {
            string include;
            if (!DbHelpers.TryParsePath(path.Body, out include) || include == null)
            {
                throw new ArgumentException(Strings.DbExtensions_InvalidIncludePathExpression, "path");
            }

            return new IncludeDbQuery<TQuery, TProperty>(Include(include), include);
        }
    }
}