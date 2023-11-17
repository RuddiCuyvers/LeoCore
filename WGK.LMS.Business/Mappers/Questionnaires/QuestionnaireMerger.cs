using System.Linq;

using WGK.LMS.Business.Helpers;
using LeodbModel;
using WGK.Lib.Mappers;
using System;
using WGK.LMS.Business.Dtos.Questionnaires;
using LeodbModel;
using WGK.Lib.Mappers;
using System;

namespace WGK.LMS.Business.Mappers.Questionnaires
{
    /// <summary>
    /// Helper class containing merge methods for Dossier base table and child tables
    /// </summary>
    public class QuestionnaireMerger :
         // Special merger for copying base Dossier table data to specific dossier business model (class derived from DossierDetail)
        IMerger<QUESTIONNAIRE, QUESTIONNAIREDetail>,
        IMerger<QUESTIONNAIREDetail, QUESTIONNAIRE>,
        IMerger<QUESTIONNAIRE_QUESTION, QUESTIONNAIRE_QUESTIONDetail>,
        IMerger<QUESTIONNAIRE_QUESTIONDetail, QUESTIONNAIRE_QUESTION>,

        IMerger<PERSON_QUESTIONNAIREDetail, PERSON_QUESTIONNAIRE>,
        IMerger<PERSON_QUESTIONNAIRE, PERSON_QUESTIONNAIREDetail>,

         IMerger<PERSON_QUESTIONNAIRE_ANSWERDetail, PERSON_QUESTIONNAIRE_ANSWER>

