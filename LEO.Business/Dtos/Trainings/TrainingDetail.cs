using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Common.Constants.Trainings;
using LEO.Business.Dtos.Trainings;
using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Dtos.Persons;

namespace LEO.Business.Dtos.Trainings
{

    public class TRAININGDetail
    {
        #region Field names constants
        private const string cClassName = "TRAININGDetail";
        public static string ClassName { get { return cClassName; } }

        private const string cIDFieldName = "ID";
        public static string IDFieldName { get { return cIDFieldName; } }
        private const string cQRFieldName = "QR";
        public static string QRFieldName { get { return cQRFieldName; } }
        private const string cSUBJECTFieldName = "SUBJECT";
        public static string SUBJECTFieldName { get { return cSUBJECTFieldName; } }
        private const string cAPPLICANT_CLIENTIDFieldName = "APPLICANT_CLIENTID";
        public static string APPLICANT_CLIENTIDFieldName { get { return cAPPLICANT_CLIENTIDFieldName; } }
        private const string cMETHODOLOGYFieldName = "METHODOLOGY";
        public static string METHODOLOGYFieldName { get { return cMETHODOLOGYFieldName; } }
        private const string cLINKFieldName = "LINK";
        public static string LINKFieldName { get { return cLINKFieldName; } }
        private const string cTRAINING_TYPEFieldName = "TRAINING_TYPE";
        public static string TRAINING_TYPEFieldName { get { return cTRAINING_TYPEFieldName; } }
        private const string cTRAINING_TYPE_FTFieldName = "TRAINING_TYPE_FT";
        public static string TRAINING_TYPE_FTFieldName { get { return cTRAINING_TYPE_FTFieldName; } }
        private const string cTRAINER_INT_EXTFieldName = "TRAINER_INT_EXT";
        public static string TRAINER_INT_EXTFieldName { get { return cTRAINER_INT_EXTFieldName; } }



        private const string cTRAINER_EMAILFieldName = "TRAINER_EMAIL";
        public static string TRAINER_EMAILFieldName { get { return cTRAINER_EMAILFieldName; } }


        private const string cNOMENCL_CONV_YNFieldName = "NOMENCL_CONV_YN";
        public static string NOMENCL_CONV_YNFieldName { get { return cNOMENCL_CONV_YNFieldName; } }


