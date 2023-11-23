using AutoMapper;
using LEO.Common.Codes;
using LeoCore.Data;
using LeoCore.Data.Models;
using LeoCore.Data.Models.Trainings;
using LeoCore.Models.IBF;
using Microsoft.IdentityModel.Tokens;
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
        private readonly IUserCodeRepository _usercoderepository;
        private readonly ITrainingRepository _repository;

        public TrainingIdentificationViewModel(IUserCodeRepository usercoderepository, ITrainingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
            _usercoderepository = usercoderepository;
            UniqueID = Guid.NewGuid().ToString();
            List<LeoCore.Data.Models.TRAINING>  AlleTrainings = _repository.FindAllTRAININGs().ToList();

            //https://groups.google.com/g/automapper-users/c/z_NttKf-gp4?pli=1 automapper met afermap fct  werkt helaas  niet bij Ieneumerables in de Map
            //var infoviewmodels = _mapper.Map<IEnumerable<Data.Models.TRAINING>, IEnumerable<TrainingInfoViewModel>>(AlleTrainings); //, opt => opt.AfterMap((src, dest) => dest.TRAINING_TYPE = usercoderepository.GetUserCodeDescription(src.TRAINING_TYPE, UserCodeGroupCode.cTypeTrainingLijst)));
            //var infoviewmodelsX = _mapper.Map<IEnumerable<Data.Models.TRAINING>, IEnumerable<TrainingInfoViewModel>>(AlleTrainings, opt => opt.AfterMap((src, dest) => dest.TRAINING_TYPE = _usercoderepository.GetUserCodeDescription(src.TRAINING_TYPE, UserCodeGroupCode.cTypeTrainingLijst)));

            AlleTrainingInfo = MapAndConvertTRAININGtoTrainingInfoViewModel(AlleTrainings).AsQueryable();
            SearchCriteria = new TrainingSearchCriteria();
            SearchCriteria.APPLICANT_CLIENT_ID = "ruddi.cuyvers@limburg.wgk.be";
        }

        public IQueryable<TrainingInfoViewModel> AlleTrainingInfo;

        public string UniqueID;
        public TrainingSearchCriteria SearchCriteria { get; set; }

        private List<TrainingInfoViewModel> MapAndConvertTRAININGtoTrainingInfoViewModel(List<TRAINING> pTrainingList)
        {
            //we hebben deze fct moeten maken omdat automapper met IEnumerables niet werkt als je de waarden in de source ook nog eens wilt opzoeken in Usercode tbl. bv. via aftermap
            // zie deze link    //https://groups.google.com/g/automapper-users/c/z_NttKf-gp4?pli=1 automapper werkt helaas  niet bij Ieneumerables
            List<TrainingInfoViewModel> a = new List<TrainingInfoViewModel>();
            foreach (TRAINING t in pTrainingList)
            {
                TrainingInfoViewModel b = new TrainingInfoViewModel();
                b.TrainingID = t.ID;
                b.ONDERWERP = t.SUBJECT;
                b.TRAINING_TYPE = t.TRAINING_TYPE.IsNullOrEmpty() ? "" : _usercoderepository.GetUserCodeDescription(t.TRAINING_TYPE, UserCodeGroupCode.cTypeTrainingLijst);
                b.NOMENCLATUUR_YN = t.NC_NOMC_YN.IsNullOrEmpty() ? "" : _usercoderepository.GetUserCodeDescription(t.NC_NOMC_YN, UserCodeGroupCode.cJaNeeLijst);
                b.LINK = t.LINK;
                b.INTERNEXTERN = t.TRAINER_INT_EXT.IsNullOrEmpty() ? "" : _usercoderepository.GetUserCodeDescription(t.TRAINER_INT_EXT, UserCodeGroupCode.cEXT_INTLijst);
                b.EVIDENCEBASED_YN = t.EV_YN.IsNullOrEmpty() ? "" : _usercoderepository.GetUserCodeDescription(t.EV_YN, UserCodeGroupCode.cJaNeeLijst);
                a.Add(b);
            }
            return a;

        }
        #endregion


    }
}