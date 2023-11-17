using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration element for storing dispatcher task settings
    /// </summary>
    public class DispatcherTaskConfigurationElement : BaseConfigurationElement
    {
        #region Configuration Settings Properties - General
        /// <summary>
        /// UserDomain for authenticating on the Dispatcher WebService
        /// </summary>
        [ConfigurationProperty(cUserDomainSettingName, DefaultValue = "", IsRequired = true)]
        public string UserDomain
        {
            get { return (string)this[cUserDomainSettingName]; }
            set { this[cUserDomainSettingName] = value; }
        }
        private const string cUserDomainSettingName = "userDomain";

        /// <summary>
        /// UserName for authenticating on the Dispatcher WebService
        /// </summary>
        [ConfigurationProperty(cUserNameSettingName, DefaultValue = "", IsRequired = true)]
        public string UserName
        {
            get { return (string)this[cUserNameSettingName]; }
            set { this[cUserNameSettingName] = value; }
        }
        private const string cUserNameSettingName = "userName";

        /// <summary>
        /// Password for authenticating on the Dispatcher WebService
        /// </summary>
        [ConfigurationProperty(cPasswordSettingName, DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get { return (string)this[cPasswordSettingName]; }
            set { this[cPasswordSettingName] = value; }
        }
        private const string cPasswordSettingName = "password";
        #endregion
    }
}