        private const string cNC_EVD_YNFieldName = "NC_EVD_YN";
        public static string NC_EVD_YNFieldName { get { return cNC_EVD_YNFieldName; } }
        private const string cNC_EVD_DURATIONFieldName = "NC_EVD_DURATION";
        public static string NC_EVD_DURATIONFieldName { get { return cNC_EVD_DURATIONFieldName; } }
        private const string cNC_EVD_SUBJECTFieldName = "NC_EVD_SUBJECT";
        public static string NC_EVD_SUBJECTFieldName { get { return cNC_EVD_SUBJECTFieldName; } }
        private const string cNC_KATZ_YNFieldName = "NC_KATZ_YN";
        public static string NC_KATZ_YNFieldName { get { return cNC_KATZ_YNFieldName; } }
        private const string cNC_KATZ_DURATIONFieldName = "NC_KATZ_DURATION";
        public static string NC_KATZ_DURATIONFieldName { get { return cNC_KATZ_DURATIONFieldName; } }
        private const string cNC_KATZ_SUBJECTFieldName = "NC_KATZ_SUBJECT";
        public static string NC_KATZ_SUBJECTFieldName { get { return cNC_KATZ_SUBJECTFieldName; } }
        private const string cNC_THUISZORG_YNFieldName = "NC_THUISZORG_YN";
        public static string NC_THUISZORG_YNFieldName { get { return cNC_THUISZORG_YNFieldName; } }
        private const string cNC_THUISZORG_DURATIONFieldName = "NC_THUISZORG_DURATION";
        public static string NC_THUISZORG_DURATIONFieldName { get { return cNC_THUISZORG_DURATIONFieldName; } }
        private const string cNC_THUISZORG_SUBJECTFieldName = "NC_THUISZORG_SUBJECT";
        public static string NC_THUISZORG_SUBJECTFieldName { get { return cNC_THUISZORG_SUBJECTFieldName; } }
        private const string cNC_VVD_YNFieldName = "NC_VVD_YN";
        public static string NC_VVD_YNFieldName { get { return cNC_VVD_YNFieldName; } }
        private const string cNC_VVD_DURATIONFieldName = "NC_VVD_DURATION";
        public static string NC_VVD_DURATIONFieldName { get { return cNC_VVD_DURATIONFieldName; } }
        private const string cNC_VVD_SUBJECTFieldName = "NC_VVD_SUBJECT";
        public static string NC_VVD_SUBJECTFieldName { get { return cNC_VVD_SUBJECTFieldName; } }
        private const string cNC_ROL_YNFieldName = "NC_ROL_YN";
        public static string NC_ROL_YNFieldName { get { return cNC_ROL_YNFieldName; } }
        private const string cNC_ROL_DURATIONFieldName = "NC_ROL_DURATION";
        public static string NC_ROL_DURATIONFieldName { get { return cNC_ROL_DURATIONFieldName; } }
        private const string cNC_ROL_SUBJECTFieldName = "NC_ROL_SUBJECT";
        public static string NC_ROL_SUBJECTFieldName { get { return cNC_ROL_SUBJECTFieldName; } }
        private const string cEV_YNFieldName = "EV_YN";
        public static string EV_YNFieldName { get { return cEV_YNFieldName; } }
        private const string cEV_WW_YNFieldName = "EV_WW_YN";
        public static string EV_WW_YNFieldName { get { return cEV_WW_YNFieldName; } }
        private const string cEV_WW_DURATIONFieldName = "EV_WW_DURATION";
        public static string EV_WW_DURATIONFieldName { get { return cEV_WW_DURATIONFieldName; } }
        private const string cEV_ZG_SUBJFieldName = "EV_ZG_SUBJ";
        public static string EV_ZG_SUBJFieldName { get { return cEV_ZG_SUBJFieldName; } }
        private const string cEV_ZG_REFDOMFieldName = "EV_ZG_REFDOM";
        public static string EV_ZG_REFDOMFieldName { get { return cEV_ZG_REFDOMFieldName; } }
        private const string cEV_ZG_COMPLFieldName = "EV_ZG_COMPL";
        public static string EV_ZG_COMPLFieldName { get { return cEV_ZG_COMPLFieldName; } }
        private const string cEV_AWS_YNFieldName = "EV_AWS_YN";
        public static string EV_AWS_YNFieldName { get { return cEV_AWS_YNFieldName; } }
        private const string cEV_AWS_SUBJFieldName = "EV_AWS_SUBJ";
        public static string EV_AWS_SUBJFieldName { get { return cEV_AWS_SUBJFieldName; } }
        private const string cEV_AWS_ONCO_SUBJFieldName = "EV_AWS_ONCO_SUBJ";
        public static string EV_AWS_ONCO_SUBJFieldName { get { return cEV_AWS_ONCO_SUBJFieldName; } }
        private const string cEV_AWS_INCO_SUBJFieldName = "EV_AWS_INCO_SUBJ";
        public static string EV_AWS_INCO_SUBJFieldName { get { return cEV_AWS_INCO_SUBJFieldName; } }
        private const string cEV_SUBJECTFieldName = "EV_SUBJECT";
        public static string EV_SUBJECTFieldName { get { return cEV_SUBJECTFieldName; } }
        private const string cEV_ZG_DURATIONFieldName = "EV_ZG_DURATION";
        public static string EV_ZG_DURATIONFieldName { get { return cEV_ZG_DURATIONFieldName; } }
        private const string cEV_AWS_DURATIONFieldName = "EV_AWS_DURATION";
        public static string EV_AWS_DURATIONFieldName { get { return cEV_AWS_DURATIONFieldName; } }
        private const string cLOCATIONFieldName = "LOCATION";
        public static string LOCATION_ANDERSFieldName { get { return cLOCATION_ANDERSFieldName; } }
        private const string cLOCATION_ANDERSFieldName = "LOCATION_ANDERS";
        public static string LOCATIONFieldName { get { return cLOCATIONFieldName; } }
        private const string cCOSTPRICEFieldName = "COSTPRICE";
        public static string COSTPRICEFieldName { get { return cCOSTPRICEFieldName; } }
        private const string cDURATION_OVERALLFieldName = "DURATION_OVERALL";
        public static string DURATION_OVERALLFieldName { get { return cDURATION_OVERALLFieldName; } }
        private const string cNC_NOMC_YNFieldName = "NC_NOMC_YN";
        public static string NC_NOMC_YNFieldName { get { return cNC_NOMC_YNFieldName; } }
        private const string cNC_NOMC_DURATIONFieldName = "NC_NOMC_DURATION";
        public static string NC_NOMC_DURATIONFieldName { get { return cNC_NOMC_DURATIONFieldName; } }
        private const string cNC_NOMC_SUBJFieldName = "NC_NOMC_SUBJ";
        public static string NC_NOMC_SUBJFieldName { get { return cNC_NOMC_SUBJFieldName; } }
        private const string cEV_ZG_YNFieldName = "EV_ZG_YN";
        public static string EV_ZG_YNFieldName { get { return cEV_ZG_YNFieldName; } }
        private const string cEV_PERS_YNFieldName = "EV_PERS_YN";
        public static string EV_PERS_YNFieldName { get { return cEV_PERS_YNFieldName; } }
        private const string cEV_PERS_DURATIONFieldName = "EV_PERS_DURATION";
        public static string EV_PERS_DURATIONFieldName { get { return cEV_PERS_DURATIONFieldName; } }
        private const string cEV_ANDERS_YNFieldName = "EV_ANDERS_YN";
        public static string EV_ANDERS_YNFieldName { get { return cEV_ANDERS_YNFieldName; } }
        private const string cEV_ANDERS_DURATIONFieldName = "EV_ANDERS_DURATION";
        public static string EV_ANDERS_DURATIONFieldName { get { return cEV_ANDERS_DURATIONFieldName; } }
        private const string cEV_ANDERS_FTFieldName = "EV_ANDERS_FT";
        public static string EV_ANDERS_FTFieldName { get { return cEV_ANDERS_FTFieldName; } }
        #endregion

