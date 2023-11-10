using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LeoCore.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace LeoCore.Data
{
    public class PersonTrainingRepository : IPersonTrainingRepository
    {
        private readonly LeoDBContext _context;

        #region Constructors
        public PersonTrainingRepository(LeoDBContext context)
        {
            _context = context;
        }
        #endregion

        #region PersonTraining entity table
        /// <summary>
        /// Finds all PersonTrainings.
        /// </summary>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <returns>Query of PersonTraining</returns>
        public IQueryable<PERSON_TRAINING> FindAllPERSONTRAININGs(bool pIncludeSoftDeleted = false)
        {
            IQueryable<PERSON_TRAINING> vQuery = _context.PERSON_TRAINING;
            if (!pIncludeSoftDeleted)
            {
                // Don't return deleted entities
                vQuery = vQuery; //.Where(p => p.SoftDeleted == false);
            }
            return vQuery;
        }


        /// <summary>
        /// Fetches the Training.
        /// </summary>
        /// <param name="pID">The Training ID.</param>
        /// <param name="pIncludeSoftDeleted">If true then return also soft deleted entities.</param>
        /// <param name="pIncludeAllData">if set to <c>true</c> [p include all data].</param>
        /// <param name="pNoTracking">Set to true to fetch Training without tracking.</param>
        /// <returns></returns>
        public PERSON_TRAINING GetPERSONTRAINING(
            int pID,
            bool pIncludeSoftDeleted = false,
            bool pIncludeAllData = false,
            bool pNoTracking = false)
        {
            // -- Fetch PersonTraining row
            var vPersonTrainingQuery = _context.PERSON_TRAINING
                .Where(p => (p.ID == pID)).Include(p => p.TRAINING);

            var add = vPersonTrainingQuery.Count();

            
            if (!pIncludeSoftDeleted)
            {
                // Don't return deleted entities
                vPersonTrainingQuery = vPersonTrainingQuery; //.Where(p => p.SoftDeleted == false);
            }

          
            // Execute the query
            PERSON_TRAINING vPersonTraining = vPersonTrainingQuery.FirstOrDefault();

            return vPersonTraining;
        }


     
        /// <summary>
        /// Adds the specified Training to the context and mark for for creation.
        /// </summary>
        /// <param name="pTraining">The Training instance.</param>
        public void AddPersonTraining(PERSON_TRAINING pTraining)
        {
            _context.Add(pTraining);
        }

        /// <summary>
        /// Mark the specified Training for deletion.
        /// </summary>
        /// <param name="pTrainingID">The Training ID.</param>
        /// <param name="pRowVersion">The row version.</param>
        /// <param name="pHardDelete">If true then remove row from database, otherwise perform a soft delete (default).</param>
        public void DeletePersonTraining(int pTrainingID, byte[] pRowVersion, bool pHardDelete = false)
        {
            if (pHardDelete)
            {
                // Physically delete row from Database
                var vTraining = new TRAINING
                {
                    // Primary key field
                    ID = pTrainingID,
                    // Concurrency check field
                    // For this to work you must set the ConcurrencyMode property to 'Fixed' in the edmx for the RowVersion column.
                    //RowVersion = pRowVersion
                }; // dummy instance

                // Attach to context in unmodified entity state
                this._context.Attach(vTraining);

                this._context.Remove(vTraining);
            }
            else // soft delete
            {
                // Don't delete row from Database. Instead set the SoftDeleted flag on the Training row.
                var vTraining = new TRAINING
                {
                    // Primary key field
                    ID = pTrainingID,
                   // //SoftDeleted = true,
                    // Concurrency check field
                    // For this to work you must set the ConcurrencyMode property to 'Fixed' in the edmx for the RowVersion column.
                  //  //RowVersion = pRowVersion
                }; // dummy instance

                // Attach to context in unmodified entity state
                this._context.Attach(vTraining);

                // Update SoftDeleted column in database
                // Mark a single column as modified
               //ToDO: this._context.SetModifiedProperty(vTraining, TRAINING.SoftDeletedFieldName);
            }
        }


        #endregion
    }
}
