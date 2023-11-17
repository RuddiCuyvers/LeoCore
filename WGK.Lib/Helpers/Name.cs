using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace WGK.Lib.Helpers
{
    public static class Name
    {
        public static string Of<T>()
        {
            return typeof(T).Name;
        }

        public static string Of<T>(Expression<Func<T, object>> pProperty)
        {
            return GetMemberName(pProperty.Body);
        }

        public static string Of<T, TItem>(Expression<Func<T, IEnumerable<TItem>>> pProperty)
        {
            return GetMemberName(pProperty.Body);
        }

        public static string Of<T>(T pModel, Expression<Func<T, object>> pProperty)
        {
            return GetMemberName(pProperty.Body);
        }

        private static string GetMemberName(Expression pExpression)
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
                        + "_"
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
