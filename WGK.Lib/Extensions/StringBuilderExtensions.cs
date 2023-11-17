using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Extensions
{
    /// <summary>
    /// Extension methods for StringBuilder
    /// </summary>
    public static class StringBuilderExtensions
    {
        /// <summary>
        /// Appends JSON property and value to the StringBuilder content.
        /// </summary>
        /// <param name="pStringBuilder">The string builder.</param>
        /// <param name="pPropertyName">Name of the property.</param>
        /// <param name="pValue">The value.</param>
        /// <param name="pIsLast">if set to <c>true</c> [is last].</param>
        /// <returns></returns>
        public static StringBuilder AppendJsonPropertyValue(
            this StringBuilder pStringBuilder, 
            string pPropertyName, 
            object pValue,
            bool pIsLast = false)
        {
            // Append format:
            //    "property":"value",
            pStringBuilder.AppendFormat(@"""{0}"":""{1}""",
                pPropertyName,
                pValue);
            if (!pIsLast)
            {
                pStringBuilder.Append(',');                
            }
            return pStringBuilder;
        }
    }
}
