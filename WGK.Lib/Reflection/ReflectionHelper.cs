using System;
using System.Collections.Generic;
using System.Reflection;

namespace WGK.Lib.Reflection
{
    public static class ReflectionHelper
    {
        #region Public Methods
        /// <summary>
        /// Invokes a public method on a class instance. Use this method only if none of the parameter values are null.
        /// </summary>
        /// <remarks>
        /// Since none of the parameter values is null we can determine the parametertype at runtime.
        /// </remarks>
        /// <param name="pInstance">Instance of the class whose method will be invoked</param>
        /// <param name="pMethodName">Name of the method that will be invoked</param>
        /// <param name="pParameterValues">Parameters passed on when the method is invoked</param>
        /// <returns></returns>
        public static object InvokePublicMethod(object pInstance, string pMethodName, params object[] pParameterValues)
        {
            return InternalInvokeMethod(false, pInstance, pMethodName, pParameterValues);
        }

        /// <summary>
        /// Invokes a public method on a class instance. Use this method if parameter values can be null.
        /// </summary>
        /// <remarks>
        /// Since parameter values can be null we cannot determine the parametertype at runtime.
        /// </remarks>
        /// <param name="pInstance">Instance of the class whose method will be invoked</param>
        /// <param name="pMethodName">Name of the method that will be invoked</param>
        /// <param name="pParameterTypes">Array of parameter types. Must be in same order as parameter values.</param>
        /// <param name="pParameterValues">Parameters passed on when the method is invoked</param>
        /// <returns></returns>
        public static object InvokePublicMethod(object pInstance, string pMethodName, Type[] pParameterTypes, object[] pParameterValues)
        {
            return InternalInvokeMethod(false, pInstance, pMethodName, pParameterTypes, pParameterValues);
        }

        /// <summary>
        /// Invokes a private method on a class instance. Use this method only if none of the parameter values are null.
        /// </summary>
        /// <remarks>
        /// Since none of the parameter values is null we can determine the parametertype at runtime.
        /// </remarks>
        /// <param name="pInstance">Instance of the class whose method will be invoked</param>
        /// <param name="pMethodName">Name of the method that will be invoked</param>
        /// <param name="pParameterValues">Parameters passed on when the method is invoked</param>
        /// <returns></returns>
        public static object InvokePrivateMethod(object pInstance, string pMethodName, params object[] pParameterValues)
        {
            return InternalInvokeMethod(true, pInstance, pMethodName, pParameterValues);
        }

        /// <summary>
        /// Invokes a private method on a class instance. Use this method if parameter values can be null.
        /// </summary>
        /// <remarks>
        /// Since parameter values can be null we cannot determine the parametertype at runtime.
        /// </remarks>
        /// <param name="pInstance">Instance of the class whose method will be invoked</param>
        /// <param name="pMethodName">Name of the method that will be invoked</param>
        /// <param name="pParameterTypes">Array of parameter types. Must be in same order as parameter values.</param>
        /// <param name="pParameterValues">Parameters passed on when the method is invoked</param>
        /// <returns></returns>
        public static object InvokePrivateMethod(object pInstance, string pMethodName, Type[] pParameterTypes, object[] pParameterValues)
        {
            return InternalInvokeMethod(true, pInstance, pMethodName, pParameterTypes, pParameterValues);
        }


        /// <summary>
        /// Gets a public Property of a class instance.
        /// An exception is thrown if the Property does not exist.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose Property is read</param>
        /// <param name="pPropertyName">Name of the Property that will be read</param>
        /// <returns></returns>
        public static object GetPublicProperty(object pInstance, string pPropertyName)
        {
            return InternalGetProperty(false, false, null, pInstance, pPropertyName);
        }

        /// <summary>
        /// Gets a private Property of a class instance.
        /// An exception is thrown if the Property does not exist.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose Property is read</param>
        /// <param name="pPropertyName">Name of the Property that will be read</param>
        /// <returns></returns>
        public static object GetPrivateProperty(object pInstance, string pPropertyName)
        {
            return InternalGetProperty(false, true, null, pInstance, pPropertyName);
        }

