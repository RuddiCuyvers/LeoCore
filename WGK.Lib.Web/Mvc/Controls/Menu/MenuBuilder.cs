using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WGK.Lib.Web.Mvc.Controls.Menu
{
    public class MenuBuilder
    {
        private readonly MenuItemFactory iMenuItemFactory;
        private const string cStartMenu = "<div id=\"Uris\">";
        private const string cEndMenu = "</div>";

        public MenuBuilder(IHtmlHelper pHtmlHelper)
        {
            iMenuItemFactory = new MenuItemFactory(pHtmlHelper);
        }

        public MenuBuilder Items(Action<MenuItemFactory> pAction)
        {
            pAction.Invoke(iMenuItemFactory);
            return this;
        }

        public Microsoft.AspNetCore.Html.HtmlString ToHtmlString()
        {
            return new Microsoft.AspNetCore.Html.HtmlString(cStartMenu + iMenuItemFactory + cEndMenu);
        }
    }


}