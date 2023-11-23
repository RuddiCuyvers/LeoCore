
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

            private readonly IUserCodeRepository _usercoderepository;

        #endregion


        #region Constructors
        public TrainingMaintenanceViewModel(IUserCodeRepository usercoderepository)
        {
            //IUserCodeRepository usercoderepository = ;
            _usercoderepository = usercoderepository;
            //methodologielijstje
            cMETHODOLOGYLijst = _usercoderepository.GetLISTITEMSLISTUserCodesForUserCodeGroup(UserCodeGroupCode.cMETHODOLOGYLijst, false, false);
            //var mlijst = _usercoderepository.GetUserCodesForUserCodeGroup(UserCodeGroupCode.cMETHODOLOGYLijst, false, false);
            //if (mlijst != null)
            //{
            //    cMETHODOLOGYLijst = new List<SelectListItem>() ; //zodat er niks blijft hangen van een vorige keer
            //    int i = 0;
            //    foreach (var m in mlijst)
            //    {
            //        cMETHODOLOGYLijst.Insert(i, (new SelectListItem { Text = m.DESCRIPTION, Value = m.USERCODEID}));
            //        i++;
            //    };
            //};
        }


        #endregion

        #region Base class implementation
        public LeoCore.Data.Models.TRAINING TRAININGDetail { get; set; }

        public List<SelectListItem> cMETHODOLOGYLijst { get; set; }

        public string Qrcode;

        public string UniqueID = Guid.NewGuid().ToString();



        #endregion

        #region Public properties


        #endregion
    }
    }

