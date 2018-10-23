using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query.Internal;

namespace CLinq
{
    public partial class ComposableQueryProvider<T>
        : IAsyncQueryProvider
    {
        public IAsyncEnumerable<TResult> ExecuteAsync<TResult>(Expression expression)
        {
            var composed = this.ComposeExpression(expression ?? throw new ArgumentNullException(nameof(expression)));
            var asyncProvider = this._query.InnerQuery.Provider as IAsyncQueryProvider;
            return asyncProvider.ExecuteAsync<TResult>(composed);
        }

        public Task<TResult> ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken)
        {
            var composed = this.ComposeExpression(expression ?? throw new ArgumentNullException(nameof(expression)));
            return this._query.InnerQuery.Provider is IAsyncQueryProvider asyncProvider
                       ? asyncProvider.ExecuteAsync<TResult>(composed, cancellationToken)
                       : Task.FromResult(this._query.InnerQuery.Provider.Execute<TResult>(composed));
        }
    }
}
