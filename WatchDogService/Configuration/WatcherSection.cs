﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogService.Configuration
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

        [System.Configuration.ConfigurationProperty("ContingenceActionsSection", IsDefaultCollection = false)]
        [ConfigurationCollection(typeof(ContingenceActionsSection),
        AddItemName = "add",
        ClearItemsName = "clear",
        RemoveItemName = "remove")]
        public ContingenceActionsSection ContingenceActions
        {
            get
            {
                object o = this["ContingenceActionsSection"];
                return o as ContingenceActionsSection;
            }
        }
    }


    public class WindowsServiceWatcherSection : WatcherSection
    {
        [ConfigurationProperty("ServiceName", DefaultValue = "Watcher", IsRequired = true, IsKey = false)]
        public string ServiceName
        {
            get { return (string)this["ServiceName"]; }
            set { this["ServiceName"] = value; }
        }       
    }
}
