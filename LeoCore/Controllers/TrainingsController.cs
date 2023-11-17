using LeoCore.Models.Trainings;
using Microsoft.AspNetCore.Mvc;
using WGK.Lib.Web.Enumerations;
using LeoCore.Data.Models;
using LeoCore.Data;
using AutoMapper;
using LEO.Common.Constants.Trainings;
using LEO.Common.Literals;
using System.Net.Mail;
using System.Net;
using WGK.Lib.Web.Mvc.Attributes;

namespace LeoCore.Controllers
{
    public class TrainingsController : Controller
    {
        #region Constants
        /// <summary>
        /// Gets the name of the controller (without 'Controller' suffix)
        /// </summary>
        public static string ControllerName { get { return cControllerName; } }
        public const string cControllerName = "Training";


        public static string MaintenanceMethodName { get { return cMaintenanceMethodName; } }
        private const string cMaintenanceMethodName = "Maintenance";

        /// <summary>
        /// Gets the name of the MaintenancePartial method
        /// </summary>
        public static string MaintenancePartialMethodName { get { return cMaintenancePartialMethodName; } }
        private const string cMaintenancePartialMethodName = "MaintenancePartial";

        /// <summary>
        /// Gets the name of the AutoComplete json method
        /// </summary>
        public static string AutoCompleteJsonMethodName { get { return cAutoCompleteJsonMethodName; } }
        private const string cAutoCompleteJsonMethodName = "AutoComplete";

        #endregion

        #region Constructors

        private readonly IMapper _mapper;
        private readonly ITrainingRepository _repository;
        private readonly IUserCodeRepository _usercodcerepository;

        public TrainingsController(ITrainingRepository repository, IUserCodeRepository usercodcerepository, IMapper mapper)
        {
            _repository = repository;
            _usercodcerepository = usercodcerepository;
            _mapper = mapper;
        }
        #endregion

        [HttpGet]
        public ViewResult Identification()
        {
    
            var vViewModel = GetIdentificationViewModel();
            return this.View(vViewModel);
        }

        #region Action Methods - Maintenance

        [HttpGet]
        public ViewResult Maintenance(int id)
        {
            
            var vViewModel = GetMaintenanceViewModel(id);
                    
            return this.View(vViewModel);
        }

        [HttpPost]
        [NoCaching]
        public ActionResult Maintenance(long? pID, IFormCollection pFormCollection)
        {

                    return null;

            
        }
        #endregion


        private TrainingIdentificationViewModel GetIdentificationViewModel()
        {
            TrainingIdentificationViewModel vTrainingIdentificationViewModel;
            
                vTrainingIdentificationViewModel = new TrainingIdentificationViewModel(_usercodcerepository, _repository )
                {
                    
                };
            
            return vTrainingIdentificationViewModel; ;
        }
        private TrainingMaintenanceViewModel GetMaintenanceViewModel(int? pID, ActivityStatusEnum? pActivity = null)
        {
            TrainingMaintenanceViewModel vTrainingMaintenanceModel;
            if (pID == null || pActivity == ActivityStatusEnum.Select)
            {
                vTrainingMaintenanceModel = new TrainingMaintenanceViewModel()
                {
                    TRAININGDetail = new TRAINING()
                };
            }
            else
            {


                vTrainingMaintenanceModel = new TrainingMaintenanceViewModel()
                {
                    TRAININGDetail = _repository.GetTRAINING(pID.Value, false, false, false)

                };

                vTrainingMaintenanceModel.TRAININGDetail.SUBJECT = "ggggggg";
                _repository.Save();

            }
            return vTrainingMaintenanceModel; ;
        }
    }
}