    {
        #region questionnaire entity
        /// <summary>
        /// Merges a Dossier instance (data layer) into a DossierDetail instance (business layer).
        /// </summary>
        /// <remarks>
        /// This method is called from a mapper in order to merge base Dossier table data into a specific dossier business model
        /// class derived from DossierDetail. Therefore we map foreign entities and child collections to new instances (instead of merging
        /// them into existing ones).
        /// </remarks>
        public void Merge(QUESTIONNAIRE pFrom, QUESTIONNAIREDetail pInto)
        {
            // Primitive properties
            pInto.ID = pFrom.ID;
            pInto.DESCRIPTION = pFrom.DESCRIPTION;
            pInto.INFO = pFrom.INFO;
            pInto.DATE_VALID_START = pFrom.DATE_VALID_START;
            pInto.DATE_VALID_END = pFrom.DATE_VALID_END;

            // Child tables
            // !! Map instead of merge !!
            pInto.QUESTIONNAIRE_QUESTIONDetails = MapHelper.MapCollection(pFrom.QUESTIONNAIRE_QUESTIONs).ToList<QUESTIONNAIRE_QUESTIONDetail>();
        }

        /// <summary>
        /// Merges a DossierDetail instance (business layer) into a Dossier Entity instance (data layer) 
        /// </summary>
        /// 
        public void Merge(QUESTIONNAIREDetail pFrom, QUESTIONNAIRE pInto)
        {
            #region
            // Don't assign value if new value is equal to current value, otherwise an unnecessary database update occurs !!!

            // Primitive properties
            if (pInto.ID != pFrom.ID)
            {
                pInto.ID = (int)pFrom.ID;
            }

            if (pInto.DESCRIPTION != pFrom.DESCRIPTION)
            {
                pInto.DESCRIPTION = pFrom.DESCRIPTION;
            }

            if (pInto.INFO != pFrom.INFO)
            {
                pInto.INFO = pFrom.INFO;
            }

            if (pInto.DATE_VALID_START != pFrom.DATE_VALID_START)
            {
                pInto.DATE_VALID_START = pFrom.DATE_VALID_START;
            }

            if (pInto.DATE_VALID_END != pFrom.DATE_VALID_END)
            {
                pInto.DATE_VALID_END = pFrom.DATE_VALID_END;
            }


#endregion

            // Child tables
            // Use MergeChildEntitiesHelper to merge a collection of child row instances (business layer) into an EntitySet (data layer)
            //MergeHelper.MergeChildEntities(pFrom.QUESTIONNAIRE_QUESTIONDetails, p => p.ID)
            //      .Into(pInto.QUESTIONNAIRE_QUESTION, p => p.QUESTIONNAIRE_ID);



        }
        #endregion

        #region QUESTIONNAIRE_QUESTION entity

        public void Merge(QUESTIONNAIRE_QUESTION pFrom, QUESTIONNAIRE_QUESTIONDetail pInto)
        {
            // Primitive properties
            pInto.ID = pFrom.ID;
            pInto.QUESTIONNAIRE_ID = pFrom.QUESTIONNAIRE_ID;
            pInto.QUESTION_ID = pFrom.QUESTION_ID;
            pInto.MANDATORY = pFrom.MANDATORY;
            pInto.ORDER = pFrom.ORDER;
            pInto.DATE_VALID_START = pFrom.DATE_VALID_START;
            pInto.DATE_VALID_END = pFrom.DATE_VALID_END;
        }


        // <summary>
        /// Merges an QUESTIONNAIRE_QUESTIONDetail instance (business layer) into a QUESTIONNAIRE_QUESTION Entity instance (data layer) 
        /// </summary>
        public void Merge(QUESTIONNAIRE_QUESTIONDetail pFrom, QUESTIONNAIRE_QUESTION pInto)
        {
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

            if (pInto.DATE_VALID_END != pFrom.DATE_VALID_END)
            {
                pInto.DATE_VALID_END = pFrom.DATE_VALID_END;
            }
        }
        #endregion

        #region QUESTIONNAIRE_QUESTION entity
        public void Merge(PERSON_QUESTIONNAIRE pFrom, PERSON_QUESTIONNAIREDetail pInto)
        {
            // Primitive properties
            pInto.ID = pFrom.ID;
            pInto.CLIENT_ID = pFrom.CLIENT_ID;
            pInto.QUESTIONNAIRE_ID = pFrom.QUESTIONNAIRE_ID;
            pInto.TRAINING_ID = pFrom.TRAINING_ID;
            pInto.DATE_SUBMITTED = pFrom.DATE_SUBMITTED;

            // Child tables
            // !! Map instead of merge !!
            pInto.PERSON_QUESTIONNAIRE_ANSWERDetails = MapHelper.MapCollection(pFrom.PERSON_QUESTIONNAIRE_ANSWERs).ToList<PERSON_QUESTIONNAIRE_ANSWERDetail>();
        }


        // <summary>
        /// Merges an QUESTIONNAIRE_QUESTIONDetail instance (business layer) into a QUESTIONNAIRE_QUESTION Entity instance (data layer) 
        /// </summary>
        public void Merge(PERSON_QUESTIONNAIREDetail pFrom, PERSON_QUESTIONNAIRE pInto)
        {
            // Don't  assign value if new value is equal to current value, otherwise an unnecessary database update occurs !!!

            // Primitive properties
            if (pInto.ID != pFrom.ID)
            {
                pInto.ID = pFrom.ID;
            }
            if (pInto.CLIENT_ID != pFrom.CLIENT_ID)
            {
                pInto.CLIENT_ID = pFrom.CLIENT_ID;
            }
            if (pInto.QUESTIONNAIRE_ID != pFrom.QUESTIONNAIRE_ID)
            {
                pInto.QUESTIONNAIRE_ID = pFrom.QUESTIONNAIRE_ID;
            }
            if (pInto.TRAINING_ID != pFrom.TRAINING_ID)
            {
                pInto.TRAINING_ID = pFrom.TRAINING_ID;
            }
            if (pInto.DATE_SUBMITTED != pFrom.DATE_SUBMITTED)
            {
                pInto.DATE_SUBMITTED = pFrom.DATE_SUBMITTED;
            }

            // Child tables
            // Use MergeChildEntitiesHelper to merge a collection of child row instances (business layer) into an EntitySet (data layer)

            MergeHelper.MergeChildEntities(pFrom.PERSON_QUESTIONNAIRE_ANSWERDetails, p => p.ID)
                .Into(pInto.PERSON_QUESTIONNAIRE_ANSWERs, p => p.ID);
        }

        public void Merge(PERSON_QUESTIONNAIRE_ANSWERDetail pFrom, PERSON_QUESTIONNAIRE_ANSWER pInto)
        {
            // Don't  assign value if new value is equal to current value, otherwise an unnecessary database update occurs !!!

            // Primitive properties
            if (pInto.ID != pFrom.ID)
            {
                pInto.ID = pFrom.ID;
            }
            if (pInto.PERSON_QUESTIONNAIRE_ID != pFrom.pERSON_QUESTIONNAIRE_ID)
            {
                pInto.PERSON_QUESTIONNAIRE_ID = pFrom.pERSON_QUESTIONNAIRE_ID;
            }
            if (pInto.QUESTION_ID != pFrom.QUESTION_ID)
            {
                pInto.QUESTION_ID = pFrom.QUESTION_ID;
            }
            if (pInto.ANSWER_TEXT != pFrom.ANSWER_TEXT)
            {
                pInto.ANSWER_TEXT = pFrom.ANSWER_TEXT;
            }
            if (pInto.ANSWER_NUMBER != pFrom.ANSWER_NUMBER)
            {
                pInto.ANSWER_NUMBER = (int?)pFrom.ANSWER_NUMBER;
            }
            if (pInto.ANSWER_DATE != pFrom.ANSWER_DATE)
            {
                pInto.ANSWER_DATE = pFrom.ANSWER_DATE;
            }
            if (pInto.QTEXT_AS_WAS != pFrom.QTEXT_AS_WAS)
            {
                pInto.QTEXT_AS_WAS = pFrom.QTEXT_AS_WAS;
            }
            if (pInto.QQORDER_AS_WAS != pFrom.QQORDER_AS_WAS)
            {
                pInto.QQORDER_AS_WAS = (int?)pFrom.QQORDER_AS_WAS;
            }
            if (pInto.QTYPEANSWER_AS_WAS != pFrom.QTYPEANSWER_AS_WAS)
            {
                pInto.QTYPEANSWER_AS_WAS = pFrom.QTYPEANSWER_AS_WAS;
            }
            
        }
        #endregion
    }
}
