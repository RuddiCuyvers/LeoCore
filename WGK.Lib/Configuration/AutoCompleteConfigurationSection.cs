using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration section for storing general UseCase settings
    /// </summary>
    public class AutoCompleteConfigurationSection : BaseConfigurationSection<AutoCompleteConfigurationSection>
    {
        #region Configuration Settings Properties
        /// <summary>
        /// Limits the number of results returned by an autocomplete search to a maximum number
        /// </summary>
        [ConfigurationProperty(cMaxRowsSettingName, DefaultValue = "10", IsRequired = true)]
        [IntegerValidator()]
        public int MaxRows
        {
            get { return (int)this[cMaxRowsSettingName]; }
            set { this[cMaxRowsSettingName] = value; }
        }
        private const string cMaxRowsSettingName = "maxRows";

        /// <summary>
        /// Minimum required length for the input string in order to trigger an autocomplete search
        /// </summary>
        [ConfigurationProperty(cMinLengthSettingName, DefaultValue = "2", IsRequired = true)]
        [IntegerValidator()]
        public int MinLength
        {
            get { return (int)this[cMinLengthSettingName]; }
            set { this[cMinLengthSettingName] = value; }
        }
        private const string cMinLengthSettingName = "minLength";
        #endregion
    }
}