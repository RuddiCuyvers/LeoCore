using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeoCore.Data.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace LeoCore.Data
{
    public interface IPersonQuestionnaireRepository 
    {

        #region PersonQuestionnaire entity table
        /// <summary>
        /// Finds all PERSON_QUESTIONNAIREs.
        /// </summary>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <returns>Query of PERSON_QUESTIONNAIRE</returns>
        IQueryable<PERSON_QUESTIONNAIRE> FindAllPERSON_QUESTIONNAIREs(bool pIncludeSoftDeleted = false);

        /// <summary>
        /// Fetches the PERSON_QUESTIONNAIRE.
        /// </summary>
        /// <param name="pID">The Questionnaire ID.</param>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <param name="pIncludeAllData">if set to <c>true</c> [p include all data].</param>
        //PERSON_QUESTIONNAIRE        /// <returns></returns>
        PERSON_QUESTIONNAIRE GetPERSON_QUESTIONNAIRE(
            int pID,
            bool pIncludeSoftDeleted = false,
            bool pIncludeAllData = false,
            bool pNoTracking = false);

        /// <summary>
        /// Fetches the PERSON_QUESTIONNAIRE.
        /// </summary>
        /// <param name="pID">The Questionnaire ID.</param>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <param name="pIncludeAllData">if set to <c>true</c> [p include all data].</param>
        //PERSON_QUESTIONNAIRE        /// <returns></returns>
        PERSON_QUESTIONNAIRE GetPERSON_QUESTIONNAIREByTRAININGID(
            int pTrainingID,
            bool pIncludeSoftDeleted = true,
            bool pIncludeAllData = false,
            bool pNoTracking = false);


        PERSON_QUESTIONNAIRE GetPERSON_QUESTIONNAIREByTRAININGIDAndByCLIENTID(
           int pTrainingID,
           string pClientID,
           bool pIncludeSoftDeleted = true,
           bool pIncludeAllData = false,
           bool pNoTracking = false);

        /// <summary>
        /// Adds the specified Questionnaire to the context and mark for for creation.
        /// </summary>
        /// <param name="pQuestionnaire">The PERSON_QUESTIONNAIRE instance.</param>
        void AddPERSON_QUESTIONNAIRE(PERSON_QUESTIONNAIRE pQuestionnaire);

        /// <summary>
        /// Mark the specified PERSON_QUESTIONNAIRE for deletion.
        /// </summary>
        /// <param name="pQuestionnaireID">The Questionnaire ID.</param>
        /// <param name="pRowVersion">The row version.</param>
        /// <param name="pHardDelete">If true then remove row from database, otherwise perform a soft delete (default).</param>
        void DeletePERSON_QUESTIONNAIRE(int pPersonQuestionnaireID, byte[] pRowVersion, bool pHardDelete = false);


        void Save();

        void Update(PERSON_QUESTIONNAIRE pPERSON_QUESTIONNAIRE);


        #endregion





    }
}
