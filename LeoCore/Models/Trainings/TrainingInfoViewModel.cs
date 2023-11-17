using System;
using System.ComponentModel;
using LEO.Common.Constants.Trainings;

namespace LeoCore.Models.Trainings
{
    public class TrainingInfoViewModel
    {
        #region Constants - Field names
        private const string cTrainingIDFieldName = "TrainingID";
        public static string TrainingIDFieldName { get { return cTrainingIDFieldName; } }


        #endregion

        #region Properties
        // -- Visible columns
        // Put DisplayName attribute on properties that are shown as columns in grid

        [DisplayName(TRAININGDisplayNames.cTRAINING_TYPEDisplayName)]
        public string TRAINING_TYPE { get; set; }



        [DisplayName(TRAININGDisplayNames.cNOMENCL_CONV_YNDisplayName)]
        public  string NOMENCLATUUR_YN { get; set; }

       
        public string ONDERWERP { get; set; }

       
        public string EVIDENCEBASED_YN { get; set; }

        public string INTERNEXTERN { get; set; }





        [DisplayName(TRAININGDisplayNames.cLINKDisplayName)]
        public string LINK { get; set; }


        // -- Hidden columns
        /// <summary>
        /// Primary key
        /// </summary>
        public decimal TrainingID { get; set; }

       
        #endregion
    }
}