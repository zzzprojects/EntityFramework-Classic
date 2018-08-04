using System;

namespace Z.EntityFramework.Classic
{
    /// <summary>A QueryResultFilter&lt;T&gt;.</summary>
    /// <typeparam name="T">Generic type parameter.</typeparam>
    public class QueryResultFilter<T> : QueryResultFilter
    {
        /// <summary>Constructor.</summary>
        /// <param name="manager">The QueryResultFilterManager.</param>
        /// <param name="filter">The filter predicate.</param>
        public QueryResultFilter(QueryResultFilterManager manager, Func<T, bool> filter) : base(manager, typeof(T))
        {
            Filter = filter;
        }

        /// <summary>Gets or sets the filter predicate.</summary>
        /// <value>The filter predicate.</value>
        public Func<T, bool> Filter { get; internal set; }

        /// <summary>Applies the filter described by source.</summary>
        /// <param name="source">Source for the.</param>
        /// <returns>An object.</returns>
        public override object ApplyFilter(object source)
        {
            if (Filter((T) source))
            {
                return source;
            }

            return null;
        }
    }
}