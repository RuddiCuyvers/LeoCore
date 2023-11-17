using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.Web.Mvc.Controls.Grid
{
    /// <summary>
    /// Allows for a custom column editing types
    /// </summary>
    public interface IGridEditColumn
    {
        /// <summary>
        /// Renders EditType and EditOptions for a specific column type.
        /// </summary>
        /// <returns></returns>
        string Render();

        /// <summary>
        /// Gets or sets the ColumnModel this renderer applies to.
        /// </summary>
        /// <value>The column.</value>
        GridColumnModel Column { get; set; } 
    }
}
