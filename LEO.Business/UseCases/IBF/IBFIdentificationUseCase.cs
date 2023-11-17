using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Interfaces.Trainings;
using LEO.Business.Models.IBF;
using LEO.Common.Codes;
using LEO.Common.Constants.Trainings;
using WGK.Lib.Security;
using WGK.Lib.UseCases;
using LEO.Business.Interfaces.IBF;
using LEO.Business.Dtos.IBF;

namespace LEO.Business.UseCases.IBF
{
    public class IBFIdentificationUseCase : BaseUseCase, IIBFIdentificationUseCase
    {
        #region IIBFSearchCriteriaUseCase Properties implementation
        public IBFIdentificationModel Result { get; private set; }
        #endregion

        #region BaseUseCase overrides
        protected override void ExecuteOverride()
        {
            this.Result = new IBFIdentificationModel();
            this.Secure();
            this.Result.SearchCriteria = new IBFSearchCriteria();
           
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
