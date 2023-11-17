using System.Globalization;
using System.Windows;
using WGK.Lib.DataAnnotations;
using WGK.Lib.Exceptions;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// PropertyValidator for a string value.
    /// </summary>
    public class StringValidator : IStringValidator
    {
        public bool Validate(
            string pValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            int? pMaxLength = null,
            int? pMinLength = null)
        {
            bool vIsValid = true;

            // Remove leading and trailing blanks
            if (pValue != null)
            {
                pValue = pValue.Trim();                    
            }

            // Empty and blank strings are considered as 'not filled in' values
            if (pIsRequired && string.IsNullOrEmpty(pValue))
            {
                vIsValid = false;
                pValidationDictionary.AddError(
                    pPropertyKey,
                    string.Format(
                        DataAnnotationLiterals.RequiredErrorMessage,
                        pPropertyDisplayName));
            }
            else
            {
                if (pMinLength.HasValue && pMinLength > 0 && pMaxLength.HasValue && pMaxLength > 0)
                {
                    // Check if value is in range
                    if (pMinLength < pMaxLength
                        && (string.IsNullOrEmpty(pValue) || pValue.Length < pMinLength || pValue.Length > pMaxLength))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.StringLengthIncludingMinimumErrorMessage,
                                pPropertyDisplayName,
                                pMinLength.Value.ToString(CultureInfo.CurrentCulture),
                                pMaxLength.Value.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else if (pMaxLength.HasValue && pMaxLength > 0)
                {
                    // Check if value does not exceed the maximum value
                    if (pValue != null && pValue.Length > pMaxLength)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.StringLengthErrorMessage,
                                pPropertyDisplayName,
                                pMaxLength.Value.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else if (pMinLength.HasValue && pMinLength > 0)
                {
                    // Check if value is not below the minimum value
                    if (string.IsNullOrEmpty(pValue) || pValue.Length < pMinLength)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.StringLengthMinimumErrorMessage,
                                pPropertyDisplayName,
                                pMinLength.Value.ToString(CultureInfo.CurrentCulture)));
                    }
                }
            }

            return vIsValid;
        }
    }
}