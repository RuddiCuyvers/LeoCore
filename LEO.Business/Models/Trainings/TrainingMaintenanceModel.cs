using LEO.Business.Dtos.Trainings;

namespace LEO.Business.Models.Trainings
{
    // Although not strictly needed (because there are no extra properties), we define a TrainingMaintenanceModel
    // as a placeholder for the generic BaseTrainingMaintenanceModel<TRAININGDetail>.

    /// <summary>
    /// Training Maintenance data returned to the presentation layer
    /// </summary>
    public class TrainingMaintenanceModel : BaseTrainingMaintenanceModel<Business.Dtos.Trainings.TRAININGDetail>
    {
    }
}
