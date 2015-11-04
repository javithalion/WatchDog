using System.ServiceProcess;
using WatchDog.Services.Implementations;
using WatchDog.Services.Interfaces;

namespace WatchDog
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
