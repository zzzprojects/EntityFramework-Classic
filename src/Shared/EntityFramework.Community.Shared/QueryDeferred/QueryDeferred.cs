using System;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;

namespace Z.EntityFramework.Classic
{
    /// <summary>A class to store immediate LINQ IQueryable query and expression deferred.</summary>
    /// <typeparam name="TResult">Type of the result of the query deferred.</typeparam>
    public class QueryDeferred<TResult>
    {
        /// <summary>Constructor.</summary>
        /// <param name="query">The deferred query.</param>
        /// <param name="expression">The deferred expression.</param>
        public QueryDeferred(IQueryable query, Expression expression)
        {
            Expression = expression;

            if (!(query is ObjectQuery))
            {
                // TODO: ZZZ - Must improve this a lot! must probably the TryGetObjectQuery to improve
                query = query.TryGetObjectQuery();
            }

            // CREATE query from the deferred expression
            var provider = query.Provider;
            var createQueryMethod = provider.GetType().GetMethod("CreateQuery", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance, null, new[] { typeof(Expression), typeof(Type) }, null);
            Query = (IQueryable<TResult>)createQueryMethod.Invoke(provider, new object[] { expression, typeof(TResult) });
        }

        /// <summary>Gets or sets the deferred expression.</summary>
        /// <value>The deferred expression.</value>
        public Expression Expression { get; protected internal set; }

        /// <summary>Gets or sets the deferred query.</summary>
        /// <value>The deferred query.</value>
        public IQueryable<TResult> Query { get; protected internal set; }

        /// <summary>Execute the deferred expression and return the result.</summary>
        /// <returns>The result of the deferred expression executed.</returns>
        public TResult Execute()
        {
            return Query.Provider.Execute<TResult>(Expression);
        }

#if !NET40
        /// <summary>Execute asynchrounously the deferred expression and return the result.</summary>
        /// <returns>The result of the deferred expression executed asynchrounously.</returns>
        public Task<TResult> ExecuteAsync()
        {
            return ExecuteAsync(default(CancellationToken));
        }

        /// <summary>Execute asynchrounously the deferred expression and return the result.</summary>
        /// <param name="cancellationToken">The cancellation token.</param>
        /// <returns>The result of the deferred expression executed asynchrounously.</returns>
        public Task<TResult> ExecuteAsync(CancellationToken cancellationToken)
        {
            var asyncQueryProvider = Query.Provider as IDbAsyncQueryProvider;

            return asyncQueryProvider != null ? asyncQueryProvider.ExecuteAsync<TResult>(Expression, cancellationToken) : Task.Run(() => Execute(), cancellationToken);
        }
#endif
    }
}