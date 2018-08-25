using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace CLinq.Core
{
    /// <inheritdoc />
    public class ComposableQuery<T, TProvider> : IOrderedQueryable<T> 
        where TProvider : ComposableQueryProvider<T>
    {
        [NotNull]
        protected TProvider InnerProvider;

        public ComposableQuery([NotNull] IQueryable<T> query)
        {
            this.InnerQuery = query ?? throw new ArgumentNullException(nameof(query));
            this.InnerProvider = (TProvider) Activator.CreateInstance(typeof(TProvider), this);
        }
        
        [NotNull]
        public IQueryable<T> InnerQuery { get; }

        /// <inheritdoc />
        Expression IQueryable.Expression => this.InnerQuery.Expression;

        /// <inheritdoc />
        Type IQueryable.ElementType => typeof(T);

        /// <inheritdoc />
        IQueryProvider IQueryable.Provider => this.InnerProvider;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator()
            => this.InnerQuery.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator()
            => this.InnerQuery.GetEnumerator();

        /// <inheritdoc />
        public override string ToString()
            => this.InnerQuery.ToString();
    }
}
