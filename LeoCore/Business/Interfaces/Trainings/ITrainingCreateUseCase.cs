using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Dtos.Trainings;


namespace LEO.Business.Interfaces.Trainings
{
    public interface ITrainingCreateUseCase { 
        TRAININGDetail CreateData { get; set; }
        decimal Result { get; }
    }
}
