
using System;
using System.ComponentModel;
using LEO.Common.Constants.Trainings;
using LEO.Business.Dtos.Trainings;


namespace LEO.Business.Dtos.Trainings
{
    public class TrainingSearchCriteria 
    {
        #region Constants - Field names
        private const string cStartScreenModusFieldName = "StartScreenModus";
        public static string StartScreenModusFieldName { get { return cStartScreenModusFieldName; } }

        private const string cTrainingIDFieldName = "TrainingID";
        public static string TrainingIDFieldName { get { return cTrainingIDFieldName; } }

       

        private const string cTrainingTypeIDFieldName = "TRAINING_TYPE";
        public static string TrainingTypeIDFieldName { get { return cTrainingTypeIDFieldName; } }

        private const string cTrainingConventieFieldName = "CONVENTIE_YN";
        public static string TrainingConventieFieldName { get { return cTrainingTypeIDFieldName; } }

        private const string cTrainingNomenclatuurFieldName = "NOMENCLATUUR_YN";
        public static string TrainingNomenclatuurFieldName { get { return cTrainingNomenclatuurFieldName; } }


        private const string cTrainingDuration_FromFieldName = "Duration_From";
        public static string TrainingDuration_FromFieldName { get { return cTrainingDuration_FromFieldName; } }

        private const string cTrainingDuration_UntilFieldName = "Duration_Until";
        public static string TrainingDuration_UntilFieldName { get { return cTrainingDuration_UntilFieldName; } }

        private const string cTrainingEVIDENCE_BASEDFieldName = "EVIDENCE_BASED";
        public static string TrainingEVIDENCE_BASEDFieldName { get { return cTrainingEVIDENCE_BASEDFieldName; } }

        private const string cTrainingSUBJECTFieldName = "SUBJECT";
        public static string TrainingSUBJECTFieldName { get { return cTrainingSUBJECTFieldName; } }

        private const string cTrainingINTEXTFieldName = "INTERNEXTERN";
        public static string TrainingINTEXTFieldName { get { return cTrainingINTEXTFieldName; } }

        private const string cTrainingAPPLICANT_CLIENT_IDFieldName = "APPLICANT_CLIENT_ID";
        public static string TrainingAPPLICANT_CLIENT_IDFieldName { get { return cTrainingAPPLICANT_CLIENT_IDFieldName; } }
        

        #endregion

        #region Properties

        #region SearchCriteria - Algemeen



        #endregion

        #region SearchCriteria 
        /// <summary>
        /// Set to true to perform a Training search for the application startscreen.
        /// On the startscreen only those Training are shown where the current user is Trainingbeheerder
        /// AND where there is a deadline on the current TrainingStatus (= last TrainingStap).
        /// </summary>
        public bool StartScreenModus { get; set; }

        [DisplayName(TRAININGDisplayNames.cIDDisplayName)]
        public decimal? TrainingID { get; set; }

        [DisplayName(TRAININGDisplayNames.cTRAINING_TYPEDisplayName)]
        public string TRAINING_TYPE { get; set; }

        [DisplayName(TRAININGDisplayNames.cNOMENCL_CONV_YNDisplayName)]
        public string NOMENCLATUUR_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cTrainingDuration_FromFieldName)]
        public string Duration_From { get; set; }

        [DisplayName(TRAININGDisplayNames.cTrainingDuration_UntilFieldName)]
        public string Duration_Until { get; set; }

        [DisplayName(TRAININGDisplayNames.cEVIDENCE_BASEDFieldName)]
        public string EVIDENCE_BASED { get; set; }

        [DisplayName(TRAININGDisplayNames.cSUBJECTFieldName)]
        public string SUBJECT { get; set; }

        [DisplayName(TRAININGDisplayNames.cTRAINER_INT_EXTDisplayName)]
        public string INTERNEXTERN { get; set; }

        [DisplayName(TRAININGDisplayNames.cAPPLICANT_CLIENTIDDisplayName)]
        public string APPLICANT_CLIENT_ID { get; set; }
        


        #endregion

        #endregion
    }
}