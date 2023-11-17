using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Models.Trainings;



namespace LEO.Business.Interfaces.Trainings
{
    public interface IBaseTrainingUpdateUseCase<TUpdateModel, TTrainingDto>
        
        where TUpdateModel : BaseTrainingUpdateModel<TTrainingDto>
        where TTrainingDto : TRAININGDetail
    {
    }

}
