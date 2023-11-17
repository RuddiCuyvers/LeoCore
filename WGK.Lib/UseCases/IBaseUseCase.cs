using System;

namespace WGK.Lib.UseCases
{
    public interface IBaseUseCase
    {
        /// <summary>
        /// If set, the UseCase instance runs in its own transaction and does not participate in a transaction with
        /// other UseCase instances. Defaults to false (participates in a transaction with other UseCases).
        /// </summary>
        bool TransactionScopeSingle { get; set; }

        /// <summary>
        /// Gets/sets the Validation dictionary that the service will use for storing validation results.
        /// Client must set this property to an existing collection in order to obtain validation messages.
        /// </summary>
        IValidationDictionary ValidationDictionary { get; set; }

        /// <summary>
        /// Main use case execution method
        /// </summary>
        void Execute();
    }
}
