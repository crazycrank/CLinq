using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;

namespace CLinq.ComposableQuery
{
    /// <summary>
    ///     An IQueryable wrapper that allows us to visit the query's expression tree just before LINQ to SQL gets to it.
    ///     This is based on the excellent work of Tomas Petricek: http://tomasp.net/blog/linq-expand.aspx
    /// </summary>
    public class ComposableQuery<T> : IOrderedQueryable<T>, IDbAsyncEnumerable<T>
    {
        private readonly ComposableQueryProvider<T> _provider;

        internal ComposableQuery(IQueryable<T> inner)
        {
            InnerQuery = inner;
            _provider = new ComposableQueryProvider<T>(this);
        }

        internal IQueryable<T> InnerQuery 
        {
            get;
        }


        /// <inheritdoc />
        Expression IQueryable.Expression => InnerQuery.Expression;

        /// <inheritdoc />
        Type IQueryable.ElementType => typeof(T);

        /// <inheritdoc />
        IQueryProvider IQueryable.Provider => _provider;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => InnerQuery.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => InnerQuery.GetEnumerator();

        /// <inheritdoc />
        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            switch (InnerQuery)
            {
                case IDbAsyncEnumerable<T> asyncEnumerable:
                    return asyncEnumerable.GetAsyncEnumerator();
                default:
                    return new ComposableDbAsyncEnumerator<T>(InnerQuery.GetEnumerator());
            }
        }

        /// <inheritdoc />
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() => GetAsyncEnumerator();

        /// <inheritdoc />
        public override string ToString() => InnerQuery.ToString();
    }
}
