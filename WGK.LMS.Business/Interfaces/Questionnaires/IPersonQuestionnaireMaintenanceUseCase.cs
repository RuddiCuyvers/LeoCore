using WGK.LMS.Business.Dtos.Questionnaires;
using WGK.LMS.Business.Models.Questionnaires;

namespace WGK.LMS.Business.Interfaces.Questionnaires
{
    // Although not strictly needed (because there are no extra properties), we define a ITrainingMaintenanceUseCase interface
    // as a placeholder for the generic interface IBaseMaintenanceUseCase<TrainingMaintenanceModel>.

    public interface IPersonQuestionnaireMaintenanceUseCase
        : IBasePersonQuestionnaireMaintenanceUseCase<PersonQuestionnaireMaintenanceModel, QUESTIONNAIREDetail, PERSON_QUESTIONNAIREDetail>
    {
        bool IsLeraar { get; set; }
    }
}
