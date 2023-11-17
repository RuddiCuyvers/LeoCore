using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading;
using LEO.Business.Helpers;

using WGK.Lib.DataAnnotations;
using WGK.Lib.Exceptions;
using WGK.Lib.Extensions;
using WGK.Lib.Ioc;

using WGK.Lib.Security;
using WGK.Lib.UseCases;
using WGK.Lib.Validation;

using LEO.Common.Codes;
using LEO.Common.Constants.Trainings;
using LEO.Common.Literals;

using LeoCore.Data.Models;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Interfaces.IBF;
using LEO.Business.Dtos.IBF;
using WGK.Lib.UserCodes;


namespace LEO.Business.UseCases.IBF
{
    public class IBFSearchUseCase : BaseUseCase, IIBFSearchUseCase
    {
        #region Fields
        private readonly IPersonQuestionnaireRepository iPersonQuestionnaireRepository;
        //private readonly IPersonTrainingRepository iPersonTrainingRepository;

        #endregion

        #region Constructors

        public IBFSearchUseCase(
            IPersonQuestionnaireRepository pPersonQuestionnaireRepository
            )
        {
            this.iPersonQuestionnaireRepository = pPersonQuestionnaireRepository;
            
        }
        #endregion

        #region ITrainingIBFIdentificationUseCase Members
        public IBFSearchCriteria SearchCriteria { get; set; }

        public ICollection<IBFInfo> Result { get; private set; }
        #endregion

        #region BaseUseCase overrides
        protected override void ExecuteOverride()
        {
            if (this.SearchCriteria == null)
            {
                throw new ParameterMissingException(
                    CommonLiterals.SearchCriteriaDisplayName);
            }

            if (this.iValidationDictionary == null)
            {
                // Client must set ValidationDictionary through the property on the UseCase
                throw new ParameterMissingException("ValidationDictionary");
            }

            if (this.Validate())
            {
                this.Secure();
                this.FetchData();
                this.MergeBusinessLogic();
            }
            else
            {
                // Return null to indicate validation errors
                this.Result = null;
            }
        }
        #endregion

        #region Private methods
        private void Secure()
        {
            
        }

        private bool Validate()
        {
            this.ValidatePagingSortingFiltering(this.SearchCriteria);
            return this.iValidationDictionary.IsValid;
        } 

      
        private IQueryable<PERSON_QUESTIONNAIRE> GetQueryForSearchCriteria()
        {
            // Get the query
            IQueryable<PERSON_QUESTIONNAIRE> vPersonQuestionnaireQuery = this.iPersonQuestionnaireRepository.FindAllPERSON_QUESTIONNAIREs();

            // Add search criteria to query
            // CLIENT_ID search criterion
            if (!this.SearchCriteria.CLIENT_ID.IsNullOrEmptyOrBlankCodeForSearchIdentification())
            {
                // Search on UserCode value (from DropdownList) is alway on Equal
                vPersonQuestionnaireQuery = vPersonQuestionnaireQuery
                        .Where(p => p.CLIENT_ID.Trim()
                            .Equals(this.SearchCriteria.CLIENT_ID.Trim()));
            }
            // JAARTAL search criterion
            if (!this.SearchCriteria.JAARTAL.IsNullOrEmptyOrBlankCodeForSearchIdentification())
            {
                int lJAARTAL = int.Parse(this.SearchCriteria.JAARTAL);
                // Search on UserCode value (from DropdownList) is alway on Equal
                vPersonQuestionnaireQuery = vPersonQuestionnaireQuery
                    .Where(p => p.DATE_SUBMITTED.Value.Year
                     .Equals(lJAARTAL));

            }
            // INTERN EXTERN search criterion
            if (!this.SearchCriteria.INTERNEXTERN.IsNullOrEmptyOrBlankCodeForSearchIdentification())
            {
                // Search on UserCode value (from DropdownList) is alway on Equal
                vPersonQuestionnaireQuery = vPersonQuestionnaireQuery
                    .Where(p => p.TRAINING.TRAINER_INT_EXT.Trim()
                        .Equals(this.SearchCriteria.INTERNEXTERN.Trim()));

            }


            return vPersonQuestionnaireQuery;
        }

        /// <summary>
        /// Fetch identification data from database
        /// </summary>
        private void FetchData()
        {
            // Get the search query
            IQueryable<PERSON_QUESTIONNAIRE> vPersonQuestionnaireQuery = this.GetQueryForSearchCriteria();

            // Projection
            IQueryable<IBFInfo> vProjectionQuery = vPersonQuestionnaireQuery
                .Select(p => new IBFInfo
                {
                    Person_TrainingID = p.ID, 
                    TrainingID = p.TRAINING.ID,
                    SUBJECT = p.TRAINING.SUBJECT,
                    TRAINING_TYPE = p.TRAINING.TRAINING_TYPE,
                    NOMENCL_CONV_YN = (p.TRAINING.NOMENCL_CONV_YN == "N" ? "Nee" : "Ja"),
                    METHODOLOGY = p.TRAINING.METHODOLOGY,
                    //DATUMTRAINING = p.DATE_SUBMITTED,
                    LINK = p.TRAINING.LINK

                }); ;

            // Get the row count and add paging, sorting and filtering
            vProjectionQuery = AddPagingSortingFiltering(
                vProjectionQuery,
                this.SearchCriteria);

            // Execute query to fetch data
            this.Result = vProjectionQuery.ToList();
        }

        /// <summary>
        ///  Perform business logic on the retrieved data
        /// </summary>
        private void MergeBusinessLogic()
        {
//ToDo:   kijken bij DOSWLDS
        }
        #endregion
    }
}
