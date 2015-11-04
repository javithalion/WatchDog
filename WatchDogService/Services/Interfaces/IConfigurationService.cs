using System.Collections.Generic;
using WatchDogService.Watchers;

namespace WatchDogService.Services.Interfaces
{
    public interface IConfigurationService
    {
        IEnumerable<StatusWatcher> GetAllWatchers();
    }
}
