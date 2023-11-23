namespace LEO.Common.Codes
{
    /// <summary>
    /// Static class that defines UserCodeGroup codes as constants
    /// This class 'extends' constants defined in the BaseUserCodeGroupCode class.
    /// </summary>
    public static class UserCodeGroupCode
    {

        #region Constants and Properties - 
        /// <summary>
        /// Gets the Department group code.
        /// </summary>
        public static string DepartmentType { get { return cDepartmentType; } }
        public const string cDepartmentType = "DPRTMNT";
        /// <summary>
        /// Gets the Department group code.
        /// </summary>
        public static string QUESTIONNAIRE { get { return cQUESTIONNAIRE; } }
        public const string cQUESTIONNAIRE = "QSTNNRS";

        /// <summary>
        /// Gets the Training code.
        /// </summary>
        public static string Training { get { return cTraining; } }
        public const string cTraining = "TRNNG";
        #endregion



        // -- UserCodeGroups codes for groups present in the common 'UserCode' table
        #region Constants and Properties - UserCode table

        public static string JaNeeLijst { get { return cJaNeeLijst; } }
        public const string cJaNeeLijst = "DDB_JN";

        public static string Score5Lijst { get { return cScore5Lijst; } }
        public const string cScore5Lijst = "SCORE5";
        public static string Score10Lijst { get { return cScore10Lijst; } }
        public const string cScore10Lijst = "SCORE10";

        public static string TypeTrainingLijst { get { return cTypeTrainingLijst; } }
        public const string cTypeTrainingLijst = "TRAINING_TYPE";

        public static string METHODOLOGYLijst { get { return cMETHODOLOGYLijst; } }
        public const string cMETHODOLOGYLijst = "METHODOLOGY";

        public static string EXT_INTLijst { get { return cEXT_INTLijst; } }
        public const string cEXT_INTLijst = "EXT_INT";

        public static string NC_TRAINING_CATLijst { get { return cNC_TRAINING_CATLijst; } }
        public const string cNC_TRAINING_CATLijst = "NC_TRAINING_CAT";

        public static string EV_TRAINING_CATLijst { get { return cEV_TRAINING_CATLijst; } }
        public const string cEV_TRAINING_CATLijst = "EV_TRAINING_CAT";

        public static string ZG_SUBJLijst { get { return cZG_SUBJLijst; } }
        public const string cZG_SUBJLijst = "ZG_SUBJ";

        public static string ZG_REF_DOMLijst { get { return cZG_REF_DOMLijst; } }
        public const string cZG_REF_DOMLijst = "ZG_REF_DOM";

        public static string ZG_COMPL_DIENSTLijst { get { return cZG_COMPL_DIENSTLijst; } }
        public const string cZG_COMPL_DIENSTLijst = "ZG_COMPL_DIENST";

        public static string AWS_ONCO_SUBJLijst { get { return cAWS_ONCO_SUBJLijst; } }
        public const string cAWS_ONCO_SUBJLijst = "AWS_ONCO_SUBJ";

        public static string AWS_INCO_SUBJLijst { get { return cAWS_INCO_SUBJLijst; } }
        public const string cAWS_INCO_SUBJLijst = "AWS_INCO_SUBJ";

        public static string LOCATIELijst { get { return cLOCATIELijst; } }
        public const string cLOCATIELijst = "LOCATIE";

        public static string AWS_SUBJLijst { get { return cAWS_SUBJLijst; } }
        public const string cAWS_SUBJLijst = "AWS_SUBJ";

        public static string GebruikersLijst { get { return cGebruikersLijst; } }
        public const string cGebruikersLijst = "CLIENTID";

        public static string JaartallenLijst { get { return cJaartallenLijst; } }
        public const string cJaartallenLijst = "JAARTAL";

        #endregion

        // REMARK: Since static classes cannot derive from a base class we must define BaseUserCodeGroupCode properties again.
        #region Base class properties

 
        #endregion
    }
}
