using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// Interface for validating a property value
    /// </summary>
    /// <typeparam name="T">Type of the property to validate</typeparam>
    public interface IPropertyValidator<T>
    {
        /// <summary>
        /// Validates a property value and adds validation messages to a ValidationDictionary
        /// </summary>
        /// <param name="pPropertyValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <returns>True if the model is valid, false if there are validation errors</returns>
        bool Validate(
            T pPropertyValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName);
    }
}