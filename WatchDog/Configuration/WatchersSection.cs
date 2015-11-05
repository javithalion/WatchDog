using System.Configuration;

namespace WatchDog.Configuration
{
    public class WatchersSection : ConfigurationElementCollection
    {
        public WatcherSection this[int index]
        {
            get
            {
                return base.BaseGet(index) as WatcherSection;
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

        public new WatcherSection this[string responseString]
        {
            get { return (WatcherSection)BaseGet(responseString); }
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
            return new WatcherSection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WatcherSection)element).Name;
        }
      
    }
}
