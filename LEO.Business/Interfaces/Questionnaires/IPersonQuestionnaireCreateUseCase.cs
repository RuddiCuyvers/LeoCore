using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Dtos.Questionnaires;


namespace LEO.Business.Interfaces.Questionnaires
{
    public interface IPersonQuestionnaireCreateUseCase 
    {
        PERSON_QUESTIONNAIREDetail CreateData { get; set; }
        decimal Result { get; }
    }
}
