using System.Collections.Generic;
using System.Globalization;
using System.Text;
using WGK.Lib.Web.Mvc.Controls.Grid;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Defines GridCellTypes for local data sorting
    /// </summary>
    public enum GridCellType
    {
        Default,
        Int,
        Float,
        Date,
        None,
        Custom
    }

    /// <summary>
    /// Defines the alignment of the cell in the grid
    /// </summary>
    public enum Align
    {
        Default,
        Left,
        Right,
        Center
    }

    public class GridColumnModel
    {
        private static readonly Dictionary<GridCellType, string> cColumnCellTypes = new Dictionary<GridCellType, string>
        {
            {GridCellType.Int, "int"}, 
            {GridCellType.Date, "date"}, 
            {GridCellType.Float, "float"}
        };

        private static readonly Dictionary<Align, string> cAlignmentTypes = new Dictionary<Align, string>
        {
            {Align.Left, "left"}, 
            {Align.Right, "right"}, 
            {Align.Center, "center"}
        };


        private Align iAlign = Align.Default;
        private bool iAsGroup;
        private string iCaption;
        private GridCellType iCellType = GridCellType.Default;
        private IGridEditColumn iGridEditColumn;
        private string iCustomAttributes = string.Empty;
        private string iFormatter = null;
        private string iUnformatter = null;
        private bool iNullIfEmpty = false;
        private bool iEditable = true;
        private bool iReadOnly = false;
        private string iDataInit;
        private bool iHidden;
        private string iIndex;
        private bool iIsPrimaryKey;
        private string iName;
        private bool iSortable = true;
        private bool iResizable = true;
        private string iWidth;
        private string iHeaderTooltip;
        private readonly List<EditRule> iEditRules = new List<EditRule>();

        public GridColumnModel()
        {
        }

        /// <summary>
        /// </summary>
        /// <param name="pName">The name of the column as well as the member</param>
        public GridColumnModel(string pName)
        {
            this.iName = pName;
            this.iCaption = this.iName;
            this.iWidth = "50";
        }

        /// <summary>
        /// </summary>
        /// <param name="pName">The member name to use as data-source</param>
        /// <param name="pCaption">The column caption text</param>
        public GridColumnModel(string pName, string pCaption)
        {
            this.iName = pName;
            this.iCaption = pCaption;
            this.iWidth = "50";
        }

        /// <summary>
        /// </summary>
        /// <param name="pName">The member name to use as data-source</param>
        /// <param name="pCaption">The column caption text</param>
        /// <param name="pWidth">The column width, percentage is allowed by: \"100%\"</param>
        public GridColumnModel(string pName, string pCaption, string pWidth)
        {
            this.iName = pName;
            this.iCaption = pCaption;
            this.iWidth = pWidth;
        }

        /// <summary>
        /// </summary>
        /// <param name="pName">The member name to use as data-source</param>
        /// <param name="pCaption">The column caption text</param>
        /// <param name="pWidth">The column width, percentage is allowed by: \"100%\"</param>
        /// <param name="pAlign">The column alignment</param>
        public GridColumnModel(string pName, string pCaption, string pWidth, Align pAlign)
        {
            this.iName = pName;
            this.iCaption = pCaption;
            this.iWidth = pWidth;
            this.iAlign = pAlign;
        }

        /// <summary>
        /// The member name to use as data source
        /// </summary>
        public string Name
        {
            get { return this.iName; }
            set { this.iName = value; }
        }

        /// <summary>
        /// Allows the additional of custom attributes not mapped by the GridColumnModel object
        /// </summary>
        public string CustomAttributes
        {
            get { return this.iCustomAttributes; }
            set { this.iCustomAttributes = value; }
        }

        /// <summary>
        /// Whether or not the grid should be grouped by this column
        /// </summary>
        public bool AsGroup
        {
            get { return this.iAsGroup; }
            set { this.iAsGroup = value; }
        }

        /// <summary>
        /// The type of the cell. Required to support sorting
        /// If you don't want to use one of the predefined cell-types, you can specify CUSTOM,
        /// And add the custom attributes manually. 
        /// </summary>
        public GridCellType CellType
        {
            get { return this.iCellType; }
            set { this.iCellType = value; }
        }


        public bool IsPrimaryKey
        {
            get { return this.iIsPrimaryKey; }
            set { this.iIsPrimaryKey = value; }
        }

        /// <summary>
        /// Allow for nulls if empty
        /// </summary>
        public bool NullIfEmpty
        {
            get { return this.iNullIfEmpty; }
            set { this.iNullIfEmpty = value; }
        }

        /// <summary>
        /// Set edit cell to readonly
        /// </summary>
        public bool ReadOnly
        {
            get { return this.iReadOnly; }
            set { this.iReadOnly = value; }
        }

        /// <summary>
        /// Choose how the column will render in edit mode
        /// The default is <see cref="TextBoxColumn"/>
        /// Other available: <seealso cref="TextAreaColumn" />, <seealso cref="DropDownListColumn" />, etc.
        /// </summary>
        /// <param name="pGridEditColumn"></param>
        /// <returns></returns>
        public GridColumnModel SetEditType(IGridEditColumn pGridEditColumn)
        {
            this.iGridEditColumn = pGridEditColumn;
            pGridEditColumn.Column = this;
            return this;
        }

        /// <summary>
        /// Allows the addition of extra custom attributes to the current column
        /// </summary>
        /// <param name="pCustomAttributes">one or more custom attributes. e.g. arg1='value',arg2='value'</param>
        /// <returns></returns>
        public GridColumnModel SetCustomAttributes(string pCustomAttributes)
        {
            this.iCustomAttributes = pCustomAttributes;
            return this;
        }

        /// <summary>
        /// Allows setting a custom JavaScript element initialization function for the current column
        /// </summary>
        /// <param name="pFunction">Name of the JavaScript element initialization function</param>
        /// <returns></returns>
        public GridColumnModel SetDataInitFunction(string pFunction)
        {
            this.iDataInit = pFunction;
            return this;
        }

        /// <summary>
        /// Allows setting a custom JavaScript formatter function for the current column
        /// </summary>
        /// <remarks>
        /// Don't use with GridCellType.Date columns since these columns use the date formatter 
        /// </remarks>
        /// <param name="pFormatter">
        /// Name of the JavaScript formatter function. The function receives parameters cellvalue, options, rowObject .
        /// </param>
        /// <returns></returns>
        public GridColumnModel SetFormatter(string pFormatter)
        {
            this.iFormatter = pFormatter;
            return this;
        }

        /// <summary>
        /// Allows setting a custom JavaScript unformatter function for the current column
        /// </summary>
        /// <param name="pUnformatter">
        /// Name of the JavaScript unformatter function. The function receives parameters cellvalue, options, cell.
        /// </param>
        /// <returns></returns>
        public GridColumnModel SetUnformatter(string pUnformatter)
        {
            this.iUnformatter = pUnformatter;
            return this;
        }

        /// <summary>
        /// Makes the grid grouped by the values in the current column
        /// </summary>
        /// <returns></returns>
        public GridColumnModel SetAsGroup()
        {
            this.iAsGroup = true;
            return this;
        }

        /// <summary>
        /// Sets the data type of the current column. Required to support sorting
        /// If you don't want to use one of the predefined cell-types, you can specify CUSTOM,
        /// And add the custom attributes manually <see cref="SetCustomAttributes"/>
        /// </summary>
        /// <param name="pCellType">The cell data type for sorting</param>
        /// <returns></returns>
        public GridColumnModel SetSortType(GridCellType pCellType)
        {
            this.iCellType = pCellType;
            return this;
        }

        /// <summary>
        /// Sets the current column as the key value of each row
        /// This culumn value will be returned to the rowselected event
        /// </summary>
        /// <returns></returns>
        public GridColumnModel SetAsPrimaryKey()
        {
            this.iIsPrimaryKey = true;
            return this;
        }

        /// <summary>
        /// Sets null to return to server if value is empty
        /// </summary>
        /// <returns></returns>
        public GridColumnModel SetNullIfEmpty(bool pValue)
        {
            this.iNullIfEmpty = pValue;
            return this;
        }

        public GridColumnModel SetReadOnly(bool pValue)
        {
            this.iReadOnly = pValue;
            return this;
        }

        /// <summary>
        /// The member name to use as data source
        /// </summary>
        /// <param name="pName"></param>
        /// <returns></returns>
        public GridColumnModel SetName(string pName)
        {
            this.iName = pName;
            return this;
        }

        /// <summary>
        /// The column caption text
        /// </summary>
        /// <param name="pCaption"></param>
        /// <returns></returns>
        public GridColumnModel SetCaption(string pCaption)
        {
            this.iCaption = pCaption;
            return this;
        }

        /// <summary>
        /// The name of the property to bind from the model
        /// </summary>
        /// <param name="pIndex"></param>
        /// <returns></returns>
        public GridColumnModel SetIndex(string pIndex)
        {
            this.iIndex = pIndex;
            return this;
        }

        /// <summary>
        /// Set the width of the column
        /// (Overriding, if AutoSize was set)
        /// </summary>
        /// <param name="pWidth"></param>
        /// <returns></returns>
        public GridColumnModel SetWidth(string pWidth)
        {
            this.iWidth = pWidth;
            return this;
        }

        /// <summary>
        /// Choose column alignment
        /// </summary>
        /// <param name="pAlign"></param>
        /// <returns></returns>
        public GridColumnModel SetAlign(Align pAlign)
        {
            this.iAlign = pAlign;
            return this;
        }

        /// <summary>
        /// Choose whether the column is editable or not
        /// </summary>
        /// <param name="pEditable"></param>
        /// <returns></returns>
        public GridColumnModel SetEditable(bool pEditable)
        {
            this.iEditable = pEditable;
            return this;
        }

        /// <summary>
        /// Choose whether the column is resizable or not
        /// </summary>
        /// <param name="pResizable"></param>
        /// <returns></returns>
        public GridColumnModel SetResizable(bool pResizable)
        {
            this.iResizable = pResizable;
            return this;
        }

        /// <summary>
        /// Choose whether the column is sortable or not
        /// </summary>
        /// <param name="pSortable"></param>
        /// <returns></returns>
        public GridColumnModel SetSortable(bool pSortable)
        {
            this.iSortable = pSortable;
            return this;
        }

        /// <summary>
        /// Choose whether the column is visible to the user or not
        /// </summary>
        /// <param name="pHidden"></param>
        /// <returns></returns>
        public GridColumnModel SetHidden(bool pHidden)
        {
            this.iHidden = pHidden;
            return this;
        }

        /// <summary>
        /// Sets the header tooltip for this column.
        /// </summary>
        /// <param name="pHeaderTooltip">The header tooltip.</param>
        /// <returns></returns>
        public GridColumnModel SetHeaderTooltip(string pHeaderTooltip)
        {
            this.iHeaderTooltip = pHeaderTooltip;
            return this;
        }

        /// <summary>
        /// Adds an edit rule to the column
        /// </summary>
        /// <param name="pEditRuleType">Type of edit rule.</param>
        /// <param name="pValue">Optional rule value.</param>
        /// <returns></returns>
        public GridColumnModel AddEditRule(EditRuleType pEditRuleType, string pValue = null)
        {
            this.iEditRules.Add(new EditRule(pEditRuleType, pValue));

            return this;
        }

        public string GetColumnCaption()
        {
            return this.iCaption;
        }

        public string GetHeaderTooltip()
        {
            return this.iHeaderTooltip;
        }

        /// <summary>
        /// Renders the ColumnModel to HTML
        /// </summary>
        /// <returns></returns>
        public string Render()
        {
            var vBuilder = new StringBuilder();
            vBuilder.Append("{ ");
            vBuilder.Append("name: \"");
            vBuilder.Append(this.iName);
            vBuilder.Append("\", index: \"");
            if (string.IsNullOrEmpty(this.iIndex))
            {
                this.iIndex = this.iName;
            }
            vBuilder.Append(this.iIndex);
            vBuilder.Append("\", width: ");
            vBuilder.Append(this.iWidth);

            if (this.iAlign != Align.Default)
            {
                vBuilder.Append(", align: \"");
                vBuilder.Append(cAlignmentTypes[this.iAlign]);
                vBuilder.Append("\"");
            }

            // Sorting
            if (this.iCellType != GridCellType.Custom)
            {
                if (!this.iSortable)
                {
                    vBuilder.Append(", sortable: false");
                }

                if ((this.iSortable) && (this.iCellType != GridCellType.Default))
                {
                    vBuilder.Append(", sorttype:\"");
                    vBuilder.Append(cColumnCellTypes[this.iCellType]);
                    vBuilder.Append("\"");
                    if (this.iCellType == GridCellType.Date)
                    {
                        vBuilder.Append(", datefmt:\"");
                        string vDateFormat = GetSortDateFormat();
                        vBuilder.Append(vDateFormat);
                        vBuilder.Append("\"");

                        vBuilder.Append(", formatter:'date', formatoptions:{ srcformat:'" + vDateFormat + "', newformat:'" + vDateFormat + "' }");
                    }
                }
            }

            //Resizing
            if (!this.iResizable)
            {
                vBuilder.Append(", resizable: false");
            }

            if (this.IsPrimaryKey)
            {
                // Use this column as row id
                vBuilder.Append(", key: true");
            }

            if (this.iEditable)
            {
                vBuilder.Append(", editable: true");
            }

            if (this.iHidden)
            {
                vBuilder.Append(", hidden: true");
            }

            // You can define your own formatter andunformatter for a particular column. Usually this is a function.
            // When set in the formatter/unformat options this should NOT BE enclosed in quotes and not entered with ().
            //Just render the name of the function. E.g.: "formatter:currencyFormat, unformat:currencyUnFormat"
            if (!string.IsNullOrEmpty(this.iFormatter))
            {
                vBuilder.Append(", formatter:");
                vBuilder.Append(this.iFormatter);
            }

            if (!string.IsNullOrEmpty(this.iUnformatter))
            {
                vBuilder.Append(", unformat:");
                vBuilder.Append(this.iUnformatter);
            }

            // editoptions : START ; KEEP TOGETHER WITH CLOSING BRACE
            // remark: make sure to set space after the comma of each attribute addition
            vBuilder.Append(", editoptions:{");
            if (this.iNullIfEmpty)
            {
                vBuilder.Append("NullIfEmpty:'true', ");
            }
            if (this.iReadOnly)
            {
                vBuilder.Append("readonly:'readonly', ");
            }
            if (!string.IsNullOrEmpty(this.iDataInit))
            {
                vBuilder.AppendFormat("dataInit:{0}, ", this.iDataInit);
            }
            // remove last comma
            if (vBuilder.ToString().EndsWith(", "))
                vBuilder.Remove(vBuilder.Length - 2, 2);
            vBuilder.Append("}");
            // editoptions : END

            if (!string.IsNullOrEmpty(this.iCustomAttributes))
            {
                vBuilder.Append(", " + this.iCustomAttributes);
            }

            // Render EditType and EditOptions
            if (this.iGridEditColumn == null)
            {
                // Render edittype:'text' by default
                this.SetEditType(new TextBoxColumn());
            }
            vBuilder.Append(this.iGridEditColumn.Render());

            // Render EditRules
            vBuilder.Append(this.RenderEditRules());

            vBuilder.Append("}");
            return vBuilder.ToString();
        }

        /// <summary>
        /// Renders all the edit rules for this ColumnModel.
        /// </summary>
        /// <returns></returns>
        private string RenderEditRules()
        {
            if (this.iEditRules.Count != 0)
            {
                var vBuilder = new StringBuilder();
                vBuilder.Append(", editrules: { ");
                bool vFirst = true;
                foreach (EditRule vEditRule in this.iEditRules)
                {
                    if (!vFirst)
                    {
                        vBuilder.Append(", ");
                    }
                    else
                    {
                        vFirst = false;
                    }
                    vBuilder.Append(vEditRule.Render());
                }
                vBuilder.Append("}");
                return vBuilder.ToString();
            }
            return null;
        }

        /// <summary>
        /// Convert .NET ShortTimePattern to jqgrid datefmt string
        /// </summary>
        /// <returns></returns>
        private static string GetSortDateFormat()
        {
            // jqgrid datefmt string uses the PHP standard
            // Examples:
            // - convert "dd/MM/yyyy" to "d/m/Y"
            // - convert "d-M-yy" to "j-n-y"

            CultureInfo vCurrentCulture = CultureInfo.CurrentCulture;
            string vDatePattern = vCurrentCulture.DateTimeFormat.ShortDatePattern;

            // -- Convert day of week
            if (vDatePattern.Contains("dddd"))
            {
                // A full textual representation of the day of the week
                vDatePattern = vDatePattern.Replace("D", "l");
            }
            else if (vDatePattern.Contains("ddd"))
            {
                // Textual representation of a day, three letters
                vDatePattern = vDatePattern.Replace("D", "j");
            }

            // -- Convert day of month
            if (vDatePattern.Contains("dd"))
            {
                // Day of the month, 2 digits with leading zeros
                vDatePattern = vDatePattern.Replace("dd", "d");
            }
            else if (vDatePattern.Contains("d"))
            {
                // Day of the month without leading zeros
                // DELETED - jqGrid does not support days without leading zeros ...
                //vDatePattern = vDatePattern.Replace("d", "j");
            }

            // -- Convert month
            if (vDatePattern.Contains("MMMM"))
            {
                // A full textual representation of the month
                vDatePattern = vDatePattern.Replace("MMM", "F");
            }
            else if (vDatePattern.Contains("MMM"))
            {
                // A short textual representation of a month, three letters
                vDatePattern = vDatePattern.Replace("MMM", "M");
            }
            else if (vDatePattern.Contains("MM"))
            {
                // Numeric representation of a month, with leading zeros
                vDatePattern = vDatePattern.Replace("MM", "m");
            }
            else if (vDatePattern.Contains("M"))
            {
                // Numeric representation of a month, without leading zeros
                // DELETED - jqGrid does not support days without leading zeros ...
                //vDatePattern = vDatePattern.Replace("M", "n");
                vDatePattern = vDatePattern.Replace("M", "m");
            }

            // -- Convert year
            if (vDatePattern.Contains("yyyy"))
            {
                // A full numeric representation of a year, 4 digits
                vDatePattern = vDatePattern.Replace("yyyy", "Y");
            }
            else if (vDatePattern.Contains("yy"))
            {
                // A two digit representation of a year
                vDatePattern = vDatePattern.Replace("yy", "y");
            }
            return vDatePattern;
        }
    }
}