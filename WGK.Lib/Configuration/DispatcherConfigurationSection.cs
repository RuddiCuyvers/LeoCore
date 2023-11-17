using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Custom configuration section for storing a collection of DispatcherServices settings
    /// </summary>
    public class DispatcherConfigurationSection :
        BaseConfigurationSection<DispatcherConfigurationSection>
    {
        #region Configuration Settings Properties - Tasks
        /// <summary>
        /// Collection of TaskConfigurationElements
        /// </summary>
        [ConfigurationProperty(cDispatcherTasksSettingName, IsRequired = true, IsDefaultCollection = true)]
        [ConfigurationCollection(typeof(DispatcherTasksConfigurationCollection))]
        public DispatcherTasksConfigurationCollection Tasks
        {
            get { return (DispatcherTasksConfigurationCollection)this[cDispatcherTasksSettingName]; }
            set { this[cDispatcherTasksSettingName] = value; }
        }
        private const string cDispatcherTasksSettingName = "tasks";
        #endregion

        #region Public Methods
        /// <summary>
        /// Helper method to retrieve the DispatcherTaskConfigurationElement settings from the config file for the
        /// specified method name.
        /// An exception is thrown if the configuration section's element does not exist in the application's config file.
        /// </summary>
        public static DispatcherTaskConfigurationElement GetByTaskName(string pTaskName)
        {
            if (string.IsNullOrEmpty(pTaskName))
            {
                throw new WGK.Lib.Exceptions.ParameterMissingException("TaskName");
            }

            // Remark: Current throws an exception if the section does not exist in the application's config file.
            var vConfigurationSection = DispatcherConfigurationSection.Current;

            if (vConfigurationSection.Tasks == null)
            {
                throw new WGK.Lib.Exceptions.ConfigurationMissingException("tasks", cDispatcherTasksSettingName);
            }

            // Use name indexer on the Tasks collection to get the actual Task settings
            var vConfigurationElement = vConfigurationSection.Tasks[pTaskName];
            if (vConfigurationElement == null)
            {
                throw new WGK.Lib.Exceptions.ConfigurationMissingException(cDispatcherTasksSettingName, pTaskName);
            }

            return vConfigurationElement;
        }
        #endregion
    }
}