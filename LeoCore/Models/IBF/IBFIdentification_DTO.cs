using System;
using System.ComponentModel;
using LEO.Common.Constants.IBF;
using LEO.Common.Constants.Persons;

namespace LeoCore.Models.IBF
{
    public class IBFIdentification_DTO
    {
        #region Constants - Field names
        private const string cTrainingIDFieldName = "TrainingID";
        public static string TrainingIDFieldName { get { return cTrainingIDFieldName; } }

        private const string cPersonTrainingIDFieldName = "Person_TrainingID";
        public static string PersonTrainingIDFieldName { get { return cPersonTrainingIDFieldName; } }

        private const string cNOMENCLATUUR_YNFieldName = "NOMENCLATUUR_YN";
        public static string NOMENCLATUUR_YNFieldName { get { return cNOMENCLATUUR_YNFieldName; } }


        #endregion

        #region Properties
        // -- Visible columns
        // Put DisplayName attribute on properties that are shown as columns in grid
        [DisplayName(IBFDisplayNames.cSUBJECTDisplayName)]
        public string SUBJECT { get; set; }

        [DisplayName(IBFDisplayNames.cTRAINING_TYPEDisplayName)]
        public string TRAINING_TYPE { get; set; }

        [DisplayName(IBFDisplayNames.cNOMENCLATUUR_YNDisplayName)]
        public  string NOMENCLATUUR_YN { get; set; }

        [DisplayName(IBFDisplayNames.cMETHODOLOGYDisplayName)]
        public string METHODOLOGY { get; set; }


        [DisplayName(IBFDisplayNames.cDATUMTRAININGDisplayName)]
        public string DATUMTRAINING { get; set; }


        [DisplayName(IBFDisplayNames.cLINKDisplayName)]
        public string LINK { get; set; }


        // -- Hidden columns
        /// <summary>
        /// Primary key
        /// </summary>
        public int Person_TrainingID { get; set; }
        public decimal TrainingID { get; set; }

  


        #endregion
    }
}