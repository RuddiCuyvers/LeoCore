using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Common.Enums;
using WGK.LMS.Business.Dtos.Trainings;

namespace WGK.LMS.Business.Dtos.Trainings
{
    public class TrainingInfo
    {
        #region Field names constants
       



       


        private const string cTrainingIDFieldName = "TrainingID";
        public static string TrainingIDFieldName
        {
            get { return cTrainingIDFieldName; }
        }

        private const string cTRAINING_TYPEFieldName = "TRAINING_TYPE";
        public static string TRAINING_TYPEFieldName { get { return cTRAINING_TYPEFieldName; } }


        private const string cNOMENCLATUUR_YNFieldName = "NOMENCLATUUR_YN";
        public static string NOMENCLATUUR_YNFieldName { get { return cNOMENCLATUUR_YNFieldName; } }


        private const string cCONVENTIE_YNFieldName = "CONVENTIE_YN";
        public static string CONVENTIE_YNFieldName { get { return cCONVENTIE_YNFieldName; } }


        private const string cCATEGORY_IDFieldName = "CATEGORY_ID";
        public static string CATEGORY_IDFieldName { get { return cCATEGORY_IDFieldName; } }


        private const string cDURATIONFieldName = "DURATION";
        public static string DURATIONFieldName { get { return cDURATIONFieldName; } }


        private const string cLINKFieldName = "LINK";
        public static string LINKFieldName { get { return cLINKFieldName; } }


        #endregion

        #region Properties
        // Remark: no need to add DisplayName attributes since this class must be mapped to a ViewModel for displaying in a grid
        public decimal TrainingID { get; set; }


        public string TRAINING_TYPE { get; set; }

     
        public string NOMENCLATUUR_YN { get; set; }

        public string EVIDENCEBASED_YN { get; set; }

        public string INTERNEXTERN { get; set; }

        public string ONDERWERP { get; set; }
        public string CONVENTIE_YN { get; set; }

       
        public string CATEGORY_ID { get; set; }

       
        public System.DateTime DURATION { get; set; }

        
        public string LINK { get; set; }


        #endregion
    }
}
