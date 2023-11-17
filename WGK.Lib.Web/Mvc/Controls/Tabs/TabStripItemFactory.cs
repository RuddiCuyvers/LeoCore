using System.Collections.Generic;
using System.Linq;

namespace WGK.Lib.Web.Mvc.Controls.Tabs
{
    public class TabStripItemFactory
    {
        private readonly IList<TabStripItemBuilder> iTabStripItems;
        private readonly string iHtmlIDPrefix;

        public TabStripItemFactory(string pHtmlIDPrefix)
        {
            this.iHtmlIDPrefix = pHtmlIDPrefix;
            this.iTabStripItems = new List<TabStripItemBuilder>();
        }

        public TabStripItemBuilder Add()
        {
            // Use TabStripItems count to create a unique HtmlID for the panel
            var vItemBuilder = new TabStripItemBuilder(this.iHtmlIDPrefix + this.iTabStripItems.Count.ToString());
            this.iTabStripItems.Add(vItemBuilder);
            return vItemBuilder;
        }

        public override string ToString()
        {
            var vHeaders = "<ul>" + string.Join("\r\n", this.iTabStripItems.Select(i => i.GetHeaderHtml())) + "</ul>";
            var vContents = string.Join("", this.iTabStripItems.Select(i => i.GetContentHtml()));
            return vHeaders + vContents;
        }
    }
}