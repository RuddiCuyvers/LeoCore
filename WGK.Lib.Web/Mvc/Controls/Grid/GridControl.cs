using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using WGK.Lib.Extensions;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Http;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Renders a call to the jqgrid javascript function
    /// </summary>
    public class GridControl
    {
        #region Constants
        const int cActionButtonWidth = 20; // width of a column containing a single action button

        // These ColumnModelName constants correspond to column names used in the javascript library

        /// <summary>
        /// ColumnModelName constant that is used for the action buttons (first column)
        /// </summary>
        public static string ActionColumnModelName
        {
            get { return cActionColumnModelName; }
        }
        private const string cActionColumnModelName = "del";

        /// <summary>
        /// ColumnModelName constant that is used for the cancel button (last column)
        /// </summary>
        public static string CancelColumnModelName
        {
            get { return cCancelColumnModelName; }
        }
        private const string cCancelColumnModelName = "sav";
        #endregion
        
        #region Types
        [Flags]
        private enum GridModusEnum
        {
            View = 0, // grid is readonly: no rows can be added, deleted or edited (this is the default)

            EditOnly = 1, // grid rows are editable
            Select = 2, // grid is in select modus (multiple or single row select)
            DeleteOnly = 4, // grid rows can be deleted

            EditAndDelete = 5 // grid rows can be edited and deleted
        }
        #endregion

        #region Fields
        private bool iEnabled = true;
        private bool iIsInLookupMode = false;

        private readonly List<GridColumnModel> iColumns;
        private readonly GridColumnModel iActionButtonColumn;
        private string iAttributes;
        private string iCaption;
        private string iEditUrl; // URL for updating grid rows through ajax call

        private object iGridDataSource;
        private ICollection<long> iGridDeletedIDs;
        private ICollection<long> iGridErrorIDs;
        private long? iGridLastAddID;

        private string iGroupingFormat;
        private string iHeight;
        //private HttpVerbs iHttpVerb = HttpVerbs.Post;
        //private Microsoft.AspNetCore.Http.HttpMethods c = Microsoft.AspNetCore.Http.HttpMethods.Post;

        private bool? iIsAutoSize;
        private int? iActionButtonColumnWidth;
        private bool iIsRTL;
        private bool iIsRowNumber;
        private string iListParams;
        private string iListUrl; // URL for fetching grid data through ajax call
        private string iName;
        private bool iDisablePaging; // flag to force disable paging
        private int iPageSize;
        private string iPager;
        private string iSortName;
        private string iSortOrder;
        private string iOpenDetailColumnName;
        private string iOpenDetailUrl;
        private bool iOpenDetailAsLink;
        private string iOpenSearchFunction;
        private string iOpenCustomFormFunction;
        private GridControl iSubGrid;
        private string iWidth;
        private bool iShowAddButton = true;
        private bool iShowDeleteButton = true;
        private bool iShowPager = true;

        // Callback functions
        private string iLoadCompleteFunction;
        private string iLoadErrorFunction;
        private string iAfterInsertRowFunction;
        private string iAfterEditCellFunction;
        private string iOnEditFunction;
        private string iOnCloseFunction;
        private string iBeforeDeleteFunction;
        private string iOnDeleteFunction;
        private string iOnCreateFunction;
        private string iOnSelectedRow;
        #endregion

        #region Constructors
        public GridControl()
        {
            this.IsSubGrid = false;

            this.iColumns = new List<GridColumnModel>();

            // Add action buttons column as first visible column
            // Set column width in render method according to number of action buttons
            this.iActionButtonColumn = new GridColumnModel(GridControl.ActionColumnModelName, string.Empty, "0")
                .SetSortable(false)
                .SetEditable(false);
            this.iColumns.Add(this.iActionButtonColumn);
        }
        #endregion

        #region Properties
        public bool IsEnabled
        {
            get { return this.iEnabled; }
            set { this.iEnabled = value; }
        }

        public bool IsInLookupMode
        {
            get { return this.iIsInLookupMode; }
            set { this.iIsInLookupMode = value; }
        }


        private bool IsSubGrid { get; set; }

        public string GridName
        {
            get { return this.iName; }
        }

        public string Pager
        {
            get { return this.iPager; }
        }

        public bool ShowAddButton
        {
            get { return this.iShowAddButton; }
        }

        public bool ShowPager
        {
            get { return this.iShowPager; }
        }

        public string OnCreateFunction
        {
            get { return this.iOnCreateFunction; }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// If grouping has been set, configures how the grouping is formatted
        /// </summary>
        /// <param name="groupColumnShow">Whether or not to show the grouped column. Default to yes</param>
        /// <param name="groupText">The text title of the group. e.g. {0}-{1} items</param>
        /// <param name="groupCollapse">Whether or not the groups are collapsed by default. Default to no</param>
        /// <param name="groupOrder">The group order. Default to asc</param>
        /// <returns></returns>
        public GridControl SetGroupFormatting(bool groupColumnShow = true, string groupText = "",
            bool groupCollapse = false, string groupOrder = "asc")
        {
            this.iGroupingFormat = "groupColumnShow:[" + groupColumnShow.ToString().ToLower() + "]" +
                ", groupText: [\"" + groupText + "\"], groupCollapse: " + groupCollapse.ToString().ToLower() +
                    ", groupOrder: ['" + groupOrder + "']";

            //_groupColumnShow = groupColumnShow;
            //_groupCollpse = groupCollapse;
            //_groupText = groupText;
            //_groupOrder = groupOrder;
            return this;
        }

        public GridControl SetWidth(string pValue)
        {
            this.iWidth = pValue;
            return this;
        }

        public GridControl SetHeight(string pValue)
        {
            this.iHeight = pValue;
            return this;
        }

        /// <summary>
        /// Instead of setting a ListUrl with a controller url that will fetch the grid's data
        /// it is possible to give the grid it's data source statically thus reducing the amount of requests to the server
        /// </summary>
        /// <typeparam name="TRowModel">The type of the row model</typeparam>
        /// <param name="pGridViewModel"></param>
        /// <returns></returns>
        public GridControl SetDataSource<TRowModel>(GridViewModel<TRowModel> pGridViewModel) where TRowModel : IGridRowModel
        {
            this.iGridDataSource = pGridViewModel.Data;
            this.iGridDeletedIDs = pGridViewModel.DelIDs;
            this.iGridErrorIDs = pGridViewModel.ErrorIDs;
            this.iGridLastAddID = pGridViewModel.LastAddID;
            return this;
        }

        /// <summary>
        /// Sets a JavaScript function name
        /// That will raise when a row is selected
        /// </summary>
        public GridControl SetOnSelectedRowEvent(string val)
        {
            this.iOnSelectedRow = val;
            return this;
        }

        /// <summary>
        /// Sets the title of the grid
        /// </summary>
        /// <param name="caption"></param>
        /// <returns></returns>
        public GridControl SetCaption(string caption)
        {
            this.iCaption = caption;
            return this;
        }

     //   public GridControl SetHttpVerb(HttpVerbs verb)
       // {
     //       this.iHttpVerb = verb;
      //      return this;
       // }

        /// <summary>
        /// The name of the div that will contain the grid
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public GridControl SetName(string name)
        {
            this.iName = name;
            return this;
        }

        /// <summary>
        /// The name of the div that will contain the pager
        /// </summary>
        /// <param name="pager"></param>
        /// <returns></returns>
        public GridControl SetPager(string pager)
        {
            this.iPager = pager;
            return this;
        }

        /// <summary>
        /// Sets the property Id to use to fetch the sub-grid's data
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public GridControl SetListQueryParams(string url)
        {
            this.iListParams = url;
            return this;
        }

        /// <summary>
        /// The url to the controller method that will return the list data of the grid
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public GridControl SetListUrl(string url)
        {
            this.iListUrl = url;
            return this;
        }

        /// <summary>
        /// The url to the command that will update the edited row
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public GridControl SetEditUrl(string url)
        {
            this.iEditUrl = url;
            return this;
        }

        /// <summary>
        /// Disable paging
        /// </summary>
        /// <returns></returns>
        public GridControl SetPagingDisabled()
        {
            this.iDisablePaging = true;
            return this;
        }

        /// <summary>
        /// The page size
        /// </summary>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        public GridControl SetPageSize(int pageSize)
        {
            this.iPageSize = pageSize;
            return this;
        }

        /// <summary>
        /// Set wether the grid columns will autosize themself
        /// </summary>
        /// <param name="autoSize"></param>
        /// <returns></returns>
        public GridControl SetIsAutoSize(bool autoSize)
        {
            this.iIsAutoSize = autoSize;
            return this;
        }

        /// <summary>
        /// Overrides the default width of the action buttons column (first column)
        /// </summary>
        /// <param name="pActionButtonColumnWidth"></param>
        /// <returns></returns>
        public GridControl SetActionButtonColumnWidth(int pActionButtonColumnWidth)
        {
            this.iActionButtonColumnWidth = pActionButtonColumnWidth;
            return this;
        }

        /// <summary>
        /// Set whether to show row numbers or not
        /// </summary>
        /// <param name="rowNumber"></param>
        /// <returns></returns>
        public GridControl SetIsRowNumber(bool rowNumber)
        {
            this.iIsRowNumber = rowNumber;
            return this;
        }

        /// <summary>
        /// Sets the sorting column
        /// </summary>
        /// <param name="pSortName">Name of the p sort.</param>
        /// <param name="pSortOrder">The sort order.</param>
        /// <returns></returns>
        public GridControl SetSortColumn(string pSortName, string pSortOrder = null)
        {
            this.iSortName = pSortName;
            this.iSortOrder = pSortOrder;
            return this;
        }

        /// <summary>
        /// Sets the sorting column
        /// </summary>
        /// <typeparam name="T">Collection item model type</typeparam>
        /// <param name="pExpression">The property expression.</param>
        /// <param name="pSortOrder">The sort order.</param>
        /// <returns></returns>
        public GridControl SetSortColumn<T>(Expression<Func<T, object>> pExpression, string pSortOrder = null) where T : class
        {
            return this.SetSortColumn(pExpression.MemberNameWithoutInstance(), pSortOrder);
        }

        /// <summary>
        /// Sets the showing/hiding of the Add button in the grid footer
        /// </summary>
        /// <param name="pShowButton">If true the Add button will be shown, otherwise the button will be hidden</param>
        /// <returns></returns>
        public GridControl SetShowAddButton(bool pShowButton)
        {
            this.iShowAddButton = pShowButton;
            return this;
        }

        /// <summary>
        /// Sets the showing/hidding of the Delete buttons in the grid rows
        /// </summary>
        /// <param name="pShowButton">If true the Delete buttons will be shown, otherwise the buttons will be hidden</param>
        /// <returns></returns>
        public GridControl SetShowDeleteButton(bool pShowButton)
        {
            this.iShowDeleteButton = pShowButton;
            return this;
        }

        /// <summary>
        /// Sets the showing/hiding of the Pager footer row at the bottom of the grid rows
        /// </summary>
        /// <param name="pShowButton">If true the Delete buttons will be shown, otherwise the buttons will be hidden</param>
        /// <returns></returns>
        public GridControl SetShowPager(bool pShowPager)
        {
            this.iShowPager = pShowPager;
            return this;
        }

        /// <summary>
        /// Sets the key column for opening (navigating to) a detail page
        /// </summary>
        /// <typeparam name="T">Collection item model type</typeparam>
        /// <param name="pOpenDetailIDExpression">The property expression for the open detail ID value.</param>
        /// <param name="pOpenDetailUrl">The open detail URL string.</param>
        /// <param name="pOpenDetailAsLink">If true, the Open Detail button will be rendered as a static hyperlink</param>
        /// <returns></returns>
        public GridControl SetOpenDetailColumn<T>(
            Expression<Func<T, object>> pOpenDetailIDExpression,
            string pOpenDetailUrl,
            bool pOpenDetailAsLink = false) where T : class
        {
            this.iOpenDetailColumnName = pOpenDetailIDExpression.MemberNameWithoutInstance();
            this.iOpenDetailUrl = pOpenDetailUrl;
            this.iOpenDetailAsLink = pOpenDetailAsLink;
            return this;
        }

        /// <summary>
        /// Sets the open search javascript function.
        /// This functions gets passed the grid ID and row ID as parameters.
        /// </summary>
        /// <param name="pOpenSearchFunction">The open search function.</param>
        /// <returns></returns>
        public GridControl SetOpenSearchFunction(string pOpenSearchFunction)
        {
            this.iOpenSearchFunction = pOpenSearchFunction;
            return this;
        }

        /// <summary>
        /// Sets the open custom form javascript function.
        /// This functions gets passed the grid ID and row ID as parameters.
        /// </summary>
        /// <param name="pOpenCustomFormFunction">The open custom form function.</param>
        /// <returns></returns>
        public GridControl SetOpenCustomFormFunction(string pOpenCustomFormFunction)
        {
            this.iOpenCustomFormFunction = pOpenCustomFormFunction;
            return this;
        }

        /// <summary>
        /// Sets the load complete function.
        /// </summary>
        /// <param name="pLoadCompleteFunction">The load complete function.</param>
        /// <returns></returns>
        public GridControl SetLoadCompleteFunction(string pLoadCompleteFunction)
        {
            this.iLoadCompleteFunction = pLoadCompleteFunction;
            return this;
        }

        /// <summary>
        /// Sets the loadError function for the grid.
        /// This function gets passed the request, status, error parameters.
        /// </summary>
        /// <param name="pLoadErrorFunction">The loadError javascript function.</param>
        /// <returns></returns>
        public GridControl SetLoadErrorFunction(string pLoadErrorFunction)
        {
            this.iLoadErrorFunction = pLoadErrorFunction;
            return this;
        }

        /// <summary>
        /// Sets the afterInsert function for the grid.
        /// This function gets passed the rowid, rowdata, rowelem.
        /// </summary>
        /// <param name="pAfterInsertRowFunction">The afterinsertrow javascript function.</param>
        /// <returns></returns>
        public GridControl SetAfterInsertRowFunction(string pAfterInsertRowFunction)
        {
            this.iAfterInsertRowFunction = pAfterInsertRowFunction;
            return this;
        }

        /// <summary>
        /// Sets the afterEditCell function for the grid.
        /// This function gets passed the rowid, cellname, value, iRow, iCol
        /// </summary>
        /// <param name="pAfterEditCellFunction">The afterEditCell javascript function.</param>
        /// <returns></returns>
        public GridControl SetAfterEditCellFunction(string pAfterEditCellFunction)
        {
            this.iAfterEditCellFunction = pAfterEditCellFunction;
            return this;
        }

        /// <summary>
        /// Sets the onEdit function for configuring the editable row.
        /// This function gets passed the row ID as parameter.
        /// </summary>
        /// <param name="pOnEditFunction">The onEdit javascript function.</param>
        /// <returns></returns>
        public GridControl SetOnEditFunction(string pOnEditFunction)
        {
            this.iOnEditFunction = pOnEditFunction;
            return this;
        }

        /// <summary>
        /// Sets the onCreate function for configuring a newly the created row, e.g. to set default row data.
        /// This function gets passed the row ID as parameter. The function is called before the new row is put in edit mode.
        /// </summary>
        /// <param name="pOnCreateFunction">The onCreate javascript function.</param>
        /// <returns></returns>
        public GridControl SetOnCreateFunction(string pOnCreateFunction)
        {
            this.iOnCreateFunction = pOnCreateFunction;
            return this;
        }

        /// <summary>
        /// Sets the onClose function that will be called after closing (saving) an editable row.
        /// This function gets passed the row ID as parameter.
        /// </summary>
        /// <param name="pOnCloseFunction">The onClose javascript function.</param>
        /// <returns></returns>
        public GridControl SetOnCloseFunction(string pOnCloseFunction)
        {
            this.iOnCloseFunction = pOnCloseFunction;
            return this;
        }

        /// <summary>
        /// Sets the BeforeDelete function that will be called before deleting (removing) a row.
        /// This function gets passed the row ID as parameter and must return a boolean.
        /// If the function returns false the deleting of the row is canceled.
        /// </summary>
        /// <param name="pBeforeDeleteFunction">The BeforeDelete javascript function.</param>
        /// <returns></returns>
        public GridControl SetBeforeDeleteFunction(string pBeforeDeleteFunction)
        {
            this.iBeforeDeleteFunction = pBeforeDeleteFunction;
            return this;
        }

        /// <summary>
        /// Sets the onDelete function that will be called after deleting (removing) a row.
        /// This function gets passed the row ID as parameter.
        /// </summary>
        /// <param name="pOnDeleteFunction">The onDelete javascript function.</param>
        /// <returns></returns>
        public GridControl SetOnDeleteFunction(string pOnDeleteFunction)
        {
            this.iOnDeleteFunction = pOnDeleteFunction;
            return this;
        }

        /// <summary>
        /// Add a column mapping
        /// </summary>
        /// <param name="column"></param>
        /// <returns></returns>
        public GridControl AddColumn(GridColumnModel column)
        {
            this.iColumns.Add(column);
            return this;
        }

        /// <summary>
        /// Adds a range of column mappings
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="initCols"></param>
        /// <returns></returns>
        public GridControl SetColumns<T>(Action<GridColumnModelList<T>> initCols) where T : class
        {
            var cols = new GridColumnModelList<T>();
            initCols(cols);
            this.iColumns.AddRange(cols.iItems);
            return this;
        }

        /// <summary>
        /// Creates a sub grid
        /// </summary>
        /// <param name="subGrid"></param>
        /// <returns></returns>
        public GridControl CreateSubGrid(GridControl subGrid)
        {
            this.iSubGrid = subGrid;
            return this;
        }


        /// <summary>
        /// Renderes the grid as RTL
        /// </summary>
        /// <returns></returns>
        public GridControl IsRTL()
        {
            this.iIsRTL = true;
            return this;
        }

        /// <summary>
        /// Add additional custom parameters to the Grid
        /// </summary>
        /// <param name="attributes"></param>
        /// <returns></returns>
        public GridControl SetAdditionalAttributes(string attributes)
        {
            this.iAttributes = attributes;
            return this;
        }

        /// <summary>
        /// Renders the data for the grid
        /// </summary>
        /// <returns></returns>
        public string RenderData()
        {
            var vDataBuilder = new StringBuilder();
  
            // Render grid modus
            GridModusEnum vGridModus = this.IsInLookupMode
                ? GridModusEnum.Select
                : this.IsEnabled
                    ? this.iShowDeleteButton
                        ? GridModusEnum.EditAndDelete
                        : GridModusEnum.EditOnly
                    : GridModusEnum.View; 
            vDataBuilder.Append("$.WGK.gridModus['");
            vDataBuilder.Append(this.iName);
            vDataBuilder.Append("'] = ");
            vDataBuilder.Append((int)vGridModus);
            vDataBuilder.AppendLine(";");

            if (this.iListUrl == null)
            {
                // When displaying local data we need to render the datasource variable
                vDataBuilder.Append("$.WGK.gridDataSource['");
                vDataBuilder.Append(this.iName);
                vDataBuilder.Append("'] = ");
                // Render an empty array obejct when this.iGridDataSource is null
                vDataBuilder.Append(this.iGridDataSource != null
                    ? "h" //Newtonsoft.Json.JsonConvert.SerializeObject.Serialize(this.iGridDataSource)
                    : "[]");
                vDataBuilder.AppendLine(";");
            }

            if (this.IsInLookupMode)
            {
                // If grid is in select mode we need to render an empty SelectedIDs array
                vDataBuilder.AppendLine("$.WGK.gridSelectedIDs['" + this.iName + "'] = [];");
            }

            if (this.IsEnabled)
            {
                // If grid is editable we need to render LastEditID variable
                vDataBuilder.AppendLine("$.WGK.gridLastEditID['" + this.iName + "'] = null;");

                // If grid is editable render OnEdit, OnClose, BeforeDelete and OnDelete callback functions
                if (!string.IsNullOrEmpty(this.iOnEditFunction))
                {
                    vDataBuilder.AppendLine("$.WGK.gridOnEditFunc['" + this.iName + "'] = " + this.iOnEditFunction + ";");
                }

                if (!string.IsNullOrEmpty(this.iOnCloseFunction))
                {
                    vDataBuilder.AppendLine("$.WGK.gridOnCloseFunc['" + this.iName + "'] = " + this.iOnCloseFunction + ";");
                }

                if (!string.IsNullOrEmpty(this.iBeforeDeleteFunction))
                {
                    vDataBuilder.AppendLine("$.WGK.gridBeforeDeleteFunc['" + this.iName + "'] = " + this.iBeforeDeleteFunction + ";");
                }

                if (!string.IsNullOrEmpty(this.iOnDeleteFunction))
                {
                    vDataBuilder.AppendLine("$.WGK.gridOnDeleteFunc['" + this.iName + "'] = " + this.iOnDeleteFunction + ";");
                }

                if (!string.IsNullOrEmpty(this.iOpenSearchFunction))
                {
                    vDataBuilder.AppendLine("$.WGK.gridOpenSearchFunc['" + this.iName + "'] = " + this.iOpenSearchFunction + ";");
                }

                if (!string.IsNullOrEmpty(this.iOpenCustomFormFunction))
                {
                    vDataBuilder.AppendLine("$.WGK.gridOpenCustomFormFunc['" + this.iName + "'] = " + this.iOpenCustomFormFunction + ";");
                }

                if (string.IsNullOrEmpty(this.iEditUrl))
                {
                    JsonSerializer x = new JsonSerializer();
                    
                  //  string s = x.Serialize(this.iGridDeletedIDs);
                    // When editing data locally we need to render DeletedIDS, ErrorIDs and LastAddID variables
                    if (this.iGridDeletedIDs != null)
                    {
                       // vDataBuilder.AppendLine("$.WGK.gridDelIDs['" + this.iName + "'] = " + JsonConvert.SerializeObject.Serialize(this.iGridDeletedIDs) + ";");
                    }
                    if (this.iGridErrorIDs != null)
                    {
                      //  vDataBuilder.AppendLine("$.WGK.gridErrorIDs['" + this.iName + "'] = " + JsonConvert.SerializeObject.Serialize(this.iGridErrorIDs) + ";");
                    }
                    if (this.iGridLastAddID != null)
                    {
                     //   vDataBuilder.AppendLine("$.WGK.gridLastAddID['" + this.iName + "'] = " + JsonConvert.SerializeObject.Serialize(this.iGridLastAddID) + ";");
                    }
                }
            }

            if (!string.IsNullOrEmpty(this.iOpenDetailUrl))
            {
                vDataBuilder.AppendLine("$.WGK.gridOpenDetailUrl['" + this.iName + "'] = \"" + this.iOpenDetailUrl + "\";");
            }

            if (!string.IsNullOrEmpty(this.iOpenDetailColumnName))
            {
                vDataBuilder.AppendLine("$.WGK.gridOpenDetailColumnID['" + this.iName + "'] = \"" + this.iOpenDetailColumnName + "\";");
            }

            if (this.iOpenDetailAsLink)
            {
                vDataBuilder.AppendLine("$.WGK.gridOpenDetailAsLink['" + this.iName + "'] = " + (this.iOpenDetailAsLink ? "true" : "false") + ";");
            }

            if (this.iSubGrid != null)
            {
                vDataBuilder.Append(this.iSubGrid.RenderData());
            }

            // DELETED - Don't render <script> tag
            //if ((this.IsSubGrid) || (vDataBuilder.Length == 0))
            //{
            //    return vDataBuilder.ToString();
            //}
            //else
            //{
            //    return string.Format(ScriptManager.cScriptTemplate, vDataBuilder.ToString());
            //}
            return vDataBuilder.ToString();
        }

        public string Render()
        {
            // Grid works with local data (no ajax calls to fetch paged data) if iListUrl is NOT set
            bool vReadLocalData = (this.iListUrl == null);
            // Grid edits data locally (no ajax calls to edit data) if iEditUrl is not set
            bool vEditDataLocally = string.IsNullOrEmpty(this.iEditUrl);

            // Get width of ActionButton column (first column)
            int vActionButtonColumnWidth = 0;
            if (this.iActionButtonColumnWidth != null)
            {
                // Override calculated value
                vActionButtonColumnWidth = this.iActionButtonColumnWidth.Value;
            }
            else
            {
                // Calculate width of ActionButtonColumn
                if (!string.IsNullOrEmpty(this.iOpenDetailUrl))
                {
                    // Add width for open detail button
                    vActionButtonColumnWidth += cActionButtonWidth;
                }
                if (this.IsEnabled)
                {
                    if (this.iShowDeleteButton)
                    {
                        // Add width for delete button
                        vActionButtonColumnWidth += cActionButtonWidth;                    
                    }

                    if (!string.IsNullOrEmpty(this.iOpenSearchFunction))
                    {
                        // Add width for open select button
                        vActionButtonColumnWidth += cActionButtonWidth;
                    }

                    if (!string.IsNullOrEmpty(this.iOpenCustomFormFunction))
                    {
                        // Add width for open custom form button
                        vActionButtonColumnWidth += cActionButtonWidth;
                    }
                }                
            }

            this.iActionButtonColumn.SetWidth(vActionButtonColumnWidth.ToString());
            if (vActionButtonColumnWidth == 0)
            {
                // No action buttons to display => hide column
                this.iActionButtonColumn.SetHidden(true);
            }

            // Add cancel button column as last visible column only if grid is in edit mode
            if (this.IsEnabled)
            {
                this.iColumns.Add(new GridColumnModel(GridControl.CancelColumnModelName, string.Empty, cActionButtonWidth.ToString())
                    .SetSortable(false)
                    .SetEditable(false));
            }

            var vStringBuilder = new StringBuilder();

            if (!this.IsSubGrid)
            {
                vStringBuilder.AppendLine("$(\"#" + this.iName + "\").jqGrid({");
                vStringBuilder.AppendFormat("url: \"{0}\",\r\n", this.iListUrl);
            }
            else
            {
                vStringBuilder.AppendLine("jQuery(\"#\" + subgrid_table_id).jqGrid({");
                vStringBuilder.AppendFormat("url: \"{0},\r\n", this.iListUrl);
            }

            // Edit data locally (without making ajax calls) if editurl is not specified
            vStringBuilder.AppendFormat("editurl: \"{0}\",\r\n", !string.IsNullOrEmpty(this.iEditUrl)
                ? this.iEditUrl
                : "clientArray");

            // Display local data if iListUrl is NOT specified
            if (this.iListUrl == null)
            {
                vStringBuilder.AppendFormat("data: $.WGK.gridDataSource['" + this.iName + "'],\r\n");
                vStringBuilder.AppendFormat("datatype: \"local\",\r\n");
            }
            else // Get json data though ajax call
            {
                // Http Get or Post
               // vStringBuilder.AppendFormat("mtype: \"" + this.iHttpVerb.ToString().ToLower() + "\",\r\n");
                // WORKAROUND: set datatype initially to local so grid doesn't make an ajax call until the document is ready
                // This requires javascript code in the document ready handler to set the datatype to json
                //vStringBuilder.AppendFormat("datatype: \"json\",\r\n");
                vStringBuilder.AppendFormat("datatype: \"local\",\r\n");
            }

            // Render columns
            vStringBuilder.AppendFormat("colNames: [{0}],\r\n", this.RenderColumnNames());
            vStringBuilder.AppendFormat("colModel: [{0}],\r\n", this.RenderColumnsModel());

            // Disable paging when displaying local data -OR- when forced by DisablePaging setting
            if (vReadLocalData || this.iDisablePaging)
            {
                vStringBuilder.AppendLine("pgbuttons: false,");
                vStringBuilder.AppendLine("pginput: false,");
                vStringBuilder.AppendLine("rowList: -1,"); // clear the default rowList sizes
                // When displaying local data, set large rownum value (1000) for displaying all rows since -1 doesn't work
                vStringBuilder.AppendFormat("rowNum: {0},\r\n", 1000);
            }
            else
            {
                // When displaying ajax data (e.g. search results), override default rownum value if PageSize has been set
                if (this.iPageSize != 0)
                {
                    vStringBuilder.AppendFormat("rowNum: {0},\r\n", this.iPageSize);                                  
                }
            }

            // DELETED - Custom pager setup code
            //if (!this.isSubGrid)
            //{
            //    sb.AppendLine("gridComplete: function (){grid_initPager('" + this._name + "');},");
            //}

            if (this.iIsRTL)
            {
                vStringBuilder.AppendLine("direction: \"rtl\",");
            }

            if (!string.IsNullOrEmpty(this.iHeight))
            {
                vStringBuilder.AppendFormat("height: {0},\r\n", this.iHeight);
            }

            if (!string.IsNullOrEmpty(this.iWidth))
            {
                vStringBuilder.AppendFormat("width: {0},\r\n", this.iWidth);
            }

            // Don't render pager footer row if ShowPager has been set to false.
            if (this.iShowPager && !this.IsSubGrid)
            {
                vStringBuilder.AppendFormat("pager: $(\"#{0}\"),\r\n", this.GetPagerID());
            }

            // Sort by default on first column
            vStringBuilder.AppendFormat("sortname: \"{0}\",\r\n", !string.IsNullOrEmpty(this.iSortName)
                ? this.iSortName
                : this.iColumns[0].Name);

            // Sort order is ascending by default
            if (!string.IsNullOrEmpty(this.iSortOrder))
            {
                vStringBuilder.AppendFormat("sortorder: \"{0}\",\r\n", this.iSortOrder);
            }

            // Override default autowidth only if the SetIsAutoSize() method has been called
            if (this.iIsAutoSize.HasValue)
            {
                vStringBuilder.AppendFormat("autowidth: {0},\r\n", this.iIsAutoSize.Value.ToString().ToLower());
            }

            // Event handlers
            if (!this.IsSubGrid)
            {
                // -- Add row buttons on loadComplete
                // loadComplete event is executed immediately after every server request
                // Render call to $.WGK.gridLoadComplete(pGridID, pAllowEditing, pCallback)
                vStringBuilder.Append("loadComplete: function (){$.WGK.gridLoadComplete('");

                // pGridID parameter
                vStringBuilder.Append(this.iName);
                vStringBuilder.Append("'");

                // optional pCallback parameter
                if (!string.IsNullOrEmpty(this.iLoadCompleteFunction))
                {
                    vStringBuilder.Append(", ");
                    // pass as function object that can be called directly from javascript
                    vStringBuilder.Append(this.iLoadCompleteFunction);
                }
                vStringBuilder.AppendLine(");},");

                // -- Optional OnErrorFunction
                if (!string.IsNullOrEmpty(this.iLoadErrorFunction))
                {
                    vStringBuilder.Append("loadError: ");
                    vStringBuilder.Append(this.iLoadErrorFunction);
                    vStringBuilder.AppendLine(",");
                }

                // -- Optional OnSelectRowFunction
                if (!string.IsNullOrEmpty(this.iOnSelectedRow))
                {
                    vStringBuilder.Append("onSelectRow: ");
                    vStringBuilder.Append(this.iOnSelectedRow);
                    vStringBuilder.AppendLine(",");
                }

                // -- Optional afterInsertRowFunction
                if (!string.IsNullOrEmpty(this.iAfterInsertRowFunction))
                {
                    vStringBuilder.Append("afterInsertRow: ");
                    vStringBuilder.Append(this.iAfterInsertRowFunction);
                    vStringBuilder.AppendLine(",");
                }

                // -- Optional afterEditCellFunction
                if (!string.IsNullOrEmpty(iAfterEditCellFunction))
                {
                    vStringBuilder.Append("beforeSaveCell: ");
                    vStringBuilder.Append(this.iAfterEditCellFunction);
                    vStringBuilder.AppendLine(",");
                }
            }

            // Open detail page on double click row (only if grid is NOT in edit mode AND NOT in select mode)
            if (!string.IsNullOrEmpty(this.iOpenDetailColumnName)
                && !string.IsNullOrEmpty(this.iOpenDetailUrl)
                && !this.IsEnabled
                && !this.IsInLookupMode)
            {
                vStringBuilder.Append("ondblClickRow: function (id){$.WGK.gridOpenDetail('");
                vStringBuilder.Append(this.iName);
                vStringBuilder.AppendLine("', id);},");
            }

            // Set row in edit mode on select row event (only if grid is editable)
            if (this.IsEnabled)
            {
                //vStringBuilder.AppendLine("onSelectRow: " + this.iName + "EditRow,");

                vStringBuilder.Append("onSelectRow: function (id){$.WGK.gridEditRow('");
                vStringBuilder.Append(this.iName);
                vStringBuilder.Append("', ");
                vStringBuilder.Append("id");
                vStringBuilder.AppendLine(");},");
            }

            // DELETED
            //if (!string.IsNullOrEmpty(this.iOnSelectedRow))
            //{
            //    sb.AppendLine("onSelectRow: function(id){" + this.iOnSelectedRow + "(" +
            //        "$(\"#" + this.iName + "\").getRowData(id));},");
            //}
            //else
            //{
            //    sb.Append("onSelectRow: function(id){");
            //    sb.Append("if ($(\"#Id\").length == 0) {return;}");
            //    sb.Append("$(\"#Id\")[0].value = " + "$(\"#" + this.iName + "\").getRowData(id)." +
            //        this.getKeyColumnName() + ";},");
            //}

            foreach (GridColumnModel vColumn in this.iColumns)
            {
                if (vColumn.AsGroup)
                {
                    vStringBuilder.Append("grouping:true, groupingView:{groupField:['" + vColumn.Name + "']");

                    if (!string.IsNullOrEmpty(this.iGroupingFormat))
                    {
                        vStringBuilder.Append(", " + this.iGroupingFormat);
                    }

                    vStringBuilder.Append("},");

                    break;
                }
            }

            vStringBuilder.AppendFormat("caption: \"{0}\"", this.iCaption);

            if (!string.IsNullOrEmpty(this.iAttributes))
            {
                vStringBuilder.AppendLine(this.iAttributes);
            }

            if (this.iSubGrid != null)
            {
                vStringBuilder.AppendLine(",");
                vStringBuilder.AppendLine("subGrid: true,");
                vStringBuilder.AppendLine("subGridRowExpanded: function(subgrid_id, row_id) {");
                vStringBuilder.AppendLine("var subgrid_table_id;");
                vStringBuilder.AppendLine("subgrid_table_id = subgrid_id+\"_t\";");
                vStringBuilder.AppendLine(
                    "$(\"#\"+subgrid_id).html(\"<table id='\"+subgrid_table_id+\"' class='scroll'></table>\");");

                this.iSubGrid.IsSubGrid = true;
                this.iSubGrid.iListUrl += "\" + $(\"#" + this.iName + "\").getRowData(row_id)." +
                    this.GetKeyColumnName();
                this.iSubGrid.iHeight = "\"100%\"";
                vStringBuilder.AppendFormat("{0}{1}\r\n", this.iSubGrid.Render(), "}");
            }

            vStringBuilder.AppendLine("})"); // jqGrid end. NOTE: no ';' ending character!

            // Render calls to setLabel() function in order to add tooltips to column headers
            vStringBuilder.Append(this.RenderHeaderTooltips());

            return vStringBuilder.ToString();
        }

        public string GetPagerID()
        {
            return this.iName + "Pager";
        }

        public string GetKeyColumnName()
        {
            foreach (GridColumnModel vColumn in this.iColumns)
            {
                if (vColumn.IsPrimaryKey)
                {
                    return vColumn.Name;
                }
            }
            throw new Exception("Grid Renderer Failed: Please choose a column as a primary key");
        }
        #endregion

        #region Private Methods

        private string RenderColumnNames()
        {
            var vStringBuilder = new StringBuilder();

            for (int i = 0; i < this.iColumns.Count; i++)
            {
                vStringBuilder.Append("\"");
                vStringBuilder.Append(this.iColumns[i].GetColumnCaption());
                vStringBuilder.Append("\"");
                if (i < this.iColumns.Count - 1)
                {
                    vStringBuilder.Append(",");
                }
            }
            return vStringBuilder.ToString();
        }

        private string RenderColumnsModel()
        {
            var vStringBuilder = new StringBuilder();

            for (int i = 0; i < this.iColumns.Count; i++)
            {
                vStringBuilder.Append(this.iColumns[i].Render());
                if (i < this.iColumns.Count - 1)
                {
                    vStringBuilder.AppendLine(", ");
                }
            }
            return vStringBuilder.ToString();
        }

        private string RenderHeaderTooltips()
        {
            var vStringBuilder = new StringBuilder();

            for (int i = 0; i < this.iColumns.Count; i++)
            {
                string vHeaderTooltip = this.iColumns[i].GetHeaderTooltip();
                if (!string.IsNullOrEmpty(vHeaderTooltip))
                {
                    vStringBuilder.AppendLine(".setLabel ('" + this.iColumns[i].Name + "','','',{'title':'" + vHeaderTooltip + "'})");
                }
            }
            return vStringBuilder.ToString();
        }
        #endregion
    }
}
