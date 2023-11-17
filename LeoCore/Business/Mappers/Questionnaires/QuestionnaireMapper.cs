using System;
using LEO.Business.Dtos.Questionnaires;
using AutoMapper;

using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Linq;

namespace LEO.Business.Mappers.Questionnaires
{
  

    public class QuestionnaireMapper  : Profile
    {
        public QuestionnaireMapper()
        {
            this.CreateMap<LeoCore.Data.Models.QUESTIONNAIRE, QUESTIONNAIREDetail>()
                .ForMember(dest => dest.ID, opt => opt.MapFrom(src => src.ID))
                .ForMember(dest => dest.DESCRIPTION, opt => opt.MapFrom(src => src.DESCRIPTION))
                .ForMember(dest => dest.INFO, opt => opt.MapFrom(src => src.INFO))
                .ForMember(dest => dest.QUESTIONNAIRE_QUESTIONDetails, opt => opt.MapFrom(src => src.QUESTIONNAIRE_QUESTIONs))
                ;

            this.CreateMap<LeoCore.Data.Models.QUESTIONNAIRE_QUESTION, QUESTIONNAIRE_QUESTIONDetail>();
        }


        //#region QUESTIONNAIRE entity
        ///// <summary>
        ///// Maps a questionnaire (data layer) to a new QUESTIONNAIREDetail instance (business layer)
        ///// </summary>
        //public QUESTIONNAIREDetail Map(QUESTIONNAIRE pSource)
        //{
        //    // Map null source type to null target type
        //    if (pSource == null)
        //    {
        //        return null;
        //    }

        //    var vQUESTIONNAIREDetail = new QUESTIONNAIREDetail
        //    {
        //        // Primitive properties                
        //        ID = pSource.ID,
        //        // DATE_VALID_END = pSource.DATE_VALID_END,
        //        // DATE_VALID_START = pSource.DATE_VALID_START,
        //        DESCRIPTION = pSource.DESCRIPTION,
        //        INFO = pSource.INFO,
        //        QUESTIONNAIRE_QUESTIONDetails = (List<QUESTIONNAIRE_QUESTIONDetail>)_mapper.Map<IEnumerable<QUESTIONNAIRE_QUESTION>, IEnumerable<QUESTIONNAIRE_QUESTIONDetail>>(pSource.QUESTIONNAIRE_QUESTIONs)  //     infoviewmodels;    MapHelper.MapCollection(pSource.QUESTIONNAIRE_QUESTIONs).ToList<QUESTIONNAIRE_QUESTIONDetail>()
        //    };

        //    //// Merge fields from Questionnaire table into the QUESTIONNAIREDetail instance 
    
        //    vQUESTIONNAIREDetail.QUESTIONNAIRE_QUESTIONDetails.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));
        //    return vQUESTIONNAIREDetail;
        //}
        //#endregion

        //#region Child entities

        //#region QUESTIONNAIRE_QUESTION entity
        //public QUESTIONNAIRE_QUESTIONDetail Map(QUESTIONNAIRE_QUESTION pSource)
        //{
        //    // Map null source type to null target type
        //    if (pSource == null)
        //    {
        //        return null;
        //    }

        //    var vQUESTIONNAIRE_QUESTIONDetail = new QUESTIONNAIRE_QUESTIONDetail
        //    {
        //        // Primitive properties
        //        ID = pSource.ID,
        //        QUESTIONNAIRE_ID = pSource.QUESTIONNAIRE_ID,
        //        QUESTION_ID = pSource.QUESTION_ID,
        //        MANDATORY = pSource.MANDATORY,
        //        ORDER = pSource.ORDER,
        //        //DATE_VALID_START = pSource.DATE_VALID_START,
        //       // DATE_VALID_END = pSource.DATE_VALID_END,
        //        QUESTIONDetails = _mapper.Map<QUESTION, QUESTIONDetail>(pSource.QUESTION)    // MapHelper.MapSingle(pSource.QUESTION).To<QUESTIONDetail>()
        //    };
        //    vQUESTIONNAIRE_QUESTIONDetail?.QUESTIONDetails?.QUESTIONNAIRE_QUESTIONDetails?.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));
        //    return vQUESTIONNAIRE_QUESTIONDetail;
        //}

        //#endregion

        //#region QUESTIONDetail

