﻿
using System;
using System.ComponentModel;
using LEO.Common.Constants.Trainings;

using LEO.Business.Dtos.IBF;
using LEO.Common.Constants.Persons;

namespace LEO.Business.Dtos.IBF
{
    public class IBFSearchCriteria 
    {
        #region Constants - Field names
        private const string cStartScreenModusFieldName = "StartScreenModus";
        public static string StartScreenModusFieldName { get { return cStartScreenModusFieldName; } }

        private const string cPersonTrainingIDFieldName = "Person_TrainingID";
        public static string PersonTrainingIDFieldName { get { return cPersonTrainingIDFieldName; } }

        private const string cPersonTrainingClientIDFieldName = "CLIENT_ID";
        public static string PersonTrainingClientIDFieldName { get { return cPersonTrainingClientIDFieldName; } }

        private const string cPersonTrainingJaartalFieldName = "JAARTAL";
        public static string PersonTrainingJaartalFieldName { get { return cPersonTrainingJaartalFieldName; } }

        private const string cPersonTrainingInternExternFieldName = "INTERNEXTERN";
        public static string PersonTrainingInternExternFieldName { get { return cPersonTrainingInternExternFieldName; } }
        
        #endregion

        #region Properties

        #region SearchCriteria - Algemeen



        #endregion

        #region SearchCriteria - Other
        /// <summary>
        /// Set to true to perform a Training search for the application startscreen.
        /// On the startscreen only those Training are shown where the current user is Trainingbeheerder
        /// AND where there is a deadline on the current TrainingStatus (= last TrainingStap).
        /// </summary>
        public bool StartScreenModus { get; set; }

        [DisplayName(PERSON_TRAININGDisplayNames.cIDDisplayName)]
        public decimal? Person_TrainingID { get; set; }

        [DisplayName(PERSON_TRAININGDisplayNames.cCLIENT_IDDisplayName)]
        public string CLIENT_ID { get; set; }

        [DisplayName(PERSON_TRAININGDisplayNames.cJAARTALDisplayName)]
        public string JAARTAL { get; set; }

        [DisplayName(PERSON_TRAININGDisplayNames.cINTERNEXTERNDisplayName)]
        public string INTERNEXTERN { get; set; }

        #endregion

        #endregion
    }
}