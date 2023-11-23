using LeoCore.Models;

using System.Linq;
using System.Threading.Tasks;
using System.Web;

using WGK.Lib.Web.Mvc.Controllers;
using System.Collections.Generic;
using LeoCore.Data;

using WGK.Lib.Web.Enumerations;
using WGK.Lib.Web.Mvc.Attributes;

using LEO.Common.Codes;


using LEO.Common.Constants.Questionnaire;
using LeoCore.Models;
using LeoCore.Data.Models;
using LEO.Common.Literals;

using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;

using LeoCore.Models.Questionnaires;
using AutoMapper;

namespace LeoCore.Controllers
{
   // [Authorize]
    public class QuestionnairesController : Controller
    {
        #region Constants
        /// <summary>
        /// Gets the name of the controller (without 'Controller' suffix)
        /// </summary>
        public static string ControllerName { get { return cControllerName; } }
        public const string cControllerName = "Questionnaires";

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
        private readonly IQuestionnaireRepository _repository;
        private readonly IUserCodeRepository _usercodcerepository;

        public QuestionnairesController(IQuestionnaireRepository repository, IUserCodeRepository usercodcerepository, IMapper mapper)
        {
            _repository = repository;
            _usercodcerepository = usercodcerepository;
            _mapper = mapper;
        }
        #endregion


        #region Action Methods - Maintenance


        /// <summary>
        /// GET: /Questionnaires/Maintenance/xxx
        /// </summary>
        [HttpGet]
        [NoCaching]
        public ActionResult Maintenance(int? pID, WGK.Lib.Web.Enumerations.ActivityStatusEnum? pActivity, bool pIsLeraar = false)
        {
           
            QuestionnaireMaintenanceViewModel vViewModel = this.GetMaintenanceViewModel(pID, pActivity, pIsLeraar);
            //if (vViewModel.Person_QuestionnaireDetail.ID == -1)  //-1 als ID is de waarde voor een nieuwe questionnaire
            //{
            //    vViewModel.ActivityStatus = ActivityStatusEnum.Insert;  //deze activity status bepaald welke knoppen zictbaar of enabled
            //}
            //else //bestaande questionnaire. Je mag enkel kijken maar niet meer aanpassen
            //{
            //    this.InfoMessageForRedirect = @"Vragenlijst voor training met vorminingsnummer " + vViewModel.Person_QuestionnaireDetail.TRAINING_ID + " is verzonden. Verzenddatum " + vViewModel.Person_QuestionnaireDetail.DATE_SUBMITTED;
            //    vViewModel.ActivityStatus = ActivityStatusEnum.View; //deze activity status bepaald welke knoppen zictbaar of enabled
            //}
            //vViewModel.ActivePanels = QuestionnaireMaintenanceViewModel.AccordionPanelHtmlIDDeel1 ;
           //**** vViewModel.Title = IocManager.Resolve<ITrainingRepository>().GetTRAINING((int)vViewModel.Person_QuestionnaireDetail.TRAINING_ID)?.SUBJECT;
            return this.View(vViewModel);
        }



