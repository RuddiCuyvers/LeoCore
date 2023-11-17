using System.Threading;
using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Interfaces.Questionnaires;
using LEO.Business.Models.Questionnaires;
using LeoCore.Data.Models;
using LeoCore.Data;
using AutoMapper;

namespace LEO.Business.UseCases.Questionnaires
{
    public class PersonQuestionnaireMaintenanceUseCase 
    {

        #region Fields
        public decimal? Person_QuestionnaireID;
        public decimal ID { get; set; }

        public string CurrentUserClientID { get; set; }
        
        #endregion

        #region Constructors
        private readonly IMapper _mapper;
        private readonly ITrainingRepository iTrainingRepository;
        private readonly IPersonQuestionnaireRepository iPersonQuestionnaireRepository;
        private readonly IQuestionnaireRepository iQuestionnaireRepository;

        public PersonQuestionnaireMaintenanceUseCase(IMapper mapper, ITrainingRepository trainingrepository, IPersonQuestionnaireRepository personquestionnairerepository, IQuestionnaireRepository questionnairerepository)
       {
                   _mapper = mapper;
                   iTrainingRepository = trainingrepository;
                   iPersonQuestionnaireRepository = personquestionnairerepository;
                   iQuestionnaireRepository = questionnairerepository;
       }

    public bool IsLeraar { get; set; }
    #endregion

    #region Protected Methods


    /// <summary>
    /// Fetches Questionnaire detail table from database
    /// </summary>
    protected void FetchQuestionnaireDetailTable(ref QUESTIONNAIRE pQuestionnaire)
        {
            QUESTIONNAIRE vQuestionnaire = new QUESTIONNAIRE();
            if (IsLeraar == false)
            {
               vQuestionnaire = this.iQuestionnaireRepository.GetQUESTIONNAIREByTRAININGID(
                    pTrainingID: (int)this.ID,
                    pIncludeSoftDeleted: false,
                    pIncludeAllData: true,
                    pNoTracking: false);
                if (vQuestionnaire == null)
                {
                    
                }
            }
            else
            {

                vQuestionnaire = this.iQuestionnaireRepository.GetQUESTIONNAIRE(
                   pID: 2,  // questionnaire 2 is de leraren questionnaire    // ToDo:
                   pIncludeSoftDeleted: false,
                   pIncludeAllData: true,
                   pNoTracking: false);
                if (vQuestionnaire == null)
                {
                   
                }
            }

            pQuestionnaire = vQuestionnaire;
        }

        protected void FetchPersonQuestionnaireDetailTable(ref PERSON_QUESTIONNAIRE pPersonQuestionnaire)
        {
            var vPersonQuestionnaire = this.iPersonQuestionnaireRepository.GetPERSON_QUESTIONNAIREByTRAININGIDAndByCLIENTID(
                    pTrainingID: (int)this.ID,
                    pClientID: this.CurrentUserClientID,
                    pIncludeSoftDeleted: false,
                    pIncludeAllData: true,
                    pNoTracking: false);

            pPersonQuestionnaire = vPersonQuestionnaire;

        }

        /// <summary>
        /// Merges the business logic for read/create.
        /// </summary>
        protected void MergeBusinessLogic()
        {
            // Merges the business logic on the base DossierDetail DTO and child table collections
          

            // Merge business logic on QuestionnaireDetail
            this.MergeBusinessLogicForQuestionnaire();
        }
        #endregion

        #region Protected Methods - Questionnaire table
        /// <summary>
        /// Merge business logic on QuestionnaireDetail
        /// </summary>
        private void MergeBusinessLogicForQuestionnaire()
        {

            //QUESTIONNAIREDetail vQuestionnaireDetail = this.Result.QuestionnaireDetail;
            //PERSON_QUESTIONNAIREDetail vPersonQuestionnaireDetail = this.Result.PersonQuestionnaireDetail;
            //vQuestionnaireDetail?.QUESTIONNAIRE_QUESTIONDetails?.Sort((a, b) => a.ORDER.CompareTo(b.ORDER));  //zeker sorteren zodat volgorde gaat overeen komen met 

            //foreach (var i in vQuestionnaireDetail.QUESTIONNAIRE_QUESTIONDetails)
            //{
            //    if(vPersonQuestionnaireDetail.PERSON_QUESTIONNAIRE_ANSWERDetails == null || vPersonQuestionnaireDetail.PERSON_QUESTIONNAIRE_ANSWERDetails.Exists(m=>m.QUESTION_ID == i.QUESTION_ID) == false)
            //    {
            //        PERSON_QUESTIONNAIRE_ANSWERDetail lPersonQuestionnaireAnswer = new PERSON_QUESTIONNAIRE_ANSWERDetail();
            //        lPersonQuestionnaireAnswer.ID = -1;  //om aan te geven dat het een nieuwe is
            //        lPersonQuestionnaireAnswer.PERSON_QUESTIONNAIRE_ID = vPersonQuestionnaireDetail.ID;
            //        lPersonQuestionnaireAnswer.QUESTION_ID = i.QUESTION_ID;
            //        lPersonQuestionnaireAnswer.QQORDER_AS_WAS = i.ORDER;
            //        lPersonQuestionnaireAnswer.QTEXT_AS_WAS = i.QUESTIONDetails.TEXT;
            //        lPersonQuestionnaireAnswer.QTYPEANSWER_AS_WAS = i.QUESTIONDetails.TYPE_ANSWER;
            //        vPersonQuestionnaireDetail.PERSON_QUESTIONNAIRE_ANSWERDetails.Add(lPersonQuestionnaireAnswer);
            //    }

            //}
         
        }
        #endregion

    }
}
