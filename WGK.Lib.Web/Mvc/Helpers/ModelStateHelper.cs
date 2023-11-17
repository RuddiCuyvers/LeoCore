using System;
using System.Linq;
using System.Collections;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace WGK.Lib.Web.Mvc.Helpers
{
    /// <summary>
    /// Modelstate helper class
    /// </summary>
    public static class ModelStateHelper
    {
        /// <summary>
        /// Gets an unordered list of errors from the ModelStateDictionary
        /// </summary>
        /// <param name="pModelState"></param>
        /// <returns></returns>
        public static string GetValidationSummary(ModelStateDictionary pModelState)
        {
            //const string cHiddenListItem = @"<li style=""display:none""></li>"; 
            //var vHtmlSummary = new StringBuilder();
            //var vUnorderedList = new TagBuilder("ul"); 
            //foreach (ModelState vModelState in pModelState.Values)
            //{
            //    foreach (ModelError vModelError in vModelState.Errors)
            //    {
            //        if (!String.IsNullOrEmpty(vModelError.ErrorMessage))
            //        {
            //            var vListItem = new TagBuilder("li");
            //            vListItem.SetInnerText(vModelError.ErrorMessage);
            //            vHtmlSummary.AppendLine(vListItem.ToString(TagRenderMode.Normal));
            //        }
            //        else // error without error message
            //        {
            //            string vAttemptedValue = (vModelState.Value != null) ? vModelState.Value.AttemptedValue : null;
            //            if (!String.IsNullOrEmpty(vAttemptedValue))
            //            {
            //                var vListItem = new TagBuilder("li");
            //                vListItem.SetInnerText(string.Format(MvcLiterals.InvalidPropertyValueMessage, vAttemptedValue));
            //                vHtmlSummary.AppendLine(vListItem.ToString(TagRenderMode.Normal));
            //            }                            
            //        }
            //    }
            //}
            //if (vHtmlSummary.Length == 0)
            //{
            //    vHtmlSummary.AppendLine(cHiddenListItem);
            //}
            //vUnorderedList.InnerHtml = vHtmlSummary.ToString();
            //return vUnorderedList.ToString(TagRenderMode.Normal);
            return "tobedeveloped in WGK.Lib.web ModelStateHelper";
        }

      
    }
}