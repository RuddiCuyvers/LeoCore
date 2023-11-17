using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



using LeoCore.Data.Models;

namespace LeoCore.Data
{
    public class QuestionnaireRepository : IQuestionnaireRepository
    {
        private readonly LeoDBContext _context;

        #region Constructors
        public QuestionnaireRepository(LeoDBContext context)
        {
            _context = context;
        }
        #endregion

        #region QUESTIONNAIRE entity table
        /// <summary>
        /// Finds all QUESTIONNAIREs.
        /// </summary>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <returns>Query of QUESTIONNAIRE</returns>
        public IQueryable<QUESTIONNAIRE> FindAllQUESTIONNAIREs(bool pIncludeSoftDeleted = false)
        {
            IQueryable<QUESTIONNAIRE> vQuery = this._context.QUESTIONNAIRE;
            // DELETED - Join on QUESTIONNAIREType and QUESTIONNAIREentatus
            //.Include(p => p.QUESTIONNAIREType)
            //.Include(p => p.QUESTIONNAIREStatus)

            if (!pIncludeSoftDeleted)
            {
                // Don't return deleted entities
                vQuery = vQuery; //.Where(p => p.SoftDeleted == false);
            }

           
            return vQuery;
        }


        /// <summary>
        /// Fetches the QUESTIONNAIRE.
        /// </summary>
        /// <param name="pID">The QUESTIONNAIRE ID.</param>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <param name="pIncludeAllData">if set to <c>true</c> [p include all data].</param>
        /// <param name="pNoTracking">Set to true to fetch QUESTIONNAIRE without tracking.</param>
        /// <returns></returns>
        public QUESTIONNAIRE GetQUESTIONNAIRE(
            int pID,
            bool pIncludeSoftDeleted = false,
            bool pIncludeAllData = false,
            bool pNoTracking = false)
        {
            // -- Fetch QUESTIONNAIRE row
            var vQUESTIONNAIREQuery = this._context.QUESTIONNAIRE
                .Where(p => (p.ID == pID));

            if (pIncludeAllData)
            {
                // Include entities referred to by foreign key
                vQUESTIONNAIREQuery = vQUESTIONNAIREQuery.Include(p => p.QUESTIONNAIRE_QUESTIONs);
                //vQUESTIONNAIREQuery = vQUESTIONNAIREQuery.Include(p => p.QUESTIONNAIRE_QUESTIONs.Select(qq => qq.QUESTION));

            }

            if (!pIncludeSoftDeleted)
            {
                // Don't return deleted entities
                vQUESTIONNAIREQuery = vQUESTIONNAIREQuery; //.Where(p => p.SoftDeleted == false);
            }

          
            // Execute the query
            QUESTIONNAIRE vQUESTIONNAIRE = vQUESTIONNAIREQuery.FirstOrDefault();

             return vQUESTIONNAIRE;
        }


        /// <summary>
        ///  Fetches the QUESTIONNAIREtype
        /// </summary>
        /// <param name="pQUESTIONNAIREID">The QUESTIONNAIRE ID.</param>
        /// <returns></returns>
        /// 
        public QUESTIONNAIRE GetQUESTIONNAIREByTRAININGID(
           int pTrainingID,
           bool pIncludeSoftDeleted = true,
           bool pIncludeAllData = true,
           bool pNoTracking = false)
        {
            var vQUESTIONNAIREQuery = this._context.TRAINING_QUESTIONNNAIRE
                   .Where(p => p.TRAINING_ID == pTrainingID).Select(p => p.QUESTIONNAIRE).Include(p => p.QUESTIONNAIRE_QUESTIONs.Select(qq => qq.QUESTION)).Include(p => p.QUESTIONNAIRE_QUESTIONs).ToList();
                
           
            if (!pIncludeSoftDeleted)
            {
                // Don't return deleted entities
                vQUESTIONNAIREQuery = vQUESTIONNAIREQuery; //.Where(p => p.SoftDeleted == false);
            }

            

            return vQUESTIONNAIREQuery.FirstOrDefault();
        }

        

        /// <summary>
        /// Adds the specified QUESTIONNAIRE to the context and mark for for creation.
        /// </summary>
        /// <param name="pQUESTIONNAIRE">The QUESTIONNAIRE instance.</param>
        public void AddQUESTIONNAIRE(QUESTIONNAIRE pQUESTIONNAIRE)
        {
            this._context.Add(pQUESTIONNAIRE);
        }

        /// <summary>
        /// Mark the specified QUESTIONNAIRE for deletion.
        /// </summary>
        /// <param name="pQUESTIONNAIREID">The QUESTIONNAIRE ID.</param>
        /// <param name="pRowVersion">The row version.</param>
        /// <param name="pHardDelete">If true then remove row from database, otherwise perform a soft delete (default).</param>
        public void DeleteQUESTIONNAIRE(int pQUESTIONNAIREID, byte[] pRowVersion, bool pHardDelete = false)
        {
            if (pHardDelete)
            {
                // Physically delete row from Database
                var vQUESTIONNAIRE = new QUESTIONNAIRE
                {
                    // Primary key field
                    ID = pQUESTIONNAIREID,
                    // Concurrency check field
                    // For this to work you must set the ConcurrencyMode property to 'Fixed' in the edmx for the RowVersion column.
                    //RowVersion = pRowVersion
                }; // dummy instance

                // Attach to context in unmodified entity state
                this._context.Attach(vQUESTIONNAIRE);

                this._context.Remove(vQUESTIONNAIRE);
            }
            else // soft delete
            {
                // Don't delete row from Database. Instead set the SoftDeleted flag on the QUESTIONNAIRE row.
                var vQUESTIONNAIRE = new QUESTIONNAIRE
                {
                    // Primary key field
                    ID = pQUESTIONNAIREID,
                   // //SoftDeleted = true,
                    // Concurrency check field
                    // For this to work you must set the ConcurrencyMode property to 'Fixed' in the edmx for the RowVersion column.
                  //  //RowVersion = pRowVersion
                }; // dummy instance

                // Attach to context in unmodified entity state
                this._context.Attach(vQUESTIONNAIRE);

                // Update SoftDeleted column in database
                // Mark a single column as modified
               //ToDO: this._context.SetModifiedProperty(vQUESTIONNAIRE, QUESTIONNAIRE.SoftDeletedFieldName);
            }
        }


        #endregion
    }
}
