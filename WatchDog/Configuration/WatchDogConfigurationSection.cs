using System.Configuration;

namespace WatchDog.Configuration
{
    public class WatchDogConfigurationSection : ConfigurationSection
    {          

        [System.Configuration.ConfigurationProperty("Watchers", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(WatchersSection),
        AddItemName = "Watcher",
        ClearItemsName = "clearWatcher",
        RemoveItemName = "removeWatcher")]
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