

//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Web;
//using LEO.Business.Dtos.Trainings;
//using LEO.Business.Helpers;
//using LEO.Business.Models.Trainings;
//using LEO.Business.UseCases.Trainings;
//using LEO.Common.Codes;
//using LEO.Common.Constants.Trainings;
//using LEO.Web.Models.Trainings;

//using WGK.Lib.Exceptions;
//using WGK.Lib.Extensions;
//using WGK.Lib.Ioc;
//using WGK.Lib.Mappers;
//using WGK.Lib.UserCodes;

//using System.IO;
//using LEO.Data.Interfaces;

//namespace LEO.Web.Code.Mappers.Trainings
//{
//    public class TrainingViewMapper  :
//        IMapper<TrainingMaintenanceModel, TrainingMaintenanceViewModel>,
//        IMapper<TrainingIdentificationModel, TrainingIdentificationViewModel>,
//        IMapper<TrainingInfo, TrainingInfoViewModel>,
//        IMapper<TrainingMaintenanceViewModel, TrainingUpdateModel>
//    {

//        private IUserCodeManager iUserCodeManager;
//        public TrainingViewMapper()
//        {
//            this.iUserCodeManager = IocManager.Resolve<IUserCodeManager>();
//        }

//        /// <summary>
//        /// Maps a TrainingMaintenanceModel (business layer) to a new TrainingMaintenanceViewModel (presentation layer) instance
//        /// </summary>
//        public TrainingMaintenanceViewModel Map(TrainingMaintenanceModel pSource)
//        {
//            // Get a new instance of TrainingMaintenanceViewModel with UserCodeManager resolved by the Ioc container
//            var vResult = IocManager.Resolve<TrainingMaintenanceViewModel>();

//            // -- Map fields
//            // Remark: shallow mapping of TRAININGDetail
//            // - TrainingMaintenanceViewModel.TRAININGDetail refers to the same TRAININGDetail instance retrieved
//            //   from the business layer.
//            // - TrainingMaintenanceViewModel.Indiener refers to the same DossierPersoonDetail instance retrieved
//            //   from the business layer.
//            vResult.TRAININGDetail = pSource.TRAININGDetail;
          
//            // -- Base class fields
//            vResult.EntityID = pSource.TRAININGDetail.ID.ToString();
//            if (pSource.TRAININGDetail.QR != null)
//            {
//                string lBitmapQR = "";
//                if (pSource.TRAININGDetail?.QR != null)
//                {
//                    // lBitmapQR = System.Text.Encoding.UTF8.GetString(pSource.TRAININGDetail.QR);
//                    lBitmapQR = "data:image/png;base64," + Convert.ToBase64String(pSource.TRAININGDetail.QR);
//                }
//                if (string.IsNullOrEmpty(lBitmapQR) == false)
//                {
//                    vResult.Qrcode = lBitmapQR;
//                }
//            }

//            return vResult;
//        }

//        /// <summary>
//        /// Maps a DossierIdentificationModel (business layer) to a new DossierIdentificationViewModel (presentation layer) instance
//        /// </summary>
//        public TrainingIdentificationViewModel Map(TrainingIdentificationModel pSource)
//        {
//            // Get a new instance of DossierIdentificationViewModel with UserCodeManager resolved by the Ioc container
//            var vResult = IocManager.Resolve<TrainingIdentificationViewModel>();

//            // Map fields
//            vResult.SearchCriteria = pSource.SearchCriteria;
//            vResult.UserCodeManager = iUserCodeManager;

//            // Map pseudo UserCode lists
//            // ...

//            return vResult;
//        }

//        /// <summary>
//        /// Maps a DossierInfo (business layer) instance to a new DossierInfoViewModel (presentation layer) instance
//        /// </summary>
//        public TrainingInfoViewModel Map(Business.Dtos.Trainings.TrainingInfo pSource)
//        {
          
//            //var vResult = IocManager.Resolve<TrainingInfoViewModel>();

//            if (pSource == null)
//            {
//                return null;
//            }

//            var vResult = new TrainingInfoViewModel
//            {
//                TrainingID = pSource.TrainingID,
//                TRAINING_TYPE = this.iUserCodeManager.GetUserCodeDescription(pSource.TRAINING_TYPE, UserCodeGroupCode.cTypeTrainingLijst),
//                ONDERWERP = pSource.ONDERWERP,
//                NOMENCLATUUR_YN = pSource.NOMENCLATUUR_YN,
//                EVIDENCEBASED_YN = pSource.EVIDENCEBASED_YN,
//                INTERNEXTERN = this.iUserCodeManager.GetUserCodeDescription(pSource.INTERNEXTERN, UserCodeGroupCode.cEXT_INTLijst),
//                LINK = pSource.LINK
//            };

//            return vResult;
//        }

//        /// <summary>
//        /// Maps TrainingMaintenanceViewModel (presentation layer) to a new TrainingUpdateModel (business layer) instance
//        /// </summary>
//        public TrainingUpdateModel Map(TrainingMaintenanceViewModel pSource)
//        {
//            var vResult = new TrainingUpdateModel
//            {
//                // Remark: shallow mapping of TRAININGDetail and child tables
//                // - TrainingUpdateModel.TRAININGDetail refers to the same TRAININGDetail instance that was merged (binding) in the presentation layer.
//                // - TrainingUpdateModel.Training.DossierStapDetails refers to the same DossierStapDetail collection that was merged (binding) in the presentation layer.
//                TRAININGDetail = pSource.TRAININGDetail,

//                // Workflow
//                //WorkflowEventID = pSource.WorkflowEventID
//            };

//            return vResult;
//        }



//    }
//}