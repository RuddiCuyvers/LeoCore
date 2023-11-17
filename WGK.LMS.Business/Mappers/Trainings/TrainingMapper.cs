using System;
using WGK.LMS.Business.Dtos.Trainings;
using LeodbModel;
using WGK.Lib.Extensions;
using WGK.Lib.Mappers;


namespace WGK.LMS.Business.Mappers.Trainings
{
    /// <summary>
    /// Helper class containing map methods for Dossier base table and child tables
    /// </summary>
    public class TrainingMapper :
        // Data layer to business layer
        IMapper<TRAINING, TRAININGDetail>,  
        IMapper<TRAINING_QUESTIONNNAIRE, WGK.LMS.Business.Dtos.Trainings.TRAINING_QUESTIONNNAIREDetail>
    {
        #region TRAINING entity

        /// <summary>
        /// Maps a Dossier instance (data layer) to a new TRAININGDetail instance (business layer)
        /// </summary>
        public TRAININGDetail Map(TRAINING pSource)
        {
            // Map null source type to null target type
            if (pSource == null)
            {
                return null;
            }

            var vTRAININGDetail = new TRAININGDetail
            {
                // Primitive properties                
                ID = pSource.ID,
                SUBJECT = pSource.SUBJECT,
                APPLICANT_CLIENTID = pSource.APPLICANT_CLIENTID,
                METHODOLOGY = pSource.METHODOLOGY,
                LINK = pSource.LINK,
                TRAINING_TYPE = pSource.TRAINING_TYPE,
                TRAINING_TYPE_FT = pSource.TRAINING_TYPE_FT,
                TRAINER_INT_EXT = pSource.TRAINER_INT_EXT,
                TRAINER_EMAIL = pSource.TRAINER_EMAIL,
                NOMENCL_CONV_YN = pSource.NOMENCL_CONV_YN,
                NC_EVD_YN = pSource.NC_EVD_YN == "J" ? true : false,
                NC_EVD_DURATION = TijdMinutenNaarStringconversie(pSource.NC_EVD_DURATION),
                NC_EVD_SUBJECT = pSource.NC_EVD_SUBJECT,
                NC_KATZ_YN = pSource.NC_KATZ_YN == "J"? true : false,
                NC_KATZ_DURATION = TijdMinutenNaarStringconversie(pSource.NC_KATZ_DURATION),
                NC_KATZ_SUBJECT = pSource.NC_KATZ_SUBJECT,
                NC_NOMC_YN = pSource.NC_NOMC_YN == "J" ? true : false,
                NC_NOMC_DURATION = TijdMinutenNaarStringconversie(pSource.NC_NOMC_DURATION),
                NC_NOMC_SUBJ = pSource.NC_NOMC_SUBJ,
                NC_THUISZORG_YN = pSource.NC_THUISZORG_YN == "J" ? true : false,
                NC_THUISZORG_DURATION = TijdMinutenNaarStringconversie(pSource.NC_THUISZORG_DURATION),
                NC_THUISZORG_SUBJECT = pSource.NC_THUISZORG_SUBJECT,
                NC_VVD_YN = pSource.NC_VVD_YN == "J" ? true : false,
                NC_VVD_DURATION = TijdMinutenNaarStringconversie(pSource.NC_VVD_DURATION),
                NC_VVD_SUBJECT = pSource.NC_VVD_SUBJECT,
                NC_ROL_YN = pSource.NC_ROL_YN == "J" ? true : false,
                NC_ROL_DURATION = TijdMinutenNaarStringconversie(pSource.NC_ROL_DURATION),
                NC_ROL_SUBJECT = pSource.NC_ROL_SUBJECT,
                EV_YN = pSource.EV_YN,
                EV_WW_YN = pSource.EV_WW_YN == "J" ? true : false,
                EV_WW_DURATION = TijdMinutenNaarStringconversie(pSource.EV_WW_DURATION),
                EV_ZG_SUBJ = pSource.EV_ZG_SUBJ,
                EV_ZG_REFDOM = pSource.EV_ZG_REFDOM,
                EV_ZG_COMPL = pSource.EV_ZG_COMPL,
                EV_AWS_YN = pSource.EV_AWS_YN,
                EV_AWS_SUBJ = pSource.EV_AWS_SUBJ,
                EV_AWS_ONCO_SUBJ = pSource.EV_AWS_ONCO_SUBJ,
                EV_AWS_INCO_SUBJ = pSource.EV_AWS_INCO_SUBJ,
                EV_SUBJECT = pSource.EV_SUBJECT,
                EV_ZG_DURATION = TijdMinutenNaarStringconversie(pSource.EV_ZG_DURATION),
                EV_AWS_DURATION = TijdMinutenNaarStringconversie(pSource.EV_AWS_DURATION),
                LOCATION = pSource.LOCATION,
                LOCATION_ANDERS = pSource.LOCATION_ANDERS,
                COSTPRICE = pSource.COSTPRICE,
                DURATION_OVERALL = TijdMinutenNaarStringconversie(pSource.DURATION_OVERALL),
                EV_ZG_YN = pSource.EV_ZG_YN,
                EV_PERS_YN = pSource.EV_PERS_YN == "J" ? true : false,
                EV_PERS_DURATION = pSource.EV_PERS_DURATION,
                EV_ANDERS_YN = pSource.EV_ANDERS_YN == "J" ? true : false,
                EV_ANDERS_DURATION = pSource.EV_ANDERS_DURATION,
                EV_ANDERS_FT = pSource.EV_ANDERS_FT,
                QR = pSource.QR,
        
                //Child tables
                TRAINING_QUESTIONNNAIREDetails = MapHelper.MapCollection(pSource.TRAINING_QUESTIONNNAIREs).ToList<TRAINING_QUESTIONNNAIREDetail>()

            };

            // Merge fields from base Dossier table into the TRAININGDetail instance 
            MergeHelper.MergeSingle(pSource).Into<TRAININGDetail>(vTRAININGDetail);

            return vTRAININGDetail;
        }

        private String TijdMinutenNaarStringconversie(int? pTijd)
        {
            if(pTijd == null)
            {
                return ""; ;
            }
            else
            {
                string lUurgedeelteString = "";
                int lUurgedeelteGetal = Convert.ToInt32(Math.Floor((double)pTijd / 60));  
                if(lUurgedeelteGetal < 10)
                {
                    lUurgedeelteString =  "0" + lUurgedeelteGetal.ToString();  //bijvoorbeeld "02"
                }
                else
                {
                    lUurgedeelteString = lUurgedeelteGetal.ToString();  //"10" of "11" of "12"
                }
                string lMinuutgedeelteString = "";
                double lMinuutgedeelteGetal = ((double)pTijd % 60);  //% is restgetal
                if (lMinuutgedeelteGetal < 10)
                {
                    lMinuutgedeelteString = "0" + lMinuutgedeelteGetal.ToString();
                }
                else
                {
                    lMinuutgedeelteString = lMinuutgedeelteGetal.ToString();

                }
                return lUurgedeelteString + ":" + lMinuutgedeelteString;
            }

        }


        #endregion

        #region Child Entities

        public TRAINING_QUESTIONNNAIREDetail Map(TRAINING_QUESTIONNNAIRE pSource)
        {
            // Map null source type to null target type
            if (pSource == null)
            {
                return null;
            }

            var vTRAINING_QUESTIONNNAIREDetail = new TRAINING_QUESTIONNNAIREDetail
            {
                // Primitive properties                
                ID = pSource.ID,
                QUESTIONNAIRE_ID = pSource.QUESTIONNAIRE_ID,
                TRAINING_ID = pSource.TRAINING_ID,
                DATE_VALID_END = pSource.DATE_VALID_END,
                DATE_VALID_START = pSource.DATE_VALID_START,
                INFO = pSource.INFO,
                
                TIME_LIFESPAN = pSource.TIME_LIFESPAN,

                QUESTIONNAIREDetails = MapHelper.MapSingle(pSource.QUESTIONNAIRE).To<WGK.LMS.Business.Dtos.Questionnaires.QUESTIONNAIREDetail>()
            };


            // Merge fields from base Dossier table into the TRAININGDetail instance 
            //MergeHelper.MergeSingle(pSource).Into<TRAINING_OFFICEDetail>(vTRAINING_OFFICEDetail);

            return vTRAINING_QUESTIONNNAIREDetail;
        }
       




        #endregion
    }
}
