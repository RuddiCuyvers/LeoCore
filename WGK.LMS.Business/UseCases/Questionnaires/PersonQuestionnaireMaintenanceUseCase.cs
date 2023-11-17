using System.Threading;
using WGK.LMS.Business.Dtos.Questionnaires;
using WGK.LMS.Business.Interfaces.Questionnaires;
using WGK.LMS.Business.Models.Questionnaires;
using LeodbModel;
using WGK.LMS.Data.Interfaces;

using WGK.Lib.Exceptions;


namespace WGK.LMS.Business.UseCases.Questionnaires
{
    public class PersonQuestionnaireMaintenanceUseCase : BasePersonQuestionnaireMaintenanceUseCase<PersonQuestionnaireMaintenanceModel, QUESTIONNAIREDetail, PERSON_QUESTIONNAIREDetail>,IPersonQuestionnaireMaintenanceUseCase
    {

        #region Fields
        protected readonly IPersonQuestionnaireCreateUseCase iPersonQuestionnaireCreateUseCase;
        public int? Person_QuestionnaireID;
       
        #endregion

        #region Constructors

        public PersonQuestionnaireMaintenanceUseCase(
           
            IQuestionnaireRepository pQuestionnaireRepository,
            IPersonQuestionnaireRepository pPersonQuestionnaireRepository,
            IPersonQuestionnaireCreateUseCase pPersonQuestionnaireCreateUseCase
            )
            : base(pQuestionnaireRepository, pPersonQuestionnaireRepository)
            {
                iPersonQuestionnaireCreateUseCase = pPersonQuestionnaireCreateUseCase;
            }
        #endregion

        #region Protected Methods

        public bool IsLeraar { get; set; }
        /// <summary>
        /// Fetches Questionnaire detail table from database
        /// </summary>
        protected override void FetchQuestionnaireDetailTable(ref QUESTIONNAIRE pQuestionnaire)
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
                    throw new NoResultFoundException(
                        "Questionnaire ID",
                        this.ID);
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
                    throw new NoResultFoundException(
                        "Questionnaire ID",
                        this.ID);
                }
            }

            pQuestionnaire = vQuestionnaire;
        }

        protected override void FetchPersonQuestionnaireDetailTable(ref PERSON_QUESTIONNAIRE pPersonQuestionnaire)
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
        protected override void MergeBusinessLogic()
        {
            // Merges the business logic on the base DossierDetail DTO and child table collections
            base.MergeBusinessLogic();

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

            QUESTIONNAIREDetail vQuestionnaireDetail = this.Result.QuestionnaireDetail;
            PERSON_QUESTIONNAIREDetail vPersonQuestionnaireDetail = this.Result.PersonQuestionnaireDetail;
            vQuestionnaireDetail?.QUESTIONNAIRE_QUESTIONDetails?.Sort((a, b) => a.ORDER.CompareTo(b.ORDER));  //zeker sorteren zodat volgorde gaat overeen komen met 

            foreach (var i in vQuestionnaireDetail.QUESTIONNAIRE_QUESTIONDetails)
            {
                if(vPersonQuestionnaireDetail.PERSON_QUESTIONNAIRE_ANSWERDetails == null || vPersonQuestionnaireDetail.PERSON_QUESTIONNAIRE_ANSWERDetails.Exists(m=>m.QUESTION_ID == i.QUESTION_ID) == false)
                {
                    PERSON_QUESTIONNAIRE_ANSWERDetail lPersonQuestionnaireAnswer = new PERSON_QUESTIONNAIRE_ANSWERDetail();
                    lPersonQuestionnaireAnswer.ID = -1;  //om aan te geven dat het een nieuwe is
                    lPersonQuestionnaireAnswer.pERSON_QUESTIONNAIRE_ID = vPersonQuestionnaireDetail.ID;
                    lPersonQuestionnaireAnswer.QUESTION_ID = i.QUESTION_ID;
                    lPersonQuestionnaireAnswer.QQORDER_AS_WAS = i.ORDER;
                    lPersonQuestionnaireAnswer.QTEXT_AS_WAS = i.QUESTIONDetails.TEXT;
                    lPersonQuestionnaireAnswer.QTYPEANSWER_AS_WAS = i.QUESTIONDetails.TYPE_ANSWER;
                    vPersonQuestionnaireDetail.PERSON_QUESTIONNAIRE_ANSWERDetails.Add(lPersonQuestionnaireAnswer);
                }

            }
         
        }
        #endregion

    }
}
