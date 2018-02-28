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

        internal IQueryable<T> InnerQuery // Original query, that we're wrapping
        {
            get;
        }


        /// <summary> Enumerator for async-await </summary>
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

        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() => GetAsyncEnumerator();

        Expression IQueryable.Expression => InnerQuery.Expression;

        Type IQueryable.ElementType => typeof(T);

        IQueryProvider IQueryable.Provider => _provider;

        /// <summary> IQueryable enumeration </summary>
        public IEnumerator<T> GetEnumerator() => InnerQuery.GetEnumerator();

        IEnumerator IEnumerable.GetEnumerator() => InnerQuery.GetEnumerator();

        /// <summary> IQueryable string presentation.  </summary>
        public override string ToString() => InnerQuery.ToString();
    }
}
