
namespace WGK.Lib.UseCases
{
    /// <summary>
    /// Model class for paging, sorting and filtering criteria
    /// </summary>
    public class PagingSortingFilteringCriteria
    {
        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="PagingSortingFilteringCriteria"/> class.
        /// </summary>
        public PagingSortingFilteringCriteria()
        {
        }
        #endregion

        #region Properties
        // -- Input parameters

        /// <summary>
        /// The sort index (i.e. the sort column or a sort expression)
        /// </summary>
        public string Sidx { get; set; }

        /// <summary>
        /// Sort order, asc or desc
        /// </summary>
        public string Sord { get; set; }

        /// <summary>
        /// Index of the page to fetch (first page is 1).
        /// Set to zero in order to disable paging and fetch all data
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// Number of rows per page to fetch
        /// Set to zero in order to disable paging and fetch all data
        /// </summary>
        public int Rows { get; set; }

        // jqGrid filter parameters
        public bool Search { get; set; }
        public string SearchField { get; set; }
        public string SearchString { get; set; }
        public string SearchOper { get; set; }

        // -- Output parameters

        /// <summary>
        /// Number of rows over all pages
        /// </summary>
        public int RecordCount { get; set; }

        /// <summary>
        /// Total number of pages
        /// </summary>
        public int Total { get; set; }
        #endregion

    }
}
