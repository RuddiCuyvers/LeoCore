using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using WGK.LMS.Business.Dtos.Questionnaires;
using WGK.LMS.Business.Helpers;
using WGK.LMS.Business.Interfaces.Questionnaires;

using WGK.LMS.Business.Models.Questionnaires;
using WGK.LMS.Business.Validators;
using WGK.LMS.Common.Codes;
using WGK.LMS.Common.Constants;
using WGK.LMS.Common.Constants.Questionnaire;
using WGK.LMS.Common.Literals;
using LeodbModel;
using WGK.LMS.Data.Interfaces;

using WGK.Lib.DataAnnotations;
using IronBarCode;

using WGK.Lib.Exceptions;
using WGK.Lib.Extensions;
using WGK.Lib.Ioc;

using WGK.Lib.Mappers;

using WGK.Lib.Security;

using WGK.Lib.UseCases;

using WGK.Lib.Validation;
using System.Drawing;
using System.IO;

namespace WGK.LMS.Business.UseCases.Questionnaires
{
    /// <summary>
    /// Abstract generic Questionnaire Update UseCase base class providing functionality that is common to all QuestionnaireTypes.
    /// </summary>
    /// <typeparam name="TUpdateModel">The UpdateModel type. The model must derive BaseQuestionnaireUpdateModel </typeparam>
    /// <typeparam name="TQuestionnaireDto">The DTO type for the specific Questionnaire. The DTO class must derive from QuestionnairerDetail</typeparam>
    public abstract class BasePersonQuestionnaireUpdateUseCase<TUpdateModel, TQuestionnaireDto, TPersonQuestionnaireDto>
        : BaseUpdateUseCase<TUpdateModel>
        where TUpdateModel : BasePersonQuestionnaireUpdateModel<TQuestionnaireDto, TPersonQuestionnaireDto>, new()
        where TQuestionnaireDto : QUESTIONNAIREDetail
        where TPersonQuestionnaireDto : PERSON_QUESTIONNAIREDetail
    {

        #region Constants
      
        #endregion

        #region Fields
        protected readonly IQuestionnaireRepository iQuestionnaireRepository;
        protected readonly IPersonQuestionnaireRepository iPersonQuestionnaireRepository;


        // Base table instance
        protected QUESTIONNAIRE iQuestionnaire;
        protected PERSON_QUESTIONNAIRE iPersonQuestionnaire;

        #endregion

        #region Constructors

        protected BasePersonQuestionnaireUpdateUseCase(           
            IQuestionnaireRepository pQuestionnaireRepository, IPersonQuestionnaireRepository pPersonQuestionnaireRepository)
        {
            this.iQuestionnaireRepository = pQuestionnaireRepository;
            this.iPersonQuestionnaireRepository = pPersonQuestionnaireRepository;
        }
        #endregion

        #region BaseUseCase overrides - DoExecute
        protected override void ExecuteOverride()
        {
            if (this.UpdateData == null)
            {
                throw new ParameterMissingException(QUESTIONNAIREDisplayNames.cDESCRIPTIONDisplayName);
            }

            if (this.UpdateData.QuestionnaireDetail == null)
            {
                throw new ParameterMissingException(
                    QUESTIONNAIREDisplayNames.cDESCRIPTIONDisplayName);
            }

            if (this.UpdateData.PersonQuestionnaireDetail == null)
            {
                throw new ParameterMissingException(
                    PERSON_QUESTIONNAIREDisplayNames.cQUESTIONNAIRE_IDDisplayName);
            }

            if (this.iValidationDictionary == null)
            {
                // Client must set ValidationDictionary through its property
                throw new ParameterMissingException("ValidationDictionary");
            }

            // Fetch Questionnaire instance with specific detail table and related child table instances from database
            this.FetchData();

            MergeHelper.MergeSingle(this.UpdateData.PersonQuestionnaireDetail).Into(this.iPersonQuestionnaire);

            this.Secure();

          
            this.PreValidationBusinessLogic();

           
            if (this.Validate())
            {
                // Add business logic to Questionnaire instance and related child row instances
                this.MergeBusinessLogic();

                // Save Questionnaire instance and related child row instances to database in a single transaction
                this.SaveData();
            }
            else
            {
                // Return ID 0 result to indicate validation errors
                this.ResultID = 0;
            }
        }
        #endregion

        #region Abstract Methods
        protected abstract void FetchDetailTable();
        #endregion

        #region Protected Methods - General
        protected virtual void Secure()
        {
          
        }

        protected virtual void FetchData()
        {
            var vPersonQuestionnaireID = this.UpdateData.PersonQuestionnaireDetail.ID;
            PERSON_QUESTIONNAIRE vPersonQuestionnaire = null;
            if (vPersonQuestionnaireID == -1)
            {
                // -- Get default data for creating a new Questionnaire
                // Create a new Questionnaire instance (data layer)
                vPersonQuestionnaire = new PERSON_QUESTIONNAIRE();
                // Add the new Questionnaire instance to repository context and mark for creation
                this.iPersonQuestionnaireRepository.AddPERSON_QUESTIONNAIRE(vPersonQuestionnaire);
            }
            else
            {
                // -- Fetch existing Questionnaire from database
                // REMARK: repository does not fetch the specific detail tables automatically
                vPersonQuestionnaire = this.iPersonQuestionnaireRepository.GetPERSON_QUESTIONNAIRE(
                    pID: (int)vPersonQuestionnaireID,
                    pIncludeSoftDeleted: false,
                    pIncludeAllData: true,
                    pNoTracking: false);
                if (vPersonQuestionnaire == null)
                {
                    throw new NoResultFoundException(
                        PERSON_QUESTIONNAIREDisplayNames.cIDDisplayName,
                        vPersonQuestionnaireID);
                }

             }

            this.iPersonQuestionnaire = vPersonQuestionnaire;
        }

        /// <summary>
        /// Validates the Questionnaire main table and child tables that are common to all QuestionnaireTypes
        /// </summary>
        protected virtual bool Validate()
        {
            // -- Validate Questionnaire main table
            ValidateHelper.Validate(this.iQuestionnaire, this.iValidationDictionary);
            return this.iValidationDictionary.IsValid;
        }

        /// <summary>
        /// Specific business logic that is needed BEFORE validation
        /// </summary>
        protected virtual void PreValidationBusinessLogic()
        {
            // Add business logic for Questionnaire that is needed BEFORE validation
            this.PreValidationBusinessLogicQuestionnaire();
        }

        /// <summary>
        /// Merge Business Logic for update/create
        /// </summary>
        protected virtual void MergeBusinessLogic()
        {
            // -- Merge business logic for main table
            this.MergeBusinessLogicForQuestionnaire();
        }

        /// <summary>
        /// Save maintenance data to database for update/create
        /// </summary>
        /// <returns></returns>
        protected virtual void SaveData()
        {
            EntityState vEntityState = this.iPersonQuestionnaireRepository.GetObjectState(this.iPersonQuestionnaire);
            this.iPersonQuestionnaireRepository.Save();
            this.ResultID = this.iPersonQuestionnaire.ID;
        }
        #endregion

        #region protected Methods - Questionnaire table
        /// <summary>
        /// Specific business logic for Questionnaire base table that is needed BEFORE validation
        /// </summary>
        protected virtual void PreValidationBusinessLogicQuestionnaire()
        {
            
        }

        /// <summary>
        /// Merges business logic for Questionnaire table
        /// </summary>
        protected virtual void MergeBusinessLogicForQuestionnaire()
        {
            
        }
        #endregion

    }
}
