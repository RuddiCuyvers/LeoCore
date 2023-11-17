namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Configuration element collection that contains custom DispatcherTask configuration elements
    /// </summary>
    public class DispatcherTasksConfigurationCollection :
        BaseBasicMapCollection<DispatcherTaskConfigurationElement>
    {
        /// <summary>
        /// Defines the name of the elements in the config file
        /// </summary>
        protected override string ConfigurationName
        {
            get { return "task"; }
        }
    }
}