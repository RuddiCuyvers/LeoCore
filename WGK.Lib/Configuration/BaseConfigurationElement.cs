using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration element for storing settings in a collection
    /// </summary>
    public class BaseConfigurationElement : ConfigurationElement
    {
        #region Configuration Settings Properties
        /// <summary>
        /// Configuration name for this configuration element instance.
        /// This is the key property for the ConfigurationElement.
        /// </summary>
        [ConfigurationProperty(cNameSettingName, DefaultValue = "", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this[cNameSettingName]; }
            set { this[cNameSettingName] = value; }
        }

        private const string cNameSettingName = "name";
        #endregion
    }
}