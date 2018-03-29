using System;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;
using System.Threading;
using System.Threading.Tasks;
using CLinq.Core.ComposableQuery.Core;
using JetBrains.Annotations;

namespace CLinq.Core.ComposableQuery.Entity
{
    internal class DbAsyncComposableQueryProvider<T> : ComposableQueryProvider<T>, IDbAsyncQueryProvider
    {
        internal DbAsyncComposableQueryProvider([NotNull] DbAsyncComposableQuery<T> query)
            : base(query)
        { }

        public Task<object> ExecuteAsync([NotNull] Expression expression, CancellationToken cancellationToken)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var composed = this.ComposeExpression(expression);
            return this.Query.InnerQuery.Provider is IDbAsyncQueryProvider asyncProvider
                       ? asyncProvider.ExecuteAsync(composed, cancellationToken)
                       : Task.FromResult(this.Query.InnerQuery.Provider.Execute(composed));
        }

        public Task<TResult> ExecuteAsync<TResult>([NotNull] Expression expression, CancellationToken cancellationToken)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            var composed = this.ComposeExpression(expression);
            return this.Query.InnerQuery.Provider is IDbAsyncQueryProvider asyncProvider
                       ? asyncProvider.ExecuteAsync<TResult>(composed, cancellationToken)
                       : Task.FromResult(this.Query.InnerQuery.Provider.Execute<TResult>(composed));
        }
    }
}