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

        [System.Configuration.ConfigurationProperty("Watchers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(WatchersSection),
        AddItemName = "add",
        ClearItemsName = "clear",
        RemoveItemName = "remove")]
        public WatchersSection Watchers
        {
            get
            {
                object o = this["Watchers"];
                return o as WatchersSection;
            }
        }
    }
}