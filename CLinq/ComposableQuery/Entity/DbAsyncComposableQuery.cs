using System.Data.Entity.Infrastructure;
using System.Linq;
using CLinq.Core.ComposableQuery.Core;
using JetBrains.Annotations;

namespace CLinq.Core.ComposableQuery.Entity
{
    /// <summary>
    ///     An IQueryable wrapper that allows us to visit the query's expression tree just before LINQ to SQL gets to it.
    ///     This is based on the excellent work of Tomas Petricek: http://tomasp.net/blog/linq-expand.aspx
    /// </summary>
    public class DbAsyncComposableQuery<T> : ComposableQuery<T>, IDbAsyncEnumerable<T>
    {
        internal DbAsyncComposableQuery([NotNull] IQueryable<T> innerQuery) : base(innerQuery)
        {
            this.InnerProvider = new DbAsyncComposableQueryProvider<T>(this);
        }

        /// <inheritdoc />
        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            switch (this.InnerQuery)
            {
                case IDbAsyncEnumerable<T> asyncEnumerable:
                    return asyncEnumerable.GetAsyncEnumerator();
                default:
                    return new DbAsyncComposableEnumerator<T>(this.InnerQuery.GetEnumerator());
            }
        }

        /// <inheritdoc />
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator() => this.GetAsyncEnumerator();
    }
}
