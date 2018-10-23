using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CLinq
{
    public sealed class AsyncComposableEnumerator<T> : IAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public AsyncComposableEnumerator(IEnumerator<T> inner)
            => this._inner = inner ?? throw new ArgumentNullException(nameof(inner));

        /// <inheritdoc />
        public Task<bool> MoveNext(CancellationToken cancellationToken)
            => Task.FromResult(this._inner.MoveNext());

        /// <inheritdoc />
        public T Current => this._inner.Current;

        /// <inheritdoc />
        T IAsyncEnumerator<T>.Current => this.Current;

        public void Dispose()
        {
            this._inner.Dispose();
        }
    }
}
