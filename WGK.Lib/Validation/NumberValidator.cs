using System.Globalization;
using WGK.Lib.DataAnnotations;
using WGK.Lib.Exceptions;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// PropertyValidator for a numeric value.
    /// </summary>
    public class NumberValidator : INumberValidator
    {
        #region INumberValidator implementation - decimal
        public bool Validate(
            decimal? pNullableValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            decimal? pRangeMin = null,
            decimal? pRangeMax = null)
        {
            bool vIsValid = true;

            // decimal.MinValue represents the 'not filled in' value
            if (pNullableValue == decimal.MinValue)
            {
                pNullableValue = null;
            }

            if (pIsRequired && (pNullableValue == null))
            {
                vIsValid = false;
                pValidationDictionary.AddError(
                    pPropertyKey,
                    string.Format(
                        DataAnnotationLiterals.RequiredErrorMessage,
                        pPropertyDisplayName));
            }

            if (pNullableValue.HasValue)
            {
                if (!this.Validate(
                    pNullableValue.Value,
                    pValidationDictionary,
                    pPropertyKey,
                    pPropertyDisplayName,
                    pIsRequired,
                    pMustBePositive,
                    pRangeMin,
                    pRangeMax))
                {
                    vIsValid = false;
                }
            }
            return vIsValid;
        }

        public bool Validate(
            decimal pValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            decimal? pRangeMin = null,
            decimal? pRangeMax = null)
        {
            bool vIsValid = true;

            // decimal.MinValue value represents the 'not filled in' value
            if (pIsRequired && pValue == decimal.MinValue)
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
                if (pMustBePositive && (pValue < decimal.Zero))
                {
                    vIsValid = false;
                    pValidationDictionary.AddError(
                    pPropertyKey,
                        string.Format(
                            ExceptionLiterals.ParameterValueNegative,
                            pPropertyDisplayName,
                            pValue,
                            null));                    
                }

                if (pRangeMin.HasValue && pRangeMax.HasValue)
                {
                    // Check if value is in range
                    if (pRangeMin < pRangeMax
                        && (pValue < pRangeMin || pValue > pRangeMax))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.RangeErrorMessage,
                                pPropertyDisplayName,
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture)));
                    }                   
                }
                else if (pRangeMax.HasValue)
                {
                    // Check if value does not exceed the maximum value
                    if (pValue > pRangeMax)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueLarge,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
                else if (pRangeMin.HasValue)
                {
                    // Check if value is not below the minimum value
                    if (pValue < pRangeMin)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueSmall,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }                
            }

            return vIsValid;
        }
        #endregion

        #region INumberValidator implementation - double
        public bool Validate(
            double? pNullableValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            double? pRangeMin = null,
            double? pRangeMax = null)
        {
            bool vIsValid = true;

            // double.MinValue represents the 'not filled in' value
            if (pNullableValue == double.MinValue)
            {
                pNullableValue = null;
            }

            if (pIsRequired && (pNullableValue == null))
            {
                vIsValid = false;
                pValidationDictionary.AddError(
                    pPropertyKey,
                    string.Format(
                        DataAnnotationLiterals.RequiredErrorMessage,
                        pPropertyDisplayName));
            }

            if (pNullableValue.HasValue)
            {
                if (!this.Validate(
                    pNullableValue.Value,
                    pValidationDictionary,
                    pPropertyKey,
                    pPropertyDisplayName,
                    pIsRequired,
                    pMustBePositive,
                    pRangeMin,
                    pRangeMax))
                {
                    vIsValid = false;
                }
            }
            return vIsValid;
        }

        public bool Validate(
            double pValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            double? pRangeMin = null,
            double? pRangeMax = null)
        {
            bool vIsValid = true;

            // double.MinValue value represents the 'not filled in' value
            if (pIsRequired && pValue == double.MinValue)
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
                if (pMustBePositive && (pValue < 0.0))
                {
                    vIsValid = false;
                    pValidationDictionary.AddError(
                    pPropertyKey,
                        string.Format(
                            ExceptionLiterals.ParameterValueNegative,
                            pPropertyDisplayName,
                            pValue,
                            null));
                }

                if (pRangeMin.HasValue && pRangeMax.HasValue)
                {
                    // Check if value is in range
                    if (pRangeMin < pRangeMax
                        && (pValue < pRangeMin || pValue > pRangeMax))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.RangeErrorMessage,
                                pPropertyDisplayName,
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else if (pRangeMax.HasValue)
                {
                    // Check if value does not exceed the maximum value
                    if (pValue > pRangeMax)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueLarge,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
                else if (pRangeMin.HasValue)
                {
                    // Check if value is not below the minimum value
                    if (pValue < pRangeMin)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueSmall,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
            }

            return vIsValid;
        }
        #endregion

        #region INumberValidator implementation - float
        public bool Validate(
            float? pNullableValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            float? pRangeMin = null,
            float? pRangeMax = null)
        {
            bool vIsValid = true;

            // float.MinValue represents the 'not filled in' value
            if (pNullableValue == float.MinValue)
            {
                pNullableValue = null;
            }

            if (pIsRequired && (pNullableValue == null))
            {
                vIsValid = false;
                pValidationDictionary.AddError(
                    pPropertyKey,
                    string.Format(
                        DataAnnotationLiterals.RequiredErrorMessage,
                        pPropertyDisplayName));
            }

            if (pNullableValue.HasValue)
            {
                if (!this.Validate(
                    pNullableValue.Value,
                    pValidationDictionary,
                    pPropertyKey,
                    pPropertyDisplayName,
                    pIsRequired,
                    pMustBePositive,
                    pRangeMin,
                    pRangeMax))
                {
                    vIsValid = false;
                }
            }
            return vIsValid;
        }

        public bool Validate(
            float pValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            float? pRangeMin = null,
            float? pRangeMax = null)
        {
            bool vIsValid = true;

            // float.MinValue value represents the 'not filled in' value
            if (pIsRequired && pValue == float.MinValue)
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
                if (pMustBePositive && (pValue < 0.0f))
                {
                    vIsValid = false;
                    pValidationDictionary.AddError(
                    pPropertyKey,
                        string.Format(
                            ExceptionLiterals.ParameterValueNegative,
                            pPropertyDisplayName,
                            pValue,
                            null));
                }

                if (pRangeMin.HasValue && pRangeMax.HasValue)
                {
                    // Check if value is in range
                    if (pRangeMin < pRangeMax
                        && (pValue < pRangeMin || pValue > pRangeMax))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.RangeErrorMessage,
                                pPropertyDisplayName,
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else if (pRangeMax.HasValue)
                {
                    // Check if value does not exceed the maximum value
                    if (pValue > pRangeMax)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueLarge,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
                else if (pRangeMin.HasValue)
                {
                    // Check if value is not below the minimum value
                    if (pValue < pRangeMin)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueSmall,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
            }

            return vIsValid;
        }
        #endregion

        #region INumberValidator implementation - long
        public bool Validate(
            long? pNullableValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            long? pRangeMin = null,
            long? pRangeMax = null)
        {
            bool vIsValid = true;

            // long.MinValue represents the 'not filled in' value
            if (pNullableValue == long.MinValue)
            {
                pNullableValue = null;
            }

            if (pIsRequired && (pNullableValue == null))
            {
                vIsValid = false;
                pValidationDictionary.AddError(
                    pPropertyKey,
                    string.Format(
                        DataAnnotationLiterals.RequiredErrorMessage,
                        pPropertyDisplayName));
            }

            if (pNullableValue.HasValue)
            {
                if (!this.Validate(
                    pNullableValue.Value,
                    pValidationDictionary,
                    pPropertyKey,
                    pPropertyDisplayName,
                    pIsRequired,
                    pMustBePositive,
                    pRangeMin,
                    pRangeMax))
                {
                    vIsValid = false;
                }
            }
            return vIsValid;
        }

        public bool Validate(
            long pValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            long? pRangeMin = null,
            long? pRangeMax = null)
        {
            bool vIsValid = true;

            // long.MinValue value represents the 'not filled in' value
            if (pIsRequired && pValue == long.MinValue)
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
                if (pMustBePositive && (pValue < 0L))
                {
                    vIsValid = false;
                    pValidationDictionary.AddError(
                    pPropertyKey,
                        string.Format(
                            ExceptionLiterals.ParameterValueNegative,
                            pPropertyDisplayName,
                            pValue,
                            null));
                }

                if (pRangeMin.HasValue && pRangeMax.HasValue)
                {
                    // Check if value is in range
                    if (pRangeMin < pRangeMax
                        && (pValue < pRangeMin || pValue > pRangeMax))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.RangeErrorMessage,
                                pPropertyDisplayName,
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else if (pRangeMax.HasValue)
                {
                    // Check if value does not exceed the maximum value
                    if (pValue > pRangeMax)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueLarge,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
                else if (pRangeMin.HasValue)
                {
                    // Check if value is not below the minimum value
                    if (pValue < pRangeMin)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueSmall,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
            }

            return vIsValid;
        }
        #endregion

        #region INumberValidator implementation - int
        public bool Validate(
            int? pNullableValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            int? pRangeMin = null,
            int? pRangeMax = null)
        {
            bool vIsValid = true;

            // int.MinValue represents the 'not filled in' value
            if (pNullableValue == int.MinValue)
            {
                pNullableValue = null;
            }

            if (pIsRequired && (pNullableValue == null))
            {
                vIsValid = false;
                pValidationDictionary.AddError(
                    pPropertyKey,
                    string.Format(
                        DataAnnotationLiterals.RequiredErrorMessage,
                        pPropertyDisplayName));
            }

            if (pNullableValue.HasValue)
            {
                if (!this.Validate(
                    pNullableValue.Value,
                    pValidationDictionary,
                    pPropertyKey,
                    pPropertyDisplayName,
                    pIsRequired,
                    pMustBePositive,
                    pRangeMin,
                    pRangeMax))
                {
                    vIsValid = false;
                }
            }
            return vIsValid;
        }

        public bool Validate(
            int pValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            int? pRangeMin = null,
            int? pRangeMax = null)
        {
            bool vIsValid = true;

            // int.MinValue value represents the 'not filled in' value
            if (pIsRequired && pValue == int.MinValue)
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
                if (pMustBePositive && (pValue < 0))
                {
                    vIsValid = false;
                    pValidationDictionary.AddError(
                    pPropertyKey,
                        string.Format(
                            ExceptionLiterals.ParameterValueNegative,
                            pPropertyDisplayName,
                            pValue,
                            null));
                }

                if (pRangeMin.HasValue && pRangeMax.HasValue)
                {
                    // Check if value is in range
                    if (pRangeMin < pRangeMax
                        && (pValue < pRangeMin || pValue > pRangeMax))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.RangeErrorMessage,
                                pPropertyDisplayName,
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else if (pRangeMax.HasValue)
                {
                    // Check if value does not exceed the maximum value
                    if (pValue > pRangeMax)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueLarge,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
                else if (pRangeMin.HasValue)
                {
                    // Check if value is not below the minimum value
                    if (pValue < pRangeMin)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueSmall,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
            }

            return vIsValid;
        }
        #endregion

        #region INumberValidator implementation - short
        public bool Validate(
            short? pNullableValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            short? pRangeMin = null,
            short? pRangeMax = null)
        {
            bool vIsValid = true;

            // short.MinValue represents the 'not filled in' value
            if (pNullableValue == short.MinValue)
            {
                pNullableValue = null;
            }

            if (pIsRequired && (pNullableValue == null))
            {
                vIsValid = false;
                pValidationDictionary.AddError(
                    pPropertyKey,
                    string.Format(
                        DataAnnotationLiterals.RequiredErrorMessage,
                        pPropertyDisplayName));
            }

            if (pNullableValue.HasValue)
            {
                if (!this.Validate(
                    pNullableValue.Value,
                    pValidationDictionary,
                    pPropertyKey,
                    pPropertyDisplayName,
                    pIsRequired,
                    pMustBePositive,
                    pRangeMin,
                    pRangeMax))
                {
                    vIsValid = false;
                }
            }
            return vIsValid;
        }

        public bool Validate(
            short pValue,
            UseCases.IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pMustBePositive = false,
            short? pRangeMin = null,
            short? pRangeMax = null)
        {
            bool vIsValid = true;

            // short.MinValue value represents the 'not filled in' value
            if (pIsRequired && pValue == short.MinValue)
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
                if (pMustBePositive && (pValue < 0))
                {
                    vIsValid = false;
                    pValidationDictionary.AddError(
                    pPropertyKey,
                        string.Format(
                            ExceptionLiterals.ParameterValueNegative,
                            pPropertyDisplayName,
                            pValue,
                            null));
                }

                if (pRangeMin.HasValue && pRangeMax.HasValue)
                {
                    // Check if value is in range
                    if (pRangeMin < pRangeMax
                        && (pValue < pRangeMin || pValue > pRangeMax))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.RangeErrorMessage,
                                pPropertyDisplayName,
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture)));
                    }
                }
                else if (pRangeMax.HasValue)
                {
                    // Check if value does not exceed the maximum value
                    if (pValue > pRangeMax)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueLarge,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMax.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
                else if (pRangeMin.HasValue)
                {
                    // Check if value is not below the minimum value
                    if (pValue < pRangeMin)
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                ExceptionLiterals.ParameterValueSmall,
                                pPropertyDisplayName,
                                pValue.ToString(CultureInfo.CurrentCulture),
                                pRangeMin.Value.ToString(CultureInfo.CurrentCulture),
                                null));
                    }
                }
            }

            return vIsValid;
        }
        #endregion
    }
}