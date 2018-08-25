using System;
using System.Data.Entity.Infrastructure;
using System.Linq;
using CLinq.Core;
using JetBrains.Annotations;

namespace CLinq.EntityFramework
{
    /// <inheritdoc cref="IDbAsyncEnumerable{T}"/>
    public class DbAsyncComposableQuery<T, TProvider> : ComposableQuery<T, TProvider>, IDbAsyncEnumerable<T>
        where TProvider : ComposableQueryProvider<T>
    {
        internal DbAsyncComposableQuery([NotNull] IQueryable<T> innerQuery)
            : base(innerQuery)
        {
            this.InnerProvider = (TProvider)Activator.CreateInstance(typeof(TProvider), this);
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
