using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration section for storing Personeel WebService settings
    /// </summary>
    public class PersoneelServiceConfigurationSection : BaseConfigurationSection<PersoneelServiceConfigurationSection>
    {
        #region Configuration Settings Properties
        // DELETED - Personeel Web Service requires no authentication

        ///// <summary>
        ///// UserName for authenticating on the Personeel WebService
        ///// </summary>
        //[ConfigurationProperty(cUserNameSettingName, DefaultValue = "", IsRequired = true)]
        //public string UserName
        //{
        //    get { return (string)this[cUserNameSettingName]; }
        //    set { this[cUserNameSettingName] = value; }
        //}
        //private const string cUserNameSettingName = "userName";

        ///// <summary>
        ///// Password for authenticating on the Personeel WebService
        ///// </summary>
        //[ConfigurationProperty(cPasswordSettingName, DefaultValue = "", IsRequired = true)]
        //public string Password
        //{
        //    get { return (string)this[cPasswordSettingName]; }
        //    set { this[cPasswordSettingName] = value; }
        //}
        //private const string cPasswordSettingName = "password";

        /// <summary>
        /// Endpoint in the configuration file for connecting to the Personeel WebService
        /// </summary>
        [ConfigurationProperty(cEndpointNameSettingName, DefaultValue = "", IsRequired = true)]
        public string EndpointName
        {
            get { return (string)this[cEndpointNameSettingName]; }
            set { this[cEndpointNameSettingName] = value; }
        }
        private const string cEndpointNameSettingName = "endpointName";

        /// <summary>
        /// Maximum number of months 'UitDienst' to be considered as 'InDienstEnActief' (e.g., for filling an autocomplete list)
        /// </summary>
        [ConfigurationProperty(cInactiveMaxMonthsSettingName, DefaultValue = 13, IsRequired = false)]
        public int InactiveMaxMonths
        {
            get { return (int)this[cInactiveMaxMonthsSettingName]; }
            set { this[cInactiveMaxMonthsSettingName] = value; }
        }
        private const string cInactiveMaxMonthsSettingName = "inactiveMaxMonths";
        #endregion
    }
}