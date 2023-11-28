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
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography;

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
        public ViewResult Maintenance(int? pID, bool pIsLeraar = false)
        {
            var vViewModel = GetMaintenanceViewModel(pID);
            return this.View(vViewModel);
        }

        [HttpPost]
        [NoCaching]
        public async Task<IActionResult> Maintenance(int? pID)
        {
            //if (this.ModelState.IsValid)
            //{
            if (pID.Value == null)
            {
                var vTraining = new TRAINING();
                if (await TryUpdateModelAsync<Data.Models.TRAINING>(vTraining, "TRAININGDetail"))
                {
                    try
                    {
                        _repository.AddTraining(vTraining);
                        _repository.Save();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction(nameof(Identification));
                }

            }
            else //heeft wel een id dus bestaande training.
            {

                //binding
                var TrainingToUpdate = _repository.GetTRAINING(pID);
                if (await TryUpdateModelAsync<Data.Models.TRAINING>(TrainingToUpdate, "TRAININGDetail"))
                {
                    try
                    {
                        _repository.Save();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction(nameof(Identification));
                }
            }

            // mochht in de if or else iets mus zijn gegaan
            return RedirectToAction(nameof(Identification));
            //}
            //else
            //{
            //this.ModelState.AddModelError("Error error error ", "ieieie");
            //    return this.View(GetMaintenanceViewModel(id));
            //}
        }

        #endregion


        private TrainingIdentificationViewModel GetIdentificationViewModel()
        {
            TrainingIdentificationViewModel vTrainingIdentificationViewModel;
            
                vTrainingIdentificationViewModel = new TrainingIdentificationViewModel(_usercodcerepository, _repository, _mapper )
                {
                    
                };
            
            return vTrainingIdentificationViewModel; ;
        }
        private TrainingMaintenanceViewModel GetMaintenanceViewModel(int? pID, ActivityStatusEnum? pActivity = null)
        {
            TrainingMaintenanceViewModel vTrainingMaintenanceModel;
            if (((pID == null || pID == 0) || (pActivity == ActivityStatusEnum.Select)))
            {
                vTrainingMaintenanceModel = new TrainingMaintenanceViewModel(_usercodcerepository)
                {
                    TRAININGDetail = new TRAINING()
                };
            }
            else
            {
                vTrainingMaintenanceModel = new TrainingMaintenanceViewModel(_usercodcerepository)
                {
                    TRAININGDetail = _repository.GetTRAINING(pID.Value, false, false, false)
                };
            }
            return vTrainingMaintenanceModel; ;
        }
    }
}
