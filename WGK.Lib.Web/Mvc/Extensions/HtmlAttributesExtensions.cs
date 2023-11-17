using System.Collections.Generic;

namespace WGK.Lib.Web.Mvc.Extensions
{
    /// <summary>
    /// Static class containing extensions for HtmlAttributes dictionary
    /// </summary>
    public static class HtmlAttributesExtensions
    {
        /// <summary>
        /// Extension method that sets or adds the maxlength attribute to the HtmlAttributes dictionary
        /// </summary>
        /// <param name="pHtmlAttributes"></param>
        /// <param name="pMaxLength"></param>
        public static void AddMaxLengthAttribute(
           this IDictionary<string, object> pHtmlAttributes,
           int pMaxLength)
        {
            if (!pHtmlAttributes.ContainsKey("maxlength"))
            {
                pHtmlAttributes.Add("maxlength ", pMaxLength);
            }
            else
            {
                pHtmlAttributes["maxlength "] = pMaxLength;
            }
        }
    }
}