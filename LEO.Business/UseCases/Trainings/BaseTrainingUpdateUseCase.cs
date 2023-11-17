using System;
using System.CodeDom;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Helpers;
using LEO.Business.Interfaces.Trainings;

using LEO.Business.Models.Trainings;
using LEO.Business.Validators;
using LEO.Common.Codes;
using LEO.Common.Constants;
using LEO.Common.Constants.Trainings;
using LEO.Common.Literals;

using LEO.Data.Interfaces;

using WGK.Lib.DataAnnotations;
using IronBarCode;

using WGK.Lib.Exceptions;
using WGK.Lib.Extensions;
using WGK.Lib.Ioc;

using WGK.Lib.Mappers;

using WGK.Lib.Security;

using WGK.Lib.UseCases;

using WGK.Lib.Validation;
using System.Drawing;
using System.IO;

using QRCoder;


using System.Data.Entity;
using Microsoft.AspNetCore.Mvc.Routing;
using Microsoft.AspNetCore.Http;
using LEO.Data.Models;


namespace LEO.Business.UseCases.Trainings
{
    /// <summary>
    /// Abstract generic Training Update UseCase base class providing functionality that is common to all TrainingTypes.
    /// </summary>
    /// <typeparam name="TUpdateModel">The UpdateModel type. The model must derive BaseTrainingUpdateModel </typeparam>
    /// <typeparam name="TTrainingDto">The DTO type for the specific Training. The DTO class must derive from TrainingrDetail</typeparam>
    public abstract class BaseTrainingUpdateUseCase<TUpdateModel, TTrainingDto>
        : BaseUpdateUseCase<TUpdateModel>,
        IBaseTrainingUpdateUseCase<TUpdateModel, TTrainingDto>
        where TUpdateModel : BaseTrainingUpdateModel<TTrainingDto>
        where TTrainingDto : TRAININGDetail
    {

        #region Constants

        #endregion

        #region Fields
        protected readonly ITrainingRepository iTrainingRepository;


        // Base table instance
        protected TRAINING iTraining;

        #endregion

        #region Constructors
        /// <summary>
        /// Initializes a new instance of the BaseTrainingUpdateUseCase class.
        /// </summary>

        /// <param name="pTrainingRepository">The Training repository.</param>

        protected BaseTrainingUpdateUseCase(
            ITrainingRepository pTrainingRepository)
        {
            this.iTrainingRepository = pTrainingRepository;
        }
        #endregion

        #region BaseUseCase overrides - DoExecute
        protected override void ExecuteOverride()
        {
            if (this.UpdateData == null)
            {
                throw new ParameterMissingException(TRAININGDisplayNames.cTRAINING_TYPEDisplayName);
            }

            if (this.UpdateData.TRAININGDetail == null)
            {
                throw new ParameterMissingException(
                    TRAININGDisplayNames.cTrainingEntityDisplayName);
            }

            if (this.iValidationDictionary == null)
            {
                // Client must set ValidationDictionary through its property
                throw new ParameterMissingException("ValidationDictionary");
            }

            // Fetch Training instance with specific detail table and related child table instances from database
            this.FetchData();

            // Merge TTrainingDto instance (Business layer, e.g. TRAININGDetail) into Training Entity instance (Data layer)
            // Remark: this also merges the detail table (e.g. Training) and child tables (TrainingStap, TrainingPerceel, ....)
            // - TrainingStapDetails child collection (Business layer) into TrainingStappen collection (Data layer).
            // - AttachmentInfoDetails child collection (Business layer) into AttachmentItems collection (Data layer).
            // - etc...
            MergeHelper.MergeSingle(this.UpdateData.TRAININGDetail).Into(this.iTraining);  //Into Data.EF

            // Check if user is allowed to create/update Training instance and related child table instances
            // IMPORTANT: make sure the call to Secure() to check if Training is locked BEFORE creating pending edits
            // in GIS database (create pending edits is done in TrainingUpdateUseCase.PreValidationBusinessLogic method).
            this.Secure();

            // Add specific business logic that is needed BEFORE validation.
            // Derived class is responsible for creating pending edits in GIS.
            this.PreValidationBusinessLogic();

            // Validate Training instance and related child table instances.
            if (this.Validate())
            {
                // Add business logic to Training instance and related child row instances
                this.MergeBusinessLogic();

                // Save Training instance and related child row instances to database in a single transaction
                this.SaveData();
            }
            else
            {
                // Return ID 0 result to indicate validation errors
                this.ResultID = 0;
            }
        }
        #endregion

        #region Abstract Methods
        /// <summary>
        /// Derived class must implement this method in order to fetch formulier detail table
        /// </summary>
        protected abstract void FetchDetailTable();
        #endregion

        #region Protected Methods - General
        /// <summary>
        /// Secures the update/create action.
        /// </summary>
        protected virtual void Secure()
        {
            //// Check if user has Update permission on Training task AND if Training is locked by current user.

            //// Get our custom TaskPrincipal from the current thread
            //// Remark: don't use System.Security.Principal.WindowsIdentity.GetCurrent() since this returns the ASP.NET account!!!
            //var vPrincipal = Thread.CurrentPrincipal as ILMSSPrincipal;
            //if (vPrincipal == null)
            //{
            //    throw new AuthenticationException();
            //}

            //if (this.iTraining.TrainingID == 0) // Create new Training
            //{
            //    // User needs Create permission on TrainingAlle task (no Lock needed for creating new Training)
            //    // Remark: Create permission on TrainingProvincie task is not supported
            //    if (vPrincipal.HasPermissionOnTask(TaskCode.TrainingAlle, PermissionCode.Create))
            //    {
            //        // OK, user is allowed to create Trainings in the database
            //        return;
            //    }

            //    // If we get here then the current user is not allowed to create Trainings
            //    throw new WGK.Lib.Exceptions.CreateDeniedException(
            //        vPrincipal.UserName,
            //        TrainingDisplayNames.cTrainingEntityDisplayName);
            //}
            //else // Update existing Training
            //{
            //    // Check if Training is locked by current user
            //    // It is not allowed for a user to create/save GIS pending edits without first locking the Training !!!
            //    if (!this.iLockManager.IsLockedByCurrentUser(
            //        LockTypeCode.Training,
            //        this.UpdateData.TRAININGDetail.TrainingID.ToString(CultureInfo.InvariantCulture)))
            //    {
            //        throw new NotLockedByUserException(
            //            vPrincipal.UserName,
            //            TrainingDisplayNames.cTrainingEntityDisplayName);
            //    }

            //    if (vPrincipal.HasPermissionOnTask(TaskCode.TrainingAlle, PermissionCode.Update))
            //    {
            //        // OK, user is allowed to update all Trainingen in the database
            //        return;
            //    }

            //    if (vPrincipal.HasPermissionOnTask(TaskCode.TrainingProvincie, PermissionCode.Update))
            //    {
            //        string vTrainingProvincieID = this.iTraining.ProvincieID;
            //        if (!vTrainingProvincieID.IsNullOrEmptyOrBlankCode())
            //        {
            //            // Check if Provincie of the Training is one of the provincies of the user
            //            if (vPrincipal.ProvincieIDs.Any(p => p == vTrainingProvincieID))
            //            {
            //                // OK, user is allowed to update Trainings for his provincie(s)
            //                return;
            //            }
            //        }
            //    }

            //    // If we get here then the current user is not allowed to update this Training
            //    throw new WGK.Lib.Exceptions.UpdateDeniedException(
            //        vPrincipal.UserName,
            //        TrainingDisplayNames.cTrainingEntityDisplayName);
            //}
        }

        /// <summary>
        /// Fetches the maintenance data from database.
        /// </summary>
        /// <returns></returns>
        protected virtual void FetchData()
        {
            var vTrainingID = this.UpdateData.TRAININGDetail.ID;
            TRAINING vTraining = null;
            if (vTrainingID == 0)
            {
                // -- Get default data for creating a new Training
                // Create a new Training instance (data layer)
                vTraining = new TRAINING();

                // Add the new Training instance to repository context and mark for creation
                this.iTrainingRepository.AddTraining(vTraining);
            }
            else
            {
                // -- Fetch existing Training from database
                // REMARK: repository does not fetch the specific detail tables automatically
                vTraining = this.iTrainingRepository.GetTRAINING(
                    pID: (int)vTrainingID,
                    pIncludeSoftDeleted: false,
                    pIncludeAllData: true,
                    pNoTracking: false);
                if (vTraining == null)
                {
                    throw new NoResultFoundException(
                        TRAININGDisplayNames.cIDDisplayName,
                        vTrainingID);
                }

            }

            this.iTraining = vTraining;

        }

        /// <summary>
        /// Validates the Training main table and child tables that are common to all TrainingTypes
        /// </summary>
        protected virtual bool Validate()
        {
             // -- Validate Training main table
            ValidateHelper.Validate(this.iTraining, this.iValidationDictionary);
            return this.iValidationDictionary.IsValid;
        }

        /// <summary>
        /// Specific business logic that is needed BEFORE validation
        /// </summary>
        protected virtual void PreValidationBusinessLogic()
        {
            // Add business logic for Training that is needed BEFORE validation
            this.PreValidationBusinessLogicTraining();
        }

        /// <summary>
        /// Merge Business Logic for update/create
        /// </summary>
        protected virtual void MergeBusinessLogic()
        {
            // -- Merge business logic for main table
            this.MergeBusinessLogicForTraining();
            // -- Merge business logic for TrainingPersoon child table
            this.MergeBusinessLogicForTRAINING_QUESTIONNAIRES();
        }

        /// <summary>
        /// Save maintenance data to database for update/create
        /// </summary>
        /// <returns></returns>
        protected virtual void SaveData()
        {
            // Save all changes to the database in a single transaction
            // Remark: Only the topmost UseCase instance participating in the TransactionContext will actually call the
            // save method on the ObjectContext.
           // EntityState vEntityState = this.iTrainingRepository.GetObjectState(this.iTraining);  //ToDo: Ruddi deze regel verwijderen. Was om te testen
            //this.iTrainingRepository.Save();
            this.ResultID = this.iTraining.ID;
        }
        #endregion

        #region protected Methods - Training table
        /// <summary>
        /// Specific business logic for Training base table that is needed BEFORE validation
        /// </summary>
        protected virtual void PreValidationBusinessLogicTraining()
        {
           
        }


        /// <summary>
        /// Merges business logic for Training table
        /// </summary>
        protected virtual void MergeBusinessLogicForTraining()
        {
            //EntityState vEntityState = this.iTrainingRepository.GetObjectState(this.iTraining);
            bool vIsCreate = true; // (vEntityState == EntityState.Added);

            if (vIsCreate) // only when creating a new Training
            {
                //ToDo: onderstaande code verhuizen naar plaats achter de create van nieuwe training omdat we dan pas de trainingID hebben die we nodig hebben om 
                //QR code maken 

                //  var requestContext = HttpContext.Request.HttpContext.ToString();
                string MyValue = "https://localhost:44335";//+ new UrlHelper(requestContext).Action("Maintenance", "Questionnaires", new { pID = this.iTraining.ID });




                //ToDo: hier moet link naar evaluatie controller komen https://localhost:44335/Trainings/Evaluatie/pID=55
                //de pID is de ID van de training en ook iets voorzien dat ipv localhost:44335 er gekeken wordt naar de current URL (dus automatisch e aanpassng aan PROD en TST omgeving)

                #region Logo WGK Limburg
                //logo in BASE64 formaat (string)
                var logo = "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EJYgAgAAwhJEAABAWIIImJ3nfitL688UFwAA0yeIgNn52u+l1H+uuAAAmD5BBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAIDxaF6R/9lRXABUgyACZmf44DR8gALqYeGalDqPHSyuACpBEAGzM3xwGj5AAQDMiCACAADCEkTAbFk2B/WQf457zaXiAqA6BBEwW5bNQT3kn+NW537vDwGVI4gAgEtjdzmgwgQRMHO9xtUnHqiAarK7HFBhggiYudbSvZbNQVWZDgEVJ4iAUjje+HZTIqgi0yGg4gQRUArblu4yJYKqaV6RBs3XFBcA1SSIgNJ4of8mUyKokoVrUqPzkOkQUGmCCCiNy/e8++Dm4g+LIqiC/HO6MnhDcQFQXYIIKJW5pY8cHGy/XhRBmeWfz+Ptt6btu3/KdAioPEEElE7jyocOpva1ogjKKP9c9hbfnLZ17hJDQC0IIqCchrtWiSIol/zz2G+/IbWW7hNDQG0IIqC8RlF0nSiCMigmQ83OA2IIqBVBBJRb58DB4TfSoghmKP/8bS6+zWQIqKXGYDB4vPgZoLQ2ln8xmx/895TWn0mp/1zxr8BEDb+IWLgmvdD//nT5np8UQ0AtCSKgWro3ZWn1S6IIJqkIoV5zV2p1Pi6EgFoTREDlrB7+5azd+MKJCxMjGJ8ihFJzx4l3+AACEERAtXX3Zql/TBjBpTgVQjvzEHpUCAGhCCKgFnrLH8lag78srgoiCV7uZPxssdH4rjS/dIcQAkISREB9nZweAV9nORzAaQQRAAAQlnOIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAAhCWIAACAsAQRAAAQliACAADCEkQAAEBYgggAAAhLEAEAAGEJIgAAICxBBAAABJXS/wf6yWdcekN90AAAAABJRU5ErkJggg==";
                // Convert Base64 String to byte[]
                byte[] logoArray = Convert.FromBase64String(logo);
                MemoryStream ms2 = new MemoryStream(logoArray, 0,
                  logoArray.Length);

                // Convert byte[] to Image
                ms2.Write(logoArray, 0, logoArray.Length);
                Bitmap logoBitmap = (Bitmap)Image.FromStream(ms2, true);



                #endregion

                QRCodeGenerator qrGenerator = new QRCodeGenerator();
                QRCodeData qrCodeData = qrGenerator.CreateQrCode(MyValue, QRCodeGenerator.ECCLevel.H);
                QRCode qrCode = new QRCode(qrCodeData);
                Bitmap qrCodeImage = qrCode.GetGraphic(40, Color.Black, Color.White, logoBitmap, 45, 0);

                byte[] QRbyteImage;
                using (Bitmap BarcodeBmp = qrCodeImage)
                {
                    using (MemoryStream ms = new MemoryStream())
                    {
                        BarcodeBmp.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                        QRbyteImage = ms.ToArray();

                    }
                    if (QRbyteImage != null)
                    {
                        this.iTraining.QR = QRbyteImage;
                    }

                }

            }
            else // only when updating an existing Training
            {
               
            }
        }
        #endregion

        #region protected Methods - DossierPersoon child table

        /// <summary>
        /// Merges business logic for TRAINING_QUESTIONNAIRE child rows that is needed BEFORE validation
        /// </summary>
        protected virtual void PreValidationBusinessLogicForTRAINING_QUESTIONNAIRES()
        {
        }

        /// <summary>
        /// Merges business logic for DossierPersoon child rows
        /// </summary>
        protected virtual void MergeBusinessLogicForTRAINING_QUESTIONNAIRES()
        {
        }
        #endregion

    }
}
