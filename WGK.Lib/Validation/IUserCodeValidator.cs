using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    public interface IUserCodeValidator
    {
        /// <summary>
        /// Validates a UserCode property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pUserCodeGroupCodeID"> </param>
        /// <param name="pIsModified">Set to true if the property value is modified (or created)</param>
        /// <returns>True if the model is valid, false if there are validation errors</returns>
        bool Validate(
            string pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired,
            string pUserCodeGroupCodeID,
            bool pIsModified);
    }
}