using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogService.Model
{
    public class ConsoleAction : ContingenceAction
    {
        public override void Execute(StatusWatcher watcher)
        {
            Console.WriteLine("{0}: {1} is not working", DateTime.Now.ToString(), watcher.GetWatcherInformation());
        }
    }
}
