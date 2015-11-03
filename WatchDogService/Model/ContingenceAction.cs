using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogService.Model
{
    public abstract class ContingenceAction
    {
        public abstract void Execute(StatusWatcher watcher);
    }
}