        //public LEO.Business.Dtos.Questionnaires.QUESTIONDetail  Map(QUESTION pSource)
        //{
        //    // Map null source type to null target type
        //    if (pSource == null)
        //    {
        //        return null;
        //    }

        //    var vQUESTIONDetail = new QUESTIONDetail
        //    {
        //        // Primitive properties
        //        ID = pSource.ID,
        //        //DATE_VALID_END = pSource.DATE_VALID_END,
        //        //DATE_VALID_START = pSource.DATE_VALID_START,
        //        INFO = pSource.INFO,
        //        TEXT = pSource.TEXT,
        //        TYPE_ANSWER = pSource.TYPE_ANSWER,
        //        VALUE_MAX = pSource.VALUE_MAX,
        //        VALUE_MIN = pSource.VALUE_MIN
        //    };
        //    return vQUESTIONDetail;

        //}

        //#endregion

        //#region PERSON_QUESTIONNAIRE entity
        //public PERSON_QUESTIONNAIREDetail Map(PERSON_QUESTIONNAIRE pSource)
        //{
        //    // Map null source type to null target type
        //    if (pSource == null)
        //    {
        //        return null;
        //    }

        //    var vPERSON_QUESTIONNAIREDetail = new PERSON_QUESTIONNAIREDetail
        //    {
        //        // Primitive properties
        //        ID = pSource.ID,
        //        CLIENT_ID = pSource.CLIENT_ID,
        //        QUESTIONNAIRE_ID = pSource.QUESTIONNAIRE_ID,
        //        TRAINING_ID = pSource.TRAINING_ID,
        //        //DATE_SUBMITTED = pSource.DATE_SUBMITTED,
        //        PERSON_QUESTIONNAIRE_ANSWERDetails = (List<PERSON_QUESTIONNAIRE_ANSWERDetail>)_mapper.Map<IEnumerable<PERSON_QUESTIONNAIRE_ANSWER>, IEnumerable<PERSON_QUESTIONNAIRE_ANSWERDetail>>(pSource.PERSON_QUESTIONNAIRE_ANSWERs)
        //        //MapHelper.MapCollection(pSource.PERSON_QUESTIONNAIRE_ANSWERs).ToList<PERSON_QUESTIONNAIRE_ANSWERDetail>()

        //    };

        //      vPERSON_QUESTIONNAIREDetail?.PERSON_QUESTIONNAIRE_ANSWERDetails?.Sort((x, y) => x.QQORDER_AS_WAS.CompareTo(y.QQORDER_AS_WAS));
        //      vPERSON_QUESTIONNAIREDetail?.QUESTIONNAIREDetails?.QUESTIONNAIRE_QUESTIONDetails?.Sort((x, y) => x.ORDER.CompareTo(y.ORDER));
        //    return vPERSON_QUESTIONNAIREDetail;
        //}

        //#endregion

        //#region PERSON_QUESTIONNAIRE_ANSWER entity
        //public PERSON_QUESTIONNAIRE_ANSWERDetail Map(PERSON_QUESTIONNAIRE_ANSWER pSource)
        //{
        //    // Map null source type to null target type
        //    if (pSource == null)
        //    {
        //        return null;
        //    }

        //    var vPERSON_QUESTIONNAIRE_ANSWERDetail = new PERSON_QUESTIONNAIRE_ANSWERDetail
        //    {
        //        // Primitive properties
        //        ID = pSource.ID,
        //        PERSON_QUESTIONNAIRE_ID = pSource.PERSON_QUESTIONNAIRE_ID,
        //        QUESTION_ID = pSource.QUESTION_ID,
        //        ANSWER_TEXT = pSource.ANSWER_TEXT,
        //        ANSWER_NUMBER = pSource.ANSWER_NUMBER,
        //        //ANSWER_DATE = pSource.ANSWER_DATE,
        //        QTEXT_AS_WAS = pSource.QTEXT_AS_WAS,
        //        QQORDER_AS_WAS = pSource.QQORDER_AS_WAS??0,
        //        QTYPEANSWER_AS_WAS= pSource.QTYPEANSWER_AS_WAS,



        //    };

           

        //    return vPERSON_QUESTIONNAIRE_ANSWERDetail;
        //}

        //#endregion

        //#endregion
    }
}
