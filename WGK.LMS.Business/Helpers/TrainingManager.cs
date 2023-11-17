using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Interfaces.Trainings;
using WGK.LMS.Data.Interfaces;

namespace WGK.LMS.Business.Helpers
{
    public class TrainingManager : ITrainingManager
    {

        private readonly WGK.LMS.Data.Interfaces.ITrainingRepository iTrainingRepository;

        public TrainingManager(ITrainingRepository pTrainingRepository)
        {
            iTrainingRepository = pTrainingRepository;
        }

        

    }
}
