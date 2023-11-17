using System;
using System.Web;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace WGK.Lib.Web.Mvc.Attributes
{
    /// <summary>
    /// ActionFilter attribute making sure absolutely no caching is done on the action method's  result,
    /// neither server-side nor client-side nor in the internet proxy.
    /// </summary>
    /// <remarks>
    /// Use this attribute instead of System.Web.Mvc.OutputCacheAttribute if you need to disable all caching on an action method.
    /// </remarks>
    public class NoCachingAttribute : ActionFilterAttribute
    {
        /// <summary>
        /// Called when [result executing].
        /// </summary>
        /// <param name="pFilterContext">The filter context.</param>
        public override void OnResultExecuting(ResultExecutingContext pFilterContext)
        {
            if (pFilterContext == null)
            {
                //throw new ParameterMissingException("FilterContext");
            }

            // Setting the Expires header in the past. Old versions of IE didn't respect Cache-Control
          //  pFilterContext.HttpContext.Response.Cache.SetExpires(DateTime.UtcNow.AddDays(-1));
          //  pFilterContext.HttpContext.Response.Cache.SetValidUntilExpires(false);
           // pFilterContext.HttpContext.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);
         //   pFilterContext.HttpContext.Response.Cache.SetCacheability(HttpCacheability.NoCache);
          //  pFilterContext.HttpContext.Response.Cache.SetNoStore();
        }
    }
}