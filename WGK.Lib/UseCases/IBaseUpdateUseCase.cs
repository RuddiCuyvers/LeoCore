namespace WGK.Lib.UseCases
{
    public interface IBaseUpdateUseCase<TUpdateModel> : IBaseUseCase
    {
        TUpdateModel UpdateData { get; set; }
        decimal ResultID { get; }
    }
}