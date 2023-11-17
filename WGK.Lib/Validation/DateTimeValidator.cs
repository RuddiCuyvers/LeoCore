using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using WGK.Lib.Data;
using WGK.Lib.DataAnnotations;
using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// PropertyValidator for a DateTime or Nullable DataTime value.
    /// </summary>
    public class DateTimeValidator : IDateTimeValidator
    {
        #region IDateTimeValidator implementation
        public bool Validate(
            DateTime? pNullableValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pIncludeTime = false,
            bool pMustBeInPast = false,
            bool pMustBeInFuture = false,
            DateTime? pRangeMin = null,
            DateTime? pRangeMax = null)
        {
            bool vIsValid = true;

            // DateTime.MinValue represents the 'not filled in' value
            if (pNullableValue == DateTime.MinValue)
            {
                pNullableValue = null;
            }

            if (pIsRequired && pNullableValue == null)
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
                    pIncludeTime,
                    pMustBeInPast,
                    pMustBeInFuture,
                    pRangeMin,
                    pRangeMax))
                {
                    vIsValid = false;                    
                }
            }

            return vIsValid;
        }

        public bool Validate(
            DateTime pValue,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName,
            bool pIsRequired = false,
            bool pIncludeTime = false,
            bool pMustBeInPast = false,
            bool pMustBeInFuture = false,
            DateTime? pRangeMin = null,
            DateTime? pRangeMax = null)
        {
            bool vIsValid = true;

            // Default DateTime value (not filled in) is DateTime.MinValue
            if (pIsRequired && pValue == DateTime.MinValue)
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
                if (!pIncludeTime)
                {
                    // Remove time information from date
                    // REMARK: since DateTime is a value type this does not remove time from calling parameter !!!
                    pValue = pValue.Date;
                }

                if (pMustBeInPast)
                {
                    // Check if date is in the past
                    if (pValue > (pIncludeTime ? DateTime.Now : DateTime.Today))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.DateInFutureErrorMessage,
                                pPropertyDisplayName));
                    }
                }

                if (pMustBeInFuture)
                {
                    // Check if date is in the future
                    if (pValue < (pIncludeTime ? DateTime.Now : DateTime.Today))
                    {
                        vIsValid = false;
                        pValidationDictionary.AddError(
                            pPropertyKey,
                            string.Format(
                                DataAnnotationLiterals.DateInPastErrorMessage,
                                pPropertyDisplayName));
                    }
                }

                // Check SQL server valid date range
                DateTime vRangeMin = pRangeMin.HasValue ? pRangeMin.Value : SqlDataHelper.SqlMinimalDateTime;
                DateTime vRangeMax = pRangeMax.HasValue ? pRangeMax.Value : SqlDataHelper.SqlMaximalDateTime;

                if (vRangeMin < vRangeMax
                    && (pValue < vRangeMin || pValue > vRangeMax))
                {
                    vIsValid = false;
                    pValidationDictionary.AddError(
                        pPropertyKey,
                        string.Format(
                            DataAnnotationLiterals.RangeErrorMessage,
                            pPropertyDisplayName,
                            vRangeMin.ToShortDateString(),
                            vRangeMax.ToShortDateString()));
                }
            }

            return vIsValid;
        }
        #endregion
    }
}
