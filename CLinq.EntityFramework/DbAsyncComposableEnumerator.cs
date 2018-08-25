using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace CLinq.EntityFramework
{
    public sealed class DbAsyncComposableEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        public DbAsyncComposableEnumerator(IEnumerator<T> inner)
            => this._inner = inner ?? throw new ArgumentNullException(nameof(inner));

        /// <inheritdoc />
        public Task<bool> MoveNextAsync(CancellationToken cancellationToken)
            => Task.FromResult(this._inner.MoveNext());

        /// <inheritdoc />
        public T Current => this._inner.Current;

        /// <inheritdoc />
        object IDbAsyncEnumerator.Current => this.Current;

        public void Dispose()
        {
            this._inner.Dispose();
        }
    }
}