using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using AutoMapper;
using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Helpers;
using LEO.Business.Interfaces.Questionnaires;
using LEO.Business.Mappers.Questionnaires;
using LEO.Business.Models.Questionnaires;
using LEO.Common.Codes;
using LEO.Common.Constants.Questionnaire;
using LEO.Common.Literals;
using LeoCore.Data;
using WGK.Lib.Security;
using WGK.Lib.UseCases;


namespace LEO.Business.UseCases.Questionnaires
{
    public abstract class BasePersonQuestionnaireMaintenanceUseCase<TMaintenanceModel, TQuestionnaireDto, TPersonQuestionnaireDto>
        : BaseMaintenanceUseCase<TMaintenanceModel>
        where TMaintenanceModel : BasePersonQuestionnaireMaintenanceModel<TQuestionnaireDto, TPersonQuestionnaireDto>, new()
        where TQuestionnaireDto : QUESTIONNAIREDetail, new()
        where TPersonQuestionnaireDto : PERSON_QUESTIONNAIREDetail, new()
    {

        #region Fields

        protected readonly IQuestionnaireRepository iQuestionnaireRepository;
        protected readonly IPersonQuestionnaireRepository iPersonQuestionnaireRepository;

        #endregion

        #region Constructors
       
        protected BasePersonQuestionnaireMaintenanceUseCase(IQuestionnaireRepository pQuestionnaireRepository, IPersonQuestionnaireRepository pPersonQuestionnaireRepository)
        {
            this.iQuestionnaireRepository = pQuestionnaireRepository;
            this.iPersonQuestionnaireRepository = pPersonQuestionnaireRepository;
        }

        #region Abstract Methods
        /// <summary>
        /// Derived class must implement this method in order to fetch formulier detail table
        /// </summary>
        protected abstract void FetchQuestionnaireDetailTable(ref LeoCore.Data.Models.QUESTIONNAIRE pQuestionnaire);

        protected abstract void FetchPersonQuestionnaireDetailTable(ref LeoCore.Data.Models.PERSON_QUESTIONNAIRE pPERSON_QUESTIONNAIRE);
        #endregion


        #region BaseUseCase overrides
        protected override void ExecuteOverride()
        {
            this.Result = new TMaintenanceModel();

            this.FetchData();
            this.MergeBusinessLogic();
            this.Secure();
        }
        #endregion

        #region Protected Methods - General
        /// <summary>
        /// Fetches the maintenance data.
        /// </summary>
        /// <returns></returns>
        protected virtual void FetchData()
        {
            TQuestionnaireDto vQuestionnaireDetail;
            TPersonQuestionnaireDto vPersonQuestionnaireDetail;
            if (this.ID == 0)  //ID is training ID. Maar die zal er dus altijd zijn
            {
                // -- Get default data for creating a new Questionnaire
                // Create a new Questionnaire instance WITHOUT adding it to the context
                // Remark: adding a new instance to the context is done in the UpdateUseCase
                vQuestionnaireDetail = new TQuestionnaireDto();
                vPersonQuestionnaireDetail = new TPersonQuestionnaireDto();
            }
            else
            {
                // 1) ---------- // -- Fetch existing Questionnaire from database
                // Remark: repository fetches main Questionnaire table and child tables but not the formulier detail table
                var vQuestionnaire = new LeoCore.Data.Models.QUESTIONNAIRE();

                FetchQuestionnaireDetailTable(ref vQuestionnaire);
                QuestionnaireMapper a = new QuestionnaireMapper;
                // Map Questionnaire (Data layer) to TQuestionnaireDto (Business layer)
                vQuestionnaireDetail = QuestionnaireMapper.MapHelper.MapSingle(vQuestionnaire).To<TQuestionnaireDto>();

                //2) ----  // Get existing Person_Questionnaire. If any?
                LeoCore.Data.Models.PERSON_QUESTIONNAIRE vPersonQuestionnaire = new LeoCore.Data.Models.PERSON_QUESTIONNAIRE();
                FetchPersonQuestionnaireDetailTable(ref vPersonQuestionnaire);

                if (vPersonQuestionnaire == null)  //er werd geen Person Questionaaire gevonden voor deze persoon bij deze training ID. Omdat deze nog niet gemaakt was wrs...
                {
                    vPersonQuestionnaireDetail = new TPersonQuestionnaireDto();
                    vPersonQuestionnaireDetail.CLIENT_ID = this.CurrentUserClientID;
                    vPersonQuestionnaireDetail.DATE_SUBMITTED = System.DateTime.Now;
                    vPersonQuestionnaireDetail.ID = -1;  //om aan te geven dat het een nieuwe is
                    vPersonQuestionnaireDetail.QUESTIONNAIRE_ID = (int)vQuestionnaireDetail.ID;
                    vPersonQuestionnaireDetail.TRAINING_ID = (int)this.ID;
                }
                else
                {
                    // Map Questionnaire (Data layer) to TQuestionnaireDto (Business layer)
                    vPersonQuestionnaireDetail = MapHelper.MapSingle(vPersonQuestionnaire).To<TPersonQuestionnaireDto>();
                }
            }

            this.Result.QuestionnaireDetail = vQuestionnaireDetail;
            this.Result.PersonQuestionnaireDetail = vPersonQuestionnaireDetail;
        }

        /// <summary>
        /// Secures the read action.
        /// </summary>
        protected virtual void Secure()
        {
            
        }

        /// <summary>
        /// Merges the business logic for read/create on the base QuestionnaireDetail DTO instance.
        /// Derived classes must override this method and add business logic for the specific QuestionnaireType (formulier) 
        /// </summary>
        protected virtual void MergeBusinessLogic()
        {
            // Merge business logic on base QuestionnaireDetail DTO
            this.MergeBusinessLogicForQuestionnaire();
        }
        #endregion

        #region protected Methods - Questionnaire table
        /// <summary>
        /// Merge business logic on the base QuestionnaireDetail DTO
        /// </summary>
        protected virtual void MergeBusinessLogicForQuestionnaire()
        {
 
        }
        #endregion
    }
    #endregion


}

