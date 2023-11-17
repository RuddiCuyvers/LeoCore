using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Configuration;
using System.Linq;
using System.Text;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Interface for a ConfigurationManager to access the application's configuration settings
    /// </summary>
    public interface IConfigurationManager
    {
        /// <summary>
        /// Gets the app setting value for the specified key
        /// </summary>
        string GetAppSetting(string pKey);

        /// <summary>
        /// Gets the ConnectionStringSettings value for the specified connection name
        /// </summary>
        ConnectionStringSettings GetConnectionString(string pName);

        /// <summary>
        /// Gets the custom configuration section for the specified section name
        /// </summary>
        /// <typeparam name="TConfigurationSection">The type of the custom configuration section</typeparam>
        /// <param name="pSectionFullName">The name of the section in the config file</param>
        /// <returns>Instance of the custom configuration section type filled in with values from config file </returns>
        TConfigurationSection GetSection<TConfigurationSection>(string pSectionFullName);
    }
}
