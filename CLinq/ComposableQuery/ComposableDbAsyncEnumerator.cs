using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Threading;
using System.Threading.Tasks;

namespace CLinq.ComposableQuery
{
    /// <summary> Class for async-await style list enumeration support (e.g. .ToListAsync())</summary>
    public sealed class ComposableDbAsyncEnumerator<T> : IDbAsyncEnumerator<T>
    {
        private readonly IEnumerator<T> _inner;

        /// <summary> Class for async-await style list enumeration support (e.g. .ToListAsync())</summary>
        public ComposableDbAsyncEnumerator(IEnumerator<T> inner) 
            => _inner = inner;

        /// <summary> Dispose, .NET using-pattern </summary>
        public void Dispose()
        {
            _inner.Dispose();
        }

        /// <summary> Enumerator-pattern: MoveNext </summary>
        public Task<bool> MoveNextAsync(CancellationToken cancellationToken) 
            => Task.FromResult(_inner.MoveNext());


        /// <summary> Enumerator-pattern: Current item </summary>
        public T Current => _inner.Current;

        object IDbAsyncEnumerator.Current => Current;
    }
}