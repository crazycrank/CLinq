using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace CLinq.Core.Visitors
{
    /// <summary>
    /// Allows for composable queries to resolve non-expression parameters before composing the query
    /// </summary>
    internal class ArgumentEvaluator : ExpressionVisitor
    {
        [CanBeNull]
        private object _result;

        [CanBeNull]
        public object Evaluate([CanBeNull] Expression expression)
        {
            if (expression is null)
                return null;

            this.Visit(expression);
            return this._result;
        }

        [NotNull]
        public Expression EvaluateAsExpression([CanBeNull] Expression expression)
        {
            var result = this.Evaluate(expression);
            switch (result)
            {
                case Expression exp:
                    return exp;
                case object o:
                    return Expression.Constant(o, o.GetType());
                case null:
                    return Expression.Constant(null);
            }
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ConstantExpression c)
                this._result = (node.Member as FieldInfo)?.GetValue(c.Value);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            this._result = node.Value;
            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var arguments = new object[node.Arguments.Count];
            for (var index = 0; index < node.Arguments.Count; index++)
            {
                var nodeArgument = node.Arguments[index];
                arguments[index] = new ArgumentEvaluator().Evaluate(nodeArgument);
            }

            this._result = node.Method.Invoke(new ArgumentEvaluator().Evaluate(node.Object), arguments);

            return node;
        }
    }
}
