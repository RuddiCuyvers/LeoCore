﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace LeoCore.Data.Models;

public partial class QUESTIONNAIRE_QUESTION
{
    public int ID { get; set; }

    public int QUESTIONNAIRE_ID { get; set; }

    public int QUESTION_ID { get; set; }

    public string MANDATORY { get; set; }

    public int SORTORDER { get; set; }

    public DateTime DATE_VALID_START { get; set; }

    public DateTime? DATE_VALID_END { get; set; }

    public virtual QUESTION QUESTION { get; set; }

    public virtual QUESTIONNAIRE QUESTIONNAIRE { get; set; }
}