        #region Constructor
        public TRAININGDetail()
        {
            // Make sure collection and navigation properties are never null
            this.PERSON_QUESTIONNAIREDetails = new List<PERSON_QUESTIONNAIREDetail>();
            this.PERSON_TRAININGDetails = new List<PERSON_TRAININGDetail>();
            this.TRAINING_QUESTIONNNAIREDetails = new List<TRAINING_QUESTIONNNAIREDetail>();
            this.TRAINING_TRAINERDetails = new List<TRAINING_TRAINERDetail>();
        }
        #endregion

        #region Primitive properties (form data layer entity)
        [DisplayName(TRAININGDisplayNames.cIDDisplayName)]
        public int ID { get; set; }

        [DisplayName(TRAININGDisplayNames.cQRDisplayName)]
        public byte[] QR { get; set; }

        [DisplayName(TRAININGDisplayNames.cSUBJECTDisplayName)]
        public string SUBJECT { get; set; }

        [DisplayName(TRAININGDisplayNames.cAPPLICANT_CLIENTIDDisplayName)]
        public string APPLICANT_CLIENTID { get; set; }

        [DisplayName(TRAININGDisplayNames.cMETHODOLOGYDisplayName)]
        public string METHODOLOGY { get; set; }

        [DisplayName(TRAININGDisplayNames.cLINKDisplayName)]
        public string LINK { get; set; }

        [DisplayName(TRAININGDisplayNames.cTRAINING_TYPEDisplayName)]
        public string TRAINING_TYPE { get; set; }

        [DisplayName(TRAININGDisplayNames.cTRAINING_TYPE_FTDisplayName)]
        public string TRAINING_TYPE_FT { get; set; }

        [DisplayName(TRAININGDisplayNames.cTRAINER_INT_EXTDisplayName)]
        public string TRAINER_INT_EXT { get; set; }

        [DisplayName(TRAININGDisplayNames.cTRAINER_EMAILDisplayName)]
        public string TRAINER_EMAIL { get; set; }

