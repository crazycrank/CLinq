using System;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace CLinq.Core.ComposableQuery.Core
{
    internal class ComposableQueryProvider<T> : IQueryProvider
    {
        [NotNull]
        private protected readonly ComposableQuery<T> Query;
        internal ComposableQueryProvider([NotNull] ComposableQuery<T> query) => this.Query = query;

        IQueryable<TElement> IQueryProvider.CreateQuery<TElement>(Expression expression)
        {
            var composed = expression.Compose();
            return this.Query.InnerQuery.Provider.CreateQuery<TElement>(composed).AsComposable();
        }

        IQueryable IQueryProvider.CreateQuery(Expression expression)
        {
            var composed = expression.Compose();
            return this.Query.InnerQuery.Provider.CreateQuery(composed);
        }

        TResult IQueryProvider.Execute<TResult>(Expression expression)
        {
            var composed = this.ComposeExpression(expression);
            return this.Query.InnerQuery.Provider.Execute<TResult>(composed);
        }

        object IQueryProvider.Execute(Expression expression)
        {
            var composed = this.ComposeExpression(expression);
            return this.Query.InnerQuery.Provider.Execute(composed);
        }
        
        [NotNull]
        private protected Expression ComposeExpression([NotNull] Expression expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));
            var composed = expression.Compose();
            var optimized = CLinqConfiguration.QueryOptimizer(composed);
            return optimized ?? throw new InvalidOperationException();
        }
    }
}