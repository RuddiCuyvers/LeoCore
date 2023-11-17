using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// Validator interface for strings
    /// </summary>
    public interface IStringValidator
    {
        /// <summary>
        /// Validates a stringl property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The string value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMaxLength">Optionally specify a maximum string length value</param>
        /// <param name="pMinLength">Optionally specify a minimum string length value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            string pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            int? pMaxLength = null,
            int? pMinLength = null);
    }
}