        /// <summary>
        /// Action post method that creates or updates a Questionnaire instance in the database
        /// POST: /Questionnaire/Maintenance/xxx
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="pFormCollection"></param>
        /// <returns>A RedirectToRouteResult on success or a ViewResult on failure</returns>
        [HttpPost]
        [Authorize]  //$$µµ 
        public ActionResult Maintenance(long? pID, FormCollection pFormCollection)
        {
            // No need to wrap method in a try/catch, ErrorHandlerAttribute takes care of logging uncaught exceptions

            // Bind form collection to maintenance ViewModel and update in database
            QuestionnaireMaintenanceViewModel vViewModel;
            string vActivepanels = pFormCollection["ActivePanels"];
            if (this.UpdateMaintenanceViewModel((int?)pID, pFormCollection, out vViewModel))
            {
                // Successful: Refresh View in Read mode to show update results to user
                // Show info message to user to indicate update was successful
                //this.InfoMessageForRedirect = string.Format(
                //    pID == 0
                //        ? CommonLiterals.CreateSuccessfulInfoMessage
                //        : CommonLiterals.UpdateSuccessfulInfoMessage,
                //    QUESTIONNAIREDisplayNames.cIDDisplayName);

                // After a successful update, redirect to a HttpGet action
                // Redirect and switch to View modus WITHOUT restoring the active panel
               //return this.RedirectToAction("Maintenance", new { pID = vViewModel.Person_QuestionnaireDetail.TRAINING_ID });
              
            }

            // If we get here we have either:
            // - Binding errors: don't update database and show binding errors in the View
            // - Failure: Show DossierUpdateUseCase update errors in the View
            if (pID == 0)
            {
                //vViewModel.Title = string.Format(CommonLiterals.MaintenancePageCreateTitle, QUESTIONNAIREDisplayNames.cIDDisplayName);
                //vViewModel.MainTitle = string.Format(CommonLiterals.MaintenancePageCreateTitle, QUESTIONNAIREDisplayNames.cIDDisplayName);
                //vViewModel.ActivityStatus = ActivityStatusEnum.Insert;
            }
            else
            {
                //vViewModel.Title = string.Format(CommonLiterals.MaintenancePageEditTitle, QUESTIONNAIREDisplayNames.cIDDisplayName);
                //vViewModel.MainTitle = string.Format(CommonLiterals.MaintenancePageEditTitle, QUESTIONNAIREDisplayNames.cIDDisplayName);
                //vViewModel.ActivityStatus = ActivityStatusEnum.Edit;
            }

            // Restore the active panel
            //vViewModel.ActivePanels = vActivepanels.IsNullOrEmptyOrBlankCode()
            //    ? QuestionnaireMaintenanceViewModel.AccordionPanelHtmlIDDeel1
            //    : vActivepanels;

           
             return this.View(vViewModel);
            
           
        }



        #endregion



        #region Private Methods - Maintenance
        /// <summary>
        /// Returns a Questionnaire maintenance viewmodel for the specified  ID
        /// </summary>
        /// <param name="pID">Questionnaire ID</param>
        /// <param name="pActivity">Optional activity status for the View </param>
        /// <returns></returns>
        /// //https://localhost:44335/Questionnaires/Maintenance?pID=22&&pTRAININGID=1
        private QuestionnaireMaintenanceViewModel GetMaintenanceViewModel(int? pID, ActivityStatusEnum? pActivity = null, bool pIsLeraar = false)
        {
            //pID is hier de TrainingID. We gaan adhv de training ID kijken of 

            ////eerst checkenof we voor deze trainingID en voor deze ClientID reeds een Questionnaire hebben ingevuld
            //var vMaintenanceUseCase = IocManager.Resolve<IPersonQuestionnaireMaintenanceUseCase>();
            //vMaintenanceUseCase.ValidationDictionary = new ModelStateWrapper(this.ModelState);
            //vMaintenanceUseCase.ID = (pID == null) ? 0 : pID.Value;
            //vMaintenanceUseCase.IsLeraar = pIsLeraar;
            //vMaintenanceUseCase.CurrentUserClientID = ""; // Request.GetOwinContext().Authentication.User.Identity.Name;  //$$µµ Session["ClaimsEmail"].ToString();
            //vMaintenanceUseCase.Execute();
            //Business.Models.Questionnaires.PersonQuestionnaireMaintenanceModel vPersonQuestionnaireMaintenanceModel = vMaintenanceUseCase.Result;

            // Map QuestionnaireMaintenanceModel (business layer) to QuestionnaireMaintenanceViewModel (presentation layer)
           // var vViewModel = null; // MapHelper.MapSingle(vPersonQuestionnaireMaintenanceModel).To<QuestionnaireMaintenanceViewModel>();
            //titel en lees/edit enz
            string vTitleFormatString;
            if (pID == 0)
            {
                // Create new Questionnaire
                
                vTitleFormatString = CommonLiterals.MaintenancePageCreateTitle;
            }
            else
            {
                // bestaande Questionnaire openen. Je mag enkel kijken maar niets aanpassen
               
                vTitleFormatString = CommonLiterals.MaintenancePageViewTitle;
            }

        
            //ViewModel good to go
            return null;
        }

