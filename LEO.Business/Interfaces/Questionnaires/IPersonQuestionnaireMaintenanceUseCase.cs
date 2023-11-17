using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Models.Questionnaires;

namespace LEO.Business.Interfaces.Questionnaires
{
    // Although not strictly needed (because there are no extra properties), we define a ITrainingMaintenanceUseCase interface
    // as a placeholder for the generic interface IBaseMaintenanceUseCase<TrainingMaintenanceModel>.

    public interface IPersonQuestionnaireMaintenanceUseCase
        : IBasePersonQuestionnaireMaintenanceUseCase<PersonQuestionnaireMaintenanceModel, QUESTIONNAIREDetail, PERSON_QUESTIONNAIREDetail>
    {
        bool IsLeraar { get; set; }
    }
}
