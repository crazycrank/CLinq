using System;
using System.Linq.Expressions;
using JetBrains.Annotations;

namespace CLinq.Core
{
    public static class CLinqConfiguration
    {
        [NotNull]
        private static Func<Expression, Expression> _queryOptimizer = e => e;


        [NotNull]
        public static Func<Expression, Expression> QueryOptimizer
        {
            get => _queryOptimizer;
            set => _queryOptimizer = value ?? throw new ArgumentNullException(nameof(value));
        }
    }
}
