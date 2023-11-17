using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeoCore.Data.Models;

namespace LeoCore.Data
{ 
    public interface ITrainingRepository
    {

        #region Training entity table
        /// <summary>
        /// Finds all TRAININGs.
        /// </summary>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <returns>Query of TRAINING</returns>
        IQueryable<TRAINING> FindAllTRAININGs(bool pIncludeSoftDeleted = false);

        /// <summary>
        /// Fetches the Training.
        /// </summary>
        /// <param name="pTrainingID">The Training ID.</param>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <param name="pIncludeAllData">if set to <c>true</c> [p include all data].</param>
        /// <param name="pNoTracking">Set to true to fetch Training without tracking.</param>
        /// <returns></returns>
        TRAINING GetTRAINING(
            int pID,
            bool pIncludeSoftDeleted = false,
            bool pIncludeAllData = false,
            bool pNoTracking = false);

        /// <summary>
        ///  Fetches the Trainingtype
        /// </summary>
        /// <param name="pTrainingID">The Training ID.</param>
        /// <returns></returns>
        string GetTrainingType(int pTrainingID);

        void Save();
        /// <summary>
        /// Adds the specified Training to the context and mark for for creation.
        /// </summary>
        /// <param name="pTraining">The Training instance.</param>
        void AddTraining(TRAINING pTraining);

        /// <summary>
        /// Mark the specified Training for deletion.
        /// </summary>
        /// <param name="pTrainingID">The Training ID.</param>
        /// <param name="pRowVersion">The row version.</param>
        /// <param name="pHardDelete">If true then remove row from database, otherwise perform a soft delete (default).</param>
        void DeleteTraining(int pTrainingID, byte[] pRowVersion, bool pHardDelete = false);

        


        #endregion

       



    }
}
