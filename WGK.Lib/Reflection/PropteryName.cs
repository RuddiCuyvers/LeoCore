using System;
using System.Linq.Expressions;

namespace WGK.Lib.Reflection
{
    public static class PropertyName
    {
        public static string For<T>(Expression<Func<T, object>> pExpression)
        {
            return GetMemberName(pExpression.Body);
        }

        public static string GetMemberName(Expression pExpression)
        {
            if (pExpression is MethodCallExpression)
            {
                var vMethodCallExpression = pExpression as MethodCallExpression;
                return vMethodCallExpression.Method.Name;
            }

            if (pExpression is MemberExpression)
            {
                var vMemberExpression = pExpression as MemberExpression;

                if (vMemberExpression.Expression.NodeType == ExpressionType.MemberAccess)
                {
                    return GetMemberName(vMemberExpression.Expression)
                        + "."
                            + vMemberExpression.Member.Name;
                }
                return vMemberExpression.Member.Name;
            }

            if (pExpression is UnaryExpression)
            {
                var vUnaryExpression = pExpression as UnaryExpression;

                if (vUnaryExpression.NodeType != ExpressionType.Convert)
                    throw new Exception(string.Format("Cannot interpret member from {0}", pExpression));

                return GetMemberName(vUnaryExpression.Operand);
            }

            throw new Exception(string.Format("Could not determine member from {0}", pExpression));
        }
    }
}