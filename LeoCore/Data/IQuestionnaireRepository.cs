using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeoCore.Data.Models;

namespace LeoCore.Data
{
    public interface IQuestionnaireRepository 
    {

        #region Questionnaire entity table
        /// <summary>
        /// Finds all QUESTIONNAIREs.
        /// </summary>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <returns>Query of QUESTIONNAIRE</returns>
        IQueryable<QUESTIONNAIRE> FindAllQUESTIONNAIREs(bool pIncludeSoftDeleted = false);

        /// <summary>
        /// Fetches the QUESTIONNAIRE.
        /// </summary>
        /// <param name="pID">The Questionnaire ID.</param>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <param name="pIncludeAllData">if set to <c>true</c> [p include all data].</param>
        //QUESTIONNAIRE        /// <returns></returns>
        QUESTIONNAIRE GetQUESTIONNAIRE(
            int pID,
            bool pIncludeSoftDeleted = false,
            bool pIncludeAllData = false,
            bool pNoTracking = false);

        /// <summary>
        /// Fetches the QUESTIONNAIRE.
        /// </summary>
        /// <param name="pID">The Questionnaire ID.</param>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <param name="pIncludeAllData">if set to <c>true</c> [p include all data].</param>
        //QUESTIONNAIRE        /// <returns></returns>
        QUESTIONNAIRE GetQUESTIONNAIREByTRAININGID(
            int pTrainingID,
            bool pIncludeSoftDeleted = true,
            bool pIncludeAllData = false,
            bool pNoTracking = false);

        /// <summary>
        /// Adds the specified Questionnaire to the context and mark for for creation.
        /// </summary>
        /// <param name="pQuestionnaire">The QUESTIONNAIRE instance.</param>
        void AddQUESTIONNAIRE(QUESTIONNAIRE pQuestionnaire);

        /// <summary>
        /// Mark the specified QUESTIONNAIRE for deletion.
        /// </summary>
        /// <param name="pQuestionnaireID">The Questionnaire ID.</param>
        /// <param name="pRowVersion">The row version.</param>
        /// <param name="pHardDelete">If true then remove row from database, otherwise perform a soft delete (default).</param>
        void DeleteQUESTIONNAIRE(int pQuestionnaireID, byte[] pRowVersion, bool pHardDelete = false);

        


        #endregion

       



    }
}
