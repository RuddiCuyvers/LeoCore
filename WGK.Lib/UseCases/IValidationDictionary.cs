
namespace WGK.Lib.UseCases
{
    /// <summary>
    /// ValidationDictionary interface allowing a business service class to report validation errors to the presentation layer
    /// </summary>
    public interface IValidationDictionary
    {
        /// <summary>
        /// Adds a validation error to the dictionary.
        /// </summary>
        /// <param name="pKey">The key.</param>
        /// <param name="pErrorMessage">The error message.</param>
        void AddError(string pKey, string pErrorMessage);

        /// <summary>
        /// Gets a value indicating whether this instance is valid.
        /// </summary>
        /// <value><c>true</c> if this instance is valid; otherwise, <c>false</c>.</value>
        bool IsValid { get; }

        /// <summary>
        /// Gets the validation summary message
        /// </summary>
        string GetValidationSummary();

        /// <summary>
        /// Merges validation messages into another ValidationDictionary
        /// </summary>
        void MergeInto(IValidationDictionary pTarget);
    }
}
