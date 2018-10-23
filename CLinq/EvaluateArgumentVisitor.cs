using System.Linq.Expressions;
using System.Reflection;

namespace CLinq
{
    /// <inheritdoc />
    /// <summary>
    /// Allows for composable queries to resolve non-expression parameters before composing the query
    /// </summary>
    internal class EvaluateArgumentVisitor : ExpressionVisitor
    {
        private object _result;

        public object Evaluate(Expression expression)
        {
            if (expression is null)
                return null;

            this.Visit(expression);
            return this._result;
        }

        public Expression EvaluateAsExpression(Expression expression)
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
                arguments[index] = new EvaluateArgumentVisitor().Evaluate(nodeArgument);
            }

            this._result = node.Method.Invoke(new EvaluateArgumentVisitor().Evaluate(node.Object), arguments);

            return node;
        }
    }
}
