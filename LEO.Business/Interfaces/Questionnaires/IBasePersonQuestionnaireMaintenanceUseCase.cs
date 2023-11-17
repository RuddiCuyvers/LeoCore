using LEO.Business.Dtos.Questionnaires;
using LEO.Business.Models.Questionnaires;



namespace LEO.Business.Interfaces.Questionnaires
{
    public interface IBasePersonQuestionnaireMaintenanceUseCase<TMaintenanceModel, TQuestionnaireDto, TPersonQuestionnaireDto>
        
        where TMaintenanceModel : BasePersonQuestionnaireMaintenanceModel<TQuestionnaireDto, TPersonQuestionnaireDto>, new()
        where TQuestionnaireDto : QUESTIONNAIREDetail, new()
        where TPersonQuestionnaireDto : PERSON_QUESTIONNAIREDetail, new()
    {
    }
}
