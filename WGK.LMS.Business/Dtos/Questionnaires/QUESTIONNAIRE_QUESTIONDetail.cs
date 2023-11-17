using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Common.Constants.Questionnaire;

namespace WGK.LMS.Business.Dtos.Questionnaires
{

    public class QUESTIONNAIRE_QUESTIONDetail
    {
        #region Field names constants
        private const string cClassName = "QUESTIONNAIRE_QUESTIONDetail";
        public static string ClassName { get { return cClassName; } }   
    
        private const string cIDFieldName = "ID";
        public static string IDFieldName { get { return cIDFieldName; } }
        private const string cQUESTIONNAIRE_IDFieldName = "QUESTIONNAIRE_ID";
        public static string QUESTIONNAIRE_IDFieldName { get { return cQUESTIONNAIRE_IDFieldName; } }
        private const string cQUESTION_IDFieldName = "QUESTION_ID";
        public static string QUESTION_IDFieldName { get { return cQUESTION_IDFieldName; } }
        private const string cMANDATORYFieldName = "MANDATORY";
        public static string MANDATORYFieldName { get { return cMANDATORYFieldName; } }
        private const string cORDERFieldName = "ORDER";
        public static string ORDERFieldName { get { return cORDERFieldName; } }
        private const string cDATE_VALID_STARTFieldName = "DATE_VALID_START";
        public static string DATE_VALID_STARTFieldName { get { return cDATE_VALID_STARTFieldName; } }
        private const string cDATE_VALID_ENDFieldName = "DATE_VALID_END";
        public static string DATE_VALID_ENDFieldName { get { return cDATE_VALID_ENDFieldName; } }
        #endregion

        #region Constructor
        public QUESTIONNAIRE_QUESTIONDetail()
        {
            // Make sure collection and navigation properties are never null
            this.QUESTIONDetails = new QUESTIONDetail();
            this.QUESTIONNAIREDetails = new QUESTIONNAIREDetail();
        }
        #endregion

        #region Primitive properties (form data layer entity)
        [DisplayName(QUESTIONNAIRE_QUESTIONDisplayNames.cIDDisplayName)]
        public int ID { get; set; }
    
        [DisplayName(QUESTIONNAIRE_QUESTIONDisplayNames.cQUESTIONNAIRE_IDDisplayName)]
        public int QUESTIONNAIRE_ID { get; set; }
    
        [DisplayName(QUESTIONNAIRE_QUESTIONDisplayNames.cQUESTION_IDDisplayName)]
        public int QUESTION_ID { get; set; }
    
        [DisplayName(QUESTIONNAIRE_QUESTIONDisplayNames.cMANDATORYDisplayName)]
        public string MANDATORY { get; set; }
    
        [DisplayName(QUESTIONNAIRE_QUESTIONDisplayNames.cORDERDisplayName)]
        public decimal ORDER { get; set; }
    
        [DisplayName(QUESTIONNAIRE_QUESTIONDisplayNames.cDATE_VALID_STARTDisplayName)]
        public System.DateTime DATE_VALID_START { get; set; }
    
        [DisplayName(QUESTIONNAIRE_QUESTIONDisplayNames.cDATE_VALID_ENDDisplayName)]
        public Nullable<System.DateTime> DATE_VALID_END { get; set; }
    

        #endregion

        #region Calculated and other not stored properties
        // Add your calculated and other not stored fields here
        // ...

        #endregion

        #region Collection and foreign entities properties
    
        public QUESTIONDetail QUESTIONDetails { get; set; }
        public QUESTIONNAIREDetail QUESTIONNAIREDetails { get; set; }

        #endregion

    }

}
