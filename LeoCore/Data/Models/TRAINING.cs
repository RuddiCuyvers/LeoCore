﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace LeoCore.Data.Models;

/// <summary>
/// 	
/// </summary>
public partial class TRAINING
{
    public int ID { get; set; }

    public byte[] QR { get; set; }

    public string SUBJECT { get; set; }

    /// <summary>
    /// google account van de inbrenger/lesgever
    /// </summary>
    public string APPLICANT_CLIENTID { get; set; }

    /// <summary>
    /// usertable METH
    /// </summary>
    public string METHODOLOGY { get; set; }

    /// <summary>
    /// flowsparks
    /// </summary>
    public string LINK { get; set; }

    /// <summary>
    /// usertable TT
    /// </summary>
    public string TRAINING_TYPE { get; set; }

    /// <summary>
    /// vrije tekst bij andere
    /// </summary>
    public string TRAINING_TYPE_FT { get; set; }

    /// <summary>
    /// usertable INTEXT
    /// </summary>
    public string TRAINER_INT_EXT { get; set; }

    public string TRAINER_EMAIL { get; set; }

    public string NOMENCL_CONV_YN { get; set; }

    public string NC_EVD_YN { get; set; }

    public int? NC_EVD_DURATION { get; set; }

    public string NC_EVD_SUBJECT { get; set; }

    public string NC_KATZ_YN { get; set; }

    public int? NC_KATZ_DURATION { get; set; }

    public string NC_KATZ_SUBJECT { get; set; }

    public string NC_THUISZORG_YN { get; set; }

    public int? NC_THUISZORG_DURATION { get; set; }

    public string NC_THUISZORG_SUBJECT { get; set; }

    public string NC_VVD_YN { get; set; }

    public int? NC_VVD_DURATION { get; set; }

    public string NC_VVD_SUBJECT { get; set; }

    public string NC_ROL_YN { get; set; }

    public int? NC_ROL_DURATION { get; set; }

    public string NC_ROL_SUBJECT { get; set; }

    public string EV_YN { get; set; }

    public string EV_WW_YN { get; set; }

    public int? EV_WW_DURATION { get; set; }

    /// <summary>
    /// usertable
    /// </summary>
    public string EV_ZG_SUBJ { get; set; }

    /// <summary>
    /// usertable
    /// </summary>
    public string EV_ZG_REFDOM { get; set; }

    /// <summary>
    /// usertable
    /// </summary>
    public string EV_ZG_COMPL { get; set; }

    public string EV_AWS_YN { get; set; }

    public string EV_AWS_SUBJ { get; set; }

    public string EV_AWS_ONCO_SUBJ { get; set; }

    public string EV_ZG_COMPL_SUBJ { get; set; }

    public string EV_AWS_INCO_SUBJ { get; set; }

    public string EV_SUBJECT { get; set; }

    public int? EV_ZG_DURATION { get; set; }

    public int? EV_AWS_DURATION { get; set; }

    public string LOCATION { get; set; }

    public string COSTPRICE { get; set; }

    public int? DURATION_OVERALL { get; set; }

    public string NC_NOMC_YN { get; set; }

    public int? NC_NOMC_DURATION { get; set; }

    public string NC_NOMC_SUBJ { get; set; }

    public string EV_ZG_YN { get; set; }

    public string EV_PERS_YN { get; set; }

    public int? EV_PERS_DURATION { get; set; }

    public string EV_ANDERS_YN { get; set; }

    public int? EV_ANDERS_DURATION { get; set; }

    public string EV_ANDERS_FT { get; set; }

    public string LOCATION_ANDERS { get; set; }

    public virtual ICollection<PERSON_QUESTIONNAIRE> PERSON_QUESTIONNAIREs { get; set; } = new List<PERSON_QUESTIONNAIRE>();

    public virtual ICollection<PERSON_TRAINING> PERSON_TRAININGs { get; set; } = new List<PERSON_TRAINING>();

    public virtual ICollection<TRAINING_QUESTIONNNAIRE> TRAINING_QUESTIONNNAIREs { get; set; } = new List<TRAINING_QUESTIONNNAIRE>();

    public virtual ICollection<TRAINING_TRAINER> TRAINING_TRAINERs { get; set; } = new List<TRAINING_TRAINER>();
}