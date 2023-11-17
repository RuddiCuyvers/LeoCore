using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// Validator interface for numeric types
    /// </summary>
    public interface INumberValidator
    {
        /// <summary>
        /// Validates a nullable decimal property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pNullableValue">The nullable property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            decimal? pNullableValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            decimal? pRangeMin = null,
            decimal? pRangeMax = null);

        /// <summary>
        /// Validates a decimal property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            decimal pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            decimal? pRangeMin = null,
            decimal? pRangeMax = null);

        /// <summary>
        /// Validates a nullable double property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pNullableValue">The nullable property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            double? pNullableValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            double? pRangeMin = null,
            double? pRangeMax = null);

        /// <summary>
        /// Validates a double property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            double pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            double? pRangeMin = null,
            double? pRangeMax = null);

        /// <summary>
        /// Validates a nullable float property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pNullableValue">The nullable property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            float? pNullableValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            float? pRangeMin = null,
            float? pRangeMax = null);

        /// <summary>
        /// Validates a float property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            float pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            float? pRangeMin = null,
            float? pRangeMax = null);

        /// <summary>
        /// Validates a nullable long property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pNullableValue">The nullable property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            long? pNullableValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            long? pRangeMin = null,
            long? pRangeMax = null);

        /// <summary>
        /// Validates a long property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            long pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            long? pRangeMin = null,
            long? pRangeMax = null);

        /// <summary>
        /// Validates a nullable int property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pNullableValue">The nullable property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            int? pNullableValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            int? pRangeMin = null,
            int? pRangeMax = null);

        /// <summary>
        /// Validates a int property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            int pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            int? pRangeMin = null,
            int? pRangeMax = null);

        /// <summary>
        /// Validates a nullable short property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pNullableValue">The nullable property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            short? pNullableValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            short? pRangeMin = null,
            short? pRangeMax = null);

        /// <summary>
        /// Validates a short property value and adds validation messages to a ValidationDictionary.
        /// </summary>
        /// <param name="pValue">The property value to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        /// <param name="pPropertyKey">The key of the property in the ValidationDictionary</param>
        /// <param name="pPropertyDisplayName">The display name of the property</param>
        /// <param name="pIsRequired">Set to true if the property value is required</param>
        /// <param name="pMustBePositive">Set to true if the value must be positive</param>
        /// <param name="pRangeMin">Optionally specify a range minimum value</param>
        /// <param name="pRangeMax">Optionally specify a range maximum value</param>
        /// <returns>True if the value is valid, false if there are validation errors</returns>
        bool Validate(
            short pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            short? pRangeMin = null,
            short? pRangeMax = null);
    }
}