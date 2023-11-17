using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Interfaces.Trainings;
using WGK.LMS.Business.Models.IBF;
using WGK.LMS.Common.Codes;
using WGK.LMS.Common.Constants.Trainings;
using WGK.Lib.Security;
using WGK.Lib.UseCases;
using WGK.LMS.Business.Interfaces.IBF;
using WGK.LMS.Business.Dtos.IBF;

namespace WGK.LMS.Business.UseCases.IBF
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
