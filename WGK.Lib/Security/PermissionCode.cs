using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGK.Lib.Security
{
    /// <summary>
    /// SecurityPolicy Permission codes
    /// </summary>
    public static class PermissionCode
    {
        /// <summary>
        /// Gets the Create permission code.
        /// </summary>
        public static string Create
        {
            get { return cCreateCode; }
        }
        public const string cCreateCode = "C";

        /// <summary>
        /// Gets the Read permission code.
        /// </summary>
        public static string Read
        {
            get { return cReadCode; }
        }
        public const string cReadCode = "R";

        /// <summary>
        /// Gets the Update permission code.
        /// </summary>
        public static string Update
        {
            get { return cUpdateCode; }
        }
        public const string cUpdateCode = "U";

        /// <summary>
        /// Gets the Delete permission code.
        /// </summary>
        public static string Delete
        {
            get { return cDeleteCode; }
        }
        public const string cDeleteCode = "D";

        /// <summary>
        /// Gets the Search permission code.
        /// </summary>
        public static string Search
        {
            get { return cSearchCode; }
        }
        public const string cSearchCode = "S";

        /// <summary>
        /// Gets the Export permission code.
        /// </summary>
        public static string Export
        {
            get { return cExportCode; }
        }
        public const string cExportCode = "E";

        /// <summary>
        /// Gets the Import permission code.
        /// </summary>
        public static string Import
        {
            get { return cImportCode; }
        }
        public const string cImportCode = "I";

        /// <summary>
        /// Gets the Execute permission code.
        /// </summary>
        public static string Execute
        {
            get { return cExecuteCode; }
        }
        public const string cExecuteCode = "X";

        /// <summary>
        /// Gets the Print permission code.
        /// </summary>
        public static string Print
        {
            get { return cPrintCode; }
        }
        public const string cPrintCode = "P";
    }
}
