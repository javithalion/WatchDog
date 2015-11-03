using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogService.Model;

namespace WatchDogService.Services.Interfaces
{
    public interface IConfigurationService
    {
        IEnumerable<StatusWatcher> GetAllWatchers();
    }
}
