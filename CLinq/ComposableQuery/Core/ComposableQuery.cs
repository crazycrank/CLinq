using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace CLinq.Core.ComposableQuery.Core
{
    /// <summary>
    ///     An IQueryable wrapper that allows us to visit and manipulate the query's expression tree just before LINQ to SQL gets to it.
    /// </summary>
    public class ComposableQuery<T> : IOrderedQueryable<T>
    {
        [NotNull]
        private protected ComposableQueryProvider<T> InnerProvider;

        internal ComposableQuery([NotNull] IQueryable<T> inner)
        {
            this.InnerQuery = inner ?? throw new ArgumentNullException(nameof(inner));
            this.InnerProvider = new ComposableQueryProvider<T>(this);
        }

        [NotNull]
        internal IQueryable<T> InnerQuery 
        {
            get;
        }

        /// <inheritdoc />
        Expression IQueryable.Expression => this.InnerQuery.Expression;

        /// <inheritdoc />
        Type IQueryable.ElementType => typeof(T);

        /// <inheritdoc />
        IQueryProvider IQueryable.Provider => this.InnerProvider;

        /// <inheritdoc />
        public IEnumerator<T> GetEnumerator() => this.InnerQuery.GetEnumerator();

        /// <inheritdoc />
        IEnumerator IEnumerable.GetEnumerator() => this.InnerQuery.GetEnumerator();

        /// <inheritdoc />
        public override string ToString() => this.InnerQuery.ToString();
    }
}