        /// <summary>
        /// Gets a public Property of a class instance.
        /// Specify the type of the class if reflection needs a specific (base) type of the current instance.
        /// An exception is thrown if the Property does not exist.
        /// </summary>
        /// <param name="pClassType">Type of the class whose Property is read</param>
        /// <param name="pInstance">Instance of the class whose Property is read</param>
        /// <param name="pPropertyName">Name of the Property that will be read</param>
        /// <returns></returns>
        public static object GetPublicProperty(Type pClassType, object pInstance, string pPropertyName)
        {
            return InternalGetProperty(false, false, pClassType, pInstance, pPropertyName);
        }

        /// <summary>
        /// Gets a private Property of a class instance.
        /// Specify the type of the class if reflection needs a specific (base) type of the current instance.
        /// An exception is thrown if the Property does not exist.
        /// </summary>
        /// <param name="pClassType">Type of the class whose Property is read</param>
        /// <param name="pInstance">Instance of the class whose Property is read</param>
        /// <param name="pPropertyName">Name of the Property that will be read</param>
        /// <returns></returns>
        public static object GetPrivateProperty(Type pClassType, object pInstance, string pPropertyName)
        {
            return InternalGetProperty(false, true, pClassType, pInstance, pPropertyName);
        }

        /// <summary>
        /// Try to get a public Property of a class instance. 
        /// This method does not throw an exception if the Property does not exist. In this case null is returned.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose Property is read</param>
        /// <param name="pPropertyName">Name of the Property that will be read</param>
        /// <returns></returns>
        public static object TryGetPublicProperty(object pInstance, string pPropertyName)
        {
            return InternalGetProperty(true, false, null, pInstance, pPropertyName);
        }

        /// <summary>
        /// Try to get a private Property of a class instance.
        /// This method does not throw an exception if the Property does not exist. In this case null is returned.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose Property is read</param>
        /// <param name="pPropertyName">Name of the Property that will be read</param>
        /// <returns></returns>
        public static object TryGetPrivateProperty(object pInstance, string pPropertyName)
        {
            return InternalGetProperty(true, true, null, pInstance, pPropertyName);
        }

        /// <summary>
        /// Try to get a public Property of a class instance. 
        /// Specify the type of the class if reflection needs a specific (base) type of the current instance.
        /// This method does not throw an exception if the Property does not exist. In this case null is returned.
        /// </summary>
        /// <param name="pClassType">Type of the class whose Property is read</param>
        /// <param name="pInstance">Instance of the class whose Property is read</param>
        /// <param name="pPropertyName">Name of the Property that will be read</param>
        /// <returns></returns>
        public static object TryGetPublicProperty(Type pClassType, object pInstance, string pPropertyName)
        {
            return InternalGetProperty(true, false, pClassType, pInstance, pPropertyName);
        }

        /// <summary>
        /// Try to get a private Property of a class instance.
        /// Specify the type of the class if reflection needs a specific (base) type of the current instance.
        /// This method does not throw an exception if the Property does not exist. In this case null is returned.
        /// </summary>
        /// <param name="pClassType">Type of the class whose Property is read</param>
        /// <param name="pInstance">Instance of the class whose Property is read</param>
        /// <param name="pPropertyName">Name of the Property that will be read</param>
        /// <returns></returns>
        public static object TryGetPrivateProperty(Type pClassType, object pInstance, string pPropertyName)
        {
            return InternalGetProperty(true, true, pClassType, pInstance, pPropertyName);
        }

        /// <summary>
        /// Sets a public Property on a class instance.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose Property is set</param>
        /// <param name="pPropertyName">Name of the Property that will be set</param>
        /// <param name="pValue">Value of the Property that will be set</param>
        public static void SetPublicProperty(object pInstance, string pPropertyName, object pValue)
        {
            InternalSetProperty(false, null, pInstance, pPropertyName, pValue);
        }

        /// <summary>
        /// Sets a private Property on a class instance.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose Property is set</param>
        /// <param name="pPropertyName">Name of the Property that will be set</param>
        /// <param name="pValue">Value of the Property that will be set</param>
        public static void SetPrivateProperty(object pInstance, string pPropertyName, object pValue)
        {
            InternalSetProperty(true, null, pInstance, pPropertyName, pValue);
        }

