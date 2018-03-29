using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;

namespace CLinq.Core.ComposableQuery.Entity
{
    /// <summary> Class for async-await style list enumeration support (e.g. .ToListAsync())</summary>
    public sealed class DbAsyncComposableEnumerator<T> : IDbAsyncEnumerator<T>
    {
        [NotNull]
        private readonly IEnumerator<T> _inner;

        /// <summary> Class for async-await style list enumeration support (e.g. .ToListAsync())</summary>
        public DbAsyncComposableEnumerator([NotNull] IEnumerator<T> inner)
            => this._inner = inner ?? throw new ArgumentNullException(nameof(inner));

        /// <inheritdoc />
        public void Dispose()
        {
            this._inner.Dispose();
        }

        /// <inheritdoc />
        public Task<bool> MoveNextAsync(CancellationToken cancellationToken) 
            => Task.FromResult(this._inner.MoveNext());


        /// <summary> Enumerator-pattern: Current item </summary>
        public T Current => this._inner.Current;

        object IDbAsyncEnumerator.Current => this.Current;
    }
}