using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Models.Questionnaires;
using LeoCore.Data.Models;

namespace LEO.Business.Interfaces.Questionnaires
{
    // Although not strictly needed (because there are no extra properties), we define a ITrainingMaintenanceUseCase interface
    // as a placeholder for the generic interface IBaseMaintenanceUseCase<TrainingMaintenanceModel>.

    public interface IPersonQuestionnaireMaintenanceUseCase
        
    {
        bool IsLeraar { get; set; }

        public void FetchQuestionnaireDetailTable(ref QUESTIONNAIRE pQuestionnaire) { }
    }
}
