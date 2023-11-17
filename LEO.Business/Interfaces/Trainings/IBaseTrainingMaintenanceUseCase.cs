using LEO.Business.Dtos.Trainings;
using LEO.Business.Models.Trainings;



namespace LEO.Business.Interfaces.Trainings
{
    public interface IBaseTrainingMaintenanceUseCase<TMaintenanceModel, TTrainingDto>
        
        where TMaintenanceModel : BaseTrainingMaintenanceModel<TTrainingDto>, new()
        where TTrainingDto : TRAININGDetail, new()
    {

    }
}
