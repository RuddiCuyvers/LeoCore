using System;
using System.Linq;
using System.Web;
using Microsoft.AspNetCore.Mvc;


namespace WGK.Lib.Web.Mvc.Controls.Tabs
{
    public class TabStripItemBuilder
    {
        private string iHtmlID;
        private string iText = "geen header";
        private string iContent = "geen content";
        private bool iHide;

        public TabStripItemBuilder(string pHtmlID)
        {
            // Set the default HtmlID
            this.iHtmlID = pHtmlID;
        }

        internal string GetHeaderHtml()
        {
            if (this.iHide)
            {
                return string.Empty;
            }

            return string.Format("<li><a href=\"#{0}\">{1}</a></li>", this.iHtmlID, this.iText);
        }

        internal string GetContentHtml()
        {
            if (this.iHide)
            {
                return string.Empty;
            }

            return string.Format("<div id=\"{0}\">{1}</div>", this.iHtmlID, this.iContent);
        }

        /// <summary>
        /// Sets the HTML ID for the tab panel
        /// </summary>
        public TabStripItemBuilder ID(string pHtmlID)
        {
            // Overwrite the default HtmlID
            this.iHtmlID = pHtmlID;
            return this;
        }

        /// <summary>
        /// Sets the header text for the tab panel
        /// </summary>
        public TabStripItemBuilder Header(string pText)
        {
            this.iText = pText;
            return this;
        }

        /// <summary>
        /// Sets the content for the tab panel
        /// </summary>
        public TabStripItemBuilder Content(string pContent)
        {
            this.iContent = pContent;
            return this;
        }

        /// <summary>
        /// Sets the content for the tab panel through a delegate
        /// </summary>
        public TabStripItemBuilder Content(Func<Microsoft.AspNetCore.Html.HtmlString> pFunc)
        {
            // Invoke the delegate in order to fetch the content for the tab panel
            this.iContent = pFunc.Invoke().ToString();
            return this;
        }

        /// <summary>
        /// Show tab panel only if the current user has the specified role or the specified permission on a task, 
        /// depending on the format of the parameter(s). If a parameter starts with /TASK, the method will check
        /// for permission on a task, otherwise it will check for a role. Several permissions can be specified by separating
        /// them with a '/'' character. Example: "/TASK/TEPL/R" or "/TASK/TEPL/C/R/U/D"
        /// </summary>
        /// <param name="pRollen"></param>
        /// <returns></returns>
        public TabStripItemBuilder OnlyFor(params string[] pRollen)
        {
          //  if (!pRollen.Any(p => HttpContext.Current.User.IsInRole(p)))
           // {
            //    this.iHide = true;
           // }
            return this;
        }

        /// <summary>
        /// Show tab panel only if the current user has the specified permission on a task
        /// </summary>
        public TabStripItemBuilder OnlyForPermission(string pTaskID, string pPermissionID)
        {
            //var vTaskPrincipal = HttpContext.Current.User as ITaskPrincipal;
            //if (vTaskPrincipal == null || !vTaskPrincipal.HasPermissionOnTask(pTaskID, pPermissionID))
            //{
            //    this.iHide = true;
            //}
            return this;
        }

        /// <summary>
        /// Show tab panel only if the condition is true
        /// </summary>
        public TabStripItemBuilder OnlyIf(bool pCondition)
        {
            if (!pCondition)
            {
                this.iHide = true;
            }
            return this;
        }
    }
}