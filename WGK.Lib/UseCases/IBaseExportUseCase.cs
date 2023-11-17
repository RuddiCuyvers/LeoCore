namespace WGK.Lib.UseCases
{
    public interface IBaseExportUseCase<TRequestModel, TResponseModel> : IBaseUseCase
    {
        TRequestModel ExportCriteria { get; set; }
        TResponseModel Result { get; }
    }
}