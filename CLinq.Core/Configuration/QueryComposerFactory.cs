using System;
using System.Linq;
using JetBrains.Annotations;

namespace CLinq.Core
{
    public class QueryComposerFactory
    {
        public virtual ComposableQuery<T, ComposableQueryProvider<T>> GetComposableQuery<T>([NotNull] IQueryable<T> query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));

            return query is ComposableQuery<T, ComposableQueryProvider<T>> composableQuery
                       ? composableQuery
                       : new ComposableQuery<T, ComposableQueryProvider<T>>(query);
        }

    }
}