        /// <summary>
        /// Binds form collection to a maintenance viewmodel and updates it in the database.
        /// Outputs a reference to the maintenance viewmodel with updated IndieningID or error messages.
        /// </summary>
        /// <param name="pID"></param>
        /// <param name="pFormCollection"></param>
        /// <param name="pViewModel">out parameter that receives the maintenance viewmodel in case of failure</param>
        /// <returns>True if successful, false on failure</returns>
        private bool UpdateMaintenanceViewModel(
            int? pID,
            FormCollection pFormCollection,
            out QuestionnaireMaintenanceViewModel pViewModel)
        {
            // Fetch Questionnaire from database so we have an instance to bind posted values against.
            // Otherwise read-only fields (that are not posted) would be cleared during the update!

            // Create an instance of the UseCase through dependency injection container
            //var vMaintenanceUseCase = IocManager.Resolve<IPersonQuestionnaireMaintenanceUseCase>();
            //vMaintenanceUseCase.ID = (pID == null) ? 0 : pID.Value;
            //vMaintenanceUseCase.CurrentUserClientID = ""; // Request.GetOwinContext().Authentication.User.Identity.Name; //$$µµSession["ClaimsEmail"].ToString();
            //vMaintenanceUseCase.Execute();
            //Business.Models.Questionnaires.PersonQuestionnaireMaintenanceModel vPersonQuestionnaireMaintenanceModel;
            //vPersonQuestionnaireMaintenanceModel = vMaintenanceUseCase.Result;

            //// Map QuestionnaireMaintenanceModel (business layer) to QuestionnaireMaintenanceViewModel (presentation layer)
            //// so we have GridViewModel properties to bind against.
            //var vViewModel = MapHelper.MapSingle(vPersonQuestionnaireMaintenanceModel).To<QuestionnaireMaintenanceViewModel>();

            //// Output the Maintenance ViewModel so a View can be rendered with error messages in case the update fails
            pViewModel = null; // vViewModel;
            ////ToDO: dit test dingetje hieronder weg
            ////foreach (var key in pFormCollection.AllKeys)
            ////{
            ////    var value = pFormCollection[key];
            ////}

            //// Update Questionnaire instance and child table rows with values from posted FormCollection
            //if (this.BindModel(vViewModel, pFormCollection))
            //{
            //    //ToDo: terug enablen Ruddi20221122
            //    // Save changes to database
            //    decimal vPersonQuestionnaireID = 0;

            //    //// Create an instance of the UseCase through dependency injection container
            //    IPersonQuestionnaireUpdateUseCase vUpdateUseCase = IocManager.Resolve<IPersonQuestionnaireUpdateUseCase>();

            //    //// UseCase needs a reference to the ModelStateDictionary for returning validation messages
            //    vUpdateUseCase.ValidationDictionary = new ModelStateWrapper(this.ModelState);

            //    //// Map QuestionnaireMaintenanceViewModel (presentation layer) to QuestionnaireUpdateModel (business layer)
            //    vUpdateUseCase.UpdateData = MapHelper.MapSingle(vViewModel).To<PersonQuestionnaireUpdateModel>();

            //    try
            //    {
            //        vUpdateUseCase.Execute();
            //        vPersonQuestionnaireID = (decimal)vUpdateUseCase.ResultID;
            //    }
            //    catch (DbConcurrencyException)
            //    {
                    
            //    }

            //    if (vPersonQuestionnaireID != 0) // successful
            //    {
            //        // Update the ViewModel with the created IndieningID
            //        vViewModel.Person_QuestionnaireDetail.ID = (int)vPersonQuestionnaireID;
            //        return true;
            //    }
            //    else // update failure
            //    {
                  
            //    }
                
            //}

            //// If we get here we have either:
            //// - Binding errors: don't update database and show binding errors in the View
            //// - Failure: Show QuestionnaireUpdateUseCase update errors in the View
            return false; // failure
        }

      

        ///// <summary>
        ///// Updates QuestionnaireDetail model with posted form data.
        ///// </summary>
        ///// <param name="pModel"></param>
        ///// <param name="pFormCollection"></param>
        ///// <returns></returns>
        //private bool BindQuestionnaire(QuestionnaireMaintenanceViewModel pModel, FormCollection pFormCollection)
        //{
        //    bool vHasValidationErrors = false;
          
        //    //String[] strlist = pFormCollection["ANSWER_TEXT"].Split(',');
        //    //int i = 0;
        //    //foreach (String s in strlist)
        //    //{
        //    //    pModel.Person_QuestionnaireDetail.PERSON_QUESTIONNAIRE_ANSWERDetails[i].ANSWER_TEXT = s;
        //    //    i++;
        //    //}
        //    //if (!this.TryUpdateModel(pModel.Person_QuestionnaireDetail, "Person_QuestionnaireDetail"))
        //    //{
        //    //    vHasValidationErrors = true;
        //    //}
           
        //    return !vHasValidationErrors;
        //}
        #endregion

    }
}