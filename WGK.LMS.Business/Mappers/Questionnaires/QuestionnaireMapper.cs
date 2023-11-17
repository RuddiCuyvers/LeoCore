using System;
using WGK.LMS.Business.Dtos.Questionnaires;
using LeodbModel;
using WGK.Lib.Extensions;
using WGK.Lib.Mappers;
using System.Collections.Generic;

namespace WGK.LMS.Business.Mappers.Questionnaires
{
    /// <summary>
    /// Helper class containing map methods for Dossier base table and child tables
    /// </summary>
    public class QuestionnaireMapper :
        // Data layer to business layer
        IMapper<QUESTIONNAIRE, WGK.LMS.Business.Dtos.Questionnaires.QUESTIONNAIREDetail>,
        IMapper<QUESTIONNAIRE_QUESTION, WGK.LMS.Business.Dtos.Questionnaires.QUESTIONNAIRE_QUESTIONDetail>,
        IMapper<QUESTION, WGK.LMS.Business.Dtos.Questionnaires.QUESTIONDetail>,
        IMapper<PERSON_QUESTIONNAIRE, WGK.LMS.Business.Dtos.Questionnaires.PERSON_QUESTIONNAIREDetail>,
        IMapper<PERSON_QUESTIONNAIRE_ANSWER, WGK.LMS.Business.Dtos.Questionnaires.PERSON_QUESTIONNAIRE_ANSWERDetail>

    {
        #region QUESTIONNAIRE entity
        /// <summary>
        /// Maps a Dossier instance (data layer) to a new QUESTIONNAIREDetail instance (business layer)
        /// </summary>
        public QUESTIONNAIREDetail Map(QUESTIONNAIRE pSource)
        {
            // Map null source type to null target type
            if (pSource == null)
            {
                return null;
            }

            var vQUESTIONNAIREDetail = new QUESTIONNAIREDetail
            {
                // Primitive properties                
                ID = pSource.ID,
                DATE_VALID_END = pSource.DATE_VALID_END,
                DATE_VALID_START = pSource.DATE_VALID_START,
                DESCRIPTION = pSource.DESCRIPTION,
                INFO = pSource.INFO,

                QUESTIONNAIRE_QUESTIONDetails = MapHelper.MapCollection(pSource.QUESTIONNAIRE_QUESTIONs).ToList<QUESTIONNAIRE_QUESTIONDetail>()
            };

            //// Merge fields from Questionnaire table into the QUESTIONNAIREDetail instance 
            //MergeHelper.MergeSingle(pSource).Into<QUESTIONNAIREDetail>(vQUESTIONNAIREDetail);
            vQUESTIONNAIREDetail.QUESTIONNAIRE_QUESTIONDetails.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));
            return vQUESTIONNAIREDetail;
        }
        #endregion

        #region Child entities

        #region QUESTIONNAIRE_QUESTION entity
        public QUESTIONNAIRE_QUESTIONDetail Map(LeodbModel.QUESTIONNAIRE_QUESTION pSource)
        {
            // Map null source type to null target type
            if (pSource == null)
            {
                return null;
            }

            var vQUESTIONNAIRE_QUESTIONDetail = new QUESTIONNAIRE_QUESTIONDetail
            {
                // Primitive properties
                ID = pSource.ID,
                QUESTIONNAIRE_ID = pSource.QUESTIONNAIRE_ID,
                QUESTION_ID = pSource.QUESTION_ID,
                MANDATORY = pSource.MANDATORY,
                ORDER = pSource.ORDER,
                DATE_VALID_START = pSource.DATE_VALID_START,
                DATE_VALID_END = pSource.DATE_VALID_END,
                QUESTIONDetails = MapHelper.MapSingle(pSource.QUESTION).To<QUESTIONDetail>()
            };
            vQUESTIONNAIRE_QUESTIONDetail?.QUESTIONDetails?.QUESTIONNAIRE_QUESTIONDetails?.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));
            return vQUESTIONNAIRE_QUESTIONDetail;
        }

        #endregion

        #region QUESTIONDetail

        public WGK.LMS.Business.Dtos.Questionnaires.QUESTIONDetail  Map(  LeodbModel.QUESTION pSource)
        {
            // Map null source type to null target type
            if (pSource == null)
            {
                return null;
            }

            var vQUESTIONDetail = new QUESTIONDetail
            {
                // Primitive properties
                ID = pSource.ID,
                DATE_VALID_END = pSource.DATE_VALID_END,
                DATE_VALID_START = pSource.DATE_VALID_START,
                INFO = pSource.INFO,
                TEXT = pSource.TEXT,
                TYPE_ANSWER = pSource.TYPE_ANSWER,
                VALUE_MAX = pSource.VALUE_MAX,
                VALUE_MIN = pSource.VALUE_MIN
            };
            return vQUESTIONDetail;

        }

        #endregion

        #region PERSON_QUESTIONNAIRE entity
        public PERSON_QUESTIONNAIREDetail Map(LeodbModel.PERSON_QUESTIONNAIRE pSource)
        {
            // Map null source type to null target type
            if (pSource == null)
            {
                return null;
            }

            var vPERSON_QUESTIONNAIREDetail = new PERSON_QUESTIONNAIREDetail
            {
                // Primitive properties
                ID = pSource.ID,
                CLIENT_ID = pSource.CLIENT_ID,
                QUESTIONNAIRE_ID = pSource.QUESTIONNAIRE_ID,
                TRAINING_ID = pSource.TRAINING_ID,
                DATE_SUBMITTED = pSource.DATE_SUBMITTED,
                PERSON_QUESTIONNAIRE_ANSWERDetails = MapHelper.MapCollection(pSource.PERSON_QUESTIONNAIRE_ANSWERs).ToList<PERSON_QUESTIONNAIRE_ANSWERDetail>()

            };

              vPERSON_QUESTIONNAIREDetail?.PERSON_QUESTIONNAIRE_ANSWERDetails?.Sort((x, y) => x.QQORDER_AS_WAS.CompareTo(y.QQORDER_AS_WAS));
              vPERSON_QUESTIONNAIREDetail?.QUESTIONNAIREDetails?.QUESTIONNAIRE_QUESTIONDetails?.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));
            return vPERSON_QUESTIONNAIREDetail;
        }

        #endregion

        #region PERSON_QUESTIONNAIRE_ANSWER entity
        public PERSON_QUESTIONNAIRE_ANSWERDetail Map(LeodbModel.PERSON_QUESTIONNAIRE_ANSWER pSource)
        {
            // Map null source type to null target type
            if (pSource == null)
            {
                return null;
            }

            var vPERSON_QUESTIONNAIRE_ANSWERDetail = new PERSON_QUESTIONNAIRE_ANSWERDetail
            {
                // Primitive properties
                ID = pSource.ID,
                pERSON_QUESTIONNAIRE_ID = pSource.PERSON_QUESTIONNAIRE_ID,
                QUESTION_ID = pSource.QUESTION_ID,
                ANSWER_TEXT = pSource.ANSWER_TEXT,
                ANSWER_NUMBER = pSource.ANSWER_NUMBER,
                ANSWER_DATE = pSource.ANSWER_DATE,
                QTEXT_AS_WAS = pSource.QTEXT_AS_WAS,
                QQORDER_AS_WAS = pSource.QQORDER_AS_WAS??0,
                QTYPEANSWER_AS_WAS= pSource.QTYPEANSWER_AS_WAS,



            };

           

            return vPERSON_QUESTIONNAIRE_ANSWERDetail;
        }

        #endregion


        #endregion
    }
}
