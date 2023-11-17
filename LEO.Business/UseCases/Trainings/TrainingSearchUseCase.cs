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


using LEO.Common.Constants.Trainings;
using LEO.Common.Literals;

using LEO.Data.Interfaces;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Interfaces.Trainings;
using LEO.Data.Models;

namespace LEO.Business.UseCases.Trainings
{
    public class TrainingSearchUseCase : BaseUseCase, ITrainingSearchUseCase
    {
        #region Fields
        private readonly ITrainingRepository iTrainingRepository;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingSearchUseCase"/> class.
        /// </summary>
        /// <param name="pTrainingRepository">The Training repository.</param>
        public TrainingSearchUseCase(
            ITrainingRepository pTrainingRepository
            )
        {
            this.iTrainingRepository = pTrainingRepository;
            
        }
        #endregion

        #region ITrainingIdentificationUseCase Members
        public TrainingSearchCriteria SearchCriteria { get; set; }

        public ICollection<TrainingInfo> Result { get; private set; }
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
        /// <summary>
        /// Secures the search action.
        /// </summary>
        private void Secure()
        {
            //// Check if user has Search permission on TrainingAlle task

            //// Get our custom TaskPrincipal from the current thread
            //// Remark: don't use System.Security.Principal.WindowsIdentity.GetCurrent() since this returns the ASP.NET account!!!
            //var vTaskPrincipal = Thread.CurrentPrincipal as ITaskPrincipal;
            //if (vTaskPrincipal == null)
            //{
            //    throw new WGK.Lib.Exceptions.AuthenticationException();
            //}

            //if (!vTaskPrincipal.HasPermissionOnTask(TaskCode.TrainingAlle, PermissionCode.Search))
            //{
            //    throw new WGK.Lib.Exceptions.SearchDeniedException(
            //        vTaskPrincipal.UserName,
            //        TrainingDisplayNames.cTrainingEntityDisplayName);
            //}
        }

        /// <summary>
        /// Validate the search criteria
        /// </summary>
        private bool Validate()
        {
            this.ValidatePagingSortingFiltering(this.SearchCriteria);


            // Example: required search parameter is missing (TrainingTypeID)
            //if (this.SearchCriteria.TrainingTypeID.IsNullOrEmptyOrBlankCode())
            //{
            //    this.iValidationDictionary.AddError(
            //    "SearchCriteria" + "." + TrainingSearchCriteria.TrainingTypeIDFieldName,
            //    string.Format(
            //        DataAnnotationLiterals.RequiredErrorMessage,
            //        TrainingDisplayNames.cTrainingTypeIDDisplayName));                
            //}

            return this.iValidationDictionary.IsValid;
        }

        /// <summary>
        /// Returns a Training Query for the search criteria
        /// </summary>
        /// <returns></returns>
        private IQueryable<TRAINING> GetQueryForSearchCriteria()
        {
            // Get the query
            IQueryable<TRAINING> vTrainingQuery = this.iTrainingRepository.FindAllTRAININGs();

            // If primary key search criteria is filled in then search on primary key only
            if ((this.SearchCriteria.TrainingID != null) && (this.SearchCriteria.TrainingID.Value != 0))
            {
                vTrainingQuery = vTrainingQuery
                    .Where(p => p.ID == this.SearchCriteria.TrainingID.Value);
            }
            else
            {
                // Add search criteria to query
                // Remark: search on a string value can be either on Equals, StartsWith or Contains
                //StartScreenModus search criterion
                if (this.SearchCriteria.StartScreenModus)
                {
                    // On the startscreen only those Training are shown where the current user is Trainingbeheerder
                    // AND where there is a deadline on the current TrainingStatus (= last TrainingStap).
                    //ToDo:   //var vPrincipal = Thread.CurrentPrincipal as IPersoneelPrincipal;
                    //vTrainingQuery = vTrainingQuery
                    //    .Where(p => p.TRAINING_SPECIALISATION == vPrincipal.specialsiusee);
                }
                // TrainingType search criterion
                if (!this.SearchCriteria.TRAINING_TYPE.IsNullOrEmptyOrBlankCodeForSearchIdentification())
                {
                    // Search on UserCode value (from DropdownList) is alway on Equal
                    vTrainingQuery = vTrainingQuery
                        .Where(p => p.TRAINING_TYPE.Trim()
                            .Equals(this.SearchCriteria.TRAINING_TYPE.Trim()));
                }
                // Nomenclatuur search criterion
                if (!this.SearchCriteria.NOMENCLATUUR_YN.IsNullOrEmptyOrBlankCodeForSearchIdentification())
                {
                    // Search on UserCode value (from DropdownList) is alway on Equal
                    vTrainingQuery = vTrainingQuery
                        .Where(p => p.NOMENCL_CONV_YN.Trim()
                            .Equals(this.SearchCriteria.NOMENCLATUUR_YN.Trim()));
                }
                
             
                if (!this.SearchCriteria.SUBJECT.IsNullOrEmptyOrBlankCodeForSearchIdentification() && this.SearchCriteria.SUBJECT.Length > 2)
                {
                    vTrainingQuery = vTrainingQuery
                        .Where(p => p.SUBJECT.Trim().ToLower()
                            .Contains(this.SearchCriteria.SUBJECT.Trim().ToLower()));
                }
                if (!this.SearchCriteria.EVIDENCE_BASED.IsNullOrEmptyOrBlankCodeForSearchIdentification())
                {
                    vTrainingQuery = vTrainingQuery
                        .Where(p => p.EV_YN.Trim()
                            .Equals(this.SearchCriteria.EVIDENCE_BASED.Trim()));
                }
                if (!this.SearchCriteria.INTERNEXTERN.IsNullOrEmptyOrBlankCodeForSearchIdentification())
                {
                    vTrainingQuery = vTrainingQuery
                        .Where(p => p.TRAINER_INT_EXT.Trim()
                            .Equals(this.SearchCriteria.INTERNEXTERN.Trim()));
                }
                if (!this.SearchCriteria.APPLICANT_CLIENT_ID.IsNullOrEmptyOrBlankCodeForSearchIdentification())
                {
                    vTrainingQuery = vTrainingQuery
                        .Where(p => p.APPLICANT_CLIENTID.Trim()
                            .Equals(this.SearchCriteria.APPLICANT_CLIENT_ID.Trim()));
                }
            }
            return vTrainingQuery;
        }

        /// <summary>
        /// Fetch identification data from database
        /// </summary>
        private void FetchData()
        {
            // Get the search query
            IQueryable<TRAINING> vTrainingQuery = this.GetQueryForSearchCriteria();

            // Projection
            IQueryable<TrainingInfo> vProjectionQuery = vTrainingQuery
                .Select(p => new TrainingInfo
                {
                    TrainingID = p.ID,
                    TRAINING_TYPE = p.TRAINING_TYPE,
                    LINK = p.LINK,
                    ONDERWERP = p.SUBJECT,
                    INTERNEXTERN = p.TRAINER_INT_EXT,
                    EVIDENCEBASED_YN = (p.EV_YN == "N" ? "Nee" : "Ja"),
                    NOMENCLATUUR_YN = (p.NOMENCL_CONV_YN == "N" ? "Nee" : "Ja")
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
