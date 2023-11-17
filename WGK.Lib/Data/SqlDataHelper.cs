using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Data
{
    /// <summary>
    /// Helper class for SQL Server database data
    /// </summary>
    public class SqlDataHelper
    {
        #region Public Properties
        /// <summary>
        /// Return Minimal datatime value used within SQL Server
        /// </summary>
        public static DateTime SqlMinimalDateTime
        {
            get
            {
                return new DateTime(1753, 1, 1, 0, 0, 0, 0);
            }
        }

        /// <remarks>
        /// Returns the last datetime value usable in SQL for business applications
        /// e.g. SQLMaximalDateTime = 9998/12/31 23:59:59.998
        /// milliseconds 998 and not 999 as SQL Server DB rounds 999 to the next second: 998 is rounded to 997
        /// </remarks>
        public static DateTime SqlMaximalDateTime
        {
            get
            {
                return new DateTime(9998, 12, 31, 23, 59, 59, 998);
            }
        }
        #endregion
    }
}
