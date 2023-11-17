using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Models.Questionnaires;

namespace LEO.Business.Interfaces.Questionnaires
{
    public interface IPersonQuestionnaireUpdateUseCase
        : IBasePersonQuestionnaireUpdateUseCase<PersonQuestionnaireUpdateModel, QUESTIONNAIREDetail, PERSON_QUESTIONNAIREDetail>
    {

  
    }
}
