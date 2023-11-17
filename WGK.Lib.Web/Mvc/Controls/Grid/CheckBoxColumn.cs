using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Grid edit column for edittype:'checkbox'.
    /// Renders the column as a CheckBox in edit mode.
    /// </summary>
    public class CheckBoxColumn : BaseEditColumn<CheckBoxColumn>, IGridEditColumn
    {
        #region Fields
        private string iCheckedValue = "True";
        private string iUncheckedValue = "False";
        #endregion

        #region Public Methods
        /// <summary>
        /// Defines the checked and unchecked values for the CheckBox.
        /// If SetValues is not called, jqGrid searches for the following values (false|0|no|off|undefined) in order to construct the checkbox.
        /// If the cell content does not contain one of these values, then the value attribute becomes the cell content and offval is set to off.
        /// </summary>
        /// <param name="pCheckedValue">The checked value.</param>
        /// <param name="pUncheckedValue">The unchecked value.</param>
        /// <returns></returns>
        public CheckBoxColumn SetValues(string pCheckedValue, string pUncheckedValue)
        {
            this.iCheckedValue = pCheckedValue;
            this.iUncheckedValue = pUncheckedValue;
            return this;
        }
        #endregion

        #region IGridEditColumn implementation
        public override string Render()
        {
            var vBuilder = new StringBuilder();
            vBuilder.Append(", edittype: \"checkbox\"");
            vBuilder.Append(", formatter:\"checkbox\"");

            string vDataEvents = this.RenderDataEvents();
            bool vIsFirstOption = true;

            if (!string.IsNullOrEmpty(this.iCheckedValue)
                || !string.IsNullOrEmpty(this.iUncheckedValue)
                || !string.IsNullOrEmpty(vDataEvents))
            {
                vBuilder.Append(", editoptions: { ");

                if (!string.IsNullOrEmpty(vDataEvents))
                {
                    vIsFirstOption = false;
                    vBuilder.Append(vDataEvents);
                }

                if (!string.IsNullOrEmpty(this.iCheckedValue)
                    || !string.IsNullOrEmpty(this.iUncheckedValue))
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("value: \"");
                    vBuilder.Append(this.iCheckedValue);
                    vBuilder.Append(":");
                    vBuilder.Append(this.iUncheckedValue);
                    vBuilder.Append("\"");
                }

                vBuilder.Append(" }"); // end editoptions               
            }
            return vBuilder.ToString();
        }
        #endregion
    }
}
