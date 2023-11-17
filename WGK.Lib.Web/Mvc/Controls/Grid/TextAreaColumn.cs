using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Grid edit column for edittype:'textarea'.
    /// Renders the column as a textarea in edit mode.
    /// </summary>
    public class TextAreaColumn : BaseEditColumn<TextAreaColumn>, IGridEditColumn
    {
        #region Fields
        private int iRows;
        private int iCols;
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the number of rows of the TextArea.
        /// </summary>
        /// <param name="pRows">number of rows.</param>
        /// <returns></returns>
        public TextAreaColumn SetRows(int pRows)
        {
            this.iRows = pRows;
            return this;
        }

        /// <summary>
        /// Sets the number of columns of the TextArea
        /// </summary>
        /// <param name="pCols">number of columns.</param>
        /// <returns></returns>
        public TextAreaColumn SetCols(int pCols)
        {
            this.iCols = pCols;
            return this;
        }
        #endregion

        #region IGridEditColumn implementation
        public override string Render()
        {
            var vBuilder = new StringBuilder();
            vBuilder.Append(", edittype: \"textarea\"");

            string vDataEvents = this.RenderDataEvents();
            bool vIsFirstOption = true;

            if (this.iRows != 0
                || this.iCols != 0
                || !string.IsNullOrEmpty(vDataEvents))
            {
                vBuilder.Append(", editoptions: { ");

                if (!string.IsNullOrEmpty(vDataEvents))
                {
                    vIsFirstOption = false;
                    vBuilder.Append(vDataEvents);
                }

                if (this.iRows != 0)
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("rows: ");
                    vBuilder.Append(this.iRows);
                }

                if (this.iCols != 0)
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("cols: ");
                    vBuilder.Append(this.iCols);
                }

                vBuilder.Append(" }"); // end editoptions               
            }
            return vBuilder.ToString();
        }
        #endregion
    }
}
