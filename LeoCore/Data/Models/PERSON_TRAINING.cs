﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
#nullable disable
using System;
using System.Collections.Generic;

namespace LeoCore.Data.Models;

public partial class PERSON_TRAINING
{
    public int ID { get; set; }

    public int TRAINING_ID { get; set; }

    public string CLIENT_ID { get; set; }

    public DateOnly? DATUM_START { get; set; }

    public string COMPLETED { get; set; }

    /// <summary>
    /// getal
    /// </summary>
    public int? RESULTSCORE { get; set; }

    public int? PROGRESS { get; set; }

    public virtual TRAINING TRAINING { get; set; }
}