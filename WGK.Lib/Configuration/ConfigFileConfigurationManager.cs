using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// ConfigurationManager that gets its data from the application's configuration file (web.config, app.config)
    /// </summary>
    public class ConfigFileConfigurationManager : IConfigurationManager
    {
        public string GetAppSetting(string pKey)
        {
            return ConfigurationManager.AppSettings[pKey];
        }

        public ConnectionStringSettings GetConnectionString(string pName)
        {
            return ConfigurationManager.ConnectionStrings[pName];
        }

        public TConfigurationSection GetSection<TConfigurationSection>(string pSectionFullName)
        {
            return (TConfigurationSection)ConfigurationManager.GetSection(pSectionFullName);
        }
    }
}