using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration section for storing Application settings
    /// </summary>
    public class ApplicationConfigurationSection : BaseConfigurationSection<ApplicationConfigurationSection>
    {
        // DELETED - This code has been moved to the generic base class BaseConfigurationSection<T>
        //#region Constants
        //// Define a section with this name in the config file under <configSections> and set its type to this class
        //// e.g. <section name="application" type="WGK.Lib.Configuration.ApplicationConfigurationSection, WGK.Lib" />
        //private const string cApplicationSectionName = "application";

        //// The section's full name is prefixed with the sectionGroup name from the config file
        //// e.g. <sectionGroup name="WGKSettingsGroup">
        //private const string cApplicationSectionFullName = BaseAppSettingsKey.cWGKSectionGroupName + "/" + cApplicationSectionName;
        //#endregion
        
        //#region Fields
        //// .NET Framework internally guarantees thread safety on static type initialization.
        //private static readonly object sInternalSyncObject = new object();

        //private static ApplicationConfigurationSection sCurrent;
        //#endregion
        
        //#region Current instance
        ///// <summary>
        ///// Retrieves and caches the configuration section from the application's config file. Subsequent calls return the cached instance.
        ///// </summary>
        //public static ApplicationConfigurationSection Current
        //{
        //    get
        //    {
        //        lock (sInternalSyncObject)
        //        {
        //            if (sCurrent == null)
        //            {
        //                try
        //                {
        //                    // Retrieves the configuration section from the application's config file.
        //                    sCurrent = (ApplicationConfigurationSection)ConfigurationManager.GetSection(cApplicationSectionFullName);
        //                    if (sCurrent == null)
        //                    {
        //                        sCurrent = new ApplicationConfigurationSection();                                
        //                    }
        //                }
        //                catch (ConfigurationErrorsException vException)
        //                {
        //                    WGK.Lib.Logging.Logging.PublishError(
        //                        new object[] { cApplicationSectionFullName },
        //                        "Config file error",
        //                        vException);
        //                    sCurrent = new ApplicationConfigurationSection();
        //                }
        //            }
        //        }
        //        return sCurrent;
        //    }
        //}
        //#endregion
                    
        #region Configuration Settings Properties
        /// <summary>
        /// Name of the Application.
        /// Used for authentication of user credentials.
        /// </summary>
        [ConfigurationProperty(cApplicationNameSettingName)]
        public string ApplicationName
        {
            get { return (string)this[cApplicationNameSettingName]; }
            set { this[cApplicationNameSettingName] = value; }
        }
        private const string cApplicationNameSettingName = "applicationName";

        /// <summary>
        /// Deployment environment.
        /// Shown in help about View.
        /// </summary>
        [ConfigurationProperty(cEnvironmentSettingName, IsRequired = true)]
        public string Environment
        {
            get { return (string)this[cEnvironmentSettingName]; }
            set { this[cEnvironmentSettingName] = value; }
        }
        private const string cEnvironmentSettingName = "environment";

        /// <summary>
        /// Optional authorization setting.
        /// Used to specify different authorization implementations (Principals for certain deployment environments.
        /// </summary>
        [ConfigurationProperty(cAuthorizationSettingName, IsRequired = false, DefaultValue = null)]
        public string Authorization
        {
            get { return (string)this[cAuthorizationSettingName]; }
            set { this[cAuthorizationSettingName] = value; }
        }
        private const string cAuthorizationSettingName = "authorization";

        /// <summary>
        /// Optional Domain setting.
        /// Used to specify the Domain for users in ASPNETDB membership database.
        /// </summary>
        [ConfigurationProperty(cDomainSettingName, IsRequired = false, DefaultValue = null)]
        public string Domain
        {
            get { return (string)this[cDomainSettingName]; }
            set { this[cDomainSettingName] = value; }
        }
        private const string cDomainSettingName = "domain";
        #endregion
    }
}