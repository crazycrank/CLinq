using System.Linq.Expressions;
using System.Reflection;

namespace LinqOnSteroids.Visitors
{
    internal class ArgumentVisitor : ExpressionVisitor
    {
        private readonly Expression _expression;
        private object _result;

        public ArgumentVisitor(Expression expression) => 
            _expression = expression;


        public object Evaluate()
        {
            if (_result == null)
                Visit(_expression);
            return _result;
        }

        public Expression EvaluateAsExpression()
        {
            var result = Evaluate();
            switch (result)
            {
                case Expression expression:
                    return expression;
                case object o:
                    return Expression.Constant(o, o.GetType());
                case null:
                    return Expression.Constant(null);
            }
        }

        protected override Expression VisitMember(MemberExpression node)
        {
            if (node.Expression is ConstantExpression c)
                _result = (node.Member as FieldInfo)?.GetValue(c.Value);

            return node;
        }

        protected override Expression VisitConstant(ConstantExpression node)
        {
            _result = node.Value;

            return node;
        }

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            var arguments = new object[node.Arguments.Count];
            for (var index = 0; index < node.Arguments.Count; index++)
            {
                var nodeArgument = node.Arguments[index];
                var argumentVisitor = new ArgumentVisitor(nodeArgument);
                arguments[index] = argumentVisitor.Evaluate();
            }

            var objectVisitor = new ArgumentVisitor(node.Object);
            objectVisitor.Visit(node.Object);
            _result = node.Method.Invoke(objectVisitor.Evaluate(), arguments);

            return node;
        }
    }
}