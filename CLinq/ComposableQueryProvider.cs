using System;
using System.Linq;
using System.Linq.Expressions;

namespace CLinq
{
    public partial class ComposableQueryProvider<T>
        : IQueryProvider
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

        private Expression ComposeExpression(Expression expression)
            => (expression ?? throw new ArgumentNullException(nameof(expression))).Compose();
    }
}
