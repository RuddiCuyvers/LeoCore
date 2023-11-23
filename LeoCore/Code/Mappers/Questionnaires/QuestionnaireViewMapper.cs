

//using System;
//using System.Collections.Generic;
//using System.Globalization;
//using System.Linq;
//using System.Web;
//using LEO.Business.Dtos.Questionnaires;
//using LEO.Business.Helpers;
//using LEO.Business.Models.Questionnaires;
//using LEO.Business.UseCases.Questionnaires;
//using LEO.Common.Codes;
//using LEO.Common.Constants.Questionnaire;
//using LeoCore.Models.Questionnaires;

//using WGK.Lib.Exceptions;
//using WGK.Lib.Extensions;
//using WGK.Lib.Ioc;
//using WGK.Lib.Mappers;
//using WGK.Lib.UserCodes;

//using System.IO;

//namespace LeoCore.Code.Mappers.Questionnaires
//{
//    public class QuestionnaireViewMapper  :
//        IMapper<PersonQuestionnaireMaintenanceModel, QuestionnaireMaintenanceViewModel>,
//        IMapper<QuestionnaireMaintenanceViewModel, PersonQuestionnaireUpdateModel>
//    {

//        private IUserCodeManager iUserCodeManager;
//        public QuestionnaireViewMapper()
//        {
//            this.iUserCodeManager = IocManager.Resolve<IUserCodeManager>();
//        }

//        /// <summary>
//        /// Maps a QuestionnaireMaintenanceModel (business layer) to a new QuestionnaireMaintenanceViewModel (presentation layer) instance
//        /// </summary>
//        public QuestionnaireMaintenanceViewModel Map(PersonQuestionnaireMaintenanceModel pSource)
//        {
//            // Get a new instance of QuestionnaireMaintenanceViewModel with UserCodeManager resolved by the Ioc container
//            var vResult = IocManager.Resolve<QuestionnaireMaintenanceViewModel>();
//            vResult.QuestionnaireDetail = pSource.QuestionnaireDetail;
//            vResult.Person_QuestionnaireDetail = pSource.PersonQuestionnaireDetail;
//            // -- Base class fields
//            vResult.EntityID = pSource.QuestionnaireDetail.ID.ToString();

//            return vResult;
//        }


//        /// <summary>
//        /// Maps QuestionnaireMaintenanceViewModel (presentation layer) to a new QuestionnaireUpdateModel (business layer) instance
//        /// </summary>
//        public PersonQuestionnaireUpdateModel Map(QuestionnaireMaintenanceViewModel pSource)
//        {
//            var vResult = new PersonQuestionnaireUpdateModel
//            {
//                QuestionnaireDetail = pSource.QuestionnaireDetail,
//                PersonQuestionnaireDetail = pSource.Person_QuestionnaireDetail,
//            };
//            return vResult;
//        }


//    }
//}