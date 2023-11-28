
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;

using LEO.Common.Codes;




using LeoCore.Data;
using LeoCore.Data.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json.Linq;
using WGK.Lib.Web.Enumerations;
using static System.Net.Mime.MediaTypeNames;

namespace LeoCore.Models.Questionnaires
{
    public class QuestionnaireMaintenanceViewModel 
    {

        #region Constants - Field names
        private const string cQuestionnaireDetailFieldName = "QuestionnaireDetail";
        public static string QuestionnaireDetailFieldName { get { return cQuestionnaireDetailFieldName; } }

        public string Title { get; set; }
        public string MainTitle { get; set; }
        public string ActivePanels { get; set; }
        public ActivityStatusEnum ActivityStatus { get; set; }
        public bool IsInLookupMode { get; set; }
        public string TaskID { get; set; }
        public string UniqueID { get; set; }
        public bool UsePrintStyle { get; set; }


        public string ActionType { get; set; }

        //**********
        public List<SelectListItem> cJaNeeLijst { get; set; }
        public List<SelectListItem> cScore5Lijst { get; set; }
        public List<SelectListItem> cScore10Lijst { get; set; }

        #endregion

        #region Constants - Accordion HtmlID prefix and suffixes

        private const string cAccordionPanelHtmlIDInhoud = "InhoudPanel";
        public static string AccordionPanelHtmlIDInhoud { get { return cAccordionPanelHtmlIDInhoud; } }

        private const string cAccordionPanelHtmlIDDee2 = "Deel2";
        public static string AccordionPanelHtmlIDDeel2 { get { return cAccordionPanelHtmlIDDee2; } }

        private const string cAccordionPanelHtmlIDDeel1 = "Evaluatie";
        public static string AccordionPanelHtmlIDDeel1 { get { return cAccordionPanelHtmlIDDeel1; } }

        private readonly IUserCodeRepository _usercoderepository;

        #endregion

        #region Constructors
        public QuestionnaireMaintenanceViewModel(IUserCodeRepository usercoderepository)
        {
            _usercoderepository = usercoderepository;

            //methodologielijstje
            cJaNeeLijst = _usercoderepository.GetLISTITEMSLISTUserCodesForUserCodeGroup(UserCodeGroupCode.cJaNeeLijst, false, false);
            cScore5Lijst = _usercoderepository.GetLISTITEMSLISTUserCodesForUserCodeGroup(UserCodeGroupCode.cScore5Lijst, false, false);
            cScore10Lijst = _usercoderepository.GetLISTITEMSLISTUserCodesForUserCodeGroup(UserCodeGroupCode.cScore10Lijst, false, false);
        }
        #endregion

        #region Base class implementation

        public  LeoCore.Data.Models.QUESTIONNAIRE  QuestionnaireDetail { get; set; }
        public LeoCore.Data.Models.PERSON_QUESTIONNAIRE Person_QuestionnaireDetail { get; set; }

        #endregion

        #region Public properties


        #endregion
    }
}