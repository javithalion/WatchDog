using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WatchDogService.Model
{
    public abstract class StatusWatcher
    {
        public Guid Identifier { get; protected set; }
        public string Name { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime LastCheck { get; protected set; }
        public TimeSpan RefreshPeriod { get; protected set; }



    }
}
