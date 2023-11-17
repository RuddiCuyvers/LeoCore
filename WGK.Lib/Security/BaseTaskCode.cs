using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WGK.Lib.Security
{
    public static class BaseTaskCode
    {
        /// <summary>
        /// Gets the Administration Task Code.
        /// </summary>
        public static string Administration { get { return cAdministrationCode; } }
        public const string cAdministrationCode = "ADMN";

        /// <summary>
        /// Gets the Demo Task Code.
        /// </summary>
        public static string Demo { get { return cDemoCode; } }
        public const string cDemoCode = "DEMO";

        /// <summary>
        /// Gets the Attachment Task Code.
        /// </summary>
        public static string Attachment { get { return cAttachmentCode; } }
        public const string cAttachmentCode = "ATCH";

       

        /// <summary>
        /// Gets the Sjabloon Task Code.
        /// </summary>
        public static string Sjabloon { get { return cSjabloonCode; } }
        public const string cSjabloonCode = "SJBL";

        /// <summary>
        /// Gets the User Task Code.
        /// </summary>
        public static string User { get { return cUserCode; } }
        public const string cUserCode = "USER";

        /// <summary>
        /// Gets the Werknemer Task Code.
        /// </summary>
        public static string Werknemer { get { return cWerknemerCode; } }
        public const string cWerknemerCode = "WRKN";

    }
}
