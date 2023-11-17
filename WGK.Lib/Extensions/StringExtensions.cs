
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Text;

namespace WGK.Lib.Extensions
{
    public static class StringExtensions
    {
        #region Null, Empty, BlankCode, Zero check methods
        /// <summary>
        /// Indicates whether the specified System.String object is null or a System.String.Empty string
        /// or a  UserCode.BlankCode string.
        /// </summary>
        public static bool IsNullOrEmptyOrBlankCode(this string pString)
        {
            return string.IsNullOrEmpty(pString); //|| pString.Equals(UserCodes.UserCode.BlankCode);
        }

        /// <summary>
        /// Indicates whether the specified System.String object is null or a System.String.Empty string
        /// or a  UserCode.BlankCode string.
        /// </summary>
        public static bool IsNullOrEmptyOrBlankCodeForSearchIdentification(this string pString)
        {
            return string.IsNullOrEmpty(pString)|| pString.Equals(UserCodes.UserCode.BlankCode);
        }

        /// <summary>
        /// Indicates if the specified System.String object is a UserCode.BlankCode string.
        /// </summary>
        public static bool IsBlankCode(this string pString)
        {
            return !string.IsNullOrEmpty(pString) && pString.Equals(UserCodes.UserCode.BlankCode);
        }

        /// <summary>
        /// Indicates whether the specified System.String object is null or a zero string
        /// or a  UserCode.ZeroCode string.
        /// </summary>
        public static bool IsNullOrEmptyOrZero(this string pString)
        {
            return string.IsNullOrEmpty(pString); // || pString.Equals(UserCodes.UserCode.ZeroCode);
        }

        /// <summary>
        /// Indicates if the specified System.String object is a UserCode.ZeroCode string.
        /// </summary>
        public static bool IsZeroCode(this string pString)
        {
            return !string.IsNullOrEmpty(pString); // && pString.Equals(UserCodes.UserCode.ZeroCode);
        }
        #endregion
        
        #region TryParseToNullableXXX methods
        // Use thes delegates to pass in the value type's TryParse function
        private delegate bool TryParseFunc<T>(string pValue, out T pResult) where T : struct;
        private delegate bool TryParseFuncNumeric<T>(
            string pValue,
            NumberStyles pNumberStyle,
            IFormatProvider pFormatProvider,
            out T pResult) where T : struct;

