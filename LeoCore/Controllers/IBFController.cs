using LeoCore.Models;

using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Collections.Generic;
using LEO.Common.Codes;
using LeoCore.Models.IBF;
using LEO.Common.Literals;
using LEO.Common.Constants.IBF;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using LeoCore.Data;
using AutoMapper;
using System.IO;
using static System.Formats.Asn1.AsnWriter;


namespace LeoCore.Controllers
{
    // [Authorize]
    public class IBFController : Controller
    {
        #region Constants
        /// <summary>
        /// Gets the name of the controller (without 'Controller' suffix)
        /// </summary>
        public static string ControllerName { get { return cControllerName; } }
        public const string cControllerName = "Mijn IBF";

        /// <summary>
        /// Gets the name of the Identification method
        /// </summary>
        public static string IdentificationMethodName { get { return cIdentificationMethodName; } }
        private const string cIdentificationMethodName = "Identification";

        /// <summary>
        /// Gets the name of the IdentificationPartial method
        /// </summary>
        public static string IdentificationPartialMethodName { get { return cIdentificationPartialMethodName; } }
        private const string cIdentificationPartialMethodName = "IdentificationPartial";

        /// <summary>
        /// Gets the name of the Identification json method
        /// </summary>
        public static string IdentificationJsonMethodName { get { return cIdentificationJsonMethodName; } }
        private const string cIdentificationJsonMethodName = "Identification";

        /// <summary>
        /// Gets the name of the Maintenance method
        /// </summary>
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
        
        public IBFController(ITrainingRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        #endregion

        #region Action Methods - Identification

        [HttpGet]
        public ViewResult Identification(string pID, string pJaartal, string pInternExtern)
        {
            var vViewModel = GetIdentificationViewModel();
            return this.View(vViewModel);
        }
        #endregion


        #region Private Methods - Identification
        private IBFIdentificationViewModel GetIdentificationViewModel()
        {
            IBFIdentificationViewModel vIBFIdentificationModel = new IBFIdentificationViewModel();
            IEnumerable<Data.Models.TRAINING> trainingsItems = _repository.FindAllTRAININGs();
            var infoviewmodels = _mapper.Map<IEnumerable<Data.Models.TRAINING>, IEnumerable<IBFInfoViewModel>>(trainingsItems);
            vIBFIdentificationModel.MijnIBFTrainingen = (ICollection<IBFInfoViewModel>)infoviewmodels;
            return vIBFIdentificationModel;
        }
        #endregion



    }
}