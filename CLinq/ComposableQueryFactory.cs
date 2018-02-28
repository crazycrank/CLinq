using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using CLinq.ComposableQuery;

namespace CLinq
{
    internal class ComposableQueryFactory<T>
    {
        internal static readonly Func<IQueryable<T>, ComposableQuery<T>> Create;

        static ComposableQueryFactory()
        {
            if (!typeof(T).IsClass)
            {
                Create = query => new ComposableQuery<T>(query);
                return;
            }

            var queryableType = typeof(IQueryable<T>);
            var ctorInfo = typeof(ComposableQueryOfClass<>).MakeGenericType(typeof(T)).GetConstructor(new[] { queryableType });
            var queryParam = Expression.Parameter(queryableType);

            Debug.Assert(ctorInfo != null);
            var newExpr = Expression.New(ctorInfo, queryParam);
            var createExpr = Expression.Lambda<Func<IQueryable<T>, ComposableQuery<T>>>(newExpr, queryParam);
            Create = createExpr.Compile();
        }
    }
}
