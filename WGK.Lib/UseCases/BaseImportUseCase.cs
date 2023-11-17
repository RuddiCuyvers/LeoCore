namespace WGK.Lib.UseCases
{
    public abstract class BaseImportUseCase<TRequestModel, TResponseModel>
        : BaseUseCase,
        IBaseImportUseCase<TRequestModel, TResponseModel>
    {
        #region IBaseImportUseCase Properties
        public TRequestModel ImportData { get; set; }
        public TResponseModel Result { get; protected set; }
        #endregion
    }
}