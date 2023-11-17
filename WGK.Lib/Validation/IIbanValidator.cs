using System;
using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    /// <summary>
    /// Validator interface for both DateTime and Nullable DateTime types
    /// </summary>
    public interface IIbanValidator
    {
        /// <summary>
        /// Validator Iban
        /// </summary>
        /// <param name="pIban"></param>
        /// <param name="pValidationDictionary"></param>
        /// <returns></returns>
        bool Validate(
            string pIban,
            IValidationDictionary pValidationDictionary,
            string pPropertyKey,
            string pPropertyDisplayName);
    }
}