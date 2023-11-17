using System;
using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// Validator interface for both DateTime and Nullable DateTime types
    /// </summary>
    public interface IDateTimeValidator
    {
        /// <summary>
        /// Validates a DateTime property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pNullableValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pIncludeTime">Set to true if the value includes time data</param>
        /// <param name="pMustBeInPast">Set to true if the date must be in the past</param>
        /// <param name="pMustBeInFuture">Set to true if the date must be in the future</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            DateTime? pNullableValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pIncludeTime = false,
            bool pMustBeInPast = false,
            bool pMustBeInFuture = false,
            DateTime? pRangeMin = null,
            DateTime? pRangeMax = null);

        /// <summary>
        /// Validates a DateTime property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pIncludeTime">Set to true if the value includes time data</param>
        /// <param name="pMustBeInPast">Set to true if the date must be in the past</param>
        /// <param name="pMustBeInFuture">Set to true if the date must be in the future</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            DateTime pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pIncludeTime = false,
            bool pMustBeInPast = false,
            bool pMustBeInFuture = false,
            DateTime? pRangeMin = null,
            DateTime? pRangeMax = null);
    }
}