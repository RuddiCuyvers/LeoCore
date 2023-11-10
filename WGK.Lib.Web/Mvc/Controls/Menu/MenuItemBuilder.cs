using System;
using System.Linq;
using System.Linq.Expressions;
using System.Web;
using Microsoft.AspNetCore.Mvc;

using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http;

namespace WGK.Lib.Web.Mvc.Controls.Menu
{
    public class MenuItemBuilder
    {
        private readonly IHtmlHelper iHtmlHelper;
        private string iText;
        private readonly MenuItemFactory iMenuItemFactory;
        private bool iHide;
        private string iUrl;
        private string iActionName;
        private string iControllerName;
        private readonly RouteValueDictionary iRouteValues;


        public MenuItemBuilder(IHtmlHelper pHtmlHelper)
        {
            iRouteValues = new RouteValueDictionary();
            iMenuItemFactory = new MenuItemFactory(pHtmlHelper);
            iHtmlHelper = pHtmlHelper;
            iText = "---";
        }

        public MenuItemBuilder Text(string pText)
        {
            iText = pText;
            return this;
        }

        public override string ToString()
        {
            if (iHide) return "";
            string contains = Link.ToString().ToLower();
            string substringUrl = "";  //startpunt

            switch (contains)  //dit om prentje te tonen ipv tekst in een menu
            {
               case var s when contains.Contains("mijn ibf"):
                    substringUrl = contains.Replace("mijn ibf", "<i style='font-size:30px' class=\"fa-solid fa-graduation-cap\"></i>");
                    return "<div class=\"Nav-Item\">" + substringUrl + Link + iMenuItemFactory.WithUl() + "</div>";

                case var s when contains.Contains("les aanmaken"):
                    substringUrl = contains.Replace("les aanmaken", "<i style='font-size:30px' class=\"fa-solid fa-plus\"></i>");
                    return "<div class=\"Nav-Item\">" + substringUrl+ Link + iMenuItemFactory.WithUl() + "</div>";

                case var s when contains.Contains("les zoeken"):
                    substringUrl = contains.Replace("les zoeken", "<i style='font-size:30px' class=\"fa-solid fa-magnifying-glass\"></i>");
                    return "<div class=\"Nav-Item\">" + substringUrl +Link + iMenuItemFactory.WithUl() + "</div>";

                case var s when contains.Contains("contact"):
                    substringUrl = Link.ToString().Replace("Contact", "<i style='font-size:30px' class=\"fa-solid fa-circle-question\"></i>");
                    return "<div class=\"Nav-Item\">" + substringUrl + Link + iMenuItemFactory.WithUl() + "</div>";

                case var s when contains.Contains("profiel"):
                    substringUrl = contains.Replace("profiel", "<i style='font-size:30px' class=\"fa-solid fa-user\"></i>");
                    return "<div class=\"Nav-Item\">" + substringUrl + Link + iMenuItemFactory.WithUl() + "</div>";
              
                default:
                    return "<div class=\"Nav-Item\">" + Link + iMenuItemFactory.WithUl() + "</div>";
                    
            }
        }

        private Microsoft.AspNetCore.Html.HtmlString Link
        {
            get
            {
                if (!string.IsNullOrEmpty(iActionName) && !string.IsNullOrEmpty(iControllerName))
                {
                    return new Microsoft.AspNetCore.Html.HtmlString(@"<a href=\" + iControllerName + @"\" + iActionName + @">" + iText + @"</a>");
                    // return iHtmlHelper.ActionLink(iText, iActionName, iControllerName, iRouteValues, null);
                }
                if (!string.IsNullOrEmpty(iUrl))
                {
                    return new Microsoft.AspNetCore.Html.HtmlString("<a href=\"" + iUrl + "\">" + iText + "</a>");
                }

                return new Microsoft.AspNetCore.Html.HtmlString("<a href=\"#\">" + iText + "</a>");
               
            }
        }

    

        public void Url(string pUrl)
        {
  
            iUrl = pUrl;
        }

        public MenuItemBuilder Action<T>(Expression<Action<T>> pAction)
        {
            
            var vMethodCallExpression = (MethodCallExpression)pAction.Body;
            iActionName = vMethodCallExpression.Method.Name;
            iControllerName = typeof(T).Name.ToLower().Replace("controller", "");
            var vParameters = vMethodCallExpression.Method.GetParameters();
            for (var vIndex = 0; vIndex < vParameters.Length; ++vIndex)
            {
                var vExpression = vMethodCallExpression.Arguments[vIndex];
                var vConstantExpression = vExpression as ConstantExpression;
                var obj = vConstantExpression == null ? Expression.Lambda<Func<object>>(Expression.Convert(vExpression, typeof(object)), new ParameterExpression[0]).Compile()() : vConstantExpression.Value;
                iRouteValues.Add(vParameters[vIndex].Name, obj);
            }
            return this;
        }


        public MenuItemBuilder Items(Action<MenuItemFactory> pAction)
        {
            pAction.Invoke(iMenuItemFactory);
            return this;
        }

       

        /// <summary>
        /// Show menu item only if the current user has the specified permission on a task
        /// </summary>
        public MenuItemBuilder OnlyForPermission(string pTaskID, string pPermissionID)
        {
            //var vTaskPrincipal = HttpContext.Current.User as ITaskPrincipal;
            //if (vTaskPrincipal == null || !vTaskPrincipal.HasPermissionOnTask(pTaskID, pPermissionID))
            //{
            //    this.iHide = true;
            //}
            return this;
        }

        /// <summary>
        /// Show menu item only if the condition is true
        /// </summary>
        public MenuItemBuilder OnlyIf(bool pCondition)
        {
            if (!pCondition)
            {
                this.iHide = true;
            }
            return this;
        }
    }
}