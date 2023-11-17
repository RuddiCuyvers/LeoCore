using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using WGK.Lib.UseCases;

namespace WGK.Lib.Data
{
    /// <summary>
    /// Transaction orchestrator allowing multiple UseCase instances to participate in a single transaction.
    /// </summary>
    public class TransactionContext : ITransactionContext
    {        
        #region Fields
        // UseCase nesting level
        private int iLevel = 0;
        #endregion

        #region Constructor
        public TransactionContext()
        {
        }
        #endregion
        
        #region ITransactionContext implementation - Properties
        /// <summary>
        /// Returns true if the current executing UseCase is allowed to commit the transaction.
        /// </summary>
        /// <value> </value>
        public bool CanCommit
        {
            get
            {
                // Only the top level UseCase instance is allowed to commit the transaction
                return this.iLevel <= 1;
            }
        }
        #endregion

        #region ITransactionContext implementation - Methods
        /// <summary>
        /// Enlists a UseCase instance to the TransactionContext
        /// </summary>
        /// <param name="pBaseUseCase"></param>
        public void Push(IBaseUseCase pBaseUseCase)
        {
            // Increment UseCase nesting level
            // Use Interlocked Increment to be thread safe
            Interlocked.Increment(ref this.iLevel);
        }

        /// <summary>
        /// Removes the last UseCase instance from the TransactionContext
        /// </summary>
        public void Pop()
        {
            if (this.iLevel > 0)
            {
                // Decrement UseCase nesting level
                // Use Interlocked Decrement to be thread safe
                Interlocked.Decrement(ref this.iLevel);                
            }
        }
        #endregion
    }
}
