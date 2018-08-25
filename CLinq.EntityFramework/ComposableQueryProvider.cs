using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

namespace CLinq
{
    public class ComposableQueryProvider<T> : IDbAsyncQueryProvider
    {
        private readonly ComposableQuery<T> _query;

        public ComposableQueryProvider(ComposableQuery<T> query)
            => this._query = query;

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            var composed = expression.Compose();
            return this._query.InnerQuery.Provider.CreateQuery<TElement>(composed).AsComposable();
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            var composed = expression.Compose();
            return this._query.InnerQuery.Provider.CreateQuery(composed);
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            var composed = this.ComposeExpression(expression);
            return this._query.InnerQuery.Provider.Execute<TResult>(composed);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            var composed = this.ComposeExpression(expression);
            return this._query.InnerQuery.Provider.Execute(composed);
        }

        public Task<object> ExecuteAsync(Expression expression, CancellationToken cancellationToken)
        {
            var composed = this.ComposeExpression(expression ?? throw new ArgumentNullException(nameof(expression)));
            return this._query.InnerQuery.Provider is IDbAsyncQueryProvider asyncProvider
                       ? asyncProvider.ExecuteAsync(composed, cancellationToken)
                       : Task.FromResult(this._query.InnerQuery.Provider.Execute(composed));
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            var composed = this.ComposeExpression(expression ?? throw new ArgumentNullException(nameof(expression)));
            return this._query.InnerQuery.Provider is IDbAsyncQueryProvider asyncProvider
                       ? asyncProvider.ExecuteAsync<TResult>(composed, cancellationToken)
                       : Task.FromResult(this._query.InnerQuery.Provider.Execute<TResult>(composed));
        }

        private Expression ComposeExpression(Expression expression)
            => (expression ?? throw new ArgumentNullException(nameof(expression))).Compose();
    }
}
