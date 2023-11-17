using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Models.Questionnaires;



namespace LEO.Business.Interfaces.Questionnaires
{
    public interface IBasePersonQuestionnaireUpdateUseCase<TUpdateModel, TQuestionnaireDto, TPersonQuestionnaireDto>
       
        where TUpdateModel :  BasePersonQuestionnaireUpdateModel<TQuestionnaireDto, TPersonQuestionnaireDto>
        where TQuestionnaireDto : QUESTIONNAIREDetail
        where TPersonQuestionnaireDto : PERSON_QUESTIONNAIREDetail, new()
    {
    }

}
