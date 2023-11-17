using WGK.Lib.Ioc;
using WGK.Lib.UseCases;

namespace WGK.Lib.Validation
{
    public static class ValidateHelper
    {
        #region Register Validators
        /// <summary>
        /// Register a custom Validator class for validating a model type with lifetime specification.
        /// </summary>
        /// <typeparam name="TModel">Type of the model to validate</typeparam>
        /// <typeparam name="TValidator">Validator class that implements the IValidator interface for the model type</typeparam>
        /// <param name="pLifeTimeEnum">Optionally specifies the lifetime for the Validator instance. Defaults to Singleton.</param>
        /// <returns></returns>
        public static void Register<TModel, TValidator>(LifeTimeEnum pLifeTimeEnum = LifeTimeEnum.Singleton)
            where TValidator : IValidator<TModel>
        {
            // Register the TValidator class with the Ioc Container (by default as a Singleton instance) for validating TModel instances.
            IocManager.RegisterType<IValidator<TModel>, TValidator>(pLifeTimeEnum);
        }
        #endregion

        #region Validate model instance
        /// <summary>
        /// Validates a model instance and adds validation messages to a ValidationDictionary
        /// </summary>
        /// <typeparam name="TModel">Type of the model to validate</typeparam>
        /// <param name="pModel">The model instance to validate</param>
        /// <param name="pValidationDictionary">Dictionary to add validation messages to</param>
        public static bool Validate<TModel>(TModel pModel, IValidationDictionary pValidationDictionary)
        {
            var vValidator = IocManager.Resolve<IValidator<TModel>>();
            return vValidator.Validate(pModel, pValidationDictionary);
        }
        #endregion
    }
}