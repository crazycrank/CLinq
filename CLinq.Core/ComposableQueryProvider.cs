using System;
using System.Linq;
using System.Linq.Expressions;

namespace CLinq.Core
{
    public class ComposableQueryProvider<T> : IQueryProvider
    {
        
        protected virtual ComposableQuery<T> Query { get; }

        public ComposableQueryProvider(ComposableQuery<T> query) => this.Query = query;

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
        
        protected virtual Expression ComposeExpression(Expression expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            return expression.Compose();
        }
    }
}