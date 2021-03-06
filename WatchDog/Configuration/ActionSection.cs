﻿using System.Configuration;

namespace WatchDog.Configuration
{
    public class ActionSection : ConfigurationSection
    {
        [ConfigurationProperty("Name", DefaultValue = "Watcher", IsRequired = true, IsKey = true)]
        public string Name
        {
            get { return (string)this["Name"]; }
            set { this["Name"] = value; }
        }

        [ConfigurationProperty("Type", DefaultValue = "Watcher", IsRequired = true, IsKey = false)]
        public string Type
        {
            get { return (string)this["Type"]; }
            set { this["Type"] = value; }
        }

        [ConfigurationProperty("FileName", DefaultValue = "", IsRequired = false, IsKey = false)]
        public string FileName
        {
            get { return (string)this["FileName"]; }
            set { this["FileName"] = value; }
        }

        [ConfigurationProperty("MessageLayout", DefaultValue = "", IsRequired = false, IsKey = false)]
        public string MessageLayout
        {
            get { return (string)this["MessageLayout"]; }
            set { this["MessageLayout"] = value; }
        }

        [ConfigurationProperty("TriggerWhen", IsRequired = true, IsKey = false)]
        public Watchers.Status TriggerWhen
        {
            get { return (Watchers.Status)System.Enum.Parse(typeof(Watchers.Status), this["TriggerWhen"].ToString()); }
            set { this["TriggerWhen"] = value; }
        }

    }

}