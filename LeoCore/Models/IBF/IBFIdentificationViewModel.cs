using Microsoft.AspNetCore.Mvc.Rendering;
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
            JaartallenList = new List<SelectListItem>() {
            new SelectListItem { Text = "2023", Value = "2023" },
            new SelectListItem { Text = "2024", Value = "2024" },
            new SelectListItem { Text = "2025", Value = "2025" },
            new SelectListItem { Text = "2026", Value = "2026" },
            new SelectListItem { Text = "2027", Value = "2027" },
        };
        }
        #endregion

        #region Public Properties
       
        public ICollection<IBFIdentification_DTO>  MijnIBFTrainingen { get; set; }

        public int Jaartal { get; set; }

        public List<SelectListItem> JaartallenList { get; set; }

        #endregion
    }
}