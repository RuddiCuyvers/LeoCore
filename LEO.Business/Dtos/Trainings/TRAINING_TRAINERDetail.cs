using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Common.Constants.Trainings;

namespace LEO.Business.Dtos.Trainings
{
    // If needed, you can copy this class to your business project.

    public class TRAINING_TRAINERDetail
    {
        #region Field names constants
        private const string cClassName = "TRAINING_TRAINERDetail";
        public static string ClassName { get { return cClassName; } }   
    
        private const string cIDFieldName = "ID";
        public static string IDFieldName { get { return cIDFieldName; } }
        private const string cTRAINING_IDFieldName = "TRAINING_ID";
        public static string TRAINING_IDFieldName { get { return cTRAINING_IDFieldName; } }
        private const string cPERSON_IDFieldName = "PERSON_ID";
        public static string PERSON_IDFieldName { get { return cPERSON_IDFieldName; } }
        private const string cMANDATORYFieldName = "MANDATORY";
        public static string MANDATORYFieldName { get { return cMANDATORYFieldName; } }
        private const string cDATE_VALID_STARTFieldName = "DATE_VALID_START";
        public static string DATE_VALID_STARTFieldName { get { return cDATE_VALID_STARTFieldName; } }
        private const string cDATE_VALID_ENDFieldName = "DATE_VALID_END";
        public static string DATE_VALID_ENDFieldName { get { return cDATE_VALID_ENDFieldName; } }
        #endregion

        #region Constructor
        public TRAINING_TRAINERDetail()
        {
            // Make sure collection and navigation properties are never null
           // this.PERSONDetails = new PERSONDetail();
            this.TRAININGDetails = new TRAININGDetail();
        }
        #endregion

        #region Primitive properties (form data layer entity)
        [DisplayName(TRAINING_TRAINERDisplayNames.cIDDisplayName)]
        public decimal ID { get; set; }
    
        [DisplayName(TRAINING_TRAINERDisplayNames.cTRAINING_IDDisplayName)]
        public decimal TRAINING_ID { get; set; }
    
        //[DisplayName(TRAINING_TRAINERDisplayNames.c.cPERSON_IDDisplayName)]
        //public decimal PERSON_ID { get; set; }
    
        [DisplayName(TRAINING_TRAINERDisplayNames.cMANDATORYDisplayName)]
        public string MANDATORY { get; set; }
    
        [DisplayName(TRAINING_TRAINERDisplayNames.cDATE_VALID_STARTDisplayName)]
        public System.DateTime DATE_VALID_START { get; set; }
    
        [DisplayName(TRAINING_TRAINERDisplayNames.cDATE_VALID_ENDDisplayName)]
        public Nullable<System.DateTime> DATE_VALID_END { get; set; }
    

        #endregion

        #region Calculated and other not stored properties
        // Add your calculated and other not stored fields here
        // ...

        #endregion

        #region Collection and foreign entities properties
    
        //public PERSONDetail PERSONDetails { get; set; }
        public TRAININGDetail TRAININGDetails { get; set; }

        #endregion

    }
    
}
