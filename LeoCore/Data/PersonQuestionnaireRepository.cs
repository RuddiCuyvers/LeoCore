using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LeoCore.Models;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore;
using LeoCore.Data.Models;

namespace LeoCore.Data
{
    public class PersonQuestionnaireRepository : IPersonQuestionnaireRepository
    {
        private readonly LeoDBContext _context;

        #region Constructors
        public PersonQuestionnaireRepository(LeoDBContext context)
        {
            _context = context;
        }
        #endregion

        #region PERSONQUESTIONNAIRE entity table
        /// <summary>
        /// Finds all PERSON_QUESTIONNAIRE.
        /// </summary>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <returns>Query of PERSON_QUESTIONNAIRE</returns>
        public IQueryable<PERSON_QUESTIONNAIRE> FindAllPERSON_QUESTIONNAIREs(bool pIncludeSoftDeleted = false)
        {
            IQueryable<PERSON_QUESTIONNAIRE> vQuery = _context.PERSON_QUESTIONNAIRE;
            if (!pIncludeSoftDeleted)
            {
               vQuery = vQuery; //.Where(p => p.SoftDeleted == false);
            }

           
            return vQuery;
        }


        /// <summary>
        /// Fetches the PERSON_QUESTIONNAIRE.
        /// </summary>
        /// <param name="pID">The PERSON_QUESTIONNAIRE ID.</param>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <param name="pIncludeAllData">if set to <c>true</c> [p include all data].</param>
        /// <param name="pNoTracking">Set to true to fetch PERSON_QUESTIONNAIRE without tracking.</param>
        /// <returns></returns>
        public PERSON_QUESTIONNAIRE GetPERSON_QUESTIONNAIRE(
            int pID,
            bool pIncludeSoftDeleted = false,
            bool pIncludeAllData = false,
            bool pNoTracking = false)
        {
            // -- Fetch QUESTIONNAIRE row
            var vPERSONQUESTIONNAIREQuery = this._context.PERSON_QUESTIONNAIRE
                .Where(p => (p.ID == pID)).Include(p => p.PERSON_QUESTIONNAIRE_ANSWERs);

            var add = vPERSONQUESTIONNAIREQuery.Count();

            if (pIncludeAllData)
            {
                vPERSONQUESTIONNAIREQuery = vPERSONQUESTIONNAIREQuery.Include(p => p.PERSON_QUESTIONNAIRE_ANSWERs);
            }

            if (!pIncludeSoftDeleted)
            {
                // Don't return deleted entities
                vPERSONQUESTIONNAIREQuery = vPERSONQUESTIONNAIREQuery; //.Where(p => p.SoftDeleted == false);
            }

            // Execute the query
            PERSON_QUESTIONNAIRE vPERSONQUESTIONNAIRE = vPERSONQUESTIONNAIREQuery.FirstOrDefault();

           

            return vPERSONQUESTIONNAIRE;
        }


        /// <summary>
        ///  Fetches the QUESTIONNAIREtype
        /// </summary>
        /// <param name="pPERSONQUESTIONNAIREID">The QUESTIONNAIRE ID.</param>
        /// <returns></returns>
        /// 
        public PERSON_QUESTIONNAIRE GetPERSON_QUESTIONNAIREByTRAININGID(
           int pTrainingID,
           bool pIncludeSoftDeleted = true,
           bool pIncludeAllData = true,
           bool pNoTracking = false)
        {
            var vPERSONQUESTIONNAIREQuery = this._context.PERSON_QUESTIONNAIRE
                   .Where(p => p.TRAINING_ID == pTrainingID);
                
            if (pIncludeAllData)
            {
                // Include entities referred to by foreign key
                vPERSONQUESTIONNAIREQuery = vPERSONQUESTIONNAIREQuery.Include(p => p.PERSON_QUESTIONNAIRE_ANSWERs);
            }

            if (!pIncludeSoftDeleted)
            {
                // Don't return deleted entities
                vPERSONQUESTIONNAIREQuery = vPERSONQUESTIONNAIREQuery; //.Where(p => p.SoftDeleted == false);
            }

            

            return vPERSONQUESTIONNAIREQuery.FirstOrDefault();
        }

