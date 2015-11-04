using System.Collections.Generic;
using WatchDog.Watchers;

namespace WatchDog.Services.Interfaces
{
    public interface IConfigurationService
    {
        IEnumerable<StatusWatcher> GetAllWatchers();
    }
}
