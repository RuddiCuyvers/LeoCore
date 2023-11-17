namespace WGK.Lib.UseCases
{
    public interface IBaseImportUseCase<TRequestModel, TResponseModel> : IBaseUseCase
    {
        TRequestModel ImportData { get; set; }
        TResponseModel Result { get; }
    }
}