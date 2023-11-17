using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Questionnaires;
using WGK.Lib.UseCases;

namespace WGK.LMS.Business.Interfaces.Questionnaires
{
    public interface IPersonQuestionnaireCreateUseCase : IBaseUseCase
    {
        PERSON_QUESTIONNAIREDetail CreateData { get; set; }
        decimal Result { get; }
    }
}
