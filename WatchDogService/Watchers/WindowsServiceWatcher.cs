using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

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
