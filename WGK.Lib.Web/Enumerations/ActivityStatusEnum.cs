using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Web.Enumerations
{
    /// <summary>
    /// Enumeration denoting the status (modus) of the View.
    /// A View can only be in one status at a time.
    /// </summary>
    public enum ActivityStatusEnum
    {
        /// <summary>
        /// Modus not specified
        /// </summary>
        None = 0,

        /// <summary>
        /// Select entity primary key (maintenance) or select entity search criteria (identification) modus 
        /// </summary>
        Select = 1,

        /// <summary>
        /// Create new entity modus (maintenance)
        /// </summary>
        Insert = 2,

        /// <summary>
        /// 
        /// </summary>
        View = 3, // View entity in read-only modus (maintenance)

        /// <summary>
        /// 
        /// </summary>
        Edit = 4 // Update existing entity modus (maintenance)

        // DELETED - We probably don't need a search results modus
        ///// <summary>
        ///// Show entities search results modus (identification)
        ///// </summary>
        //Search = 5
    }
}
