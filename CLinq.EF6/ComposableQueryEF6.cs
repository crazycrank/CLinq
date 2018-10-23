#if !EF6_NOASYNC

using System.Data.Entity.Infrastructure;


namespace CLinq
{
    public sealed partial class ComposableQuery<T>
        : IDbAsyncEnumerable<T>
    {
        /// <inheritdoc />
        public IDbAsyncEnumerator<T> GetAsyncEnumerator()
            => this.InnerQuery is IDbAsyncEnumerable<T> asyncEnumerable
                   ? asyncEnumerable.GetAsyncEnumerator()
                   : new DbAsyncComposableEnumerator<T>(this.InnerQuery.GetEnumerator());

        /// <inheritdoc />
        IDbAsyncEnumerator IDbAsyncEnumerable.GetAsyncEnumerator()
            => this.GetAsyncEnumerator();
    }
}

#endif
