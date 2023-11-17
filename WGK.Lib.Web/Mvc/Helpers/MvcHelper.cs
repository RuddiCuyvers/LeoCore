using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;


namespace WGK.Lib.Web.Mvc.Helpers
{
    public static class MvcHelper
    {
        /// <summary>
        /// Gets value from ViewState for specified key and converts value to specified type
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="pHtmlHelper">The HTML helper.</param>
        /// <param name="pKey">The key.</param>
        /// <returns></returns>
        // public static T GetModelStateValue<T>(HtmlHelper pHtmlHelper, string pKey)
        //  {
        //  Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateEntry vModelStateItem;
        //      if (pHtmlHelper.ViewData.ModelState.TryGetValue(pKey, out vModelStateItem) && (vModelStateItem.RawValue != null))
        ////      {
        //        return (T)vModelStateItem.RawValue.ConvertTo(typeof(T), null);
        //}
        //      return default(T);
        //}
    }
}
