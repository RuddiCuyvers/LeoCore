using System;
using System.Collections.Generic;
using LEO.Business.Dtos.Trainings;
using WGK.Lib.UseCases;
using LEO.Business.Dtos.Trainings;

namespace LEO.Business.Interfaces.Trainings
{
    public interface ITrainingSearchUseCase : IBaseUseCase
    {
        TrainingSearchCriteria SearchCriteria { get; set; }
        ICollection<TrainingInfo> Result { get; }
    }
}