        /// <summary>
        /// Sets a public Property on a class instance.
        /// </summary>
        /// <param name="pClassType">Type of the class whose Property is read</param>
        /// <param name="pInstance">Instance of the class whose Property is set</param>
        /// <param name="pPropertyName">Name of the Property that will be set</param>
        /// <param name="pValue">Value of the Property that will be set</param>
        public static void SetPublicProperty(Type pClassType, object pInstance, string pPropertyName, object pValue)
        {
            InternalSetProperty(false, pClassType, pInstance, pPropertyName, pValue);
        }

        /// <summary>
        /// Sets a private Property on a class instance.
        /// </summary>
        /// <param name="pClassType">Type of the class whose Property is read</param>
        /// <param name="pInstance">Instance of the class whose Property is set</param>
        /// <param name="pPropertyName">Name of the Property that will be set</param>
        /// <param name="pValue">Value of the Property that will be set</param>
        public static void SetPrivateProperty(Type pClassType, object pInstance, string pPropertyName, object pValue)
        {
            InternalSetProperty(true, pClassType, pInstance, pPropertyName, pValue);
        }

        /// <summary>
        /// Gets a public field of a class instance.
        /// An exception is thrown if the field does not exist.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose field is read</param>
        /// <param name="pFieldName">Name of the field that will be read</param>
        /// <returns></returns>
        public static object GetPublicField(object pInstance, string pFieldName)
        {
            return InternalGetField(false, false, null, pInstance, pFieldName);
        }

        /// <summary>
        /// Gets a private field of a class instance.
        /// An exception is thrown if the field does not exist.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose field is read</param>
        /// <param name="pFieldName">Name of the field that will be read</param>
        /// <returns></returns>
        public static object GetPrivateField(object pInstance, string pFieldName)
        {
            return InternalGetField(false, true, null, pInstance, pFieldName);
        }

        /// <summary>
        /// Gets a public field of a class instance.
        /// Specify the type of the class if reflection needs a specific (base) type of the current instance.
        /// An exception is thrown if the field does not exist.
        /// </summary>
        /// <param name="pClassType">Type of the class whose field is read</param>
        /// <param name="pInstance">Instance of the class whose field is read</param>
        /// <param name="pFieldName">Name of the field that will be read</param>
        /// <returns></returns>
        public static object GetPublicField(Type pClassType, object pInstance, string pFieldName)
        {
            return InternalGetField(false, false, pClassType, pInstance, pFieldName);
        }

        /// <summary>
        /// Gets a private field of a class instance.
        /// Specify the type of the class if reflection needs a specific (base) type of the current instance.
        /// An exception is thrown if the field does not exist.
        /// </summary>
        /// <param name="pClassType">Type of the class whose field is read</param>
        /// <param name="pInstance">Instance of the class whose field is read</param>
        /// <param name="pFieldName">Name of the field that will be read</param>
        /// <returns></returns>
        public static object GetPrivateField(Type pClassType, object pInstance, string pFieldName)
        {
            return InternalGetField(false, true, pClassType, pInstance, pFieldName);
        }

        /// <summary>
        /// Try to get a public field of a class instance. 
        /// This method does not throw an exception if the field does not exist. In this case null is returned.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose field is read</param>
        /// <param name="pFieldName">Name of the field that will be read</param>
        /// <returns></returns>
        public static object TryGetPublicField(object pInstance, string pFieldName)
        {
            return InternalGetField(true, false, null, pInstance, pFieldName);
        }

        /// <summary>
        /// Try to get a private field of a class instance.
        /// This method does not throw an exception if the field does not exist. In this case null is returned.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose field is read</param>
        /// <param name="pFieldName">Name of the field that will be read</param>
        /// <returns></returns>
        public static object TryGetPrivateField(object pInstance, string pFieldName)
        {
            return InternalGetField(true, true, null, pInstance, pFieldName);
        }

        /// <summary>
        /// Try to get a public field of a class instance. 
        /// Specify the type of the class if reflection needs a specific (base) type of the current instance.
        /// This method does not throw an exception if the field does not exist. In this case null is returned.
        /// </summary>
        /// <param name="pClassType">Type of the class whose field is read</param>
        /// <param name="pInstance">Instance of the class whose field is read</param>
        /// <param name="pFieldName">Name of the field that will be read</param>
        /// <returns></returns>
        public static object TryGetPublicField(Type pClassType, object pInstance, string pFieldName)
        {
            return InternalGetField(true, false, pClassType, pInstance, pFieldName);
        }

