using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LEO.Business.Dtos.Trainings;

namespace LEO.Business.Dtos.IBF
{
    public class IBFInfo
    {
        #region Field names constants
        private const string cTrainingIDFieldName = "TrainingID";
        public static string TrainingIDFieldName
        {
            get { return cTrainingIDFieldName; }
        }

        private const string cPersonTrainingIDFieldName = "Person_TrainingID";
        public static string PersonTrainingIDFieldName
        {
            get { return cPersonTrainingIDFieldName; }
        }

        private const string cTRAINING_TYPEFieldName = "TRAINING_TYPE";
        public static string TRAINING_TYPEFieldName { get { return cTRAINING_TYPEFieldName; } }

        private const string cLINKFieldName = "LINK";
        public static string LINKFieldName { get { return cLINKFieldName; } }


        #endregion

        #region Properties

        public decimal Person_TrainingID { get; set; }
        // Remark: no need to add DisplayName attributes since this class must be mapped to a ViewModel for displaying in a grid
        public decimal TrainingID { get; set; }

        public string SUBJECT { get; set; }

        public string TRAINING_TYPE { get; set; }

        public string NOMENCL_CONV_YN { get; set; }
       
        public string METHODOLOGY { get; set; }

        public DateTime? DATUMTRAINING { get; set; }

        public string LINK { get; set; }


        #endregion
    }
}
