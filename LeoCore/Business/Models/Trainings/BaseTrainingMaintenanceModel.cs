using LEO.Business.Dtos.Trainings;

namespace LEO.Business.Models.Trainings
{
    public abstract class BaseTrainingMaintenanceModel<TTrainingDto>
        where TTrainingDto : TRAININGDetail
    {
        #region Constructors
        protected BaseTrainingMaintenanceModel()
        {
            //// TrainingMaintenance uses Workflow: make sure WorkflowTransitions is never null
            //this.WorkflowTransitions = new List<WorkflowTransition>();
        }
        #endregion

        #region Public Properties
        /// <summary>
        /// The specific TRAININGDetail business-layer DTO class instance retrieved from database.
        /// </summary>
        public TTrainingDto TRAININGDetail { get; set; }

        ///// <summary>
        ///// Allowed WorkflowTransitions from the current TrainingStatus
        ///// </summary>
        //public List<WorkflowTransition> WorkflowTransitions { get; set; }
        #endregion
    }
}
