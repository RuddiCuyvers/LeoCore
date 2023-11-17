using System.Linq;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Helpers;
using LeodbModel;
using WGK.Lib.Mappers;
using System;
using System.Reflection;

namespace WGK.LMS.Business.Mappers.Trainings
{
    /// <summary>
    /// Helper class containing merge methods for Dossier base table and child tables
    /// </summary>
    public class TrainingMerger :
         // Special merger for copying base Dossier table data to specific dossier business model (class derived from DossierDetail)
         WGK.Lib.Mappers.IMerger<TRAINING, TRAININGDetail>,
        IMerger<TRAININGDetail, TRAINING>,
        IMerger<TRAINING_QUESTIONNNAIRE, TRAINING_QUESTIONNNAIREDetail>,
        IMerger<WGK.LMS.Business.Dtos.Trainings.TRAINING_QUESTIONNNAIREDetail,TRAINING_QUESTIONNNAIRE>
    {
        #region Training entity
        /// <summary>
        /// Merges a Dossier instance (data layer) into a DossierDetail instance (business layer).
        /// </summary>
        /// <remarks>
        /// This method is called from a mapper in order to merge base Dossier table data into a specific dossier business model
        /// class derived from DossierDetail. Therefore we map foreign entities and child collections to new instances (instead of merging
        /// them into existing ones).
        /// </remarks>
        public void Merge(TRAINING pFrom, TRAININGDetail pInto)
        {
            // Primitive properties
            pInto.ID = pFrom.ID;
            pInto.NOMENCL_CONV_YN = pFrom.NOMENCL_CONV_YN;
          
            pInto.LINK = pFrom.LINK;
          
            pInto.QR = pFrom.QR;
            pInto.TRAINING_TYPE = pFrom.TRAINING_TYPE;

            pInto.SUBJECT = pFrom.SUBJECT;
            // AttachmentSet foreign entity
            // !! Map instead of merge !!


            // Child tables
            // !! Map instead of merge !!
            pInto.TRAINING_QUESTIONNNAIREDetails = MapHelper.MapCollection(pFrom.TRAINING_QUESTIONNNAIREs).ToList<TRAINING_QUESTIONNNAIREDetail>();

        

        }

        /// <summary>
        /// Merges a DossierDetail instance (business layer) into a Dossier Entity instance (data layer) 
        /// </summary>
        /// 
        public void Merge(TRAININGDetail pFrom, TRAINING pInto)
        {
            // Don't assign value if new value is equal to current value, otherwise an unnecessary database update occurs !!!
            if (pInto.ID != pFrom.ID)
            {
                pInto.ID = pFrom.ID;
            }
            if (pInto.SUBJECT != pFrom.SUBJECT)
            {
                pInto.SUBJECT = pFrom.SUBJECT;
            }
            if (pInto.APPLICANT_CLIENTID != pFrom.APPLICANT_CLIENTID)
            {
                pInto.APPLICANT_CLIENTID = pFrom.APPLICANT_CLIENTID;
            }
            if (pInto.METHODOLOGY != pFrom.METHODOLOGY)
            {
                pInto.METHODOLOGY = pFrom.METHODOLOGY;
            }
            if (pInto.LINK != pFrom.LINK)
            {
                pInto.LINK = pFrom.LINK;
            }
            if (pInto.TRAINING_TYPE != pFrom.TRAINING_TYPE)
            {
                pInto.TRAINING_TYPE = pFrom.TRAINING_TYPE;
            }
            if (pInto.TRAINING_TYPE_FT != pFrom.TRAINING_TYPE_FT)
            {
                pInto.TRAINING_TYPE_FT = pFrom.TRAINING_TYPE_FT;
            }
            if (pInto.TRAINER_INT_EXT != pFrom.TRAINER_INT_EXT)
            {
                pInto.TRAINER_INT_EXT = pFrom.TRAINER_INT_EXT;
            }
            if (pInto.TRAINER_EMAIL != pFrom.TRAINER_EMAIL)
            {
                pInto.TRAINER_EMAIL = pFrom.TRAINER_EMAIL;
            }
            if (pInto.NOMENCL_CONV_YN != pFrom.NOMENCL_CONV_YN)
            {
                pInto.NOMENCL_CONV_YN = pFrom.NOMENCL_CONV_YN;
            }
            if (pInto.NC_EVD_YN != (pFrom.NC_EVD_YN?"J":"N"))
            {
                pInto.NC_EVD_YN = pFrom.NC_EVD_YN?"J":"N";
            }
          //  if (pInto.NC_EVD_DURATION != (pFrom.NC_EVD_DURATION))
           // {
                pInto.NC_EVD_DURATION = TijdStringNaarMinutenconversie(pFrom.NC_EVD_DURATION); 
           // }
            if (pInto.NC_EVD_SUBJECT != (pFrom.NC_EVD_SUBJECT))
            {
                pInto.NC_EVD_SUBJECT = pFrom.NC_EVD_SUBJECT;
            }
            if (pInto.NC_KATZ_YN != (pFrom.NC_KATZ_YN ? "J" : "N"))
            {
                pInto.NC_KATZ_YN = pFrom.NC_KATZ_YN ? "J" : "N";
            }
           //if (pInto.NC_KATZ_DURATION != (pFrom.NC_KATZ_DURATION))
            //{
                pInto.NC_KATZ_DURATION = TijdStringNaarMinutenconversie(pFrom.NC_KATZ_DURATION);
            //}
            if (pInto.NC_KATZ_SUBJECT != (pFrom.NC_KATZ_SUBJECT))
            {
                pInto.NC_KATZ_SUBJECT = pFrom.NC_KATZ_SUBJECT;
            }
            if (pInto.NC_NOMC_YN != (pFrom.NC_NOMC_YN ? "J" : "N"))
            {
                pInto.NC_NOMC_YN = pFrom.NC_NOMC_YN ? "J" : "N";
            }
            //if (pInto.NC_NOMC_DURATION != (pFrom.NC_NOMC_DURATION))
           // {
                pInto.NC_NOMC_DURATION = TijdStringNaarMinutenconversie(pFrom.NC_NOMC_DURATION);
            //}
            if (pInto.NC_NOMC_SUBJ != (pFrom.NC_NOMC_SUBJ))
            {
                pInto.NC_NOMC_SUBJ = pFrom.NC_NOMC_SUBJ;
            }
            if (pInto.NC_THUISZORG_YN != (pFrom.NC_THUISZORG_YN ? "J" : "N"))
            {
                pInto.NC_THUISZORG_YN = pFrom.NC_THUISZORG_YN ? "J" : "N";
            }
         //   if (pInto.NC_THUISZORG_DURATION != (pFrom.NC_THUISZORG_DURATION))
            //{
                pInto.NC_THUISZORG_DURATION = TijdStringNaarMinutenconversie(pFrom.NC_THUISZORG_DURATION);
           // }
            if (pInto.NC_THUISZORG_SUBJECT != (pFrom.NC_THUISZORG_SUBJECT))
            {
                pInto.NC_THUISZORG_SUBJECT = pFrom.NC_THUISZORG_SUBJECT;
            }
            if (pInto.NC_VVD_YN != (pFrom.NC_VVD_YN ? "J" : "N"))
            {
                pInto.NC_VVD_YN = pFrom.NC_VVD_YN ? "J" : "N";
            }
           // if (pInto.NC_VVD_DURATION != (pFrom.NC_VVD_DURATION))
           // {
                pInto.NC_VVD_DURATION = TijdStringNaarMinutenconversie(pFrom.NC_VVD_DURATION);
           // }
            if (pInto.NC_VVD_SUBJECT != (pFrom.NC_VVD_SUBJECT))
            {
                pInto.NC_VVD_SUBJECT = pFrom.NC_VVD_SUBJECT;
            }
            if (pInto.NC_ROL_YN != (pFrom.NC_ROL_YN ? "J" : "N"))
            {
                pInto.NC_ROL_YN = pFrom.NC_ROL_YN ? "J" : "N";
            }
         //   if (pInto.NC_ROL_DURATION != (pFrom.NC_ROL_DURATION))
          //  {
                pInto.NC_ROL_DURATION = TijdStringNaarMinutenconversie(pFrom.NC_ROL_DURATION);
          //  }
            if (pInto.NC_ROL_SUBJECT != (pFrom.NC_ROL_SUBJECT))
            {
                pInto.NC_ROL_SUBJECT = pFrom.NC_ROL_SUBJECT;
            }
            if (pInto.EV_YN != (pFrom.EV_YN))
            {
                pInto.EV_YN = pFrom.EV_YN;
            }
            if (pInto.EV_WW_YN != (pFrom.EV_WW_YN ? "J" : "N"))
            {
                pInto.EV_WW_YN = pFrom.EV_WW_YN ? "J" : "N";
            }
           // if (pInto.EV_WW_DURATION != (pFrom.EV_WW_DURATION))
            //{
                pInto.EV_WW_DURATION = TijdStringNaarMinutenconversie(pFrom.EV_WW_DURATION);
           // }
            if (pInto.EV_ZG_SUBJ != (pFrom.EV_ZG_SUBJ))
            {
                pInto.EV_ZG_SUBJ = pFrom.EV_ZG_SUBJ;
            }
            if (pInto.EV_ZG_REFDOM != (pFrom.EV_ZG_REFDOM))
            {
                pInto.EV_ZG_REFDOM = pFrom.EV_ZG_REFDOM;
            }
            if (pInto.EV_ZG_COMPL != (pFrom.EV_ZG_COMPL))
            {
                pInto.EV_ZG_COMPL = pFrom.EV_ZG_COMPL;
            }
            if (pInto.EV_AWS_YN != pFrom.EV_AWS_YN)
            {
                pInto.EV_AWS_YN = pFrom.EV_AWS_YN ;
            }
            if (pInto.EV_AWS_SUBJ != (pFrom.EV_AWS_SUBJ))
            {
                pInto.EV_AWS_SUBJ = pFrom.EV_AWS_SUBJ;
            }
            if (pInto.EV_AWS_ONCO_SUBJ != (pFrom.EV_AWS_ONCO_SUBJ))
            {
                pInto.EV_AWS_ONCO_SUBJ = pFrom.EV_AWS_ONCO_SUBJ;
            }
            if (pInto.EV_AWS_INCO_SUBJ != (pFrom.EV_AWS_INCO_SUBJ))
            {
                pInto.EV_AWS_INCO_SUBJ = pFrom.EV_AWS_INCO_SUBJ;
            }
            if (pInto.EV_SUBJECT != (pFrom.EV_SUBJECT))
            {
                pInto.EV_SUBJECT = pFrom.EV_SUBJECT;
            }
           // if (pInto.EV_ZG_DURATION != (pFrom.EV_ZG_DURATION))
           // {
                pInto.EV_ZG_DURATION = TijdStringNaarMinutenconversie(pFrom.EV_ZG_DURATION);
           // }
           // if (pInto.EV_AWS_DURATION != (pFrom.EV_AWS_DURATION))
           // {
                pInto.EV_AWS_DURATION = TijdStringNaarMinutenconversie(pFrom.EV_AWS_DURATION);
            //}
            if (pInto.LOCATION != (pFrom.LOCATION))
            {
                pInto.LOCATION = pFrom.LOCATION;
            }
            if (pInto.LOCATION_ANDERS != (pFrom.LOCATION_ANDERS))
            {
                pInto.LOCATION_ANDERS = pFrom.LOCATION_ANDERS;
            }
            if (pInto.COSTPRICE != (pFrom.COSTPRICE))
            {
                pInto.COSTPRICE = pFrom.COSTPRICE;
            }
            //if (pInto.DURATION_OVERALL != (pFrom.DURATION_OVERALL))
            //{
                pInto.DURATION_OVERALL = TijdStringNaarMinutenconversie(pFrom.DURATION_OVERALL);
            //}
            if (pInto.EV_ZG_YN != pFrom.EV_ZG_YN)
            {
                pInto.EV_ZG_YN = pFrom.EV_ZG_YN ;
            }
            if (pInto.EV_PERS_YN != (pFrom.EV_PERS_YN ? "J" : "N"))
            {
                pInto.EV_PERS_YN = pFrom.EV_PERS_YN ? "J" : "N";
            }
           
            if (pInto.EV_PERS_DURATION != (pFrom.EV_PERS_DURATION))
            {
                pInto.EV_PERS_DURATION = (int?)pFrom.EV_PERS_DURATION;
            }
            if (pInto.EV_ANDERS_YN != (pFrom.EV_ANDERS_YN ? "J" : "N"))
            {
                pInto.EV_ANDERS_YN = pFrom.EV_ANDERS_YN ? "J" : "N";
            }
            if (pInto.EV_ANDERS_DURATION != (pFrom.EV_ANDERS_DURATION))
            {
                pInto.EV_ANDERS_DURATION = (int?)pFrom.EV_ANDERS_DURATION;
            }
            if (pInto.EV_ANDERS_FT != (pFrom.EV_ANDERS_FT))
            {
                pInto.EV_ANDERS_FT = pFrom.EV_ANDERS_FT;
            }


            // Child tables
            // Use MergeChildEntitiesHelper to merge a collection of child row instances (business layer) into an EntitySet (data layer)

            MergeHelper.MergeChildEntities(pFrom.TRAINING_QUESTIONNNAIREDetails, p => p.ID)
                .Into(pInto.TRAINING_QUESTIONNNAIREs, p => p.ID);


        }

        private int? TijdStringNaarMinutenconversie(String pTijd)
        {
            if (String.IsNullOrWhiteSpace(pTijd))
            {
                return null;
            }
            else
            {
                int? lAantalMinuten = 0;
                string[] a = pTijd.Split(new string[] { ":" }, StringSplitOptions.None);
                int n;
                bool isUurNumeric = int.TryParse(a[0], out n);
                int m;
                bool isMinuutNumeric = int.TryParse(a[1], out n);
                if (isUurNumeric == true)
                {
                    lAantalMinuten = lAantalMinuten + (int.Parse(a[0]) * 60);
                }
                if (isMinuutNumeric == true)
                {
                    lAantalMinuten = lAantalMinuten + (int.Parse(a[1]));
                }
                return lAantalMinuten;
            }

        }
        #endregion

        #region QUESTIONNAIRE_QUESTION entity
        public void Merge(TRAINING_QUESTIONNNAIRE pFrom, TRAINING_QUESTIONNNAIREDetail pInto)
        {
            // Primitive properties
            pInto.ID = pFrom.ID;
            pInto.DATE_VALID_END = pFrom.DATE_VALID_END;
            pInto.DATE_VALID_START = pFrom.DATE_VALID_START;
            pInto.QUESTIONNAIRE_ID = pFrom.QUESTIONNAIRE_ID;
            pInto.TRAINING_ID = pFrom.TRAINING_ID;
        }


        // <summary>
        /// Merges an QUESTIONNAIRE_QUESTIONDetail instance (business layer) into a QUESTIONNAIRE_QUESTION Entity instance (data layer) 
        /// </summary>
        public void Merge(TRAINING_QUESTIONNNAIREDetail pFrom, TRAINING_QUESTIONNNAIRE pInto)
        {
            #region primitive types
            // Don't  assign value if new value is equal to current value, otherwise an unnecessary database update occurs !!!

            // Primitive properties
            if (pInto.ID != pFrom.ID)
            {
                pInto.ID = pFrom.ID;
            }

            if (pInto.QUESTIONNAIRE_ID != pFrom.QUESTIONNAIRE_ID)
            {
                pInto.QUESTIONNAIRE_ID = pFrom.QUESTIONNAIRE_ID;
            }

            if (pInto.TRAINING_ID != pFrom.TRAINING_ID)
            {
                pInto.TRAINING_ID = pFrom.TRAINING_ID;
            }

            if (pInto.DATE_VALID_END != pFrom.DATE_VALID_END)
            {
                pInto.DATE_VALID_END = pFrom.DATE_VALID_END;
            }
            #endregion

          
        }
        #endregion
    }
}
