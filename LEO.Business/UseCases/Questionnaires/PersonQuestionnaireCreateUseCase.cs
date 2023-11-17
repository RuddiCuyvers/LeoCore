﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

using System.Linq;
using System.Threading;

using LEO.Business.Helpers;
using WGK.Lib.DataAnnotations;
using WGK.Lib.Exceptions;
using WGK.Lib.Extensions;
using WGK.Lib.Ioc;
using WGK.Lib.Mappers;

using WGK.Lib.Security;
using WGK.Lib.UseCases;

using WGK.Lib.Validation;

using LEO.Common.Codes;
using LEO.Common.Constants.Questionnaire;
using LEO.Common.Literals;
using LEO.Data.Models;
using LEO.Data.Interfaces;
using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Interfaces.Questionnaires;

namespace LEO.Business.UseCases.Questionnaires
{
    public class PersonQuestionnaireCreateUseCase : BaseUseCase, LEO.Business.Interfaces.Questionnaires.IPersonQuestionnaireCreateUseCase
    {
        #region Fields
        private readonly IPersonQuestionnaireRepository iPersonQuestionnaireRepository;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the <see cref="QuestionnaireSearchUseCase"/> class.
        /// </summary>
        /// <param name="pUserCodeManager">The UserCodeManager.</param>
        /// /// <param name="pQuestionnaireRepository">The Questionnaire repository.</param>
        public PersonQuestionnaireCreateUseCase( IPersonQuestionnaireRepository pPersonQuestionnaireRepository)
        {
            this.iPersonQuestionnaireRepository = pPersonQuestionnaireRepository;
        }
        #endregion

        #region IQuestionnaireIdentificationUseCase Members
        public PERSON_QUESTIONNAIREDetail CreateData { get; set; }
        public decimal Result { get; private set; }
        #endregion

        #region BaseUseCase overrides
        protected override void ExecuteOverride()
        {
            if (this.CreateData == null)
            {
                throw new ParameterMissingException(
                    "CreateData missing for Questionnaire");
            }

            if (this.Validate())
            {
               this.FetchData();
            }
            else
            {
                // Return null to indicate validation errors
                this.Result = 0;
            }
        }
        #endregion

        #region Private methods
     
        private void Secure()
        {
           
        }

       
        private bool Validate()
        {
           
            return true;
        }

        
        private void FetchData()
        {
            var vPersonQuestionnaire = new PERSON_QUESTIONNAIRE();
            vPersonQuestionnaire.CLIENT_ID = CreateData.CLIENT_ID;
            iPersonQuestionnaireRepository.AddPERSON_QUESTIONNAIRE(vPersonQuestionnaire);
            //iPersonQuestionnaireRepository.Save();
            // Execute query to fetch data
            this.Result = vPersonQuestionnaire.ID;
        }

        #endregion
    }
}
