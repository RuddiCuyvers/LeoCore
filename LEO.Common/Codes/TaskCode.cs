using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using WGK.Lib.Security;

namespace LEO.Common.Codes
{
    /// <summary>
    /// SecurityPolicy Task codes
    /// </summary>
    /// <remarks>
    /// This class extends tasks defined in the BaseTaskCode class
    /// </remarks>
    public static class TaskCode
    {
        #region BaseTaskCode properties
        // Since static classes cannot derive from another static class we must redefine all task properties
        // from the BaseTaskCode class here.

        /// <summary>
        /// Gets the Administration Task Code.
        /// </summary>
        public static string Administration { get { return BaseTaskCode.cAdministrationCode; } }

        /// <summary>
        /// Gets the Demo Task Code.
        /// </summary>
        public static string Demo { get { return BaseTaskCode.cDemoCode; } }

        /// <summary>
        /// Gets the Attachment Task Code.
        /// </summary>
        public static string Attachment { get { return BaseTaskCode.cAttachmentCode; } }

        /// <summary>
        /// Gets the SharePoint Task Code.
        /// </summary>
        public static string SharePoint { get { return BaseTaskCode.cSharePointCode; } }

        /// <summary>
        /// Gets the Sjabloon Task Code.
        /// </summary>
        public static string Sjabloon { get { return BaseTaskCode.cSjabloonCode; } }

        /// <summary>
        /// Gets the User Task Code.
        /// </summary>
        public static string User { get { return BaseTaskCode.cUserCode; } }

        /// <summary>
        /// Gets the Werknemer Task Code.
        /// </summary>
        public static string Werknemer { get { return BaseTaskCode.cWerknemerCode; } }
        #endregion

        /// <summary>
        /// Gets the Template Task Code.
        /// </summary>
        public static string Template { get { return cTemplateCode; } }
        public const string cTemplateCode = "TEPL";

        /// <summary>
        /// Gets the TemplateItem Task Code.
        /// </summary>
        public static string TemplateItem { get { return cTemplateItemCode; } }
        public const string cTemplateItemCode = "TEIM";

        /// <summary>
        /// Gets the Persoon Task Code.
        /// </summary>
        public static string Persoon { get { return cPersoonCode; } }
        public const string cPersoonCode = "DOWS_PERS";

        /// <summary>
        /// Gets the DossierAlle Task Code.
        /// </summary>
        public static string DossierAlle { get { return cDossierAlleCode; } }
        public const string cDossierAlleCode = "DOWS_DOSR_ALL";

        /// <summary>
        /// Gets the DossierProvincie Task Code.
        /// </summary>
        public static string DossierProvincie { get { return cDossierProvincieCode; } }
        public const string cDossierProvincieCode = "DOWS_DOSR_PROV";
    }
}
