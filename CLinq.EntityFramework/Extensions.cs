using System;
using System.Linq;

namespace CLinq.EntityFramework
{
    /// <summary>
    /// Extensions methods used for composable queries. 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Makes a query composable, which allows to pass it dynamic parameters which are evaluated before execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<T> AsComposable<T>(this IQueryable<T> query)
        {
            if (query is null)
                throw new ArgumentNullException(nameof(query));
            
            return query is DbAsyncComposableQuery<T> composableQuery
                       ? composableQuery
                       : new DbAsyncComposableQuery<T>(query);
        }
    }
}
