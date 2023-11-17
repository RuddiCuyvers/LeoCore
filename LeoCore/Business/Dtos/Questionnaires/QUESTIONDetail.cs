using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Common.Constants.Questionnaire;

namespace LEO.Business.Dtos.Questionnaires
{
    public class QUESTIONDetail
    {
        #region Field names constants
        private const string cClassName = "QUESTIONDetail";
        public static string ClassName { get { return cClassName; } }

        private const string cIDFieldName = "ID";
        public static string IDFieldName { get { return cIDFieldName; } }
        private const string cTEXTFieldName = "TEXT";
        public static string TEXTFieldName { get { return cTEXTFieldName; } }
        private const string cTYPE_ANSWERFieldName = "TYPE_ANSWER";
        public static string TYPE_ANSWERFieldName { get { return cTYPE_ANSWERFieldName; } }
        private const string cVALUE_MINFieldName = "VALUE_MIN";
        public static string VALUE_MINFieldName { get { return cVALUE_MINFieldName; } }
        private const string cVALUE_MAXFieldName = "VALUE_MAX";
        public static string VALUE_MAXFieldName { get { return cVALUE_MAXFieldName; } }
        private const string cINFOFieldName = "INFO";
        public static string INFOFieldName { get { return cINFOFieldName; } }
        private const string cDATE_VALID_STARTFieldName = "DATE_VALID_START";
        public static string DATE_VALID_STARTFieldName { get { return cDATE_VALID_STARTFieldName; } }
        private const string cDATE_VALID_ENDFieldName = "DATE_VALID_END";
        public static string DATE_VALID_ENDFieldName { get { return cDATE_VALID_ENDFieldName; } }
        #endregion

        #region Constructor
        public QUESTIONDetail()
        {
            // Make sure collection and navigation properties are never null
            this.PERSON_QUESTIONNAIRE_ANSWERDetails = new List<PERSON_QUESTIONNAIRE_ANSWERDetail>();
            this.QUESTIONNAIRE_QUESTIONDetails = new List<QUESTIONNAIRE_QUESTIONDetail>();
        }
        #endregion

        #region Primitive properties (form data layer entity)
        [DisplayName(QUESTIONDisplayNames.cIDDisplayName)]
        public int ID { get; set; }

        [DisplayName(QUESTIONDisplayNames.cTEXTDisplayName)]
        public string TEXT { get; set; }

        [DisplayName(QUESTIONDisplayNames.cTYPE_ANSWERDisplayName)]
        public string TYPE_ANSWER { get; set; }

        [DisplayName(QUESTIONDisplayNames.cVALUE_MINDisplayName)]
        public Nullable<decimal> VALUE_MIN { get; set; }

        [DisplayName(QUESTIONDisplayNames.cVALUE_MAXDisplayName)]
        public Nullable<decimal> VALUE_MAX { get; set; }

        [DisplayName(QUESTIONDisplayNames.cINFODisplayName)]
        public string INFO { get; set; }

        [DisplayName(QUESTIONDisplayNames.cDATE_VALID_STARTDisplayName)]
        public System.DateTime DATE_VALID_START { get; set; }

        [DisplayName(QUESTIONDisplayNames.cDATE_VALID_ENDDisplayName)]
        public Nullable<System.DateTime> DATE_VALID_END { get; set; }


        #endregion

        #region Calculated and other not stored properties
        // Add your calculated and other not stored fields here
        // ...

        #endregion

        #region Collection and foreign entities properties
        public List<PERSON_QUESTIONNAIRE_ANSWERDetail> PERSON_QUESTIONNAIRE_ANSWERDetails { get; set; }
        public List<QUESTIONNAIRE_QUESTIONDetail> QUESTIONNAIRE_QUESTIONDetails { get; set; }


        #endregion

    }
}
