using System;
using System.ServiceProcess;

namespace WatchDog.Watchers
{
    public class WindowsServiceWatcher : StatusWatcher
    {
        private readonly string _serviceToMonitor;

        public WindowsServiceWatcher(string serviceNameToMonitor, int refreshTimeInSeconds, string name = "WindowsServiceWatcher")
            : base(refreshTimeInSeconds, name)
        {
            _serviceToMonitor = serviceNameToMonitor;
        }        

        protected override Status CheckStatus(object state)
        {
            try
            {
                var serviceController = new ServiceController(_serviceToMonitor);
                return serviceController.Status == ServiceControllerStatus.Running ?
                    Status.Working :
                    Status.NotWorking;
            }
            catch
            {
                return Status.NotWorking;
            }            
        }
    }
}
