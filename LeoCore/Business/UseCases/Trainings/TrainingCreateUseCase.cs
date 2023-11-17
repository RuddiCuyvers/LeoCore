using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Threading;

using LEO.Business.Helpers;
using WGK.Lib.DataAnnotations;
using WGK.Lib.Exceptions;
using WGK.Lib.Extensions;
using WGK.Lib.Ioc;
using WGK.Lib.Mappers;

using WGK.Lib.Security;
using WGK.Lib.UseCases;

using WGK.Lib.Validation;
using LEO.Common.Constants.Trainings;
using LEO.Common.Literals;

using LEO.Data.Interfaces;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Interfaces.Trainings;

namespace LEO.Business.UseCases.Trainings
{
    public class TrainingCreateUseCase : BaseUseCase, ITrainingCreateUseCase
    {
        #region Fields
        private readonly ITrainingRepository iTrainingRepository;
  
        #endregion


        #region ITrainingIdentificationUseCase Members
        public TRAININGDetail CreateData { get; set; }
        public decimal Result { get; private set; }
        #endregion

        #region BaseUseCase overrides
        protected override void ExecuteOverride()
        {
            if (this.CreateData == null)
            {
                throw new ParameterMissingException(
                    "CreateData missing for Training");
            }

            //if (this.iValidationDictionary == null)
            //{
            //    // Client must set ValidationDictionary through the property on the UseCase
            //    throw new ParameterMissingException("ValidationDictionary");
            //}

            if (this.Validate())
            {
                //this.Secure();
                this.FetchData();
            }
            else
            {
                // Return null to indicate validation errors
                this.Result = 0;
            }
        }
        #endregion

        #region Private methods
        /// <summary>
        /// Secures the search action.
        /// </summary>
        private void Secure()
        {
            
        }

        /// <summary>
        /// Validate the create data
        /// </summary>
        private bool Validate()
        {
            //return this.iValidationDictionary.IsValid;
            return true;
        }

        /// <summary>
        /// Fetch identification data from database
        /// </summary>
        private void FetchData()
        {
           
        }

        #endregion
    }
}
