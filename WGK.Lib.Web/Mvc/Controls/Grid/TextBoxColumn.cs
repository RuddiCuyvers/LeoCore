using System.Text;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Grid edit column for edittype:'text'.
    /// Renders the column as a textbox in edit mode.
    /// </summary>
    public class TextBoxColumn : BaseEditColumn<TextBoxColumn>, IGridEditColumn
    {
        #region Fields
        private int iMaxLength;
        private int iSize;
        #endregion

        #region Public Methods
        /// <summary>
        /// Sets the maximum number of characters that can be typed in the textbox.
        /// </summary>
        /// <param name="pMaxLength">Length of the p max.</param>
        /// <returns></returns>
        public TextBoxColumn SetMaxLength(int pMaxLength)
        {
            this.iMaxLength = pMaxLength;
            return this;
        }

        /// <summary>
        /// Sets the size of the texbox in number of characters.
        /// </summary>
        /// <param name="pSize">Size of the p.</param>
        /// <returns></returns>
        public TextBoxColumn SetSize(int pSize)
        {
            this.iSize = pSize;
            return this;
        }
        #endregion

        #region IGridEditColumn implementation
        public override string Render()
        {
            var vBuilder = new StringBuilder();
            // No need to render edittype:'text' since it is the default.
            //vBuilder.Append(", edittype: \"text\"");

            // NOT TODO: WGKLIB - next formatter line is necessary to correct dates at rendering and hence saving
            //if (this.Column.CellType == GridCellType.Date)
            //    vBuilder.Append(", formatter:\"date\"");

            string vDataEvents = this.RenderDataEvents();
            
            bool vIsFirstOption = true;

            if (this.iMaxLength != 0
                || this.iSize != 0
                || !string.IsNullOrEmpty(vDataEvents))
            {
                vBuilder.Append(", editoptions: { ");

                if (!string.IsNullOrEmpty(vDataEvents))
                {
                    vIsFirstOption = false;
                    vBuilder.Append(vDataEvents);
                }

                if (this.iMaxLength != 0)
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("maxlength: ");
                    vBuilder.Append(this.iMaxLength);
                }

                if (this.iSize != 0)
                {
                    if (!vIsFirstOption)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vIsFirstOption = false;
                    }
                    vBuilder.Append("size: ");
                    vBuilder.Append(this.iSize);
                }

                vBuilder.Append(" }"); // end editoptions               
            }
            return vBuilder.ToString();
        }
        #endregion
    }
}