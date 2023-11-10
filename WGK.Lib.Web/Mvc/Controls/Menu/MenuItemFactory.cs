using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace WGK.Lib.Web.Mvc.Controls.Menu
{
    public class MenuItemFactory
    {
        private readonly IHtmlHelper iHtmlHelper;
        private readonly IList<MenuItemBuilder> iMenuItems;

        public MenuItemFactory(IHtmlHelper pHtmlHelper)
        {
            iHtmlHelper = pHtmlHelper;
            iMenuItems = new List<MenuItemBuilder>();
        }

        public MenuItemBuilder Add()
        {
            var vItemBuilder = new MenuItemBuilder(iHtmlHelper);
        
            iMenuItems.Add(vItemBuilder);
            return vItemBuilder;
        }

        public override string ToString()
        {
            return string.Join("", iMenuItems.Select(i => i.ToString()));
        }

        public string WithUl()
        {
            var vResult = ToString();
            if (string.IsNullOrEmpty(vResult)) return vResult;
            return  vResult ;
        }
    }
}