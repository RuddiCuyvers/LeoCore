﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace LeoCore.Data.Models;

public partial class PERSON_QUESTIONNAIRE_ANSWER
{
    public int ID { get; set; }

    public int PERSON_QUESTIONNAIRE_ID { get; set; }

    public int QUESTION_ID { get; set; }

    public string ANSWER_TEXT { get; set; }

    public int? ANSWER_NUMBER { get; set; }

    public DateTime? ANSWER_DATE { get; set; }

    public string QTEXT_AS_WAS { get; set; }

    public int? QQORDER_AS_WAS { get; set; }

    public string QTYPEANSWER_AS_WAS { get; set; }

    public string PERSON_QUESTIONNAIRE_ANSWERcol { get; set; }

    public virtual PERSON_QUESTIONNAIRE PERSON_QUESTIONNAIRE { get; set; }

    public virtual QUESTION QUESTION { get; set; }
}