﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Questionnaires;

namespace WGK.LMS.Business.Models.Questionnaires
{
    /// <summary>
    /// Questionnaire Update data from the presentation layer to update in the database
    /// </summary>
    public class PersonQuestionnaireUpdateModel : BasePersonQuestionnaireUpdateModel<QUESTIONNAIREDetail, PERSON_QUESTIONNAIREDetail>
    {
    }
}
