using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration section for storing general Sjabloon settings
    /// </summary>
    public class SjabloonConfigurationSection : BaseConfigurationSection<SjabloonConfigurationSection>
    {
        #region Configuration Settings Properties
        /// <summary>
        /// Defines the folder where the templates will be uploaded to
        /// </summary>
        [ConfigurationProperty(cSjabloonFolderSettingName, IsRequired = true)]
        [StringValidator()]
        public string SjabloonFolder
        {
            get { return (string)this[cSjabloonFolderSettingName]; }
            set { this[cSjabloonFolderSettingName] = value; }
        }
        private const string cSjabloonFolderSettingName = "sjabloonFolder";

        /// <summary>
        /// Defines the font
        /// </summary>
        [ConfigurationProperty(cFontSettingName, IsRequired = true)]
        [StringValidator()]
        public string Font
        {
            get { return (string)this[cFontSettingName]; }
            set { this[cFontSettingName] = value; }
        }
        private const string cFontSettingName = "font";

        /// <summary>
        /// Defines the fontsize
        /// </summary>
        [ConfigurationProperty(cFontSizeSettingName, IsRequired = true)]
        [StringValidator()]
        public string FontSize
        {
            get { return (string)this[cFontSizeSettingName]; }
            set { this[cFontSizeSettingName] = value; }
        }
        private const string cFontSizeSettingName = "fontSize";
        #endregion
    }
}