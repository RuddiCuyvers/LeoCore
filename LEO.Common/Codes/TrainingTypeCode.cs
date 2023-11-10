using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LEO.Common.Codes
{
    public static class TrainingTypeCode
    {
        /// <summary>
        /// Gets the TrainingType code.
        /// </summary>
        public static string WebTraining { get { return cWebTraining; } }
        public const string cWebTraining = "WEB";

        public static string LiveTraining { get { return cLiveTraining; } }
        public const string cLiveTraining = "LIV";
    }
}
