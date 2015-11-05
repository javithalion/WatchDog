using System.Configuration;

namespace WatchDog.Configuration
{
    public class ActionsSection : ConfigurationElementCollection
    {
        public ActionSection this[int index]
        {
            get
            {
                return base.BaseGet(index) as ActionSection;
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

        public new ActionSection this[string responseString]
        {
            get { return (ActionSection)BaseGet(responseString); }
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
            return new ActionSection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((ActionSection)element).Name;
        }
        
    }
}
