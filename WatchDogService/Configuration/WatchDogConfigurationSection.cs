using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogService.Configuration
{
    public class WatchDogConfigurationSection : ConfigurationSection
    {          

        [System.Configuration.ConfigurationProperty("WindowsServiceWatchersSection", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(WindowsServiceWatchersSection),
        AddItemName = "add",
        ClearItemsName = "clear",
        RemoveItemName = "remove")]
        public WindowsServiceWatchersSection WindowsServiceWatchers
        {
            get
            {
                object o = this["WindowsServiceWatchersSection"];
                return o as WindowsServiceWatchersSection;
            }
        }
    }
}