using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CLinq.ComposableQuery
{
    internal class ComposableQueryProvider<T> : IDbAsyncQueryProvider
    {
        private readonly ComposableQuery<T> _query;

        internal ComposableQueryProvider(ComposableQuery<T> query) => _query = query;

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            var composed = expression.Compose();
            return _query.InnerQuery.Provider.CreateQuery<TElement>(composed).AsComposable();
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            var composed = expression.Compose();
            return _query.InnerQuery.Provider.CreateQuery(composed);
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            var composed = ComposeExpression(expression);
            return _query.InnerQuery.Provider.Execute<TResult>(composed);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            var composed = ComposeExpression(expression);
            return _query.InnerQuery.Provider.Execute(composed);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            var asyncProvider = _query.InnerQuery.Provider as IDbAsyncQueryProvider;
            var composed = ComposeExpression(expression);

            return asyncProvider != null
                       ? asyncProvider.ExecuteAsync(composed, cancellationToken)
                       : Task.FromResult(_query.InnerQuery.Provider.Execute(composed));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            var asyncProvider = _query.InnerQuery.Provider as IDbAsyncQueryProvider;

            var composed = ComposeExpression(expression);
            return asyncProvider != null
                       ? asyncProvider.ExecuteAsync<TResult>(composed, cancellationToken)
                       : Task.FromResult(_query.InnerQuery.Provider.Execute<TResult>(composed));
        }
        
        private static Expression ComposeExpression(Expression expression)
        {
            var composed = expression.Compose();
            var optimized = CLinqConfiguration.QueryOptimizer(composed);
            return optimized;
        }
    }
}