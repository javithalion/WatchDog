using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogService.Configuration
{
    public class WindowsServiceWatchersSection : ConfigurationElementCollection
    {
        public WindowsServiceWatcherSection this[int index]
        {
            get
            {
                return base.BaseGet(index) as WindowsServiceWatcherSection;
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

        public new WindowsServiceWatcherSection this[string responseString]
        {
            get { return (WindowsServiceWatcherSection)BaseGet(responseString); }
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
            return new WindowsServiceWatcherSection();
        }

        protected override object GetElementKey(ConfigurationElement element)
        {
            return ((WindowsServiceWatcherSection)element).Name;
        }

        public void Remove(WindowsServiceWatcherSection ovenConfig)
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
