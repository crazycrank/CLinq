using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace CLinq.Visitors
{
    internal class QueryVisitor : ExpressionVisitor
    {
        private readonly IDictionary<ParameterExpression, Expression> _parametersToReplace;

        internal QueryVisitor()
        {
        }


        private QueryVisitor(IEnumerable<ParameterExpression> parametersToReplace, IEnumerable<Expression> replaceByExpression)
        {
            var parameterExpressions = parametersToReplace as ParameterExpression[] ?? parametersToReplace.ToArray();
            var byExpression = replaceByExpression as Expression[] ?? replaceByExpression.ToArray();

            if(parameterExpressions.Length != byExpression.Length)
                throw new Exception(); //Todo Exception Concept

            _parametersToReplace = new Dictionary<ParameterExpression, Expression>();

            for (var i = 0; i < parameterExpressions.Length; i++)
            {
                _parametersToReplace[parameterExpressions[i]] = byExpression[i];
            }
        }

        protected override Expression VisitParameter(ParameterExpression node) =>
            _parametersToReplace?.ContainsKey(node) ?? false
                ? Visit(_parametersToReplace[node])
                : base.VisitParameter(node);

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(CLinqExtensions.Pass) && node.Method.DeclaringType == typeof(CLinqExtensions))
            {
                LambdaExpression lambda;
                switch (node.Arguments[0])
                {
                    case MemberExpression e:
                        lambda = ParseMemberExpression(e) as LambdaExpression;
                        break;
                    case MethodCallExpression e:
                        lambda = ParseMethodCallExpression(e) as LambdaExpression;
                        break;
                    case ConstantExpression e when e.Value is LambdaExpression t:
                        lambda = t;
                        break;
                    case UnaryExpression e when e.Operand is LambdaExpression t:
                        lambda = t;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                Debug.Assert(lambda != null);

                return new QueryVisitor(lambda.Parameters, node.Arguments.Skip(1)).Visit(lambda.Body);
            }

            return base.VisitMethodCall(node);
        }

        private Expression ParseMemberExpression(MemberExpression memberExpression)
        {
            switch (memberExpression)
            {
                case var m when m.NodeType == ExpressionType.MemberAccess
                                             && m.Member is FieldInfo fi:
                {
                    var argumentVisitor = new ArgumentVisitor(memberExpression.Expression);
                    return Visit(fi.GetValue(argumentVisitor.Evaluate()) as Expression);
                }
                case var m when m.NodeType == ExpressionType.MemberAccess
                                             && m.Member is PropertyInfo pi:
                {
                    var argumentVisitor = new ArgumentVisitor(memberExpression.Expression);
                    var result = argumentVisitor.Evaluate();
                    return Visit(pi.GetValue(result) as Expression);
                }
                default:
                    return Expression.Constant(null);
            }
        }

        private Expression ParseMethodCallExpression(MethodCallExpression methodCallExpression)
        {
            if (!typeof(Expression).IsAssignableFrom(methodCallExpression.Method.ReturnType))
            {
                throw new Exception(); //TODO exception concept
            }
            var visitor = new ArgumentVisitor(methodCallExpression);
            return Visit(visitor.EvaluateAsExpression());
        }
    }
}
