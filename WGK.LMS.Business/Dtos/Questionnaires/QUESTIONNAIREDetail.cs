using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Common.Constants.Questionnaire;

namespace WGK.LMS.Business.Dtos.Questionnaires
{

    public class QUESTIONNAIREDetail
    {
        #region Field names constants
        private const string cClassName = "QUESTIONNAIREDetail";
        public static string ClassName { get { return cClassName; } }   
    
        private const string cIDFieldName = "ID";
        public static string IDFieldName { get { return cIDFieldName; } }
        private const string cDESCRIPTIONFieldName = "DESCRIPTION";
        public static string DESCRIPTIONFieldName { get { return cDESCRIPTIONFieldName; } }
        private const string cINFOFieldName = "INFO";
        public static string INFOFieldName { get { return cINFOFieldName; } }
        private const string cDATE_VALID_STARTFieldName = "DATE_VALID_START";
        public static string DATE_VALID_STARTFieldName { get { return cDATE_VALID_STARTFieldName; } }
        private const string cDATE_VALID_ENDFieldName = "DATE_VALID_END";
        public static string DATE_VALID_ENDFieldName { get { return cDATE_VALID_ENDFieldName; } }
        #endregion

        #region Constructor
        public QUESTIONNAIREDetail()
        {
            // Make sure collection and navigation properties are never null
            this.PERSON_QUESTIONNAIREDetails = new List<PERSON_QUESTIONNAIREDetail>();
            this.QUESTIONNAIRE_QUESTIONDetails = new List<QUESTIONNAIRE_QUESTIONDetail>();
            this.TRAINING_QUESTIONNNAIREDetails = new List<TRAINING_QUESTIONNNAIREDetail>();
        }
        #endregion

        #region Primitive properties (form data layer entity)
        [DisplayName(QUESTIONNAIREDisplayNames.cIDDisplayName)]
        public decimal ID { get; set; }
    
        [DisplayName(QUESTIONNAIREDisplayNames.cDESCRIPTIONDisplayName)]
        public string DESCRIPTION { get; set; }
    
        [DisplayName(QUESTIONNAIREDisplayNames.cINFODisplayName)]
        public string INFO { get; set; }
    
        [DisplayName(QUESTIONNAIREDisplayNames.cDATE_VALID_STARTDisplayName)]
        public System.DateTime DATE_VALID_START { get; set; }
    
        [DisplayName(QUESTIONNAIREDisplayNames.cDATE_VALID_ENDDisplayName)]
        public Nullable<System.DateTime> DATE_VALID_END { get; set; }
    

        #endregion

        #region Calculated and other not stored properties
        // Add your calculated and other not stored fields here
        // ...

        #endregion

        #region Collection and foreign entities properties
        public List<PERSON_QUESTIONNAIREDetail> PERSON_QUESTIONNAIREDetails { get; set; }
        public List<QUESTIONNAIRE_QUESTIONDetail> QUESTIONNAIRE_QUESTIONDetails { get; set; }
        public List<TRAINING_QUESTIONNNAIREDetail> TRAINING_QUESTIONNNAIREDetails { get; set; }
    

        #endregion

    }


}
