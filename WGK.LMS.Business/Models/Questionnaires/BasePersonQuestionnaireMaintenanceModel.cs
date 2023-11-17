using WGK.LMS.Business.Dtos.Questionnaires;

namespace WGK.LMS.Business.Models.Questionnaires
{
    public abstract class BasePersonQuestionnaireMaintenanceModel<TQuestionnaireDto, TPersonQuestionnaireDto>
        where TQuestionnaireDto : QUESTIONNAIREDetail
        where TPersonQuestionnaireDto : PERSON_QUESTIONNAIREDetail
    {
        #region Constructors
        protected BasePersonQuestionnaireMaintenanceModel()
        {
      
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The specific QuestionnaireDetail business-layer DTO class instance retrieved from database.
        /// </summary>
        public TQuestionnaireDto QuestionnaireDetail { get; set; }

        public TPersonQuestionnaireDto PersonQuestionnaireDetail { get; set; }

        #endregion
    }
}
