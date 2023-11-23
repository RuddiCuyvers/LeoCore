
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
using static System.Net.Mime.MediaTypeNames;

namespace LeoCore.Models.Questionnaires
{
    public class QuestionnaireMaintenanceViewModel 
    {

        #region Constants - Field names
        private const string cQuestionnaireDetailFieldName = "QuestionnaireDetail";
        public static string QuestionnaireDetailFieldName { get { return cQuestionnaireDetailFieldName; } }
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
        }
        #endregion

        #region Base class implementation
        /// <summary>
        /// Gets the QuestionnaireDetail instance cast as the base DossierDetail business-layer model class.
        /// </summary>
        public  LeoCore.Data.Models.QUESTIONNAIRE  QuestionnaireDetail { get; set; }

        public string UniqueID = Guid.NewGuid().ToString();

        #endregion

        #region Public properties


        #endregion
    }
}