using WGK.Lib.UseCases;

namespace WGK.Lib.Data
{
    public interface ITransactionContext
    {
        /// <summary>
        /// Returns true if the current executing UseCase is allowed to commit the transaction.
        /// </summary>
        /// <value> </value>
        bool CanCommit { get; }

        /// <summary>
        /// Enlists a UseCase instance to the TransactionContext
        /// </summary>
        /// <param name="pBaseUseCase"></param>
        void Push(IBaseUseCase pBaseUseCase);

        /// <summary>
        /// Removes the last UseCase instance from the TransactionContext
        /// </summary>
        void Pop();

    }
}