using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Models.Trainings;
using WGK.Lib.UseCases;


namespace WGK.LMS.Business.Interfaces.Trainings
{
    public interface IBaseTrainingMaintenanceUseCase<TMaintenanceModel, TTrainingDto>
        : IBaseMaintenanceUseCase<TMaintenanceModel>
        where TMaintenanceModel : BaseTrainingMaintenanceModel<TTrainingDto>, new()
        where TTrainingDto : TRAININGDetail, new()
    {

    }
}
