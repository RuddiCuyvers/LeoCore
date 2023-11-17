using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.Lib.UseCases;
using WGK.LMS.Business.Models.Trainings;


namespace WGK.LMS.Business.Interfaces.Trainings
{
    public interface  ITrainingIdentificationUseCase : IBaseUseCase
    {

        TrainingIdentificationModel Result { get; }
    }
}
