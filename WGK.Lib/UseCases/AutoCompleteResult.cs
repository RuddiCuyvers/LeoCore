using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.UseCases
{
    /// <summary>
    /// Generic model for autocompleted lists (ID is generic type)
    /// </summary>
    /// <typeparam name="TID">Type if the ID field in AutoCompleteResult </typeparam>
    public class AutoCompleteResult<TID>
    {
        public TID ID { get; set; }
        public string Description { get; set; }
    }

    /// <summary>
    /// Default model for autocompleted lists (ID is of long type)
    /// </summary>
    public class AutoCompleteResult : AutoCompleteResult<long>
    {
    }

    /// <summary>
    /// Model for autocompleted lists where ID is of string type
    /// </summary>
    public class AutoCompleteCodeResult : AutoCompleteResult<string>
    {
    }
}
