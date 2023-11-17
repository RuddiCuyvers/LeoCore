using System.Threading;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Business.Interfaces.Trainings;
using WGK.LMS.Business.Models.Trainings;
using WGK.LMS.Common.Codes;
using WGK.LMS.Common.Constants.Trainings;
using LeodbModel;
using WGK.LMS.Data.Interfaces;

using WGK.Lib.Exceptions;


namespace WGK.LMS.Business.UseCases.Trainings
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
