namespace WGK.Lib.UseCases
{
    public abstract class BaseExportUseCase<TRequestModel, TResponseModel>
        : BaseUseCase,
        IBaseExportUseCase<TRequestModel, TResponseModel>
    {
        #region IBaseExportUseCase Properties
        public TRequestModel ExportCriteria { get; set; }
        public TResponseModel Result { get; protected set; }
        #endregion
    }
}