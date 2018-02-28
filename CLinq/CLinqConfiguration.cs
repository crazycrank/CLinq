using System;
using System.Linq.Expressions;

namespace CLinq
{
    public static class CLinqConfiguration
    {
        public static Func<Expression, Expression> QueryOptimizer = e => e;
    }
}
