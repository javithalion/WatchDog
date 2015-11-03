using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogService.Watchers;

namespace WatchDogService.ContingenceActions
{
    public class ConsoleAction : ContingenceAction
    {
        public ConsoleAction(StatusWatcher statusWatcher, string name = "ConsoleAction")
            :base(statusWatcher,name)
        {
        }

        public override void Execute()
        {
            Console.WriteLine(
                        string.Format("{0}: {1} is not working",
                        DateTime.Now.ToString(),
                        _statusWatcher.GetWathcDescription()));
        }
    }
}
