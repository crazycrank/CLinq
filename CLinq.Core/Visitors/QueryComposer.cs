using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using JetBrains.Annotations;

namespace CLinq.Core.Visitors
{
    /// <summary>
    /// Visits a query and merges all methods, which are marked with <see cref="Extensions.Pass{TResult}"/>, into the base query
    /// </summary>
    internal class QueryComposer : ExpressionVisitor
    {
        [NotNull]
        private readonly IDictionary<ParameterExpression, Expression> _parametersToReplace = new Dictionary<ParameterExpression, Expression>();

        internal QueryComposer()
        { }

        private QueryComposer([NotNull] IEnumerable<(ParameterExpression parameter, Expression replaceBy)> replaceParameters)
        {
            if (replaceParameters is null)
                throw new ArgumentNullException(nameof(replaceParameters));

            foreach (var (parameter, replaceBy) in replaceParameters)
            {
                this._parametersToReplace[parameter] = replaceBy;
            }
        }

        protected override Expression VisitParameter(ParameterExpression node)
            => this._parametersToReplace.ContainsKey(node)
                   ? this.Visit(this._parametersToReplace[node]) ?? throw new InvalidOperationException()
                   : base.VisitParameter(node);

        protected override Expression VisitMethodCall(MethodCallExpression node)
        {
            if (node.Method.Name == nameof(Extensions.Pass) && node.Method.DeclaringType == typeof(Extensions))
            {
                LambdaExpression lambda;
                switch (node.Arguments[0])
                {
                    case MemberExpression e:
                        lambda = this.ParseMemberExpression(e) as LambdaExpression;
                        break;
                    case MethodCallExpression e:
                        lambda = this.ParseMethodCallExpression(e) as LambdaExpression;
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

                if (lambda is null)
                {
                    throw new InvalidOperationException();
                }

                return new QueryComposer(lambda.Parameters.Zip(node.Arguments.Skip(1),
                                                              (parameter, replaceBy) => (parameter, replaceBy)))
                           .Visit(lambda.Body)
                       ?? throw new InvalidOperationException();
            }

            return base.VisitMethodCall(node);
        }

        private Expression ParseMemberExpression([NotNull] MemberExpression memberExpression)
        {
            var argumentVisitor = new ArgumentEvaluator();
            switch (memberExpression)
            {
                case var m when m.NodeType == ExpressionType.MemberAccess
                                && m.Member is FieldInfo fi:
                    return this.Visit(fi.GetValue(argumentVisitor.Evaluate(memberExpression.Expression)) as Expression);

                case var m when m.NodeType == ExpressionType.MemberAccess
                                && m.Member is PropertyInfo pi:
                    return this.Visit(pi.GetValue(argumentVisitor.Evaluate(memberExpression.Expression)) as Expression);

                default:
                    return Expression.Constant(null);
            }
        }

        private Expression ParseMethodCallExpression([NotNull] MethodCallExpression methodCallExpression)
        {
            if (!(typeof(Expression).GetTypeInfo()?.IsAssignableFrom(methodCallExpression.Method.ReturnType.GetTypeInfo()) ?? false))
            {
                throw new InvalidOperationException();
            }

            return this.Visit(new ArgumentEvaluator().EvaluateAsExpression(methodCallExpression));
        }
    }
}
