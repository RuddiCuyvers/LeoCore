namespace WGK.Lib.Web.Constants
{
    /// <summary>
    /// Static class containing WGK CSS class names.
    /// These class names correspond with the styles defined in the site.css file of the ASP.NET project
    /// </summary>
    public static class CssClassName
    {
        /// <summary>
        /// Style used with WGKActionLink widget
        /// </summary>
        public static string ActionLink { get { return cActionLink; } }
        public const string cActionLink = "btn btn-secondary";

        /// <summary>
        /// Style used with WGKButton widget
        /// </summary>
        public static string Button { get { return cButton; } }
        public const string cButton = "btn btn-primary";

        /// <summary>
        /// Style used with jqgrid widget medium priority row
        /// </summary>
        public static string RowPriorityMedium { get { return cRowPriorityMedium; } }
        public const string cRowPriorityMedium = "wgk-jqgrid-row-priority-medium";

        /// <summary>
        /// Style used with jqgrid widget high priority row
        /// </summary>
        public static string RowPriorityHigh { get { return cRowPriorityHigh; } }
        public const string cRowPriorityHigh = "wgk-jqgrid-row-priority-high";

        /// <summary>
        /// Style used for non-printable widgets
        /// </summary>
        public static string PrintNone { get { return cPrintNone; } }
        public const string cPrintNone = "print-none";

        /// <summary>
        /// Style used for inserting page breaks when printing
        /// </summary>
        public static string PrintBreakBefore { get { return cPrintBreakBefore; } }
        public const string cPrintBreakBefore = "print-break-before";

        /// <summary>
        /// Style used for input fields with validation errors
        /// </summary>
        public static string InputValidationError { get { return cInputValidationError; } }
        public const string cInputValidationError = "input-validation-error";
    }
}