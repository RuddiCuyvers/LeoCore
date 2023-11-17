using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Dtos.Trainings;
using LEO.Common.Constants.Trainings;

namespace LEO.Business.Dtos.Questionnaires
{

    public class TRAINING_QUESTIONNNAIREDetail
    {
        #region Field names constants
        private const string cClassName = "TRAINING_QUESTIONNNAIREDetail";
        public static string ClassName { get { return cClassName; } }   
    
        private const string cIDFieldName = "ID";
        public static string IDFieldName { get { return cIDFieldName; } }
        private const string cTRAINING_IDFieldName = "TRAINING_ID";
        public static string TRAINING_IDFieldName { get { return cTRAINING_IDFieldName; } }
        private const string cQUESTIONNAIRE_IDFieldName = "QUESTIONNAIRE_ID";
        public static string QUESTIONNAIRE_IDFieldName { get { return cQUESTIONNAIRE_IDFieldName; } }
        private const string cINFOFieldName = "INFO";
        public static string INFOFieldName { get { return cINFOFieldName; } }
        private const string cDATE_VALID_STARTFieldName = "DATE_VALID_START";
        public static string DATE_VALID_STARTFieldName { get { return cDATE_VALID_STARTFieldName; } }
        private const string cDATE_VALID_ENDFieldName = "DATE_VALID_END";
        public static string DATE_VALID_ENDFieldName { get { return cDATE_VALID_ENDFieldName; } }
        private const string cTIME_LIFESPANFieldName = "TIME_LIFESPAN";
        public static string TIME_LIFESPANFieldName { get { return cTIME_LIFESPANFieldName; } }
        #endregion

        #region Constructor
        public TRAINING_QUESTIONNNAIREDetail()
        {
            // Make sure collection and navigation properties are never null
            this.QUESTIONNAIREDetails = new QUESTIONNAIREDetail();
            this.TRAININGDetails = new Trainings.TRAININGDetail();
        }
        #endregion

        #region Primitive properties (form data layer entity)
        [DisplayName(TRAINING_QUESTIONNNAIREDisplayNames.cIDDisplayName)]
        public int ID { get; set; }
    
        [DisplayName(TRAINING_QUESTIONNNAIREDisplayNames.cTRAINING_IDDisplayName)]
        public int TRAINING_ID { get; set; }
    
        [DisplayName(TRAINING_QUESTIONNNAIREDisplayNames.cQUESTIONNAIRE_IDDisplayName)]
        public int QUESTIONNAIRE_ID { get; set; }
    
        [DisplayName(TRAINING_QUESTIONNNAIREDisplayNames.cINFODisplayName)]
        public string INFO { get; set; }
    
        [DisplayName(TRAINING_QUESTIONNNAIREDisplayNames.cDATE_VALID_STARTDisplayName)]
        public System.DateTime DATE_VALID_START { get; set; }
    
        [DisplayName(TRAINING_QUESTIONNNAIREDisplayNames.cDATE_VALID_ENDDisplayName)]
        public Nullable<System.DateTime> DATE_VALID_END { get; set; }
    
        [DisplayName(TRAINING_QUESTIONNNAIREDisplayNames.cTIME_LIFESPANDisplayName)]
        public Nullable<decimal> TIME_LIFESPAN { get; set; }
    

        #endregion

        #region Calculated and other not stored properties
        // Add your calculated and other not stored fields here
        // ...

        #endregion

        #region Collection and foreign entities properties
    
        public QUESTIONNAIREDetail QUESTIONNAIREDetails { get; set; }
        public TRAININGDetail TRAININGDetails { get; set; }

        #endregion

    }

}
