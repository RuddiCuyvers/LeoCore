using System;
using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Configuration element collection that contains custom configuration elements of type TConfigurationElement.
    /// This collection implements a basic map of ConfigurationElements.
    /// Derived classes must implement the ElementName property to return the name of the elements used in the config file.
    /// </summary>
    /// <example>
    /// Usage: add the following attributes to the property of your custom ConfigurationSection class
    ///  
    ///         [ConfigurationProperty(cMySettingName, IsRequired = true, IsDefaultCollection = true)]
    ///         [ConfigurationCollection(typeof(MyCollectionClass))]
    ///         public MyCollectionClass MySetting
    ///         {
    ///             get { return (MyCollectionClass)this[cMySettingName]; }
    ///             set { this[cMySettingName] = value; }
    ///         }
    /// </example>
    public abstract class BaseBasicMapCollection<TConfigurationElement> : ConfigurationElementCollection
        where TConfigurationElement : BaseConfigurationElement, new()
    {
        #region Overriden properties
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.BasicMap; }
        }

        protected override string ElementName
        {
            // Chain to abstract property in order to force a derived class implementation
            get { return this.ConfigurationName; }
        }

        /// <summary>
        /// Defines the name of the elements in the config file
        /// </summary>
        protected abstract string ConfigurationName { get; }
        #endregion

        #region Overriden methods
        protected override bool IsElementName(string pElementName)
        {
            return pElementName.Equals(this.ElementName, StringComparison.InvariantCultureIgnoreCase);
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new TConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement pElement)
        {
            return ((TConfigurationElement)pElement).Name;
        }
        #endregion

        #region Indexers
        public TConfigurationElement this[int pIndex]
        {
            get
            {
                return (TConfigurationElement)this.BaseGet(pIndex);
            }
            //set
            //{
            //    if (this.BaseGet(pIndex) != null)
            //    {
            //        this.BaseRemoveAt(pIndex);
            //    }
            //    this.BaseAdd(pIndex, value);
            //}
        }

        public new TConfigurationElement this[string pName]
        {
            get { return (TConfigurationElement)this.BaseGet(pName); }
        }
        #endregion

        #region Public methods
        public int IndexOf(TConfigurationElement pElement)
        {
            return this.BaseIndexOf(pElement);
        }
        #endregion
    }
}