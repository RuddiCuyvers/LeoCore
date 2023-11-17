using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Models.Trainings;

namespace WGK.LMS.Business.Interfaces.Trainings
{
    // Although not strictly needed (because there are no extra properties), we define a ITrainingMaintenanceUseCase interface
    // as a placeholder for the generic interface IBaseMaintenanceUseCase<TrainingMaintenanceModel>.

    public interface ITrainingMaintenanceUseCase
        : IBaseTrainingMaintenanceUseCase<TrainingMaintenanceModel, TRAININGDetail>
    {
    }
}
