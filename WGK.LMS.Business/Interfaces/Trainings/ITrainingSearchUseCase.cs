using System;
using System.Collections.Generic;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.Lib.UseCases;
using WGK.LMS.Business.Dtos.Trainings;

namespace WGK.LMS.Business.Interfaces.Trainings
{
    public interface ITrainingSearchUseCase : IBaseUseCase
    {
        TrainingSearchCriteria SearchCriteria { get; set; }
        ICollection<TrainingInfo> Result { get; }
    }
}
