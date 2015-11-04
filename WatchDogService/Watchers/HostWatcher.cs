using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogService.Watchers
{
    public class HostWatcher : StatusWatcher
    {
        private const short TimeoutInSeconds = 5;
        private readonly string _hostAddress;

        public HostWatcher(string hostAddress, int refreshTimeInSeconds, string name)
            : base(refreshTimeInSeconds, name)
        {
            _hostAddress = hostAddress;
        }

        public override string GetWathcDescription()
        {
            return string.Format("'{0}' host", _hostAddress);
        }

        protected override Status CheckStatus(object state)
        {
            try
            {
                var ping = new Ping();
                var reply = ping.Send(_hostAddress, TimeoutInSeconds * 1000);
                return reply.Status == IPStatus.Success ? Status.Working : Status.NotWorking;
            }
            catch
            {
                return Status.NotWorking;
            }
        }
    }
}
