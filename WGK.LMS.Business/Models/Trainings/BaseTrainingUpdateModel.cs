using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Trainings;

namespace WGK.LMS.Business.Models.Trainings
{
    /// <summary>
    /// Abstract generic Training Update Model base class for updating data coming from the presentation layer.
    /// This class provides properties that are common to all TrainingTypes. 
    /// </summary>
    /// <typeparam name="TTrainingDto">The DTO type for the specific Training. The DTO class must derive from TRAININGDetail</typeparam>
    public abstract class BaseTrainingUpdateModel<TTrainingDto>
        where TTrainingDto : TRAININGDetail
    {
        /// <summary>
        /// The specific TRAININGDetail business-layer DTO class instance to update to database.
        /// </summary>
        public TTrainingDto TRAININGDetail { get; set; }

        /// <summary>
        /// WorkflowEvent (transition) to trigger during the create/update process
        /// </summary>
        public string WorkflowEventID { get; set; }
    }
}
