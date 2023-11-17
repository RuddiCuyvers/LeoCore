
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;




using LeoCore.Data;


namespace LeoCore.Models.Trainings
{
    public class TrainingMaintenanceViewModel
    {
   

            #region Constants - Field names
            private const string cTRAININGDetailFieldName = "LesDetail";
            public static string TRAININGDetailFieldName { get { return cTRAININGDetailFieldName; } }

            #endregion

            #region Constants - Accordion HtmlID prefix and suffixes

            private const string cAccordionPanelHtmlIDInhoud = "InhoudPanel";
            public static string AccordionPanelHtmlIDInhoud { get { return cAccordionPanelHtmlIDInhoud; } }

            private const string cAccordionPanelHtmlIDOpmerkingen = "Opmerkingen";
            public static string AccordionPanelHtmlIDOpmerkingen { get { return cAccordionPanelHtmlIDOpmerkingen; } }

            private const string cAccordionPanelHtmlIDAlgemeen = "Algemeen";
            public static string AccordionPanelHtmlIDAlgemeen { get { return cAccordionPanelHtmlIDAlgemeen; } }

            #endregion
            #region Constructors
            public TrainingMaintenanceViewModel()
            {

            }
        #endregion

        #region Base class implementation
        public LeoCore.Data.Models.TRAINING TRAININGDetail { get; set; }

        public string Qrcode;

        public string UniqueID = Guid.NewGuid().ToString();



        #endregion

        #region Public properties


        #endregion
    }
    }

