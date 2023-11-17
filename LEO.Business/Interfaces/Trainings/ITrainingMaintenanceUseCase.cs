using LEO.Business.Dtos.Trainings;
using LEO.Business.Models.Trainings;

namespace LEO.Business.Interfaces.Trainings
{
    // Although not strictly needed (because there are no extra properties), we define a ITrainingMaintenanceUseCase interface
    // as a placeholder for the generic interface IBaseMaintenanceUseCase<TrainingMaintenanceModel>.

    public interface ITrainingMaintenanceUseCase
        : IBaseTrainingMaintenanceUseCase<TrainingMaintenanceModel, TRAININGDetail>
    {
    }
}
