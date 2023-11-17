using System;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc;

namespace WGK.Lib.Web.Mvc.Controls
{
    /// <summary>
    /// Static class containing extension methods on Microsoft.AspNetCore.Html.HtmlString for adding jQuery UI widget javascript code to HtmlHelpers.
    /// <example>Usage:
    ///     this.Html.TextBoxFor(p => p.StartDate).AsDatePicker(new DatePicker())
    /// </example>
    /// </summary>
    public static class JqueryuiExtensions
    {
        #region Microsoft.AspNetCore.Html.HtmlString Extension Methods
        // DELETED - AsDatePicker() method is no longer used
        ///// <summary>
        ///// Creates a DatePicker for the current html element
        ///// NOTE: the element must contain an id in the following format: id="THE-ELEMENT-ID"
        ///// </summary>
        ///// <param name="pHtml"></param>
        ///// <param name="pDatePicker">DatePicker behaviour instance</param>
        ///// <returns></returns>
        //public static Microsoft.AspNetCore.Html.HtmlString AsDatePicker(this Microsoft.AspNetCore.Html.HtmlString pHtml, DatePicker pDatePicker)
        //{
        //    string vElemId = GetIdFromString(pHtml);
        //    return Microsoft.AspNetCore.Html.HtmlString.Create(pHtml.ToHtmlString() + pDatePicker.Render(vElemId));
        //}
        #endregion

        #region Private Methods
        private static string GetIdFromString(Microsoft.AspNetCore.Html.HtmlString pHtml)
        {
            string vRaw = pHtml.ToHtmlString();
            if (vRaw.IndexOf("id=") == -1)
            {
                throw new ArgumentException("MVC.Controls.DatePicker can only be used for Microsoft.AspNetCore.Html.HtmlString with an html-id"
                    + Environment.NewLine + "[" + pHtml.ToHtmlString() + "]");
            }

            vRaw = vRaw.Substring(vRaw.IndexOf("id=") + 4);
            vRaw = vRaw.Substring(0, vRaw.IndexOf("\""));

            return vRaw;
        }
        #endregion
    }

    public static class HtmlContentExtensions
    {
        public static string ToHtmlString(this IHtmlContent htmlContent)
        {
            if (htmlContent is HtmlString htmlString)
            {
                return htmlString.Value;
            }

            using (var writer = new System.IO.StringWriter())
            {
                htmlContent.WriteTo(writer, System.Text.Encodings.Web.HtmlEncoder.Default);
                return writer.ToString();
            }
        }
    }
}