using AutoMapper;
using LeoCore.Data;
using LeoCore.Data.Models;
using LeoCore.Data.Models.Trainings;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;


namespace LeoCore.Models.Trainings
{
    public class TrainingIdentificationViewModel 
    {
        #region Field names constants
        private const string cTrainingIdentificationViewModelClassName = "TrainingIdentificationViewModel";
        public static string TrainingIdentificationViewModelClassName
        {
            get { return cTrainingIdentificationViewModelClassName; }
        }

        private const string cSearchCriteriaFieldName = "SearchCriteria";
        public static string SearchCriteriaFieldName
        {
            get { return cSearchCriteriaFieldName; }
        }
        #endregion

        #region Constructor
        private readonly IMapper _mapper;
        private readonly UserCodeRepository _usercoderepository;
        private readonly ITrainingRepository _repository;

        public TrainingIdentificationViewModel(IUserCodeRepository usercoderepository, ITrainingRepository repository)
        {
            _repository = repository;
            UniqueID = Guid.NewGuid().ToString();
            AlleTrainings = _repository.FindAllTRAININGs();
            SearchCriteria = new TrainingSearchCriteria();
            SearchCriteria.APPLICANT_CLIENT_ID = "ruddi.cuyvers@limburg.wgk.be";
        }

        public IQueryable<LeoCore.Data.Models.TRAINING> AlleTrainings;

        public string UniqueID;
        public TrainingSearchCriteria SearchCriteria { get; set; }
        #endregion


    }
}