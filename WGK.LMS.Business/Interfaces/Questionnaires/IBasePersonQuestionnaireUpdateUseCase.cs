using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WGK.LMS.Business.Dtos.Questionnaires;
using WGK.LMS.Business.Models.Questionnaires;
using WGK.Lib.UseCases;


namespace WGK.LMS.Business.Interfaces.Questionnaires
{
    public interface IBasePersonQuestionnaireUpdateUseCase<TUpdateModel, TQuestionnaireDto, TPersonQuestionnaireDto>
        : IBaseUpdateUseCase<TUpdateModel>
        where TUpdateModel :  BasePersonQuestionnaireUpdateModel<TQuestionnaireDto, TPersonQuestionnaireDto>
        where TQuestionnaireDto : QUESTIONNAIREDetail
        where TPersonQuestionnaireDto : PERSON_QUESTIONNAIREDetail, new()
    {
    }

}
