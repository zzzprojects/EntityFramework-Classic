using System;

namespace Z.EntityFramework.Classic
{
    /// <summary>A QueryResultFilter.</summary>
    public class QueryResultFilter
    {
        private readonly QueryResultFilterManager _manager;

        private bool _isEnabled;

        /// <summary>Constructor.</summary>
        /// <param name="manager">The manager.</param>
        /// <param name="elementType">Type of the element.</param>
        internal QueryResultFilter(QueryResultFilterManager manager, Type elementType)
        {
            _manager = manager;
            ElementType = elementType;
        }

        /// <summary>Gets the type of the element to filter.</summary>
        /// <value>The type of the element to filter.</value>
        public Type ElementType { get; }

        /// <summary>Gets the filter id.</summary>
        /// <value>The filter id.</value>
        public string ID { get; internal set; }

        /// <summary>Gets a value indicating whether the filter and the QueryResultFilterManager is enabled.</summary>
        /// <value>True if the filter and the QueryResultFilterManager is enabled, false if not.</value>
        public bool IsEnabled => _isEnabled && _manager.IsEnabled;

        /// <summary>Disables the filter.</summary>
        public void Disable()
        {
            _isEnabled = false;
        }

        /// <summary>Enables the filter.</summary>
        public void Enable()
        {
            _isEnabled = true;
        }

        /// <summary>Applies the filter described by source.</summary>
        /// <exception cref="Exception">Thrown when an exception error condition occurs.</exception>
        /// <param name="source">Source for the.</param>
        /// <returns>An object.</returns>
        public virtual object ApplyFilter(object source)
        {
            throw new Exception("Not implemented");
        }
    }
}