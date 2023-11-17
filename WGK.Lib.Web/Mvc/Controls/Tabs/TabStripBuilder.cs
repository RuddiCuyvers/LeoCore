using System;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace WGK.Lib.Web.Mvc.Controls.Tabs
{
    public class TabStripBuilder
    {
        private string iHtmlID = "tabs"; // default HtmlID
        private string iFormHtmlID;
        private string iSelected;
        private StringBuilder iStringBuilder;
        private TabStripItemFactory iItemFactory;

        public TabStripBuilder()
        {
            this.iStringBuilder = new StringBuilder();
        }

        /// <summary>
        /// Sets HTML ID for the tabs widget
        /// </summary>
        public TabStripBuilder ID(string pHtmlID)
        {
            // Overwrite the default HtmlID
            this.iHtmlID = pHtmlID;
            return this;
        }

        /// <summary>
        /// Sets HTML ID of the parent form in order to make it possible to have tabs with same ID on a single page
        /// (e.g. in dialog boxes).
        /// </summary>
        public TabStripBuilder FormID(string pFormHtmlID)
        {
            this.iFormHtmlID = pFormHtmlID;
            return this;
        }

        /// <summary>
        /// Defines tab panels through a delegate
        /// </summary>
        public Microsoft.AspNetCore.Html.HtmlString Items(Action<TabStripItemFactory> pAction)
        {
            // Invoke the delegate to add panels to the TabStripItemFactory
            this.iItemFactory = new TabStripItemFactory(this.iHtmlID);
            pAction.Invoke(this.iItemFactory);

            this.BuildScript();
            this.BuildContent();

            return new Microsoft.AspNetCore.Html.HtmlString(this.iStringBuilder.ToString());
        }

        private void BuildScript()
        {
            this.iStringBuilder.Append(
@"<script type='text/javascript'>
$(function () {
    $('#");
            this.iStringBuilder.Append(this.iHtmlID);
            if (!string.IsNullOrEmpty(this.iFormHtmlID))
            {
                this.iStringBuilder.Append("', '#");
                this.iStringBuilder.Append(this.iFormHtmlID);
            }
            if (this.iSelected != null)
            {
                //Get number of selected index
                int index = int.Parse(iSelected.Split(',').Last().Replace("]", ""));
                this.iStringBuilder.Append(
    @"').tabs({selected: " + index + ", fx: { opacity: 'toggle', duration:'fast' } });});</script>");
            }
            else
            {
                //By default eerste selecteren
                this.iStringBuilder.Append(
    @"').tabs({selected: 0, fx: { opacity: 'toggle', duration:'fast' } });});</script>");
            }
        }

        private void BuildContent()
        {
            this.iStringBuilder.AppendFormat("<div id='{0}'>\r\n", this.iHtmlID);
            this.iStringBuilder.AppendLine(this.iItemFactory.ToString());
            this.iStringBuilder.AppendLine("</div>");
        }

        /// <summary>
        /// Opens the previously selected tab when the page is loaded
        /// </summary>
        /// <returns></returns>
        public TabStripBuilder Selected(string index)
        {
            if (!string.IsNullOrEmpty(index))
            {
                string[] splitted = index.Split(';').Where(s => s.Contains('[')).ToArray();
                if (splitted.Count(s => s.Split('[').First().Equals(this.iHtmlID)) > 0)
                {
                    this.iSelected = splitted.FirstOrDefault(s => s.Split('[').First().Equals(this.iHtmlID));
                    return this;
                }
                else
                {
                    this.iSelected = null;
                    return this;
                }
            }
            else
            {
                this.iSelected = null;
                return this;
            }
        }
    }
}