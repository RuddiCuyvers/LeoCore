﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.Lib.Exceptions;
using WGK.Lib.Validation;
using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Interfaces.Questionnaires;
using LEO.Business.Models.Questionnaires;
using LEO.Common.Constants.Questionnaire;
using LEO.Data.Interfaces;
using LEO.Data.Models;

namespace LEO.Business.UseCases.Questionnaires
{
    public class PersonQuestionnaireUpdateUseCase
          : BasePersonQuestionnaireUpdateUseCase<PersonQuestionnaireUpdateModel, QUESTIONNAIREDetail, PERSON_QUESTIONNAIREDetail>, IPersonQuestionnaireUpdateUseCase
    {
        #region Fields
      
        #endregion

        #region Constructors
           
        public PersonQuestionnaireUpdateUseCase(
            IQuestionnaireRepository pQuestionnaireRepository,
            IPersonQuestionnaireRepository pPersonQuestionnaireRepository
            )
            : base(   pQuestionnaireRepository, pPersonQuestionnaireRepository)
            {
            
            }
        #endregion

        #region Overriden Methods
        /// <summary>
        /// Fetches Training detail table from database
        /// </summary>
        protected override void FetchDetailTable()
        {
            if (this.iPersonQuestionnaire.ID == 0)
            {
                // Remark: setting the navigation property will automatically mark the Training instance for creation
                this.iPersonQuestionnaire = new PERSON_QUESTIONNAIRE();
            }
            else
            {
                var vPersonQuestionnaire = this.iPersonQuestionnaireRepository.GetPERSON_QUESTIONNAIRE(
                    pID: this.iPersonQuestionnaire.ID,
                    pIncludeSoftDeleted: false,
                    pIncludeAllData: true,
                    pNoTracking: false);
                if (vPersonQuestionnaire == null)
                {
                    throw new NoResultFoundException(
                        "ID",
                        this.iPersonQuestionnaire.ID);
                }
                this.iPersonQuestionnaire = vPersonQuestionnaire;
            }
        }

        /// <summary>
        /// Validates the maintenance data.
        /// </summary>
        protected override bool Validate()
        {
            //ToDo: Ruddi dit terug enablen
            //// Validate specific Training detail table
            //ValidateHelper.Validate(
            //    this.iQuestionnaire,
            //    this.iValidationDictionary);

            //// Chain to base class in order to validate the Dossier main table and child tables
            //base.Validate();

            //return this.iValidationDictionary.IsValid;
            return true;
        }

        /// <summary>
        /// Merge Business Logic for update/create.
        /// This method create pending edits in GIS before validation and other busines logic.
        /// </summary>
        protected override void PreValidationBusinessLogic()
        {
            if (this.UpdateData.PersonQuestionnaireDetail.ID != 0) // only for existing dossiers
            {
               
            }

            // Pre validation logic for Dossier main table and child tables
            base.PreValidationBusinessLogic();
        }

        /// <summary>
        /// Merge Business Logic for update/create
        /// </summary>
        protected override void MergeBusinessLogic()
        {
            // Merge business logic for Dossier main table and child tables
            base.MergeBusinessLogic();

            // Merge business logic for Training detail table
            this.MergeBusinessLogicForTraining();
        }

        /// <summary>
        /// Save maintenance data to database for update/create.
        /// This method has been overriden in order to implement a commit/rollback scenario for updating percelen
        /// in the GIS database using the TrainingCreateGisPendingEditsUseCase.
        /// </summary>
        /// <returns></returns>
        protected override void SaveData()
        {
            if (this.UpdateData.PersonQuestionnaireDetail.ID == 0) // create a new PersonQuestionnaire
            {
                // Save all changes to the LMS database in a single transaction
                // Remark: Only the topmost UseCase instance participating in the TransactionContext will actually call the
                // save method on the ObjectContext.
                base.SaveData();
            }
            else // update an existing dossier
            {
               
                try
                {
                  
                    // changes to the LMS database in a single transaction.
                    base.SaveData();
                }
                catch (Exception)
                {
                   
                    // Rethrow the exception after canceling the pending edits in GIS
                    throw;
                }

                try
                {
                   
                }
                catch (Exception)
                {
                    // Don't throw an exception if pending edits could not be commited in the GIS database since
                    // the dossier is already updated in the LMS database ...
                }
            }
        }
        #endregion

        #region Private Methods - Training detail table
        /// <summary>
        /// Merge Business Logic to the Training detail table for update/create
        /// </summary>
        private void MergeBusinessLogicForTraining()
        {
            if (this.iPersonQuestionnaire.ID != 0) // only for existing dossiers
            {
               //hier berekeningen net voor saven
            }
        }
        #endregion
    }
}
