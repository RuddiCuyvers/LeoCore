using System;
using System.Linq.Expressions;
using System.Reflection;

namespace WGK.Lib.Extensions
{
    public static class ExpressionExtensions
    {        
        #region Get member name
        /// <summary>
        /// Gets the member name from an expression on a model instance.
        /// Expression example: () => vModel.Property
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="pExpression">The member expression (either a property or field of the model).</param>
        /// <returns></returns>
        public static string MemberName<T>(this Expression<Func<T>> pExpression) where T : class
        {
            var vMemberExpression = pExpression.Body as MemberExpression;

            // If the method gets a lambda expression that is not a member access,
            // for example, () => x + y, then return null.
            if (vMemberExpression == null)
            {
                return null;
            }
            return vMemberExpression.Member.Name;
        }

        /// <summary>
        /// Gets the member name from an expression without model instance.
        /// Expression example: p => p.Property
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="pExpression">The member expression (either a property or field of the model).</param>
        /// <returns></returns>
        public static string MemberNameWithoutInstance<T>(this Expression<Func<T, object>> pExpression) where T : class
        {
            MemberExpression vMemberExpression = pExpression.ToMemberExpression();

            // If the method gets a lambda expression that is not a member access,
            // for example, () => x + y, then return null.
            if (vMemberExpression == null)
            {
                return null;
            }

            if (vMemberExpression.Expression.NodeType == ExpressionType.MemberAccess)
            {
                var vInnerMemberExpression = (MemberExpression)vMemberExpression.Expression;

                return vMemberExpression.ToString().Substring(vInnerMemberExpression.Expression.ToString().Length + 1);
            }

            return vMemberExpression.Member.Name;
        }

        /// <summary>
        /// Converts an expression into a member expression.
        /// Expression example: p => p.Property
        /// </summary>
        /// <typeparam name="T">The model type</typeparam>
        /// <param name="pExpression">The expression.</param>
        /// <returns></returns>
        public static MemberExpression ToMemberExpression<T>(this Expression<Func<T, object>> pExpression) where T : class
        {
            var vMemberExpression = pExpression.Body as MemberExpression;

            if (vMemberExpression == null)
            {
                var vUnaryExpression = pExpression.Body as UnaryExpression;

                if (vUnaryExpression != null)
                {
                    vMemberExpression = vUnaryExpression.Operand as MemberExpression;
                }
            }

            return vMemberExpression;
        }
        #endregion
       
        #region Get MemberInfo (e.g. for using with Reflection)
        /// <summary>
        /// Returns MemberInfo or MethodInfo for an expression representing a property, field or method of the model class.
        /// Expression example: () => vModel.SomeMethod("Test")
        /// </summary>
        /// <typeparam name="T">Type of the model</typeparam>
        /// <param name="pExpression">The member expression (either a property, field or method of the model).</param>
        /// <returns></returns>
        public static MemberInfo MemberInfo<T>(this Expression<Func<T>> pExpression)
        {
            return GetMemberInfo(pExpression.Body);
        }

        // We need to add this overload to cover scenarios when a method has a void return type.
        public static MemberInfo MemberInfo(this Expression<Action> pExpression)
        {
            return GetMemberInfo(pExpression.Body);
        }

        private static MemberInfo GetMemberInfo(Expression pExpressionBody)
        {
            // Check if the expression represents a property or field of the model
            var vMemberExpression = pExpressionBody as MemberExpression;
            if (vMemberExpression != null)
            {
                return vMemberExpression.Member;
            }

            // Check if the expression represents a method on the model
            var vMethodCallExpression = pExpressionBody as MethodCallExpression;
            if (vMethodCallExpression != null)
            {
                // Remark: MethodInfo derives from MemberInfo
                return vMethodCallExpression.Method;
            }

            throw new WGK.Lib.Exceptions.ParameterInvalidException(
                "Expression",
                pExpressionBody,
                "Is not a member access expression");
        }
        #endregion    
   
    }
}
