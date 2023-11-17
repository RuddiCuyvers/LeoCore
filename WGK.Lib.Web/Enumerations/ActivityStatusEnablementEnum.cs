using System;

namespace WGK.Lib.Web.Enumerations
{
    /// <summary>
    /// Activity Status Enablement enumeration. Widgets dictate which status they 'at least' require in order to
    /// be enabled. 'at least' signifies that e.g. an 'EditInsert' field will be enabled if the View ActivityStatus 
    /// is 'Edit' or 'Insert'.
    /// </summary>
    [Flags]
    public enum ActivityStatusEnablementEnum
    {
        /// <summary>
        /// The widget is not enabled in any ActivitySatus, i.e. widget is always disabled.
        /// ReadOnly is the default 'zero' value (secure by default). It cannot be combined with other flags.
        /// </summary>
        ReadOnly = 0,

        /// <summary>
        /// Enable widget when page is in ActivityStatus 'Select' only
        /// </summary>
        SelectOnly = 1,

        /// <summary>
        /// Enable widget when page is in ActivityStatus 'Insert' only (i.e. not enabled in ActivityStatus 'Edit')
        /// </summary>
        InsertOnly = 2,

        /// <summary>
        /// Enable widget when page is in ActivityStatus 'Edit' only (not enabled in ActivityStatus 'Insert')
        /// </summary>
        EditOnly = 4,

        /// <summary>
        /// Enable widget when page is in ActivityStatus 'View' only
        /// </summary>
        ViewOnly = 8,

        /// <summary>
        /// Don't perform status enablement on the widget, i.e. the status of the widget is not modified.
        /// This flag cannot be combined with other flags.
        /// </summary>
        Ignore = 128,


        // Combined flags

        /// <summary>
        /// Enable widget when page is in ActivityStatus 'Select' or 'Insert'
        /// </summary>
        SelectInsert = 3, 

        /// <summary>
        /// SelectEdit: Enable widget when page is in ActivityStatus 'Select' or 'Edit' or 'Insert'
        /// </summary>
        SelectEditInsert = 7,

        /// <summary>
        /// EditInsertOnly: Enable widget when page is in ActivityStatus 'Edit' or 'Insert'
        /// </summary>
        EditInsert = 6,

        /// <summary>
        /// Enable widget when page is in ActivityStatus 'View' or 'Edit' (i.e. not enabled in ActivityStatus 'Select' or 'Insert')
        /// </summary>
        ViewEdit = 12,

        /// <summary>
        /// Enable widget when page is in ActivityStatus 'View' or 'Edit' or 'Insert' (i.e. not enabled in ActivityStatus 'Select')
        /// </summary>
        ViewEditInsert = 14,

        /// <summary>
        /// Enable widget in all possible ActivityStatuses, i.e. the  widget is always enabled
        /// </summary>
        Allways = 15,
    }
}