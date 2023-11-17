using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Interfaces.Trainings;
using LEO.Business.Models.Trainings;
using LEO.Common.Codes;
using LEO.Common.Constants.Trainings;
using WGK.Lib.Security;
using WGK.Lib.UseCases;

namespace LEO.Business.UseCases.Trainings
{
    public class TrainingIdentificationUseCase : BaseUseCase, ITrainingIdentificationUseCase
    {
        #region ITrainingSearchCriteriaUseCase Properties implementation
        public TrainingIdentificationModel Result { get; private set; }
        #endregion

        #region BaseUseCase overrides
        protected override void ExecuteOverride()
        {
            this.Result = new TrainingIdentificationModel();
            this.Secure();
            // -- Set default search criteria
            this.Result.SearchCriteria = new TrainingSearchCriteria();
           
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Secures the search action.
        /// </summary>
        private void Secure()
        {
            
        }
        #endregion
    }
}