        [DisplayName(TRAININGDisplayNames.cNOMENCL_CONV_YNDisplayName)]
        public string NOMENCL_CONV_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_EVD_YNDisplayName)]
        public bool NC_EVD_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_EVD_DURATIONDisplayName)]
        public string NC_EVD_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_EVD_SUBJECTDisplayName)]
        public string NC_EVD_SUBJECT { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_KATZ_YNDisplayName)]
        public bool NC_KATZ_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_KATZ_DURATIONDisplayName)]
        public string NC_KATZ_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_KATZ_SUBJECTDisplayName)]
        public string NC_KATZ_SUBJECT { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_THUISZORG_YNDisplayName)]
        public bool NC_THUISZORG_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_THUISZORG_DURATIONDisplayName)]
        public string NC_THUISZORG_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_THUISZORG_SUBJECTDisplayName)]
        public string NC_THUISZORG_SUBJECT { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_VVD_YNDisplayName)]
        public bool NC_VVD_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_VVD_DURATIONDisplayName)]
        public string NC_VVD_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_VVD_SUBJECTDisplayName)]
        public string NC_VVD_SUBJECT { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_ROL_YNDisplayName)]
        public bool NC_ROL_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_ROL_DURATIONDisplayName)]
        public string NC_ROL_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_ROL_SUBJECTDisplayName)]
        public string NC_ROL_SUBJECT { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_YNDisplayName)]
        public string EV_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_WW_YNDisplayName)]
        public bool EV_WW_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_WW_DURATIONDisplayName)]
        public string EV_WW_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_ZG_SUBJDisplayName)]
        public string EV_ZG_SUBJ { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_ZG_REFDOMDisplayName)]
        public string EV_ZG_REFDOM { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_ZG_COMPLDisplayName)]
        public string EV_ZG_COMPL { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_AWS_YNDisplayName)]
        public string EV_AWS_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_AWS_SUBJDisplayName)]
        public string EV_AWS_SUBJ { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_AWS_ONCO_SUBJDisplayName)]
        public string EV_AWS_ONCO_SUBJ { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_AWS_INCO_SUBJDisplayName)]
        public string EV_AWS_INCO_SUBJ { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_SUBJECTDisplayName)]
        public string EV_SUBJECT { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_ZG_DURATIONDisplayName)]
        public string EV_ZG_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_AWS_DURATIONDisplayName)]
        public string EV_AWS_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cLOCATIONDisplayName)]
        public string LOCATION { get; set; }
        
        [DisplayName(TRAININGDisplayNames.cLOCATION_ANDERSDisplayName)]
        public string LOCATION_ANDERS { get; set; }

        [DisplayName(TRAININGDisplayNames.cCOSTPRICEDisplayName)]
        public string COSTPRICE { get; set; }

        [DisplayName(TRAININGDisplayNames.cDURATION_OVERALLDisplayName)]
        public string DURATION_OVERALL { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_NOMC_YNDisplayName)]
        public bool NC_NOMC_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_NOMC_DURATIONDisplayName)]
        public string NC_NOMC_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cNC_NOMC_SUBJDisplayName)]
        public string NC_NOMC_SUBJ { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_ZG_YNDisplayName)]
        public string EV_ZG_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_PERS_YNDisplayName)]
        public bool EV_PERS_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_PERS_DURATIONDisplayName)]
        public Nullable<int> EV_PERS_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_ANDERS_YNDisplayName)]
        public bool EV_ANDERS_YN { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_ANDERS_DURATIONDisplayName)]
        public Nullable<int> EV_ANDERS_DURATION { get; set; }

        [DisplayName(TRAININGDisplayNames.cEV_ANDERS_FTDisplayName)]
        public string EV_ANDERS_FT { get; set; }


        #endregion

        #region Calculated and other not stored properties
        // Add your calculated and other not stored fields here
        // ...

        #endregion

        #region Collection and foreign entities properties
        public List<PERSON_QUESTIONNAIREDetail> PERSON_QUESTIONNAIREDetails { get; set; }
        public List<PERSON_TRAININGDetail> PERSON_TRAININGDetails { get; set; }
        public List<TRAINING_QUESTIONNNAIREDetail> TRAINING_QUESTIONNNAIREDetails { get; set; }
        public List<TRAINING_TRAINERDetail> TRAINING_TRAINERDetails { get; set; }


        #endregion
    }

}


