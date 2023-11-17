using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Models.Trainings;

namespace WGK.LMS.Business.Interfaces.Trainings
{
    public interface ITrainingUpdateUseCase
        : IBaseTrainingUpdateUseCase<TrainingUpdateModel, TRAININGDetail>
    {
    }
}
