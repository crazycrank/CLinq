using System.Collections.Generic;

namespace CLinq
{
    public sealed partial class ComposableQuery<T>
        : IAsyncEnumerable<T>
    {
        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
            => (IEnumerator<T>) (this.InnerQuery is IAsyncEnumerable<T> asyncEnumerable
                                     ? asyncEnumerable.GetEnumerator()
                                     : new AsyncComposableEnumerator<T>(this.InnerQuery.GetEnumerator()));

        /// <inheritdoc />
        IAsyncEnumerator<T> IAsyncEnumerable<T>.GetEnumerator()
            => (IAsyncEnumerator<T>)this.GetEnumerator();
    }
}
