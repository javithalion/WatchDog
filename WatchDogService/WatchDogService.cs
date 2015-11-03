using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using WatchDogService.Services.Implementations;
using WatchDogService.Services.Interfaces;

namespace WatchDogService
{
    public partial class WatchDogService : ServiceBase
    {
        public WatchDogService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            Start();
        }

        protected override void OnStop()
        {            
        }

        public void Start()
        {
            IConfigurationService configurationService = new ConfigurationService();
            var watchers = configurationService.GetAllWatchers();

            foreach (var watcher in watchers)
            {
                watcher.Start();
            }
        }
    }
}
