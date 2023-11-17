using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Dtos.Trainings;
using LEO.Common.Constants.Persons;

namespace LEO.Business.Dtos.Persons
{

 public class PERSON_TRAININGDetail
    {
        
      #region Field names constants
       private const string cClassName = "PERSON_TRAININGDetail";
       public static string ClassName { get { return cClassName; } }   

       private const string cIDFieldName = "ID";
       public static string IDFieldName { get { return cIDFieldName; } }
       private const string cTRAINING_IDFieldName = "TRAINING_ID";
       public static string TRAINING_IDFieldName { get { return cTRAINING_IDFieldName; } }
       private const string cCLIENT_IDFieldName = "CLIENT_ID";
       public static string CLIENT_IDFieldName { get { return cCLIENT_IDFieldName; } }
       private const string cDATUM_STARTFieldName = "DATUM_START";
       public static string DATUM_STARTFieldName { get { return cDATUM_STARTFieldName; } }
       private const string cCOMPLETEDFieldName = "COMPLETED";
       public static string COMPLETEDFieldName { get { return cCOMPLETEDFieldName; } }
       private const string cRESULTSCOREFieldName = "RESULTSCORE";
       public static string RESULTSCOREFieldName { get { return cRESULTSCOREFieldName; } }
       private const string cPROGRESSFieldName = "PROGRESS";
       public static string PROGRESSFieldName { get { return cPROGRESSFieldName; } }
       #endregion

       #region Constructor
       public PERSON_TRAININGDetail()
       {
           // Make sure collection and navigation properties are never null
           this.TRAININGDetails = new TRAININGDetail();
       }
       #endregion

       #region Primitive properties (form data layer entity)
       [DisplayName(PERSON_TRAININGDisplayNames.cIDDisplayName)]
       public decimal ID { get; set; }

       [DisplayName(PERSON_TRAININGDisplayNames.cTRAINING_IDDisplayName)]
       public decimal TRAINING_ID { get; set; }

       [DisplayName(PERSON_TRAININGDisplayNames.cCLIENT_IDDisplayName)]
       public decimal CLIENT_ID { get; set; }

       [DisplayName(PERSON_TRAININGDisplayNames.cDATUM_STARTDisplayName)]
       public Nullable<System.DateTime> DATUM_START { get; set; }

       [DisplayName(PERSON_TRAININGDisplayNames.cCOMPLETEDDisplayName)]
       public string COMPLETED { get; set; }

       [DisplayName(PERSON_TRAININGDisplayNames.cRESULTSCOREDisplayName)]
       public Nullable<decimal> RESULTSCORE { get; set; }

       [DisplayName(PERSON_TRAININGDisplayNames.cPROGRESSDisplayName)]
       public Nullable<decimal> PROGRESS { get; set; }


       #endregion

       #region Calculated and other not stored properties
       // Add your calculated and other not stored fields here
       // ...

       #endregion

       #region Collection and foreign entities properties

       public TRAININGDetail TRAININGDetails { get; set; }

       #endregion

   }
   

    

}
