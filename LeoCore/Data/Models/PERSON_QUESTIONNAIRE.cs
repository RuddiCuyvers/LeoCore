﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace LeoCore.Data.Models;

public partial class PERSON_QUESTIONNAIRE
{
    public int ID { get; set; }

    public string CLIENT_ID { get; set; }

    public int QUESTIONNAIRE_ID { get; set; }

    public int TRAINING_ID { get; set; }

    public DateTime? DATE_SUBMITTED { get; set; }

    public virtual List<PERSON_QUESTIONNAIRE_ANSWER> PERSON_QUESTIONNAIRE_ANSWERs { get; set; } = new List<PERSON_QUESTIONNAIRE_ANSWER>();

    public virtual QUESTIONNAIRE QUESTIONNAIRE { get; set; }

    public virtual TRAINING TRAINING { get; set; }

    
}