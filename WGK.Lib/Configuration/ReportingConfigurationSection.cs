using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration section for storing SQL Server Reporting settings
    /// </summary>
    public class ReportingConfigurationSection : BaseConfigurationSection<ReportingConfigurationSection>
    {
        #region Configuration Settings Properties
        /// <summary>
        /// URL to the SQL Server ReportManager
        /// </summary>
        [ConfigurationProperty(cReportManagerUrlSettingName, DefaultValue = "", IsRequired = true)]
        public string ReportManagerUrl
        {
            get { return (string)this[cReportManagerUrlSettingName]; }
            set { this[cReportManagerUrlSettingName] = value; }
        }
        private const string cReportManagerUrlSettingName = "reportManagerUrl";

        /// <summary>
        /// ItemPath for the reports
        /// </summary>
        [ConfigurationProperty(cReportItemPathSettingName, DefaultValue = "", IsRequired = true)]
        public string ReportItemPath
        {
            get { return (string)this[cReportItemPathSettingName]; }
            set { this[cReportItemPathSettingName] = value; }
        }
        private const string cReportItemPathSettingName = "reportItemPath";

        /// <summary>
        /// URL format string for the ReportManager to display a report page
        /// </summary>
        [ConfigurationProperty(cReportManagerFormatSettingName, DefaultValue = "", IsRequired = true)]
        public string ReportManagerFormat
        {
            get { return (string)this[cReportManagerFormatSettingName]; }
            set { this[cReportManagerFormatSettingName] = value; }
        }
        private const string cReportManagerFormatSettingName = "reportManagerFormat";

        /// <summary>
        /// URL format string for the ReportServer to render a report
        /// </summary>
        [ConfigurationProperty(cReportRenderFormatSettingName, DefaultValue = "", IsRequired = true)]
        public string ReportRenderFormat
        {
            get { return (string)this[cReportRenderFormatSettingName]; }
            set { this[cReportRenderFormatSettingName] = value; }
        }
        private const string cReportRenderFormatSettingName = "reportRenderFormat";

        /// <summary>
        /// CSV string of all the report items
        /// </summary>
        [ConfigurationProperty(cReportItemsCsvSettingName, DefaultValue = "", IsRequired = true)]
        public string ReportItemsCsv
        {
            get { return (string)this[cReportItemsCsvSettingName]; }
            set { this[cReportItemsCsvSettingName] = value; }
        }
        private const string cReportItemsCsvSettingName = "reportItemsCsv";
        #endregion
    }
}