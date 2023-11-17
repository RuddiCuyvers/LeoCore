using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Interfaces.Trainings;
using LeoCore.Data;

namespace LEO.Business.Helpers
{
    public class TrainingManager : ITrainingManager
    {

        private readonly LeoCore.Data.ITrainingRepository iTrainingRepository;

        public TrainingManager(ITrainingRepository pTrainingRepository)
        {
            iTrainingRepository = pTrainingRepository;
        }

        

    }
}
