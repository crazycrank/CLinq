using System;
using System.Linq;
using System.Linq.Expressions;

namespace CLinq
{
    /// <summary>
    /// Extensions methods used for composable queries. 
    /// </summary>
    public static class Extensions
    {
        /// <summary>
        /// Marks an parameterless expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<TResult>(this Expression<Func<TResult>> expression)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, TResult>(this Expression<Func<T1, TResult>> expression, T1 param1)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, TResult>(this Expression<Func<T1, T2, TResult>> expression, T1 param1, T2 param2)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, TResult>(this Expression<Func<T1, T2, T3, TResult>> expression, T1 param1, T2 param2, T3 param3)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, TResult>(this Expression<Func<T1, T2, T3, T4, TResult>> expression,
                                                            T1 param1,
                                                            T2 param2,
                                                            T3 param3,
                                                            T4 param4)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, TResult>(this Expression<Func<T1, T2, T3, T4, T5, TResult>> expression,
                                                                T1 param1,
                                                                T2 param2,
                                                                T3 param3,
                                                                T4 param4,
                                                                T5 param5)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression,
                                                                    T1 param1,
                                                                    T2 param2,
                                                                    T3 param3,
                                                                    T4 param4,
                                                                    T5 param5,
                                                                    T6 param6)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression,
                                                                        T1 param1,
                                                                        T2 param2,
                                                                        T3 param3,
                                                                        T4 param4,
                                                                        T5 param5,
                                                                        T6 param6,
                                                                        T7 param7)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression,
                                                                            T1 param1,
                                                                            T2 param2,
                                                                            T3 param3,
                                                                            T4 param4,
                                                                            T5 param5,
                                                                            T6 param6,
                                                                            T7 param7,
                                                                            T8 param8)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression,
                                                                                T1 param1,
                                                                                T2 param2,
                                                                                T3 param3,
                                                                                T4 param4,
                                                                                T5 param5,
                                                                                T6 param6,
                                                                                T7 param7,
                                                                                T8 param8,
                                                                                T9 param9)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>> expression,
                                                                                     T1 param1,
                                                                                     T2 param2,
                                                                                     T3 param3,
                                                                                     T4 param4,
                                                                                     T5 param5,
                                                                                     T6 param6,
                                                                                     T7 param7,
                                                                                     T8 param8,
                                                                                     T9 param9,
                                                                                     T10 param10)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression,
                                                                                          T1 param1,
                                                                                          T2 param2,
                                                                                          T3 param3,
                                                                                          T4 param4,
                                                                                          T5 param5,
                                                                                          T6 param6,
                                                                                          T7 param7,
                                                                                          T8 param8,
                                                                                          T9 param9,
                                                                                          T10 param10,
                                                                                          T11 param11)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression,
                                                                                               T1 param1,
                                                                                               T2 param2,
                                                                                               T3 param3,
                                                                                               T4 param4,
                                                                                               T5 param5,
                                                                                               T6 param6,
                                                                                               T7 param7,
                                                                                               T8 param8,
                                                                                               T9 param9,
                                                                                               T10 param10,
                                                                                               T11 param11,
                                                                                               T12 param12)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> expression,
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4,
            T5 param5,
            T6 param6,
            T7 param7,
            T8 param8,
            T9 param9,
            T10 param10,
            T11 param11,
            T12 param12,
            T13 param13)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> expression,
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4,
            T5 param5,
            T6 param6,
            T7 param7,
            T8 param8,
            T9 param9,
            T10 param10,
            T11 param11,
            T12 param12,
            T13 param13,
            T14 param14)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> expression,
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4,
            T5 param5,
            T6 param6,
            T7 param7,
            T8 param8,
            T9 param9,
            T10 param10,
            T11 param11,
            T12 param12,
            T13 param13,
            T14 param14,
            T15 param15)
            => default;

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be composed when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(
            this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> expression,
            T1 param1,
            T2 param2,
            T3 param3,
            T4 param4,
            T5 param5,
            T6 param6,
            T7 param7,
            T8 param8,
            T9 param9,
            T10 param10,
            T11 param11,
            T12 param12,
            T13 param13,
            T14 param14,
            T15 param15,
            T16 param16)
            => default;

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

            return query is ComposableQuery<T> composableQuery
                       ? composableQuery
                       : new ComposableQuery<T>(query);
        }
    }
}
