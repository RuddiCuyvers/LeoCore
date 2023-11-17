using System.Configuration;
using System.Net;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration section for storing MasterData WebService settings
    /// </summary>
    public class MasterDataServiceConfigurationSection : BaseConfigurationSection<MasterDataServiceConfigurationSection>
    {
        #region Configuration Settings Properties
        /// <summary>
        /// UserName for authenticating on the MasterData WebService
        /// </summary>
        [ConfigurationProperty(cUserNameSettingName, DefaultValue = "", IsRequired = true)]
        public string UserName
        {
            get { return (string)this[cUserNameSettingName]; }
            set { this[cUserNameSettingName] = value; }
        }
        private const string cUserNameSettingName = "userName";

        /// <summary>
        /// Password for authenticating on the MasterData WebService
        /// </summary>
        [ConfigurationProperty(cPasswordSettingName, DefaultValue = "", IsRequired = true)]
        public string Password
        {
            get { return (string)this[cPasswordSettingName]; }
            set { this[cPasswordSettingName] = value; }
        }
        private const string cPasswordSettingName = "password";

        /// <summary>
        /// UserDomain for authenticating on the MasterData WebService
        /// </summary>
        [ConfigurationProperty(cUserDomainSettingDomain, DefaultValue = "", IsRequired = true)]
        public string UserDomain
        {
            get { return (string)this[cUserDomainSettingDomain]; }
            set { this[cUserDomainSettingDomain] = value; }
        }
        private const string cUserDomainSettingDomain = "userDomain";

        /// <summary>
        /// Endpoint in the configuration file for connecting to the MasterData WebService
        /// </summary>
        [ConfigurationProperty(cEndpointNameSettingName, DefaultValue = "", IsRequired = true)]
        public string EndpointName
        {
            get { return (string)this[cEndpointNameSettingName]; }
            set { this[cEndpointNameSettingName] = value; }
        }
        private const string cEndpointNameSettingName = "endpointName";
        #endregion
    }
}