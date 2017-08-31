using System;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using LinqOnSteroids.ExpandableQuery;
using LinqOnSteroids.Visitors;

namespace LinqOnSteroids
{
    /// <summary>
    /// Extensions methods used for expandable queries. 
    /// </summary>
    public static class LinqOnSteroidsExtensions
    {
        /// <summary>
        /// Marks an parameterless expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<TResult>(this Expression<Func<TResult>> expression) => expression.Compile()();

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, TResult>(this Expression<Func<T1, TResult>> expression, T1 param1)
            => expression.Compile()(param1);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, TResult>(this Expression<Func<T1, T2, TResult>> expression, T1 param1, T2 param2) 
            => expression.Compile()(param1, param2);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, TResult>(this Expression<Func<T1, T2, T3, TResult>> expression, T1 param1, T2 param2, T3 param3) 
            => expression.Compile()(param1, param2, param3);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, TResult>(this Expression<Func<T1, T2, T3, T4, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4)
            => expression.Compile()(param1, param2, param3, param4);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, TResult>(this Expression<Func<T1, T2, T3, T4, T5, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5)
            => expression.Compile()(param1, param2, param3, param4, param5);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6)
            => expression.Compile()(param1, param2, param3, param4, param5, param6);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8,TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8, param9);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10,TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12, T13 param13)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12, T13 param13, T14 param14)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15);

        /// <summary>
        /// Marks an parametered expression for LinqOnSteroids, so that it will be expanded when the query will be called
        /// </summary>
        public static TResult Pass<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>(this Expression<Func<T1, T2, T3, T4, T5, T6, T7, T8, T9, T10, T11, T12, T13, T14, T15, T16, TResult>> expression, T1 param1, T2 param2, T3 param3, T4 param4, T5 param5, T6 param6, T7 param7, T8 param8, T9 param9, T10 param10, T11 param11, T12 param12, T13 param13, T14 param14, T15 param15, T16 param16)
            => expression.Compile()(param1, param2, param3, param4, param5, param6, param7, param8, param9, param10, param11, param12, param13, param14, param15, param16);

        /// <summary>
        /// Makes a query expandable, which allows to pass it dynamic parameters which are evaluated before execution
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <returns></returns>
        public static IQueryable<T> AsExpandable<T>(this IQueryable<T> query)
        {
            if (query is ExpandableQuery<T>)
                return query;
            return ExpandableQueryFactory<T>.Create(query);
        }

        public static Expression<T> Expand<T>(this Expression<T> expression) 
            => (Expression<T>)new QueryVisitor().Visit(expression);


        public static Expression Expand(this Expression expression) 
            => new QueryVisitor().Visit(expression);


        private static class ExpandableQueryFactory<T>
        {
            public static readonly Func<IQueryable<T>, ExpandableQuery<T>> Create;

            static ExpandableQueryFactory()
            {
                if (!typeof(T).IsClass)
                {
                    Create = query => new ExpandableQuery<T>(query);
                    return;
                }

                var queryableType = typeof(IQueryable<T>);
                var ctorInfo = typeof(ExpandableQueryOfClass<>).MakeGenericType(typeof(T)).GetConstructor(new[] {queryableType});
                var queryParam = Expression.Parameter(queryableType);

                Debug.Assert(ctorInfo != null);
                var newExpr = Expression.New(ctorInfo, queryParam);
                var createExpr = Expression.Lambda<Func<IQueryable<T>, ExpandableQuery<T>>>(newExpr, queryParam);
                Create = createExpr.Compile();
            }
        }
    }
}
