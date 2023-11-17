using WGK.LMS.Business.Dtos.Questionnaires;
using WGK.LMS.Business.Models.Questionnaires;
using WGK.Lib.UseCases;


namespace WGK.LMS.Business.Interfaces.Questionnaires
{
    public interface IBasePersonQuestionnaireMaintenanceUseCase<TMaintenanceModel, TQuestionnaireDto, TPersonQuestionnaireDto>
        : IBaseMaintenanceUseCase<TMaintenanceModel>
        where TMaintenanceModel : BasePersonQuestionnaireMaintenanceModel<TQuestionnaireDto, TPersonQuestionnaireDto>, new()
        where TQuestionnaireDto : QUESTIONNAIREDetail, new()
        where TPersonQuestionnaireDto : PERSON_QUESTIONNAIREDetail, new()
    {
    }
}
