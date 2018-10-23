using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace CLinq
{
    /// <summary>
    /// Extensions methods used for composable queries. 
    /// </summary>
    internal static class Utils
    {
        internal static Expression<T> Compose<T>(this Expression<T> expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            return (Expression<T>) new ComposeQueryVisitor().Visit(expression) ?? throw new InvalidOperationException();
        }

        internal static Expression Compose(this Expression expression)
        {
            if (expression is null)
                throw new ArgumentNullException(nameof(expression));

            return new ComposeQueryVisitor().Visit(expression) ?? throw new InvalidOperationException();
        }
    }
}
