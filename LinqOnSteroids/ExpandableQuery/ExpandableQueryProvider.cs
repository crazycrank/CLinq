using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace LinqOnSteroids.ExpandableQuery
{
    internal class ExpandableQueryProvider<T> : IDbAsyncQueryProvider
    {
        private readonly ExpandableQuery<T> _query;

        internal ExpandableQueryProvider(ExpandableQuery<T> query) => _query = query;

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            var expanded = expression.Expand();
            return _query.InnerQuery.Provider.CreateQuery<TElement>(expanded).AsExpandable();
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            var expanded = expression.Expand();
            return _query.InnerQuery.Provider.CreateQuery(expanded);
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            var expanded = ExpandExpression(expression);
            return _query.InnerQuery.Provider.Execute<TResult>(expanded);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            var expanded = ExpandExpression(expression);
            return _query.InnerQuery.Provider.Execute(expanded);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            var asyncProvider = _query.InnerQuery.Provider as IDbAsyncQueryProvider;
            var expanded = ExpandExpression(expression);

            return asyncProvider != null
                       ? asyncProvider.ExecuteAsync(expanded, cancellationToken)
                       : Task.FromResult(_query.InnerQuery.Provider.Execute(expanded));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            var asyncProvider = _query.InnerQuery.Provider as IDbAsyncQueryProvider;

            var expanded = ExpandExpression(expression);
            return asyncProvider != null
                       ? asyncProvider.ExecuteAsync<TResult>(expanded, cancellationToken)
                       : Task.FromResult(_query.InnerQuery.Provider.Execute<TResult>(expanded));
        }
        
        private Expression ExpandExpression(Expression expression)
        {
            var expanded = expression.Expand();
            var optimized = LinqOnSteroidsConfiguration.QueryOptimizer(expanded);
            return optimized;
        }
    }
}