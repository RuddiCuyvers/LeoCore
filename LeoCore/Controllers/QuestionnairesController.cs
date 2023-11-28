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
using Microsoft.EntityFrameworkCore;

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


        private const string cInfoMessageKey = "INFO_MSG";
        public static string InfoMessageKey
        {
            get { return cInfoMessageKey; }
        }
        private const string cErrorMessageKey = "ERROR_MSG";
        public static string ErrorMessageKey
        {
            get { return cErrorMessageKey; }
        }

        #endregion

        #region Constructors

        private readonly IMapper _mapper;
        private readonly IPersonQuestionnaireRepository _personquestionnairerepository;
        private readonly ITrainingRepository _trainingrepository;
        private readonly IQuestionnaireRepository _questionnairerepository;
        private readonly IUserCodeRepository _usercoderepository;

        public string InfoMessageForView
        {
            get { return this.ViewData[cInfoMessageKey] as string; }
            set { this.ViewData[cInfoMessageKey] = value; }
        }

        /// <summary>
        /// Gets, sets the information message that is displayed in the redirected View
        /// </summary>
        public string InfoMessageForRedirect
        {
            get { return this.TempData[cInfoMessageKey] as string; }
            set { this.TempData[cInfoMessageKey] = value; }
        }

        /// <summary>
        /// Gets, sets the Error message that is displayed in the View
        /// </summary>
        public string ErrorMessageForView
        {
            get { return this.ViewData[cErrorMessageKey] as string; }
            set { this.ViewData[cErrorMessageKey] = value; }
        }

        /// <summary>
        /// Gets, sets the Error message that is displayed in the redirected View
        /// </summary>
        public string ErrorMessageForRedirect
        {
            get { return this.TempData[cErrorMessageKey] as string; }
            set { this.TempData[cErrorMessageKey] = value; }
        }

        public QuestionnairesController(ITrainingRepository trainingrepository, IQuestionnaireRepository questionnairerepository, IPersonQuestionnaireRepository personquestionnairerepository, IUserCodeRepository usercodcerepository, IMapper mapper)
        {
            _personquestionnairerepository = personquestionnairerepository;
            _trainingrepository = trainingrepository;
            _questionnairerepository = questionnairerepository;
            _usercoderepository = usercodcerepository;
            _mapper = mapper;
        }
        #endregion


        #region Action Methods - Maintenance

        /// <summary>
        /// GET: /Questionnaires/Maintenance/xxx
        ///  /// //https://localhost:44335/Questionnaires/Maintenance?pID=22&&pTRAININGID=1
        /// </summary>
        [HttpGet]
        [NoCaching]
        public ActionResult Maintenance(int? pID, bool pIsLeraar = false)
        {
            //TEST interface
            pID = 55;
            LeoCore.Data.Client client1 = new LeoCore.Data.Client(new LeoCore.Data.Service1());
            client1.ServeMethod();
            //pID is de trainingID.
            QuestionnaireMaintenanceViewModel vViewModel = this.GetMaintenanceViewModel(pID.Value, pIsLeraar);
            
            if (vViewModel.Person_QuestionnaireDetail.ID == -1)  //-1 als ID is de waarde voor een nieuwe questionnaire
            {
                vViewModel.ActivityStatus = ActivityStatusEnum.Insert;  //deze activity status bepaald welke knoppen zictbaar of enabled
            }
            else //bestaande questionnaire. Je mag enkel kijken maar niet meer aanpassen
            {
                this.InfoMessageForRedirect = @"Vragenlijst voor training met vorminingsnummer " + vViewModel.Person_QuestionnaireDetail.TRAINING_ID + " is verzonden. Verzenddatum " + vViewModel.Person_QuestionnaireDetail.DATE_SUBMITTED;
                vViewModel.ActivityStatus = ActivityStatusEnum.View; //deze activity status bepaald welke knoppen zictbaar of enabled
            }
           
            return this.View(vViewModel);
        }

     
        [HttpPost]
        [NoCaching]
        public async Task<IActionResult> Maintenance(int? pID)
        {
            //pID is de Person_QuestionnaireDetail.ID
            var ids = Request.Form["Person_QuestionnaireDetail.ID"];
            
            if (pID.HasValue == false)  //nieuwe
            {
                var vPerson_QuestionnaireDetail = new PERSON_QUESTIONNAIRE();
                if (await TryUpdateModelAsync<Data.Models.PERSON_QUESTIONNAIRE>(vPerson_QuestionnaireDetail, "Person_QuestionnaireDetail"))
                {
                    try
                    {
                        _personquestionnairerepository.AddPERSON_QUESTIONNAIRE(vPerson_QuestionnaireDetail);
                        _personquestionnairerepository.Save();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                        //Log the error (uncomment ex variable name and write a log.)
                        ModelState.AddModelError("", "Unable to save changes. " +
                            "Try again, and if the problem persists, " +
                            "see your system administrator.");
                    }
                    return RedirectToAction(nameof(Maintenance));
                }

            }
            else //heeft wel een id dus bestaande training.
            {

                //binding
                PERSON_QUESTIONNAIRE vPerson_QuestionnaireToUpdate = _personquestionnairerepository.GetPERSON_QUESTIONNAIRE(pID.Value,false, true,true) ;

                                                                                                     //GetPERSON_QUESTIONNAIREByTRAININGIDAndByCLIENTID(pID, "ruddi.cuyvers@limburg.wgk.be", true, true);

                //    _personquestionnairerepository.Save();





                if (await TryUpdateModelAsync<Data.Models.PERSON_QUESTIONNAIRE>(vPerson_QuestionnaireToUpdate, "Person_QuestionnaireDetail"))
                {

                    foreach(var x in vPerson_QuestionnaireToUpdate.PERSON_QUESTIONNAIRE_ANSWERs)
                    {
                        x.ANSWER_DATE = System.DateTime.Now;    
                        x.PERSON_QUESTIONNAIRE = vPerson_QuestionnaireToUpdate;  //ruddi20231128 na de tryUpdateModel werd person_questionnaire entity leeg. Dit gaf een error bij de Save. Link verbroken (severed). Door deze for each wordt dit hersteld. Niet ideaal en verder uit te zoeken waarom.

                    }
                    //vPerson_QuestionnaireToUpdate.PERSON_QUESTIONNAIRE_ANSWERs[0].PERSON_QUESTIONNAIRE = vPerson_QuestionnaireToUpdate;
                    //vPerson_QuestionnaireToUpdate.PERSON_QUESTIONNAIRE_ANSWERs[1].PERSON_QUESTIONNAIRE = vPerson_QuestionnaireToUpdate;
                    //vPerson_QuestionnaireToUpdate.PERSON_QUESTIONNAIRE_ANSWERs[2].PERSON_QUESTIONNAIRE = vPerson_QuestionnaireToUpdate;
                    //vPerson_QuestionnaireToUpdate.PERSON_QUESTIONNAIRE_ANSWERs[3].PERSON_QUESTIONNAIRE = vPerson_QuestionnaireToUpdate;
                    //vPerson_QuestionnaireToUpdate.PERSON_QUESTIONNAIRE_ANSWERs[4].PERSON_QUESTIONNAIRE = vPerson_QuestionnaireToUpdate;
                    //vPerson_QuestionnaireToUpdate.PERSON_QUESTIONNAIRE_ANSWERs[5].PERSON_QUESTIONNAIRE = vPerson_QuestionnaireToUpdate;
                    //vPerson_QuestionnaireToUpdate.PERSON_QUESTIONNAIRE_ANSWERs[6].PERSON_QUESTIONNAIRE = vPerson_QuestionnaireToUpdate;
                    try
                    {
                       // _personquestionnairerepository.Update(vPerson_QuestionnaireToUpdate);
                        _personquestionnairerepository.Save();
                    }
                    catch (DbUpdateException /* ex */)
                    {
                //        //Log the error (uncomment ex variable name and write a log.)
                //        ModelState.AddModelError("", "Unable to save changes. " +
                //            "Try again, and if the problem persists, " +
                //            "see your system administrator.");
                    }
                //    return RedirectToAction(nameof(Maintenance));
                }
            }

            // mochht in de if or else iets mus zijn gegaan
            return RedirectToAction(nameof(Maintenance));



            // No need to wrap method in a try/catch, ErrorHandlerAttribute takes care of logging uncaught exceptions

            // Bind form collection to maintenance ViewModel and update in database
            QuestionnaireMaintenanceViewModel vViewModel;
         
            //if (this.UpdateMaintenanceViewModel((int?)pID, pFormCollection, out vViewModel))
            //{
            //    // Successful: Refresh View in Read mode to show update results to user
            //    // Show info message to user to indicate update was successful
            //    //this.InfoMessageForRedirect = string.Format(
            //    //    pID == 0
            //    //        ? CommonLiterals.CreateSuccessfulInfoMessage
            //    //        : CommonLiterals.UpdateSuccessfulInfoMessage,
            //    //    QUESTIONNAIREDisplayNames.cIDDisplayName);

            //    // After a successful update, redirect to a HttpGet action
            //    // Redirect and switch to View modus WITHOUT restoring the active panel
            //   //return this.RedirectToAction("Maintenance", new { pID = vViewModel.Person_QuestionnaireDetail.TRAINING_ID });
              
            //}

            //// If we get here we have either:
            //// - Binding errors: don't update database and show binding errors in the View
            //// - Failure: Show DossierUpdateUseCase update errors in the View
            //if (pID == 0)
            //{
            //    vViewModel.Title = string.Format(CommonLiterals.MaintenancePageCreateTitle, QUESTIONNAIREDisplayNames.cIDDisplayName);
            //    vViewModel.MainTitle = string.Format(CommonLiterals.MaintenancePageCreateTitle, QUESTIONNAIREDisplayNames.cIDDisplayName);
            //    vViewModel.ActivityStatus = ActivityStatusEnum.Insert;
            //}
            //else
            //{
            //    vViewModel.Title = string.Format(CommonLiterals.MaintenancePageEditTitle, QUESTIONNAIREDisplayNames.cIDDisplayName);
            //    vViewModel.MainTitle = string.Format(CommonLiterals.MaintenancePageEditTitle, QUESTIONNAIREDisplayNames.cIDDisplayName);
            //    vViewModel.ActivityStatus = ActivityStatusEnum.Edit;
            //}

            //// Restore the active panel
            //vViewModel.ActivePanels = string.IsNullOrEmpty(vActivepanels)
            //    ? QuestionnaireMaintenanceViewModel.AccordionPanelHtmlIDDeel1
            //    : vActivepanels;

           
             return this.View(vViewModel);
            
           
        }

        #endregion



        #region Private Methods - Maintenance
        private QuestionnaireMaintenanceViewModel GetMaintenanceViewModel(int pID, bool pIsLeraar = false)
        {
            var vQuestionnaireMaintenanceViewModel = new QuestionnaireMaintenanceViewModel(_usercoderepository);
            vQuestionnaireMaintenanceViewModel.UniqueID = Guid.NewGuid().ToString();
            //pID is hier de TrainingID. We gaan adhv de training ID kijken of 
            //eerst checkenof we voor deze trainingID en voor deze ClientID reeds een Questionnaire hebben ingevuld
            if (_questionnairerepository.GetQUESTIONNAIREByTRAININGID(pID) != null)
            {
                LeoCore.Data.Models.PERSON_QUESTIONNAIRE vPersonQuestionnaire = _personquestionnairerepository.GetPERSON_QUESTIONNAIREByTRAININGIDAndByCLIENTID(pID, "ruddi.cuyvers@limburg.wgk.be", true, true);
                if(vPersonQuestionnaire == null)
                {

                    vPersonQuestionnaire = new PERSON_QUESTIONNAIRE();
                    vPersonQuestionnaire.CLIENT_ID = "ruddi.cuyvers@limburg.wgk.be";
                    vPersonQuestionnaire.TRAINING_ID = pID;
                    

                }
                else
                {
                    vQuestionnaireMaintenanceViewModel.QuestionnaireDetail = vPersonQuestionnaire.QUESTIONNAIRE;

                }
                
                
             
                //onderstaand een lijstje met alle questions. De pure Question uit de table Question. Dit om de Text en type te vinden.
                //Hier de IList maken. Zodat we niet steeds op en neer naar de repo en context moeten gaan
                IList<QUESTION>vAllQuestionsList = _questionnairerepository.FindAllQuestions();
                //alle vragen die bij een questionnaire horen 
                foreach (var i in vQuestionnaireMaintenanceViewModel.QuestionnaireDetail.QUESTIONNAIRE_QUESTIONs)
                {
                    //als er nog geen geen Person_Questionnnaire record is . Bij voorbeeld bij een nieuwe deze dan maken.
                    if (vPersonQuestionnaire.PERSON_QUESTIONNAIRE_ANSWERs == null || vPersonQuestionnaire.PERSON_QUESTIONNAIRE_ANSWERs.ToList().Exists(m => m.QUESTION_ID == i.QUESTION_ID) == false)
                    {
                        PERSON_QUESTIONNAIRE_ANSWER lPersonQuestionnaireAnswer = new PERSON_QUESTIONNAIRE_ANSWER();
                        lPersonQuestionnaireAnswer.ID = -1;  //om aan te geven dat het een nieuwe is
                        lPersonQuestionnaireAnswer.PERSON_QUESTIONNAIRE = vPersonQuestionnaire;
                        lPersonQuestionnaireAnswer.PERSON_QUESTIONNAIRE_ID = vPersonQuestionnaire.ID;
                        lPersonQuestionnaireAnswer.QUESTION_ID = i.QUESTION_ID;
                        lPersonQuestionnaireAnswer.QQORDER_AS_WAS = i.SORTORDER;
                        lPersonQuestionnaireAnswer.QTEXT_AS_WAS = vAllQuestionsList.Where(q=>q.ID == i.QUESTION_ID).Select(n=>n.TEXT).First();
                        lPersonQuestionnaireAnswer.QTYPEANSWER_AS_WAS = vAllQuestionsList.Where(q => q.ID == i.QUESTION_ID).Select(n => n.TYPE_ANSWER).First();  
                        vPersonQuestionnaire.PERSON_QUESTIONNAIRE_ANSWERs.Add(lPersonQuestionnaireAnswer);
                    }

                }
                vQuestionnaireMaintenanceViewModel.Person_QuestionnaireDetail = vPersonQuestionnaire;
            }

            return vQuestionnaireMaintenanceViewModel;
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