        /// <summary>
        /// Generic implementation of a TryParse string extension method for Nullable result types.
        /// </summary>
        private static bool TryParseToNullable<T>(
            this string pValue,
            out Nullable<T> pResult,
            TryParseFunc<T> pTryParse) where T : struct
        {
            pResult = null;
            if (!string.IsNullOrWhiteSpace(pValue))
            {
                T vParsedValue;
                if (pTryParse(pValue, out vParsedValue))
                {
                    pResult = vParsedValue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }


        /// <summary>
        /// Generic implementation of a TryParse string extension method for Nullable numeric result types.
        /// I.e. byte, short, int, long, float, double, decimal.
        /// </summary>
        private static bool TryParseToNullable<T>(
            this string pValue,
            out Nullable<T> pResult,
            NumberStyles pNumberStyle,
            IFormatProvider pFormatProvider,
            TryParseFuncNumeric<T> pTryParse) where T : struct
        {
            pResult = null;
            if (!string.IsNullOrWhiteSpace(pValue))
            {
                T vParsedValue;
                if (pTryParse(pValue, pNumberStyle, pFormatProvider, out vParsedValue))
                {
                    pResult = vParsedValue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        // DELETED - We don't need this generic method since we use the dedicated TryParse methods that are more efficient.
        ///// <summary>
        ///// Generic implementation of a TryParse method for any value type.
        ///// This method is expensive compared to using the Types' dedicated TryParse methods.
        ///// </summary>        
        //private bool TryParseStruct<T>(string pValue, out T pResult) where T : struct
        //{
        //    pResult = default(T); 
        //    try 
        //    { 
        //        var vConvertibleString = (IConvertible)pValue;
        //        pResult = (T)vConvertibleString.ToType(typeof(T), System.Globalization.CultureInfo.CurrentCulture); 
        //    } 
        //    catch(InvalidCastException) 
        //    { 
        //        return false; 
        //    } 
        //    catch (FormatException) 
        //    { 
        //        return false; 
        //    } 
        //    return true; 
        //}
    
        /// <summary>
        /// Parses the string to a Nullable DateTime value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable DateTime.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for DateTime.TryParse when you need a Nullable DateTime result. 
        /// </remarks>
        public static bool TryParseToNullableDateTime(this string pValue, out DateTime? pResult)
        {
            return pValue.TryParseToNullable<DateTime>(out pResult, DateTime.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable DateTime value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable DateTime.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for DateTime.TryParse when you need a Nullable DateTime result. 
        /// </remarks>
        public static bool TryParseToNullableDateTime(
            this string pValue,
            string pFormatString,
            IFormatProvider pFormatProvider,
            out DateTime? pResult)
        {
            pResult = null;
            if (!string.IsNullOrWhiteSpace(pValue))
            {
                DateTime vParsedValue;
                if (DateTime.TryParseExact(pValue, pFormatString, pFormatProvider, DateTimeStyles.None, out vParsedValue))
                {
                    pResult = vParsedValue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Parses the string to a Nullable Guid value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable Guid.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for Guid.TryParse when you need a Nullable Guid result. 
        /// </remarks>
        public static bool TryParseToNullableGuid(this string pValue, out Guid? pResult)
        {
            return pValue.TryParseToNullable<Guid>(out pResult, Guid.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable Guid value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable Guid.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for Guid.TryParse when you need a Nullable Guid result. 
        /// </remarks>
        public static bool TryParseToNullableGuid(
            this string pValue,
            string pFormatString,
            out Guid? pResult)
        {
            pResult = null;
            if (!string.IsNullOrWhiteSpace(pValue))
            {
                Guid vParsedValue;
                if (Guid.TryParseExact(pValue, pFormatString, out vParsedValue))
                {
                    pResult = vParsedValue;
                }
                else
                {
                    return false;
                }
            }
            return true;
        }

        /// <summary>
        /// Parses the string to a Nullable byte value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable byte.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for byte.TryParse when you need a Nullable byte result. 
        /// </remarks>
        public static bool TryParseToNullableByte(this string pValue, out byte? pResult)
        {
            return pValue.TryParseToNullable<byte>(out pResult, byte.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable byte value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable byte.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for byte.TryParse when you need a Nullable byte result. 
        /// </remarks>
        public static bool TryParseToNullableByte(
            this string pValue,
            out Byte? pResult,
            NumberStyles pNumberStyles,
            IFormatProvider pFormatProvider)
        {
            return pValue.TryParseToNullable<Byte>(out pResult, pNumberStyles, pFormatProvider, Byte.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable short value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable short.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for short.TryParse when you need a Nullable short result. 
        /// </remarks>
        public static bool TryParseToNullableShort(this string pValue, out short? pResult)
        {
            return pValue.TryParseToNullable<short>(out pResult, short.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable Short value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable Short.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for Short.TryParse when you need a Nullable Short result. 
        /// </remarks>
        public static bool TryParseToNullableShort(
            this string pValue,
            out short? pResult,
            NumberStyles pNumberStyles,
            IFormatProvider pFormatProvider)
        {
            return pValue.TryParseToNullable<short>(out pResult, pNumberStyles, pFormatProvider, short.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable int value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable int.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for int.TryParse when you need a Nullable int result. 
        /// </remarks>
        public static bool TryParseToNullableInt(this string pValue, out int? pResult)
        {
            return pValue.TryParseToNullable<int>(out pResult, int.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable int value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable int.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for int.TryParse when you need a Nullable int result. 
        /// </remarks>
        public static bool TryParseToNullableInt(
            this string pValue,
            out int? pResult,
            NumberStyles pNumberStyles,
            IFormatProvider pFormatProvider)
        {
            return pValue.TryParseToNullable<int>(out pResult, pNumberStyles, pFormatProvider, int.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable long value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable long.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for long.TryParse when you need a Nullable long result. 
        /// </remarks>
        public static bool TryParseToNullableLong(this string pValue, out long? pResult)
        {
            return pValue.TryParseToNullable<long>(out pResult, long.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable long value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable long.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for long.TryParse when you need a Nullable long result. 
        /// </remarks>
        public static bool TryParseToNullableLong(
            this string pValue,
            out long? pResult,
            NumberStyles pNumberStyles,
            IFormatProvider pFormatProvider)
        {
            return pValue.TryParseToNullable<long>(out pResult, pNumberStyles, pFormatProvider, long.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable float value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable float.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for float.TryParse when you need a Nullable float result. 
        /// </remarks>
        public static bool TryParseToNullableFloat(this string pValue, out float? pResult)
        {
            return pValue.TryParseToNullable<float>(out pResult, float.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable float value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable float.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for float.TryParse when you need a Nullable float result. 
        /// </remarks>
        public static bool TryParseToNullableFloat(
            this string pValue,
            out float? pResult,
            NumberStyles pNumberStyles,
            IFormatProvider pFormatProvider)
        {
            return pValue.TryParseToNullable<float>(out pResult, pNumberStyles, pFormatProvider, float.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable double value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable double.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for double.TryParse when you need a Nullable double result. 
        /// </remarks>
        public static bool TryParseToNullableDouble(this string pValue, out double? pResult)
        {
            return pValue.TryParseToNullable<double>(out pResult, double.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable double value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable double.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for double.TryParse when you need a Nullable double result. 
        /// </remarks>
        public static bool TryParseToNullableDouble(
            this string pValue,
            out double? pResult,
            NumberStyles pNumberStyles,
            IFormatProvider pFormatProvider)
        {
            return pValue.TryParseToNullable<double>(out pResult, pNumberStyles, pFormatProvider, double.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable Decimal value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable Decimal.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for Decimal.TryParse when you need a Nullable Decimal result. 
        /// </remarks>
        public static bool TryParseToNullableDecimal(this string pValue, out Decimal? pResult)
        {
            return pValue.TryParseToNullable<Decimal>(out pResult, Decimal.TryParse);
        }

        /// <summary>
        /// Parses the string to a Nullable decimal value.
        /// </summary>
        /// <param name="pResult">The resulting Nullable decimal.</param>
        /// <returns>True if parsing was successful or if there was nothing to parse</returns>
        /// <remarks>
        /// Use this method as a replacement for decimal.TryParse when you need a Nullable decimal result. 
        /// </remarks>
        public static bool TryParseToNullableDecimal(
            this string pValue,
            out Decimal? pResult,
            NumberStyles pNumberStyles,
            IFormatProvider pFormatProvider)
        {
            return pValue.TryParseToNullable<Decimal>(out pResult, pNumberStyles, pFormatProvider, Decimal.TryParse);
        }
        #endregion

        #region Conversion methods
        /// <summary>
        /// Returns a copy of this string with the first character converted to uppercase.
        /// </summary>
        public static string ToUpperFirst(this string pString)
        {
            return pString.ToUpperFirst(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a copy of this string with the first character converted to uppercase.
        /// </summary>
        public static string ToUpperFirst(this string pString, CultureInfo pCultureInfo)
        {
            if (pString == null)
            {
                return null;
            }
            if (pString.Length == 0)
            {
                return string.Empty;
            }
            return char.ToUpper(pString[0], pCultureInfo) + pString.Substring(1);
        }

        /// <summary>
        /// Returns a copy of this string with the first character converted to lowercase.
        /// </summary>
        public static string ToLowerFirst(this string pString)
        {
            return pString.ToLowerFirst(CultureInfo.CurrentCulture);
        }

        /// <summary>
        /// Returns a copy of this string with the first character converted to lowercase.
        /// </summary>
        public static string ToLowerFirst(this string pString, CultureInfo pCultureInfo)
        {
            if (pString == null)
            {
                return null;
            }
            if (pString.Length == 0)
            {
                return string.Empty;
            }
            return char.ToLower(pString[0], pCultureInfo) + pString.Substring(1);
        }

        /// <summary>
        /// Returns a copy of this string with all occurrences of the specified characters removed.
        /// </summary>
        public static string RemoveCharacters(this string pString, char[] pCharacters)
        {
            // Convert array to HashSet for faster lookup
            var vCharHashSet = new HashSet<char>(pCharacters);

            var vResult = new StringBuilder(pString.Length);
            for (int i = 0; i < pString.Length; i++)
            {
                if (!vCharHashSet.Contains(pString[i]))
                {
                    vResult.Append(pString[i]);
                }
            }
            return vResult.ToString();
        }
        #endregion

        #region Contains method
        /// <summary>
        /// Returns a value indicating whether the specified string value occurs within this string
        /// when compared using the specified comparison option.
        /// </summary>
        /// <param name="pString"></param>
        /// <param name="pValue">The string to seek</param>
        /// <param name="pComparisonType">One of the enumeration values that determines how this string and value are compared</param>
        /// <returns>true if the value parameter occurs within this string, or if value is the empty string (""); otherwise, false</returns>
        public static bool Contains(this string pString, string pValue, StringComparison pComparisonType)
        {
            if (string.IsNullOrEmpty(pValue) || string.IsNullOrEmpty(pString))
            {
                return true;
            }
            return pString.IndexOf(pValue, pComparisonType) >= 0;
        }
        #endregion
    }
}
