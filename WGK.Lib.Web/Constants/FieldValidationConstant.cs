using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Web.Constants
{
    /// <summary>
    /// Static class containing field validation constants.
    /// </summary>
    public static class FieldValidationConstant
    {
        /// <summary>
        /// Marker for required fields
        /// </summary>
        public static string RequiredFieldMarker { get { return cRequiredFieldMarker; } }
        public const string cRequiredFieldMarker = " *";
    }
}
