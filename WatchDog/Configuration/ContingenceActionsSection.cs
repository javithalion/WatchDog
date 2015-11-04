using System.Configuration;

namespace WatchDog.Configuration
{
    public class ContingenceActionsSection : ConfigurationElementCollection
    {
        public ContingenceActionSection this[int index]
        {
            get
            {
                return base.BaseGet(index) as ContingenceActionSection;
            }
            set
            {
                if (base.BaseGet(index) != null)
                {
                    base.BaseRemoveAt(index);
                }
                this.BaseAdd(index, value);
            }
        }

        public new ContingenceActionSection this[string responseString]
        {
            get { return (ContingenceActionSection)BaseGet(responseString); }
            set
            {
                if (BaseGet(responseString) != null)
                {
                    BaseRemoveAt(BaseIndexOf(BaseGet(responseString)));
                }
                BaseAdd(value);
            }
        }

        protected override ConfigurationElement CreateNewElement()
        {
            return new ContingenceActionSection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ContingenceActionSection)element).Name;
        }

        public void Remove(ContingenceActionSection ovenConfig)
        {
            BaseRemove(ovenConfig.Name);
        }

        public void RemoveAt(int index)
        {
            BaseRemoveAt(index);
        }

        public void Remove(string name)
        {
            BaseRemove(name);
        }
    }
}
