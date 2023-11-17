using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Persons;
using WGK.LMS.Business.Dtos.Trainings;
using WGK.LMS.Common.Constants.Questionnaire;

namespace WGK.LMS.Business.Dtos.Questionnaires
{
   

    public class PERSON_QUESTIONNAIREDetail
    {
        #region Field names constants
        private const string cClassName = "PERSON_QUESTIONNAIREDetail";
        public static string ClassName { get { return cClassName; } }

        private const string cIDFieldName = "ID";
        public static string IDFieldName { get { return cIDFieldName; } }
        private const string cCLIENT_IDFieldName = "CLIENT_ID";
        public static string CLIENT_IDFieldName { get { return cCLIENT_IDFieldName; } }
        private const string cQUESTIONNAIRE_IDFieldName = "QUESTIONNAIRE_ID";
        public static string QUESTIONNAIRE_IDFieldName { get { return cQUESTIONNAIRE_IDFieldName; } }
        private const string cTRAINING_IDFieldName = "TRAINING_ID";
        public static string TRAINING_IDFieldName { get { return cTRAINING_IDFieldName; } }
        private const string cDATE_SUBMITTEDFieldName = "DATE_SUBMITTED";
        public static string DATE_SUBMITTEDFieldName { get { return cDATE_SUBMITTEDFieldName; } }
        #endregion

        #region Constructor
        public PERSON_QUESTIONNAIREDetail()
        {
            // Make sure collection and navigation properties are never null
            this.PERSON_QUESTIONNAIRE_ANSWERDetails = new List<PERSON_QUESTIONNAIRE_ANSWERDetail>();
            this.QUESTIONNAIREDetails = new QUESTIONNAIREDetail();
            this.TRAININGDetails = new TRAININGDetail();
        }
        #endregion

        #region Primitive properties (form data layer entity)
        [DisplayName(PERSON_QUESTIONNAIREDisplayNames.cIDDisplayName)]
        public int ID { get; set; }

        [DisplayName(PERSON_QUESTIONNAIREDisplayNames.cCLIENT_IDDisplayName)]
        public string CLIENT_ID { get; set; }

        [DisplayName(PERSON_QUESTIONNAIREDisplayNames.cQUESTIONNAIRE_IDDisplayName)]
        public int QUESTIONNAIRE_ID { get; set; }

        [DisplayName(PERSON_QUESTIONNAIREDisplayNames.cTRAINING_IDDisplayName)]
        public int TRAINING_ID { get; set; }

        [DisplayName(PERSON_QUESTIONNAIREDisplayNames.cDATE_SUBMITTEDDisplayName)]
        public Nullable<System.DateTime> DATE_SUBMITTED { get; set; }


        #endregion

        #region Calculated and other not stored properties
        // Add your calculated and other not stored fields here
        // ...

        #endregion

        #region Collection and foreign entities properties
        public List<PERSON_QUESTIONNAIRE_ANSWERDetail> PERSON_QUESTIONNAIRE_ANSWERDetails { get; set; }

        public QUESTIONNAIREDetail QUESTIONNAIREDetails { get; set; }
        public TRAININGDetail TRAININGDetails { get; set; }

        #endregion

    }

}