        /// <summary>
        /// Try to get a private field of a class instance.
        /// Specify the type of the class if reflection needs a specific (base) type of the current instance.
        /// This method does not throw an exception if the field does not exist. In this case null is returned.
        /// </summary>
        /// <param name="pClassType">Type of the class whose field is read</param>
        /// <param name="pInstance">Instance of the class whose field is read</param>
        /// <param name="pFieldName">Name of the field that will be read</param>
        /// <returns></returns>
        public static object TryGetPrivateField(Type pClassType, object pInstance, string pFieldName)
        {
            return InternalGetField(true, true, pClassType, pInstance, pFieldName);
        }

        /// <summary>
        /// Sets a public field on a class instance.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose field is set</param>
        /// <param name="pFieldName">Name of the field that will be set</param>
        /// <param name="pValue">Value of the field that will be set</param>
        public static void SetPublicField(object pInstance, string pFieldName, object pValue)
        {
            InternalSetField(false, null, pInstance, pFieldName, pValue);
        }

        /// <summary>
        /// Sets a private field on a class instance.
        /// </summary>
        /// <param name="pInstance">Instance of the class whose field is set</param>
        /// <param name="pFieldName">Name of the field that will be set</param>
        /// <param name="pValue">Value of the field that will be set</param>
        public static void SetPrivateField(object pInstance, string pFieldName, object pValue)
        {
            InternalSetField(true, null, pInstance, pFieldName, pValue);
        }

        /// <summary>
        /// Sets a public field on a class instance.
        /// </summary>
        /// <param name="pClassType">Type of the class whose field is read</param>
        /// <param name="pInstance">Instance of the class whose field is set</param>
        /// <param name="pFieldName">Name of the field that will be set</param>
        /// <param name="pValue">Value of the field that will be set</param>
        public static void SetPublicField(Type pClassType, object pInstance, string pFieldName, object pValue)
        {
            InternalSetField(false, pClassType, pInstance, pFieldName, pValue);
        }

        /// <summary>
        /// Sets a private field on a class instance.
        /// </summary>
        /// <param name="pClassType">Type of the class whose field is read</param>
        /// <param name="pInstance">Instance of the class whose field is set</param>
        /// <param name="pFieldName">Name of the field that will be set</param>
        /// <param name="pValue">Value of the field that will be set</param>
        public static void SetPrivateField(Type pClassType, object pInstance, string pFieldName, object pValue)
        {
            InternalSetField(true, pClassType, pInstance, pFieldName, pValue);
        }
        #endregion

        #region Private Methods
        private static object InternalInvokeMethod(bool pPrivate, object pInstance, string pMethodName, object[] pParameterValues)
        {
            // Remark: Use this method only if none of the parameter values are null.
            List<Type> lParameterTypes = new List<Type>();
            if (pParameterValues != null)
            {
                foreach (object lParameterValue in pParameterValues)
                {
                    if (lParameterValue == null)
                    {
                        throw new WGK.Lib.Exceptions.ParameterMissingException(
                           "Parameters[]",
                           "Cannot pass null parameter through this mehod.");
                    }
                    lParameterTypes.Add(lParameterValue.GetType());
                }
            }
            return InternalInvokeMethod(pPrivate, pInstance, pMethodName, lParameterTypes.ToArray(), pParameterValues);
        }

        private static object InternalInvokeMethod(bool pPrivate, object pInstance, string pMethodName, Type[] pParameterTypes, object[] pParameterValues)
        {
            // Remark: Since parameter values can be null we cannot determine the parametertype at runtime => pass on types[] in pParameterTypes
            if (pPrivate)
            {
                CheckPermission();
            }

            if (pParameterTypes == null)
            {
                throw new WGK.Lib.Exceptions.ParameterMissingException(
                    "ParameterTypes[]",
                    "Array is not alowed to be null");
            }

            if (pInstance == null)
            {
                throw new WGK.Lib.Exceptions.ParameterMissingException(
                    "Instance");
            }
            Type lType = pInstance.GetType();

