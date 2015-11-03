using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogService.Model
{
    public class WindowsServiceWatcher : StatusWatcher
    {
        private readonly string _serviceToMonitor;

        public WindowsServiceWatcher(string serviceNameToMonitor, string name = "WindowsServiceWatcher")
        {
            Identifier = Guid.NewGuid();
            Name = name;
            RefreshPeriod = TimeSpan.FromSeconds(30);
            CreationDate = DateTime.Now;

            _serviceToMonitor = serviceNameToMonitor;
        }

        public override string GetWatcherInformation()
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
