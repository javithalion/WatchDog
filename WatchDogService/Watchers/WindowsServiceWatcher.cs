using System;
using System.ServiceProcess;

namespace WatchDogService.Watchers
{
    public class WindowsServiceWatcher : StatusWatcher
    {
        private readonly string _serviceToMonitor;

        public WindowsServiceWatcher(string serviceNameToMonitor, int refreshPeriodInSeconds, string name = "WindowsServiceWatcher")
            : base(refreshPeriodInSeconds, name)
        {
            _serviceToMonitor = serviceNameToMonitor;
        }

        public override string GetWathcDescription()
        {
            return string.Format("{0} Windows Service", _serviceToMonitor);
        }

        protected override void CheckStatus(object state)
        {
            try
            {
                var serviceController = new ServiceController(_serviceToMonitor);
                Status = serviceController.Status == ServiceControllerStatus.Running ?
                    Status.Working :
                    Status.NotWorking;
            }
            catch
            {
                Status = Status.NotWorking;
            }
            finally
            {
                LastCheck = DateTime.Now;
            }
        }
    }
}
