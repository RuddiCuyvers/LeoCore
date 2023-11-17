﻿using System.Threading;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Interfaces.Trainings;
using LEO.Business.Models.Trainings;

using LEO.Common.Constants.Trainings;

using LEO.Data.Interfaces;
using LEO.Data.Models;
using WGK.Lib.Exceptions;


namespace LEO.Business.UseCases.Trainings
{
    public class TrainingMaintenanceUseCase : BaseTrainingMaintenanceUseCase<TrainingMaintenanceModel, TRAININGDetail>,ITrainingMaintenanceUseCase
    {

        #region Fields
        protected readonly ITrainingCreateUseCase iTrainingCreateUseCase;
        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="TrainingMaintenanceUseCase"/> class.
        /// </summary>
        /// <param name="pTrainingRepository">The Training repository.</param>

        public TrainingMaintenanceUseCase(
           
            ITrainingRepository pTrainingRepository,
            ITrainingCreateUseCase pTrainingCreateUseCase
            )
            : base( pTrainingRepository )
            {
                iTrainingCreateUseCase = pTrainingCreateUseCase;
            }
        #endregion

        #region Protected Methods
        /// <summary>
        /// Fetches Training detail table from database
        /// </summary>
        protected override void FetchDetailTable(ref TRAINING pTraining)
        {
            var vTraining = this.iTrainingRepository.GetTRAINING(
                pID: (int)this.ID,
                pIncludeSoftDeleted: false,
                pIncludeAllData: true,
                pNoTracking: false);
            if (vTraining == null)
            {
                throw new NoResultFoundException(
                    "Training ID",
                    this.ID);
            }

            pTraining = vTraining;

        }

        /// <summary>
        /// Merges the business logic for read/create.
        /// </summary>
        protected override void MergeBusinessLogic()
        {
            // Merges the business logic on the base DossierDetail DTO and child table collections
            base.MergeBusinessLogic();

            // Merge business logic on TRAININGDetail
            this.MergeBusinessLogicForTraining();
        }
        #endregion

        #region Protected Methods - Training table
        /// <summary>
        /// Merge business logic on TRAININGDetail
        /// </summary>
        private void MergeBusinessLogicForTraining()
        {
           
            var vTRAININGDetail = this.Result.TRAININGDetail;

            if (this.ID == 0)
            {

            }
        }
        #endregion

    }
}
