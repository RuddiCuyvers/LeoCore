using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// Interface for validating a model
    /// </summary>
    /// <typeparam name="T">Type of the model to validate</typeparam>
    public interface IValidator<T>
    {
        /// <summary>
        /// Validates a model instance and adds validation messages to a ValidationDictionary
        /// </summary>
        /// <param name="pModel">The model instance to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <returns>True if the model is valid, false if there are validation errors</returns>
        bool Validate(T pModel, IValidationDictionary pValidationDictionary);
    }
}