            BindingFlags lBindingFlags = System.Reflection.BindingFlags.Instance | BindingFlags.FlattenHierarchy;
            if (pPrivate)
            {
                lBindingFlags |= System.Reflection.BindingFlags.NonPublic;
            }
            else
            {
                lBindingFlags |= System.Reflection.BindingFlags.Public;
            }

            System.Reflection.MethodInfo lMethodInfo = lType.GetMethod(
                pMethodName,
                lBindingFlags,
                null,
                pParameterTypes,
                null);
            if (lMethodInfo == null)
            {
                throw new WGK.Lib.Exceptions.ParameterInvalidException(
                    "MethodName",
                    string.Concat(lType.Name, ".", pMethodName),
                    "This class has no such method.");
            }

            try
            {
                return lMethodInfo.Invoke(pInstance, pParameterValues);
            }
            catch (TargetInvocationException lTargetInvocationException)
            {
                // Instead of throwing a "Exception has been thrown by the target of an invocation", throw directly the inner exception.
                if (lTargetInvocationException.InnerException != null)
                {
                    throw lTargetInvocationException.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }

        private static object InternalGetField(bool pTryGet, bool pPrivate, Type pClassType, object pInstance, string pFieldName)
        {
            if (pPrivate)
            {
                CheckPermission();
            }

            if (pInstance == null)
            {
                throw new WGK.Lib.Exceptions.ParameterMissingException(
                    "Instance");
            }
            if (pClassType == null)
            {
                // If there is no specific (base) class not specified, just get the type of the current instance
                pClassType = pInstance.GetType();
            }

            BindingFlags lBindingFlags =
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy;
            if (pPrivate)
            {
                lBindingFlags |= System.Reflection.BindingFlags.NonPublic;
            }
            else
            {
                lBindingFlags |= System.Reflection.BindingFlags.Public;
            }

            System.Reflection.FieldInfo lFieldInfo = pClassType.GetField(
                pFieldName,
                lBindingFlags);
            if (lFieldInfo == null)
            {
                if (pTryGet)
                {
                    return null;
                }
                else
                {
                    throw new WGK.Lib.Exceptions.ParameterInvalidException(
                        "FieldName",
                        pFieldName,
                        pClassType.Name);
                }
            }
            try
            {
                return lFieldInfo.GetValue(pInstance);
            }
            catch (TargetInvocationException lTargetInvocationException)
            {
                if (pTryGet)
                {
                    return null;
                }
                else
                {
                    // Instead of throwing a "Exception has been thrown by the target of an invocation", throw directly the inner exception.
                    if (lTargetInvocationException.InnerException != null)
                    {
                        throw lTargetInvocationException.InnerException;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
        }

        private static void InternalSetField(bool pPrivate, Type pClassType, object pInstance, string pFieldName, object pValue)
        {
            if (pPrivate)
            {
                CheckPermission();
            }

            if (pInstance == null)
            {
                throw new WGK.Lib.Exceptions.ParameterMissingException(
                    "Instance");
            }
            if (pClassType == null)
            {
                // If there is no specific (base) class not specified, just get the type of the current instance
                pClassType = pInstance.GetType();
            }

            BindingFlags lBindingFlags = System.Reflection.BindingFlags.Instance | BindingFlags.FlattenHierarchy;
            if (pPrivate)
            {
                lBindingFlags |= System.Reflection.BindingFlags.NonPublic;
            }
            else
            {
                lBindingFlags |= System.Reflection.BindingFlags.Public;
            }

            System.Reflection.FieldInfo lFieldInfo = pClassType.GetField(
                pFieldName,
                lBindingFlags);
            if (lFieldInfo == null)
            {
                throw new WGK.Lib.Exceptions.ParameterInvalidException(
                    "FieldName",
                    pFieldName,
                    pClassType.Name);
            }

            try
            {
                lFieldInfo.SetValue(pInstance, pValue);
            }
            catch (TargetInvocationException lTargetInvocationException)
            {
                // Instead of throwing a "Exception has been thrown by the target of an invocation", throw directly the inner exception.
                if (lTargetInvocationException.InnerException != null)
                {
                    throw lTargetInvocationException.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }

        private static object InternalGetProperty(bool pTryGet, bool pPrivate, Type pClassType, object pInstance, string pPropertyName)
        {
            if (pPrivate)
            {
                CheckPermission();
            }

            if (pInstance == null)
            {
                throw new WGK.Lib.Exceptions.ParameterMissingException(
                    "Instance");
            }
            if (pClassType == null)
            {
                // If there is no specific (base) class not specified, just get the type of the current instance
                pClassType = pInstance.GetType();
            }

            BindingFlags lBindingFlags =
                System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.FlattenHierarchy;
            if (pPrivate)
            {
                lBindingFlags |= System.Reflection.BindingFlags.NonPublic;
            }
            else
            {
                lBindingFlags |= System.Reflection.BindingFlags.Public;
            }

            System.Reflection.PropertyInfo lPropertyInfo = pClassType.GetProperty(
                pPropertyName,
                lBindingFlags);
            if (lPropertyInfo == null)
            {
                if (pTryGet)
                {
                    return null;
                }
                else
                {
                    throw new WGK.Lib.Exceptions.ParameterInvalidException(
                        "PropertyName",
                        pPropertyName,
                        pClassType.Name);
                }
            }
            if (!lPropertyInfo.CanRead)
            {
                if (pTryGet)
                {
                    return null;
                }
                else
                {
                    throw new WGK.Lib.Exceptions.ParameterInvalidException(
                        "PropertyName",
                        pPropertyName,
                        string.Format("Type {0}. Property is writeonly", pClassType.Name));
                }
            }
            try
            {
                return lPropertyInfo.GetValue(pInstance, null);
            }
            catch (TargetInvocationException lTargetInvocationException)
            {
                if (pTryGet)
                {
                    return null;
                }
                else
                {
                    // Instead of throwing a "Exception has been thrown by the target of an invocation", throw directly the inner exception.
                    if (lTargetInvocationException.InnerException != null)
                    {
                        throw lTargetInvocationException.InnerException;
                    }
                    else
                    {
                        throw;
                    }
                }
            }
            catch (Exception exc)
            {
                throw exc;
            }
        }

        private static void InternalSetProperty(bool pPrivate, Type pClassType, object pInstance, string pPropertyName, object pValue)
        {
            if (pPrivate)
            {
                CheckPermission();
            }

            if (pInstance == null)
            {
                throw new WGK.Lib.Exceptions.ParameterMissingException(
                    "Instance");
            }
            if (pClassType == null)
            {
                // If there is no specific (base) class not specified, just get the type of the current instance
                pClassType = pInstance.GetType();
            }

            BindingFlags lBindingFlags = System.Reflection.BindingFlags.Instance | BindingFlags.FlattenHierarchy;
            if (pPrivate)
            {
                lBindingFlags |= System.Reflection.BindingFlags.NonPublic;
            }
            else
            {
                lBindingFlags |= System.Reflection.BindingFlags.Public;
            }

            System.Reflection.PropertyInfo lPropertyInfo = pClassType.GetProperty(
                pPropertyName,
                lBindingFlags);
            if (lPropertyInfo == null)
            {
                throw new WGK.Lib.Exceptions.ParameterInvalidException(
                    "PropertyName",
                    pPropertyName,
                    pClassType.Name);
            }
            if (!lPropertyInfo.CanWrite)
            {
                throw new WGK.Lib.Exceptions.ParameterInvalidException(
                    "PropertyName",
                    pPropertyName,
                    string.Format("Type {0}. Property is readonly", pClassType.Name));
            }
            try
            {
                lPropertyInfo.SetValue(pInstance, pValue, null);
            }
            catch (TargetInvocationException lTargetInvocationException)
            {
                // Instead of throwing a "Exception has been thrown by the target of an invocation", throw directly the inner exception.
                if (lTargetInvocationException.InnerException != null)
                {
                    throw lTargetInvocationException.InnerException;
                }
                else
                {
                    throw;
                }
            }
        }

        private static void CheckPermission()
        {
            if (!System.Security.SecurityManager.IsGranted(
                new System.Security.Permissions.ReflectionPermission(System.Security.Permissions.PermissionState.Unrestricted)))
            {
                throw new WGK.Lib.Exceptions.UnexpectedException(
                    "Need Unrestricted permission to execute this code!");
            }
        }
        #endregion
    }
}
