using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WGK.Lib.Web.Mvc.Controls.Menu
{
    public interface IMenuConfig
    {
        Microsoft.AspNetCore.Html.HtmlString Render(IHtmlHelper pHtmlHelper);
    }
}