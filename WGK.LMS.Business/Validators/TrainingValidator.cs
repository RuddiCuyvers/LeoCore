using System.Data;
using WGK.LMS.Business.Dtos.Trainings;

using WGK.LMS.Data.Interfaces;
using WGK.Lib.Ioc;
using WGK.Lib.UseCases;
using WGK.Lib.Validation;
using System.Text.RegularExpressions;
using WGK.LMS.Common.Constants.Trainings;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using WGK.Lib.Extensions;
using LeodbModel;

namespace WGK.LMS.Business.Validators
{
    /// <summary>
    /// Validator class for validating Training and child table instances (data layer).
    /// </summary>
    /// <remarks>
    /// This class accesses the repository in order to fetch EntityState and OriginalValues for the entity that is validated.
    /// For this reason the IocContainer must be configured to use a HttpContextLifetimeManager for this validator.
    /// </remarks>
    public class TrainingValidator : IValidator<TRAINING>
    {

        #region Constants
        #endregion

        #region Fields
        private ITrainingRepository iTrainingRepository;
        #endregion

        #region Constructors
        public TrainingValidator(
            ITrainingRepository pTrainingRepository)
        {
            
            this.iTrainingRepository = pTrainingRepository;
            
        }
        #endregion

        #region IValidator<Training> implementation - Training validation
        /// <summary>
        /// Validates a Training Entity instance (data layer) 
        /// </summary>
        public bool Validate(TRAINING pModel, IValidationDictionary pValidationDictionary)
        {
            bool vIsValid = true;

            EntityState vEntityState = this.iTrainingRepository.GetObjectState(pModel);
            bool vIsCreate = (vEntityState == EntityState.Added);

            // Remark: Entity instances in Added or in Detached state don't have OriginalValues
            IDataRecord vOriginalTrainingValues = null;
            if (!vIsCreate)
            {
                vOriginalTrainingValues = this.iTrainingRepository.GetOriginalValues(pModel);
            }
            //
            // Validate string fields
            //
            var vStringValidator = IocManager.Resolve<IStringValidator>();
            var vNumberValidator = IocManager.Resolve<INumberValidator>();
            // -- Email
            if (!vStringValidator.Validate(
                pModel.TRAINER_EMAIL,
                pValidationDictionary,
                // Key for the field
                TRAININGDetail.ClassName + "." + TRAININGDetail.TRAINER_EMAILFieldName,
                // Display name for the specifiek persoon type field
                TRAININGDisplayNames.cTRAINER_EMAILDisplayName,
                false, // not required
                100))
            {
                vIsValid = false;
            }

            // Email should be in correct format
            if (!string.IsNullOrEmpty(pModel.TRAINER_EMAIL))
            {
                Regex vRegex = new Regex(@"^(.)+@([-0-9a-zA-Z]+[.])+[a-zA-Z]{2,6}$");
                if (!vRegex.IsMatch(pModel.TRAINER_EMAIL))
                {
                    vIsValid = false;
                    pValidationDictionary.AddError(
                        // Key for the specifiek persoon type field
                        TRAININGDetail.ClassName + "." + TRAININGDetail.TRAINER_EMAILFieldName,
                        "e-mail niet geldig");
                }
            }

          
            // -- Onderwerp verplicht
            if (!vStringValidator.Validate(
                pModel.SUBJECT,
                pValidationDictionary,
                // Key for the field
                TRAININGDetail.ClassName + "." + TRAININGDetail.SUBJECTFieldName,
                // Display name for the specifiek persoon type field
                TRAININGDisplayNames.cSUBJECTDisplayName,
                true,//  required
                254))  //max 255 chars in de database
            {
                vIsValid = false;
            }
            //lesMethodiel verplicht
            if (pModel.METHODOLOGY.IsNullOrEmptyOrBlankCode())
            {
                pModel.METHODOLOGY = null;
            }
            if (!vStringValidator.Validate(
              pModel.METHODOLOGY,
              pValidationDictionary,
              // Key for the field
              TRAININGDetail.ClassName + "." + TRAININGDetail.METHODOLOGYFieldName,
              // Display name for the specifiek persoon type field
              TRAININGDisplayNames.cMETHODOLOGYDisplayName,
              true,//  required
              99))  //max 100 chars in de database
            {
                vIsValid = false;
            }

            //type training vorming verplicht
            if (pModel.METHODOLOGY.IsNullOrEmptyOrBlankCode())
            {
                pModel.TRAINING_TYPE = null;
            }
            if (!vStringValidator.Validate(
              pModel.TRAINING_TYPE,
              pValidationDictionary,
              // Key for the field
              TRAININGDetail.ClassName + "." + TRAININGDetail.TRAINING_TYPEFieldName,
              // Display name for the specifiek persoon type field
              TRAININGDisplayNames.cTRAINING_TYPEDisplayName,
              true,//  required
              99))  //max 100 chars in de database
            {
                vIsValid = false;
            }

            //type training vorming verplicht

            if (!vNumberValidator.Validate(
              pModel.DURATION_OVERALL,
              pValidationDictionary,
              // Key for the field
              TRAININGDetail.ClassName + "." + TRAININGDetail.DURATION_OVERALLFieldName,
              // Display name for the specifiek persoon type field
              TRAININGDisplayNames.cDURATION_OVERALLDisplayName,
              true,  //isrequired
              true,
              //must be positive
              99))  //max 100 chars in de database
            {
                vIsValid = false;
            }

            //ge-associeerde vragenlijst moet ingevuld zijn
            if (pModel.TRAINING_QUESTIONNNAIREs.ToArray()[0].QUESTIONNAIRE_ID == 0)
            {
                vIsValid = false;

                pValidationDictionary.AddError(
                    // Key for the specifiek persoon type field
                    "GekoppeldeQuestionnaire.QUESTIONNAIRE_ID",
                    "Je moet aanduiden welke vragenlijst de cursisten moeten invullen");

            }



            //// Replace empty string or BlankCode with null
            //if (pModel.TRAINER_EMAIL.TRAINING_QUESTIONNNAIRE.Count() < 1nnaire.QUESTIONNAIRE_ID.IsNullOrEmptyOrBlankCode())
            //{
            //    pModel.SchadeTypeID = null;
            //}

            //if (!vUserCodeValidator.Validate(
            //    pModel.SchadeTypeID,
            //    pValidationDictionary,
            //    WildschadeDetail.ClassName + "." + WildschadeDetail.SchadeTypeIDFieldName,
            //    WildschadeDisplayNames.cSchadeTypeIDDisplayName,
            //    pIsRequired: true,
            //    pUserCodeGroupCodeID: UserCodeGroupCode.SchadeType,
            //    pIsModified: (vOriginalWildschadeValues == null)
            //        || (pModel.SchadeTypeID != vOriginalWildschadeValues[Wildschade.SchadeTypeIDFieldName] as string)))
            //{
            //    vIsValid = false;
            //}
            // Validate dates
            var vDateTimeValidator = IocManager.Resolve<IDateTimeValidator>();

            // -- VoorvalDatum field
            // Remove time part
       
            //if (vIsCreate == false)
            //{
            //    if (!vDateTimeValidator.Validate(
            //        pModel.DURATION.Date,
            //        pValidationDictionary,
            //        TRAININGDetail.ClassName + "." + TRAININGDetail.DURATIONFieldName,
            //        "Tijdstip op",
            //        pIsRequired: true,
            //        pIncludeTime: false,
            //        // ToDo: Dit is een testje om een validatie fout te krijgen
            //        pMustBeInFuture: true
            //        ))
            //    {
            //        vIsValid = false;
            //    }
            //}

            // Validate foreign keys


            return vIsValid;
        }
        #endregion
    }
}
