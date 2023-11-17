namespace WGK.Lib.Web.Mvc.Helpers
{
    public interface ICustomEnablementProvider
    {
        /// <summary>
        /// Returns true if the custom enablement logic requires widget to be disabled
        /// </summary>
        /// <param name="pCustomEnablementKey">Key that identifies which the custom enablement logic to apply</param>
        /// <returns></returns>
        bool IsReadOnly(string pCustomEnablementKey);
    }
}