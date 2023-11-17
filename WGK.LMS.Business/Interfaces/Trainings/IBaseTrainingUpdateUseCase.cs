using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Models.Trainings;
using WGK.Lib.UseCases;


namespace WGK.LMS.Business.Interfaces.Trainings
{
    public interface IBaseTrainingUpdateUseCase<TUpdateModel, TTrainingDto>
        : IBaseUpdateUseCase<TUpdateModel>
        where TUpdateModel : BaseTrainingUpdateModel<TTrainingDto>
        where TTrainingDto : TRAININGDetail
    {
    }

}
