using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.Linq;
using System.Text;
using WGK.Lib.Extensions;
using WGK.Lib.Ioc;
using NotImplementedException = WGK.Lib.Exceptions.NotImplementedException;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// .Generic base class for ConfigurationSection implementations.
    /// You must add the actual SectionName for each derived class to the base class implementation.
    /// </summary>
    /// <typeparam name="TConfigurationSection">The custom ConfigurationSection type</typeparam>
    public class BaseConfigurationSection<TConfigurationSection> : ConfigurationSection where TConfigurationSection : ConfigurationSection, new()
    {        
        #region Constants
        public const string cWGKSectionGroupName = "WGKSettingsGroup";
        #endregion    

        #region Fields
        // .NET Framework internally guarantees thread safety on static type initialization.
        private static readonly object sInternalSyncObject = new object();

        // Cached ConfigurationSection instance
        private static TConfigurationSection sCurrent;
        #endregion

        #region Properties
        protected static string SectionFullName
        {
            // The section's full name is prefixed with the sectionGroup name from the config file
            // e.g. <sectionGroup name="WGKSettingsGroup">
            get { return cWGKSectionGroupName + "/" + SectionName; }
         }

        protected static string SectionName
        {
            get
            {
                // Define sections with these names in the config file under <configSections> and the types to the derived classes
                // e.g. <section name="application" type="WGK.Lib.Configuration.ApplicationConfigurationSection, WGK.Lib" />

                var vType = typeof(TConfigurationSection);

                // You can override the default section name for a specific ConfigurationSection class by adding the following code
                // Example:
                //if (vType == typeof(ApplicationConfigurationSection))
                //{
                //    return "application123";
                //}

                // Default SectionName implementation:
                // Determine the section name from the type name by stripping ConfigurationSection suffix and making
                // the first letter lower case.
                // Example: type name "ApplicationConfigurationSection" => becomes section name "application"
                var vTypeName = vType.Name;
                var vIndex = vTypeName.IndexOf("ConfigurationSection", System.StringComparison.InvariantCulture);
                if (vIndex > 0)
                {
                    string vSectionName = vTypeName.Substring(0, vIndex);
                    return vSectionName.ToLowerFirst(CultureInfo.InvariantCulture);
                }

                throw new NotImplementedException(
                    String.Format("BaseConfigurationSection<{0}>", typeof(TConfigurationSection).Name),
                    "SectionName");
            }
        }
        #endregion
    
        #region Current instance
        /// <summary>
        /// Retrieves and caches the configuration section from the application's config file.
        /// Subsequent calls return the cached instance.
        /// An exception is thrown if the configuration section does not exist in the application's config file.
        /// </summary>
        public static TConfigurationSection Current
        {
            get
            {
                lock (sInternalSyncObject)
                {
                    if (sCurrent == null)
                    {
                        // Retrieves the configuration section from the application's config file.


                        // Get instance of ConfigurationManager from dependency injection container
                        var vConfigurationManager = IocManager.Resolve<IConfigurationManager>();

                        // Remark: this method throws an exception if config file is corrupt or if config section is missing
                        sCurrent = vConfigurationManager.GetSection<TConfigurationSection>(SectionFullName);
                    }
                }
                return sCurrent;
            }
        }
        #endregion
    }
}
