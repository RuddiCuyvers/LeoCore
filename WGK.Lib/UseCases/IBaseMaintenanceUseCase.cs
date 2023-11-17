namespace WGK.Lib.UseCases
{
    public interface IBaseMaintenanceUseCase<TMaintenanceModel> : IBaseUseCase
    {
        decimal ID { get; set; }

        string CurrentUserClientID { get; set; }

      
        TMaintenanceModel Result { get; }
    }
}