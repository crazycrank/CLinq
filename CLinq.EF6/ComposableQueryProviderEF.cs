#if !EF6_NOASYNC

using System;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;

using System.Data.Entity.Infrastructure;

namespace CLinq
{
    public partial class ComposableQueryProvider<T>
        : IDbAsyncQueryProvider
    {
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
    }
}
#endif
