using LEO.Business.Dtos.Questionnaires;

namespace LEO.Business.Models.Questionnaires
{
    // Although not strictly needed (because there are no extra properties), we define a QuestionnaireMaintenanceModel
    // as a placeholder for the generic BaseQuestionnaireMaintenanceModel<QuestionnaireDetail>.

    /// <summary>
    /// Questionnaire Maintenance data returned to the presentation layer
    /// </summary>
    public class PersonQuestionnaireMaintenanceModel : BasePersonQuestionnaireMaintenanceModel<Business.Dtos.Questionnaires.QUESTIONNAIREDetail, Business.Dtos.Questionnaires.PERSON_QUESTIONNAIREDetail>
    {
    }
}
