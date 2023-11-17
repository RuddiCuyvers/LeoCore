using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Helpers;
using WGK.LMS.Business.Interfaces.Trainings;
using WGK.LMS.Business.Models.Trainings;
using WGK.LMS.Common.Codes;
using WGK.LMS.Common.Constants.Trainings;
using WGK.LMS.Common.Literals;
using LeodbModel;
using WGK.LMS.Data.Interfaces;
using WGK.Lib.Exceptions;
using WGK.Lib.Extensions;
using WGK.Lib.Mappers;
using WGK.Lib.Security;
using WGK.Lib.UseCases;

namespace WGK.LMS.Business.UseCases.Trainings
{
    public abstract class BaseTrainingMaintenanceUseCase<TMaintenanceModel, TTrainingDto>
        : BaseMaintenanceUseCase<TMaintenanceModel>
        where TMaintenanceModel : BaseTrainingMaintenanceModel<TTrainingDto>, new()
        where TTrainingDto : TRAININGDetail, new()
    {

        #region Fields

        protected readonly ITrainingRepository iTrainingRepository;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the BaseTrainingMaintenanceUseCase class.
        /// </summary>
       
        /// <param name="pTrainingRepository">The Training repository.</param>
        protected BaseTrainingMaintenanceUseCase(ITrainingRepository pTrainingRepository)
        {
            this.iTrainingRepository = pTrainingRepository;
        }

        #region Abstract Methods
            /// <summary>
            /// Derived class must implement this method in order to fetch formulier detail table
                /// </summary>
            protected abstract void FetchDetailTable(ref TRAINING pTRAINING);
        #endregion


        #region BaseUseCase overrides
        protected override void ExecuteOverride()
        {
            this.Result = new TMaintenanceModel();
            this.FetchData();
            this.MergeBusinessLogic();
            this.Secure();
        }
        #endregion

        #region Protected Methods - General
        /// <summary>
        /// Fetches the maintenance data.
        /// </summary>
        /// <returns></returns>
        protected virtual void FetchData()
        {
            TTrainingDto vTRAININGDetail;
            if (this.ID == 0)
            {
                // -- Get default data for creating a new Training
                // Create a new Training instance WITHOUT adding it to the context
                // Remark: adding a new instance to the context is done in the UpdateUseCase
                vTRAININGDetail = new TTrainingDto();
            }
            else
            {
                // -- Fetch existing Training from database
                // Remark: repository fetches main Training table and child tables but not the formulier detail table
                var vTraining = new LeodbModel.TRAINING();
                // Fetch formulier detail table (implemented in derived class)
               this.FetchDetailTable(ref vTraining);
                // Map Training (Data layer) to TTrainingDto (Business layer)
                 vTRAININGDetail = MapHelper.MapSingle(vTraining).To<TTrainingDto>();
            }

            this.Result.TRAININGDetail = vTRAININGDetail;
        }

        /// <summary>
        /// Secures the read action.
        /// </summary>
        protected virtual void Secure()
        {
          
        }

        /// <summary>
        /// Merges the business logic for read/create on the base TRAININGDetail DTO instance.
        /// Derived classes must override this method and add business logic for the specific TrainingType (formulier) 
        /// </summary>
        protected virtual void MergeBusinessLogic()
        {
            // Merge business logic on base TRAININGDetail DTO
            this.MergeBusinessLogicForTraining();
            // Merge business logic for TrainingPersoon child table collection
            this.MergeBusinessLogicForTRAINING_QUESTIONNAIRE();
        }
        #endregion

        #region protected Methods - training table
        /// <summary>
        /// Merge business logic on the base TRAININGDetail DTO
        /// </summary>
        protected virtual void MergeBusinessLogicForTraining()
        {
            var vTRAININGDetail = this.Result.TRAININGDetail;

            if (this.ID == 0)
            {
                // -- Set default data for creating a new Training
                if (String.IsNullOrEmpty(vTRAININGDetail.APPLICANT_CLIENTID))
                {
                    vTRAININGDetail.APPLICANT_CLIENTID = this.CurrentUserClientID;
                }
                if (String.IsNullOrEmpty(vTRAININGDetail.TRAINER_INT_EXT))
                {
                    vTRAININGDetail.TRAINER_INT_EXT = "EXT_INT_INT";
                }
                if (String.IsNullOrEmpty(vTRAININGDetail.NOMENCL_CONV_YN))
                {
                    vTRAININGDetail.NOMENCL_CONV_YN = "N";
                }
                if (String.IsNullOrEmpty(vTRAININGDetail.EV_YN))
                {
                    vTRAININGDetail.EV_YN = "N";
                }
            }

        }
        #endregion

        #region protected Methods - TRAINING_QUESTIONNAIRE child table
        /// <summary>
        /// Merges business logic for DossierPersoon child table collection
        /// </summary>
        protected virtual void MergeBusinessLogicForTRAINING_QUESTIONNAIRE()
        {
            var vTRAININGDetail = this.Result.TRAININGDetail;
            // Make sure there is always an DossierPersoon child row of 'Indiener' type for binding to, even if it doesn't exist in the database.
            // This way the fields can be filled in and bound to in the presentation layer.
            if (vTRAININGDetail.TRAINING_QUESTIONNNAIREDetails.Count() < 1)
            {
                var vTrainingQuestionnaire = new WGK.LMS.Business.Dtos.Trainings.TRAINING_QUESTIONNNAIREDetail
                {
                    // Mark row for creation by setting a negative ID
                    ID = -1,
                    TRAINING_ID = vTRAININGDetail.ID
                };
                vTRAININGDetail.TRAINING_QUESTIONNNAIREDetails.Add(vTrainingQuestionnaire);
            }
        }
        #endregion
    }
    #endregion
}

