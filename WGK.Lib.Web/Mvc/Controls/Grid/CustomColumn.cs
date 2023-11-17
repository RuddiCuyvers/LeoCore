using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Grid edit column for edittype:'custom'.
    /// Renders the column in edit mode by calling the custom_element javascript function.
    /// </summary>
    public class CustomColumn : BaseEditColumn<CustomColumn>, IGridEditColumn
    {
        #region Fields
        private string iCustomElementFunc;
        private string iCustomValueFunc;
        #endregion

        #region Constructor
        /// <summary>
        /// For custom columns we should provide a set of two javascipt functions, one which creates the element, 
        /// and one that gets the value from the form in order to be posted to the server
        /// </summary>
        /// <param name="pCustomElementFunc">This function is used to create the element. The function should return the new DOM element. Parameters passed to this function are the value and the editoptions from colModel</param>
        /// <param name="pCustomValueFunc">This function should return the value from the element after the editing in order to post it to the server. Parameter passed to this function is the element object. This function is also used for updating the value of an element, when element is displayed in form</param>
        public CustomColumn(string pCustomElementFunc, string pCustomValueFunc)
        {
            this.iCustomElementFunc = pCustomElementFunc;
            this.iCustomValueFunc = pCustomValueFunc;            
        }
        #endregion

        #region IGridEditColumn implementation
        public override string Render()
        {
            var vBuilder = new StringBuilder();
            vBuilder.Append(", edittype: \"custom\"");

            string vDataEvents = this.RenderDataEvents();
            bool vIsFirstOption = true;

            if (!string.IsNullOrEmpty(this.iCustomElementFunc)
                || !string.IsNullOrEmpty(this.iCustomValueFunc)
                || !string.IsNullOrEmpty(vDataEvents))
            {
                vBuilder.Append(", editoptions: { ");

                if (!string.IsNullOrEmpty(vDataEvents))
                {
                    vIsFirstOption = false;
                    vBuilder.Append(vDataEvents);
                }

                if (!string.IsNullOrEmpty(this.iCustomElementFunc))
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("custom_element: ");
                    vBuilder.Append(this.iCustomElementFunc);
                }

                if (!string.IsNullOrEmpty(this.iCustomValueFunc))
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("custom_value: ");
                    vBuilder.Append(this.iCustomValueFunc);
                }

                vBuilder.Append(" }"); // end editoptions               
            }
            return vBuilder.ToString();
        }
        #endregion
    }
}
