using System;
using System.Collections.Generic;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Linq.Expressions;
using Z.EntityFramework.Classic;

public static partial class EntityFrameworkClassicExtensions
{
    /// <summary>
    ///     Specifies the related objects to include in the query results and move in the Include chain to the TProperty.
    /// </summary>
    /// <exception cref="ArgumentException">
    ///     Thrown when one or more arguments have unsupported or illegal values.
    /// </exception>
    /// <typeparam name="TQuery">The type of entity being queried.</typeparam>
    /// <typeparam name="TPropertyCurrent">The type of the current property in the IncludeChain.</typeparam>
    /// <typeparam name="TProperty">The type of navigation property being included.</typeparam>
    /// <param name="this">The IIncludeDbQuery to act on.</param>
    /// <param name="path">A lambda expression representing the path to include.</param>
    /// <returns>A new IncludeDbQuery&lt;TQuery, TProperty&gt; with the defined query path.</returns>
    public static IncludeDbQuery<TQuery, TProperty> ThenInclude<TQuery, TPropertyCurrent, TProperty>(this IIncludeDbQuery<TQuery, TPropertyCurrent> @this, Expression<Func<TPropertyCurrent, TProperty>> path)
    {
        string include;
        if (!DbHelpers.TryParsePath(path.Body, out include) || include == null)
        {
            throw new ArgumentException(Strings.DbExtensions_InvalidIncludePathExpression, "path");
        }

        include = @this.IncludePath + "." + include;

        return new IncludeDbQuery<TQuery, TProperty>(@this.Include(include), include);
    }

    /// <summary>
    ///     Specifies the related objects to include in the query results and move in the Include chain to the TProperty.
    /// </summary>
    /// <exception cref="ArgumentException">
    ///     Thrown when one or more arguments have unsupported or illegal values.
    /// </exception>
    /// <typeparam name="TQuery">The type of entity being queried.</typeparam>
    /// <typeparam name="TPropertyCurrent">The type of the current property in the IncludeChain.</typeparam>
    /// <typeparam name="TProperty">The type of navigation property being included.</typeparam>
    /// <param name="this">The IIncludeDbQuery to act on.</param>
    /// <param name="path">A lambda expression representing the path to include.</param>
    /// <returns>A new IncludeDbQuery&lt;TQuery, TProperty&gt; with the defined query path.</returns>
    public static IncludeDbQuery<TQuery, TProperty> ThenInclude<TQuery, TPropertyCurrent, TProperty>(this IIncludeDbQuery<TQuery, IEnumerable<TPropertyCurrent>> @this, Expression<Func<TPropertyCurrent, TProperty>> path)
    {
        string include;
        if (!DbHelpers.TryParsePath(path.Body, out include) || include == null)
        {
            throw new ArgumentException(Strings.DbExtensions_InvalidIncludePathExpression, "path");
        }

        include = @this.IncludePath + "." + include;

        return new IncludeDbQuery<TQuery, TProperty>(@this.Include(include), include);
    }
}