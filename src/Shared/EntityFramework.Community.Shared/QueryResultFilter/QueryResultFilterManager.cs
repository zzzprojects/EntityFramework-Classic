using System;
using System.Collections.Generic;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Core.Objects.Internal;

namespace Z.EntityFramework.Classic
{
    /// <summary>QueryResultFilterManager</summary>
    public class QueryResultFilterManager
    {
        /// <summary>Gets a value indicating whether the QueryResultFilterManager is enabled.</summary>
        /// <value>True if the QueryResultFilterManager is enabled, false if not.</value>
        public bool IsEnabled { get; } = true;

        /// <summary>Gets all filters.</summary>
        /// <value>All filters.</value>
        public List<QueryResultFilter> Filters { get; } = new List<QueryResultFilter>();

        /// <summary>Disables the QueryResultFilterManager. All QueryResultFilters are disabled when the manager is disabled.</summary>
        public void Disable()
        {
        }

        /// <summary>Disable the filter with the specified id.</summary>
        /// <param name="id">The id for the filter to disable.</param>
        public void DisableFilter(string id)
        {
            var filter = GetFilter(id);
            filter?.Disable();
        }

        /// <summary>Enables the QueryResultFilterManager.</summary>
        public void Enable()
        {
        }

        /// <summary>Enables the filter with the specified id.</summary>
        /// <param name="id">The id for the filter to enable.</param>
        public void EnableFilter(string id)
        {
            var filter = GetFilter(id);
            filter?.Enable();
        }

        /// <summary>Gets the filter with the specified id.</summary>
        /// <param name="id">The filter id.</param>
        /// <returns>The filter with the specified id.</returns>
        public QueryResultFilter GetFilter(string id)
        {
            return null;
        }
        /// <summary>
        /// Create a new QueryResultFilter that will filter the entity using a predicate.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="filter">The filter predicate.</param>
        /// <returns>A QueryResultFilter&lt;T&gt;.</returns>
        public QueryResultFilter<T> Filter<T>(Func<T, bool> filter)
        {
            return Filter(Guid.NewGuid().ToString(), filter);
        }
        /// <summary>
        /// Create a new QueryResultFilter that will filter the entity using a predicate.
        /// </summary>
        /// <typeparam name="T">Generic type parameter.</typeparam>
        /// <param name="id">The filter id.</param>
        /// <param name="filter">SThe filter predicate.</param>
        /// <returns>A QueryResultFilter&lt;T&gt;</returns>
        public QueryResultFilter<T> Filter<T>(string id, Func<T, bool> filter)
        {
            var resultFilter = new QueryResultFilter<T>(this, filter);
            Filters.Add(resultFilter);
            return resultFilter;
        }

        internal bool IsFilterRemoved(IEntityWrapper result)
        {
            if (result.Entity != null)
            {
                return IsFilterRemoved(result.Entity);
            }

            return false;
        }
        /// <summary>Applies the filter described by result.</summary>
        /// <param name="result">The result.</param>
        /// <returns>An object.</returns>
        public bool IsFilterRemoved(object result)
        {
            if (result != null)
            {
                var resultType = ObjectContext.GetObjectType(result.GetType());

                // Must break somewhere when already null!
                foreach (var filter in Filters)
                {
                    if (filter.IsEnabled && EntityFrameworkManager.IsAssignableFrom(resultType, filter.ElementType))
                    {
                        result = filter.ApplyFilter(result);

                        if (result == null)
                        {
                            return true;
                        }
                    }
                }
            }

            return false;
        }
    }
}