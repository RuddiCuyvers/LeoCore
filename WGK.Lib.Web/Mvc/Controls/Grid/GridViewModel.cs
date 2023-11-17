using System.Collections.Generic;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Model containing data for rendering a grid.
    /// Add a property of this type to your View's Model for displaying a grid.
    /// </summary>
    /// <typeparam name="TEntity">The type of the entities displayed as rows in the grid</typeparam>
    public class GridViewModel<TEntity> where TEntity : IGridRowModel
    {
        #region Constructors
        public GridViewModel()
        {
            this.DelIDs = new List<long>();
            this.ErrorIDs = new List<long>();
            this.LastAddID = 0;
        }
        #endregion

        #region Properties
        /// <summary>
        /// Gets or sets the grid data for rendering to browser as JSON string.
        /// </summary>
        /// <value>The grid data.</value>
        public IEnumerable<TEntity> Data { get; set; }

        /// <summary>
        /// Gets or sets the grid deleted row IDs for rendering to browser as JSON string.
        /// </summary>
        public List<long> DelIDs { get; set; }

        /// <summary>
        /// Gets or sets the grid error row IDs for rendering to browser as JSON string.
        /// </summary>
        public List<long> ErrorIDs { get; set; }

        /// <summary>
        /// Gets or sets the last added row ID for rendering to browser.
        /// Added IDs are temporary values that will be overwritten by identity values in the database.
        /// We generate negative values starting with -1.
        /// </summary>
        public long LastAddID { get; set; }
        #endregion

    }

    /// <summary>
    /// Static class containing constants
    /// </summary>
    public static class GridViewModel
    {
        #region Field names constants
        // Field names
        public static string DataFieldName
        {
            get { return cDataFieldName; }
        }
        public const string cDataFieldName = "Data";

        public static string DelIDsFieldName
        {
            get { return cDelIDsFieldName; }
        }
        public const string cDelIDsFieldName = "DelIDs";

        public static string ErrorIDsFieldName
        {
            get { return cErrorIDsFieldName; }
        }
        public const string cErrorIDsFieldName = "ErrorIDs";

        public static string LastAddIDFieldName
        {
            get { return cLastAddIDFieldName; }
        }
        public const string cLastAddIDFieldName = "LastAddID";
        #endregion
    }
}