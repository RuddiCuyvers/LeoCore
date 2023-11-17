using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Questionnaires;

namespace WGK.LMS.Business.Models.Questionnaires
{
    /// <summary>
    /// Abstract generic Questionnaire Update Model base class for updating data coming from the presentation layer.
    /// This class provides properties that are common to all QuestionnaireTypes. 
    /// </summary>
    /// <typeparam name="TQuestionnaireDto">The DTO type for the specific Questionnaire. The DTO class must derive from QuestionnaireDetail</typeparam>
    public abstract class BasePersonQuestionnaireUpdateModel<TQuestionnaireDto, TPersonQuestionnaireDto>
        where TQuestionnaireDto : QUESTIONNAIREDetail
        where TPersonQuestionnaireDto : PERSON_QUESTIONNAIREDetail
    {
        /// <summary>
        /// The specific QuestionnaireDetail business-layer DTO class instance to update to database.
        /// </summary>
        public TQuestionnaireDto QuestionnaireDetail { get; set; }

        public TPersonQuestionnaireDto PersonQuestionnaireDetail { get; set; }

        /// <summary>
        /// WorkflowEvent (transition) to trigger during the create/update process
        /// </summary>
        public string WorkflowEventID { get; set; }
    }
}
