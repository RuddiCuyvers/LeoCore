using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Questionnaires;
using WGK.LMS.Business.Models.Questionnaires;

namespace WGK.LMS.Business.Interfaces.Questionnaires
{
    public interface IPersonQuestionnaireUpdateUseCase
        : IBasePersonQuestionnaireUpdateUseCase<PersonQuestionnaireUpdateModel, QUESTIONNAIREDetail, PERSON_QUESTIONNAIREDetail>
    {

  
    }
}
