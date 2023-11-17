using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WGK.Lib.UseCases
{
    /// <summary>
    /// Criteria for filling autocompleted lists
    /// </summary>
    public class AutoCompleteCriteria
    {
        public string StartsWith { get; set; }
        public int? MaxRows { get; set; }
        public bool IncludeSoftDeleted { get; set; }
    }
}