        /// <summary>
        ///  Fetches the PERESON_QUESTIONNAIREtype
        /// </summary>
        /// <param name="pPERSONQUESTIONNAIREID">The PERSON_QUESTIONNAIRE ID.</param>
        /// <returns></returns>
        /// 
        public PERSON_QUESTIONNAIRE GetPERSON_QUESTIONNAIREByTRAININGIDAndByCLIENTID(
           int pTrainingID,
           string pClientID,
           bool pIncludeSoftDeleted = true,
           bool pIncludeAllData = true,
           bool pNoTracking = false)
        {
            var vPERSONQUESTIONNAIREQuery = this._context.PERSON_QUESTIONNAIRE
                   .Where(p => p.TRAINING_ID == pTrainingID).Where(p => p.CLIENT_ID == pClientID);

            if (pIncludeAllData)
            {
                // Include entities referred to by foreign key
                vPERSONQUESTIONNAIREQuery = vPERSONQUESTIONNAIREQuery.Include(p => p.PERSON_QUESTIONNAIRE_ANSWERs);
            }

            if (!pIncludeSoftDeleted)
            {
                // Don't return deleted entities
                vPERSONQUESTIONNAIREQuery = vPERSONQUESTIONNAIREQuery; //.Where(p => p.SoftDeleted == false);
            }

           

            return vPERSONQUESTIONNAIREQuery.FirstOrDefault();
        }

        /// <summary>
        /// Adds the specified QUESTIONNAIRE to the context and mark for for creation.
        /// </summary>
        /// <param name="pQUESTIONNAIRE">The QUESTIONNAIRE instance.</param>
        public void AddPERSON_QUESTIONNAIRE(PERSON_QUESTIONNAIRE pQUESTIONNAIRE)
        {
            this._context.Add(pQUESTIONNAIRE);
        }

        /// <summary>
        /// Mark the specified QUESTIONNAIRE for deletion.
        /// </summary>
        /// <param name="pQUESTIONNAIREID">The QUESTIONNAIRE ID.</param>
        /// <param name="pRowVersion">The row version.</param>
        /// <param name="pHardDelete">If true then remove row from database, otherwise perform a soft delete (default).</param>
        public void DeletePERSON_QUESTIONNAIRE(int pPERSON_QUESTIONNAIREID, byte[] pRowVersion, bool pHardDelete = false)
        {
            if (pHardDelete)
            {
                // Physically delete row from Database
                var vPERSON_QUESTIONNAIRE = new PERSON_QUESTIONNAIRE
                {
                    // Primary key field
                    ID = pPERSON_QUESTIONNAIREID,
                    // Concurrency check field
                    // For this to work you must set the ConcurrencyMode property to 'Fixed' in the edmx for the RowVersion column.
                    //RowVersion = pRowVersion
                }; // dummy instance

                // Attach to context in unmodified entity state
                this._context.Attach(vPERSON_QUESTIONNAIRE);

                this._context.Remove(vPERSON_QUESTIONNAIRE);
            }
            else // soft delete
            {
                // Don't delete row from Database. Instead set the SoftDeleted flag on the QUESTIONNAIRE row.
                var vPERSON_QUESTIONNAIRE = new PERSON_QUESTIONNAIRE
                {
                    // Primary key field
                    ID = pPERSON_QUESTIONNAIREID,
                   // //SoftDeleted = true,
                    // Concurrency check field
                    // For this to work you must set the ConcurrencyMode property to 'Fixed' in the edmx for the RowVersion column.
                  //  //RowVersion = pRowVersion
                }; // dummy instance

                // Attach to context in unmodified entity state
                this._context.Attach(vPERSON_QUESTIONNAIRE);

                // Update SoftDeleted column in database
                // Mark a single column as modified
               //ToDO: this._context.SetModifiedProperty(vQUESTIONNAIRE, QUESTIONNAIRE.SoftDeletedFieldName);
            }
        }


        #endregion
    }
}
