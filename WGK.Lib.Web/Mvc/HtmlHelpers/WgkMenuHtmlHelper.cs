using Microsoft.AspNetCore.Mvc.Rendering;
using WGK.Lib.Web.Mvc.Controls.Menu;

namespace WGK.Lib.Web.Mvc.HtmlHelpers
{
    public static class WGKMenuHtmlHelper
    {
        public static Microsoft.AspNetCore.Html.HtmlString WGKMenu(this IHtmlHelper pHtmlHelper, IMenuConfig pMenuConfig)
        {
            return pMenuConfig.Render(pHtmlHelper);
        }
    }
}