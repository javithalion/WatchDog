using System.Configuration;

namespace WatchDog.Configuration
{
    public class WatcherSection : ConfigurationSection
    {
        [ConfigurationProperty("Name", DefaultValue = "Watcher", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("RefreshTimeInSeconds", DefaultValue = "30", IsRequired = true, IsKey = false)]
        public string RefreshTimeInSeconds
        {
            get { return (string)this["RefreshTimeInSeconds"]; }
            set { this["RefreshTimeInSeconds"] = value; }
        }

        [ConfigurationProperty("Type", DefaultValue = "Watcher", IsRequired = true, IsKey = false)]
        public string Type
        {
            get { return (string)this["Type"]; }
            set { this["Type"] = value; }
        }

        [System.Configuration.ConfigurationProperty("Actions", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ActionsSection),
        AddItemName = "Action")]
        public ActionsSection ContingenceActions
        {
            get
            {
                object o = this["Actions"];
                return o as ActionsSection;
            }
        }

        [ConfigurationProperty("ServiceNameToMonitor", DefaultValue = "Watcher", IsRequired = false, IsKey = false)]
        public string ServiceNameToMonitor
        {
            get { return (string)this["ServiceNameToMonitor"]; }
            set { this["ServiceNameToMonitor"] = value; }
        }

        [ConfigurationProperty("HostAddress", DefaultValue = "Watcher", IsRequired = false, IsKey = false)]
        public string HostAddress
        {
            get { return (string)this["HostAddress"]; }
            set { this["HostAddress"] = value; }
        }
    }


    
}
