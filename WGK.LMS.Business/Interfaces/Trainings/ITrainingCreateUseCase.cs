using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.Lib.UseCases;

namespace WGK.LMS.Business.Interfaces.Trainings
{
    public interface ITrainingCreateUseCase : IBaseUseCase
    {
        TRAININGDetail CreateData { get; set; }
        decimal Result { get; }
    }
}
