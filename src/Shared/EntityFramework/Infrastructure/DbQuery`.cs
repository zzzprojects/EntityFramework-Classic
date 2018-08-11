// Copyright (c) Microsoft Open Technologies, Inc. All rights reserved. See License.txt in the project root for license information.

namespace System.Data.Entity.Infrastructure
{
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data.Entity.Internal;
    using System.Data.Entity.Internal.Linq;
    using System.Data.Entity.Resources;
    using System.Data.Entity.Utilities;
    using System.Diagnostics;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Linq.Expressions;
    using Z.EntityFramework.Classic;

    /// <summary>
    /// Represents a LINQ to Entities query against a DbContext.
    /// </summary>
    /// <typeparam name="TResult"> The type of entity to query for. </typeparam>
    [SuppressMessage("Microsoft.Naming", "CA1710:IdentifiersShouldHaveCorrectSuffix",
        Justification = "Name is intentional")]
    [DebuggerDisplay(@"{DebuggerDisplay()}")]
    public partial class DbQuery<TResult> : IOrderedQueryable<TResult>, IListSource, IInternalQueryAdapter
#if !NET40
, IDbAsyncEnumerable<TResult>
#endif
    {
        #region Fields and constructors

        // Handles the underlying ObjectQuery that backs the query.
        internal readonly IInternalQuery<TResult> _internalQuery;
        internal IQueryProvider _provider;

        // <summary>
        // Creates a new query that will be backed by the given internal query object.
        // </summary>
        // <param name="internalQuery"> The backing query. </param>
        internal DbQuery(IInternalQuery<TResult> internalQuery)
        {
            _internalQuery = internalQuery;
        }

        #endregion

        #region Include

        /// <summary>
        /// Specifies the related objects to include in the query results.
        /// </summary>
        /// <remarks>
        /// Paths are all-inclusive. For example, if an include call indicates Include("Orders.OrderLines"), not only will
        /// OrderLines be included, but also Orders.  When you call the Include method, the query path is only valid on
        /// the returned instance of the DbQuery&lt;T&gt;. Other instances of DbQuery&lt;T&gt; and the object context itself are not affected.
        /// Because the Include method returns the query object, you can call this method multiple times on an DbQuery&lt;T&gt; to
        /// specify multiple paths for the query.
        /// </remarks>
        /// <param name="path"> The dot-separated list of related objects to return in the query results. </param>
        /// <returns>
        /// A new <see cref="DbQuery{T}" /> with the defined query path.
        /// </returns>
        public virtual DbQuery<TResult> Include(string path)
        {
            Check.NotEmpty(path, "path");

            return _internalQuery == null ? this : new DbQuery<TResult>(_internalQuery.Include(path));
        }

        #endregion

        #region AsNoTracking

        /// <summary>
        /// Returns a new query where the entities returned will not be cached in the <see cref="DbContext" />.
        /// </summary>
        /// <returns> A new query with NoTracking applied. </returns>
        public virtual DbQuery<TResult> AsNoTracking()
        {
            return _internalQuery == null ? this : new DbQuery<TResult>(_internalQuery.AsNoTracking());
        }

        #endregion

        #region AsStreaming

        /// <summary>
        /// Returns a new query that will stream the results instead of buffering.
        /// </summary>
        /// <returns> A new query with AsStreaming applied. </returns>
        [Obsolete("Queries are now streaming by default unless a retrying ExecutionStrategy is used. Calling this method will have no effect.")]
        public virtual DbQuery<TResult> AsStreaming()
        {
            return _internalQuery == null ? this : new DbQuery<TResult>(_internalQuery.AsStreaming());
        }

        #endregion

        internal virtual DbQuery<TResult> WithExecutionStrategy(IDbExecutionStrategy executionStrategy)
        {
            return _internalQuery == null ? this : new DbQuery<TResult>(_internalQuery.WithExecutionStrategy(executionStrategy));
        }

        #region Data binding

        /// <summary>
        /// Returns <c>false</c>.
        /// </summary>
        /// <returns>
        /// <c>false</c> .
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        bool IListSource.ContainsListCollection
        {
            get { return false; }
        }

        /// <summary>
        /// Throws an exception indicating that binding directly to a store query is not supported.
        /// Instead populate a DbSet with data, for example by using the Load extension method, and
        /// then bind to local data.  For WPF bind to DbSet.Local.  For Windows Forms bind to
        /// DbSet.Local.ToBindingList().
        /// </summary>
        /// <returns> Never returns; always throws. </returns>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        IList IListSource.GetList()
        {
            throw Error.DbQuery_BindingToDbQueryNotSupported();
        }

        #endregion

        #region IEnumerable

        /// <summary>
        /// Returns an <see cref="IEnumerator{TElement}" /> which when enumerated will execute the query against the database.
        /// </summary>
        /// <returns> The query results. </returns>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        IEnumerator<TResult> IEnumerable<TResult>.GetEnumerator()
        {
            return GetInternalQueryWithCheck("IEnumerable<TResult>.GetEnumerator").GetEnumerator();
        }

        /// <summary>
        /// Returns an <see cref="IEnumerator{TElement}" /> which when enumerated will execute the query against the database.
        /// </summary>
        /// <returns> The query results. </returns>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetInternalQueryWithCheck("IEnumerable.GetEnumerator").GetEnumerator();
        }

        #endregion

        #region IDbAsyncEnumerable

#if !NET40

        /// <summary>
        /// Returns an <see cref="IDbAsyncEnumerator" /> which when enumerated will execute the query against the database.
        /// </summary>
        /// <returns> The query results. </returns>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
        {
            return GetInternalQueryWithCheck("IDbAsyncEnumerable.GetAsyncEnumerator").GetAsyncEnumerator();
        }

        /// <summary>
        /// Returns an <see cref="IDbAsyncEnumerator{TResult}" /> which when enumerated will execute the query against the database.
        /// </summary>
        /// <returns> The query results. </returns>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        IDbAsyncEnumerator<TResult> IDbAsyncEnumerable<TResult>.GetAsyncEnumerator()
        {
            return GetInternalQueryWithCheck("IDbAsyncEnumerable<TResult>.GetAsyncEnumerator").GetAsyncEnumerator();
        }

#endif

        #endregion

        #region IQueryable

        /// <summary>
        /// The IQueryable element type.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        Type IQueryable.ElementType
        {
            get { return GetInternalQueryWithCheck("IQueryable.ElementType").ElementType; }
        }

        /// <summary>
        /// The IQueryable LINQ Expression.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        Expression IQueryable.Expression
        {
            get { return GetInternalQueryWithCheck("IQueryable.Expression").Expression; }
        }

        /// <summary>
        /// The IQueryable provider.
        /// </summary>
        [SuppressMessage("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        IQueryProvider IQueryable.Provider
        {
            get
            {
                return _provider ?? (_provider = new DbQueryProvider(
                                                     GetInternalQueryWithCheck("IQueryable.Provider").InternalContext,
                                                     GetInternalQueryWithCheck("IQueryable.Provider")));
            }
        }

        #endregion

        #region Internal query

        // <summary>
        // The internal query object that is backing this DbQuery
        // </summary>
        IInternalQuery IInternalQueryAdapter.InternalQuery
        {
            get { return _internalQuery; }
        }

        // <summary>
        // The internal query object that is backing this DbQuery
        // </summary>
        internal IInternalQuery<TResult> InternalQuery
        {
            get { return _internalQuery; }
        }

        private IInternalQuery<TResult> GetInternalQueryWithCheck(string memberName)
        {
            if (_internalQuery == null)
            {
                throw new NotImplementedException(Strings.TestDoubleNotImplemented(memberName, GetType().Name, typeof(DbSet<>).Name));
            }

            return _internalQuery;
        }

        #endregion

        #region ToString

        /// <summary>
        /// Returns a <see cref="System.String" /> representation of the underlying query.
        /// </summary>
        /// <returns> The query string. </returns>
        public override string ToString()
        {
            return _internalQuery == null ? base.ToString() : _internalQuery.ToTraceString();
        }

        private string DebuggerDisplay()
        {
            return base.ToString();
        }

        /// <summary>
        /// Gets a <see cref="System.String" /> representation of the underlying query.
        /// </summary>
        public string Sql
        {
            get { return ToString(); }
        }

        #endregion

        #region Conversion to non-generic

        /// <summary>
        /// Returns a new instance of the non-generic <see cref="DbQuery" /> class for this query.
        /// </summary>
        /// <param name="entry">The query.</param>
        /// <returns> A non-generic version. </returns>
        [SuppressMessage("Microsoft.Design", "CA1065:DoNotRaiseExceptionsInUnexpectedLocations")]
        [SuppressMessage("Microsoft.Usage", "CA2225:OperatorOverloadsHaveNamedAlternates",
            Justification = "Intentionally just implicit to reduce API clutter.")]
        public static implicit operator DbQuery(DbQuery<TResult> entry)
        {
            if (entry._internalQuery == null)
            {
                throw new NotSupportedException(Strings.TestDoublesCannotBeConverted);
            }

            return new InternalDbQuery<TResult>(entry._internalQuery);
        }

        #endregion

        #region Hidden Object methods

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override bool Equals(object obj)
        {
            return base.Equals(obj);
        }

        /// <inheritdoc />
        [EditorBrowsable(EditorBrowsableState.Never)]
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <inheritdoc />
        [SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
        [EditorBrowsable(EditorBrowsableState.Never)]
        public new Type GetType()
        {
            return base.GetType();
        }

        #endregion

        #region Z.EntityFramework.Classic
        /// <summary>
        ///     Specifies the related objects to include in the query results and move in the Include chain to the TProperty.
        /// </summary>
        /// <remarks>
        ///     The path expression must be composed of simple property access expressions together with calls to Select for
        ///     composing additional includes after including a collection proprty.  Examples of possible include paths are:
        ///     To include a single reference: query.Include(e => e.Level1Reference)
        ///     To include a single collection: query.Include(e => e.Level1Collection)
        ///     To include a reference and then a reference one level down: query.Include(e => e.Level1Reference.Level2Reference)
        ///     To include a reference and then a collection one level down: query.Include(e => e.Level1Reference.Level2Collection)
        ///     To include a collection and then a reference one level down: query.Include(e => e.Level1Collection.Select(l1 =>
        ///     l1.Level2Reference))
        ///     To include a collection and then a collection one level down: query.Include(e => e.Level1Collection.Select(l1 =>
        ///     l1.Level2Collection))
        ///     To include a collection and then a reference one level down: query.Include(e => e.Level1Collection.Select(l1 =>
        ///     l1.Level2Reference))
        ///     To include a collection and then a collection one level down: query.Include(e => e.Level1Collection.Select(l1 =>
        ///     l1.Level2Collection))
        ///     To include a collection, a reference, and a reference two levels down: query.Include(e =>
        ///     e.Level1Collection.Select(l1 => l1.Level2Reference.Level3Reference))
        ///     To include a collection, a collection, and a reference two levels down: query.Include(e =>
        ///     e.Level1Collection.Select(l1 => l1.Level2Collection.Select(l2 => l2.Level3Reference)))
        ///     This extension method calls the Include(String) method of the source IQueryable object, if such a method exists.
        ///     If the source IQueryable does not have a matching method, then this method does nothing.
        ///     The Entity Framework ObjectQuery, ObjectSet, DbQuery, and DbSet types all have an appropriate Include method to
        ///     call.
        ///     When you call the Include method, the query path is only valid on the returned instance of the IQueryable&lt;T&gt;.
        ///     Other
        ///     instances of IQueryable&lt;T&gt; and the object context itself are not affected.  Because the Include method
        ///     returns the
        ///     query object, you can call this method multiple times on an IQueryable&lt;T&gt; to specify multiple paths for the
        ///     query.
        /// </remarks>
        /// <typeparam name="TProperty"> The type of navigation property being included. </typeparam>
        /// <param name="path"> A lambda expression representing the path to include. </param>
        /// <returns>
        ///     A new IncludeDbQuery&lt;TResult, TProperty&gt; with the defined query path.
        /// </returns>
        [SuppressMessage("Microsoft.Design", "CA1006:DoNotNestGenericTypesInMemberSignatures")]
        [SuppressMessage("Microsoft.Design", "CA1011:ConsiderPassingBaseTypesAsParameters")]
        public IncludeDbQuery<TResult, TProperty> Include<TProperty>(Expression<Func<TResult, TProperty>> path)
        {
            Check.NotNull(path, "path");

            string include;
            if (!DbHelpers.TryParsePath(path.Body, out include)
                || include == null)
            {
                throw new ArgumentException(Strings.DbExtensions_InvalidIncludePathExpression, "path");
            }

            var query = Include(include);
            return new IncludeDbQuery<TResult, TProperty>(query, include);
        }

        #endregion
    }
}