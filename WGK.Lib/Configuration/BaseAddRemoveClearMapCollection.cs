using System.Configuration;

namespace WGK.Lib.Configuration
{
    /// <summary>
    /// Generic configuration element collection that contains custom configuration elements of type TConfigurationElement.
    /// This collection implements an Add/Remove/Clear map of ConfigurationElements
    /// </summary>
    /// <example>
    /// Usage: add the following attributes to the property of your custom ConfigurationSection class
    /// 
    ///         [ConfigurationProperty(cMySettingName, IsRequired = true, IsDefaultCollection = true)]
    ///         [ConfigurationCollection(typeof(GenericAddRemoveClearMapCollection MyConfigurationElement ),
    ///             AddItemName = "add",
    ///             ClearItemsName = "clear",
    ///             RemoveItemName = "remove")]
    ///         public MyCollectionClass MySetting
    ///         {
    ///             get { return (MyCollectionClass)this[cMySettingName]; }
    ///             set { this[cMySettingName] = value; }
    ///         }
    /// </example>
    public class BaseAddRemoveClearMapCollection<TConfigurationElement> : ConfigurationElementCollection
        where TConfigurationElement : BaseConfigurationElement, new()
    {
        #region Constructor
        public BaseAddRemoveClearMapCollection()
        {
            var vElement = (TConfigurationElement)this.CreateNewElement();
            this.Add(vElement);
        }
        #endregion

        #region Indexer properties
        public TConfigurationElement this[int pIndex]
        {
            get
            {
                return (TConfigurationElement)this.BaseGet(pIndex);
            }
            set
            {
                if (this.BaseGet(pIndex) != null)
                {
                    this.BaseRemoveAt(pIndex);
                }
                this.BaseAdd(pIndex, value);
            }
        }

        public new TConfigurationElement this[string pName]
        {
            get { return (TConfigurationElement)this.BaseGet(pName); }
        }
        #endregion

        #region Overriden properties
        public override ConfigurationElementCollectionType CollectionType
        {
            get { return ConfigurationElementCollectionType.AddRemoveClearMap; }
        }
        #endregion

        #region Public methods
        public int IndexOf(TConfigurationElement pElement)
        {
            return this.BaseIndexOf(pElement);
        }

        public void Add(TConfigurationElement pElement)
        {
            this.BaseAdd(pElement);
        }

        public void Remove(TConfigurationElement pElement)
        {
            if (this.BaseIndexOf(pElement) >= 0)
            {
                this.BaseRemove(pElement.Name);
            }
        }

        public void RemoveAt(int pIndex)
        {
            this.BaseRemoveAt(pIndex);
        }

        public void Remove(string pName)
        {
            this.BaseRemove(pName);
        }

        public void Clear()
        {
            this.BaseClear();
        }
        #endregion

        #region Overriden methods
        protected override ConfigurationElement CreateNewElement()
        {
            return new TConfigurationElement();
        }

        protected override object GetElementKey(ConfigurationElement pElement)
        {
            return ((TConfigurationElement)pElement).Name;
        }

        protected override void BaseAdd(ConfigurationElement pElement)
        {
            this.BaseAdd(pElement, false);
        }
        #endregion
    }
}