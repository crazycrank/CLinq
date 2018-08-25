using System.Data.Entity.Infrastructure;
using System.Linq;
using CLinq.Core;

namespace CLinq.EntityFramework
{
    /// <inheritdoc cref="IDbAsyncEnumerable{T}"/>
    public class DbAsyncComposableQuery<T> : ComposableQuery<T>, IDbAsyncEnumerable<T>
    {
        internal DbAsyncComposableQuery(IQueryable<T> innerQuery)
            : base(innerQuery)
        {
            this.InnerProvider = new DbAsyncComposableQueryProvider<T>(this);
        }

        /// <inheritdoc />
        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
        {
            return this.InnerQuery is IDbAsyncEnumerable<T> asyncEnumerable
                       ? asyncEnumerable.GetAsyncEnumerator()
                       : new DbAsyncComposableEnumerator<T>(this.InnerQuery.GetEnumerator());
        }

        /// <inheritdoc />
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
            => this.GetAsyncEnumerator();
    }
}
