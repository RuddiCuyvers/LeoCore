using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using LEO.Common.Constants.Questionnaire;

namespace LEO.Business.Dtos.Questionnaires
{
    public class PERSON_QUESTIONNAIRE_ANSWERDetail 
    {
        #region Field names constants
        private const string cClassName = "PERSON_QUESTIONNAIRE_ANSWERDetail";
        public static string ClassName { get { return cClassName; } }

        private const string cIDFieldName = "ID";
        public static string IDFieldName { get { return cIDFieldName; } }
        private const string cPERSON_QUESTIONNAIRE_IDFieldName = "PERSON_QUESTIONNAIRE_ID";
        public static string PERSON_QUESTIONNAIRE_IDFieldName { get { return cPERSON_QUESTIONNAIRE_IDFieldName; } }
        private const string cQUESTION_IDFieldName = "QUESTION_ID";
        public static string QUESTION_IDFieldName { get { return cQUESTION_IDFieldName; } }
        private const string cANSWER_TEXTFieldName = "ANSWER_TEXT";
        public static string ANSWER_TEXTFieldName { get { return cANSWER_TEXTFieldName; } }
        private const string cANSWER_NUMBERFieldName = "ANSWER_NUMBER";
        public static string ANSWER_NUMBERFieldName { get { return cANSWER_NUMBERFieldName; } }
        private const string cANSWER_DATEFieldName = "ANSWER_DATE";
        public static string ANSWER_DATEFieldName { get { return cANSWER_DATEFieldName; } }

        private const string cQTEXT_AS_WASFieldName = "QTEXT_AS_WAS";
        public static string QTEXT_AS_WASFieldName { get { return cQTEXT_AS_WASFieldName; } }
        private const string cQQORDER_AS_WASFieldName = "QQORDER_AS_WAS";
        public static string QQORDER_AS_WASFieldName { get { return cQQORDER_AS_WASFieldName; } }

        private const string cQTYPEANSWER_AS_WASFieldName = "QTYPEANSWER_AS_WAS";
        public static string QTYPEANSWER_AS_WASFieldName { get { return cQTYPEANSWER_AS_WASFieldName; } }

        #endregion

        #region Constructor
        public PERSON_QUESTIONNAIRE_ANSWERDetail()
        {
            // Make sure collection and navigation properties are never null
            this.PERSON_QUESTIONNAIREDetails = new PERSON_QUESTIONNAIREDetail();
            this.QUESTIONDetails = new QUESTIONDetail();
        }
        #endregion

        #region Primitive properties (form data layer entity)
        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cIDDisplayName)]
        public int ID { get; set; }

        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cPERSON_QUESTIONNAIRE_IDDisplayName)]
        public int PERSON_QUESTIONNAIRE_ID { get; set; }

        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cQUESTION_IDDisplayName)]
        public int QUESTION_ID { get; set; }

        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cANSWER_TEXTDisplayName)]
        public string ANSWER_TEXT { get; set; }

        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cANSWER_NUMBERDisplayName)]
        public Nullable<int> ANSWER_NUMBER { get; set; }

        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cANSWER_DATEDisplayName)]
        public Nullable<System.DateTime> ANSWER_DATE { get; set; }

        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cQTEXT_AS_WASDisplayName)]
        public string QTEXT_AS_WAS { get; set; }

        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cQQORDER_AS_WASDisplayName)]
        public int QQORDER_AS_WAS { get; set; }

        [DisplayName(PERSON_QUESTIONNAIRE_ANSWERDisplayNames.cQTYPEANSWER_AS_WASDisplayName)]
        public string QTYPEANSWER_AS_WAS { get; set; }




        #endregion

        #region Calculated and other not stored properties
        // Add your calculated and other not stored fields here
        // ...

        #endregion

        #region Collection and foreign entities properties

        public PERSON_QUESTIONNAIREDetail PERSON_QUESTIONNAIREDetails { get; set; }
        public QUESTIONDetail QUESTIONDetails { get; set; }

        #endregion

    }

}
