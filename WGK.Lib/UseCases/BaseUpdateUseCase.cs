namespace WGK.Lib.UseCases
{
    public abstract class BaseUpdateUseCase<TUpdateModel> : BaseUseCase, IBaseUpdateUseCase<TUpdateModel>
    {
        #region IBaseUpdateUseCase Properties
        public TUpdateModel UpdateData { get; set; }
        public decimal ResultID { get; protected set; }
        #endregion
    }
}