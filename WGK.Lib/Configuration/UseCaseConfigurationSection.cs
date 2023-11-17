using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration section for storing general UseCase settings
    /// </summary>
    public class UseCaseConfigurationSection : BaseConfigurationSection<UseCaseConfigurationSection>
    {
        #region Configuration Settings Properties
        /// <summary>
        /// Limit the result of a SearchUseCase to a maximum number of rows
        /// </summary>
        [ConfigurationProperty(cSearchMaxRowsSettingName, DefaultValue = "1000", IsRequired = true)]
        [IntegerValidator()]
        public int SearchMaxRows
        {
            get { return (int)this[cSearchMaxRowsSettingName]; }
            set { this[cSearchMaxRowsSettingName] = value; }
        }
        private const string cSearchMaxRowsSettingName = "searchMaxRows";
        #endregion
    }
}