using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace LeoCore.Models.IBF
{
    public class IBFIdentificationViewModel 
    {
        #region Field names constants
        private const string cTrainingIdentificationViewModelClassName = "IBFIdentificationViewModel";
        public static string TrainingIdentificationViewModelClassName
        {
            get { return cTrainingIdentificationViewModelClassName; }
        }

        private const string cSearchCriteriaFieldName = "SearchCriteria";
        public static string SearchCriteriaFieldName
        {
            get { return cSearchCriteriaFieldName; }
        }
        #endregion

        #region Constructor
        public IBFIdentificationViewModel()
        {
            
        }
        #endregion

        #region Public Properties
       
        public ICollection<IBFInfoViewModel>  MijnIBFTrainingen { get; set; }

        #endregion
    }
}