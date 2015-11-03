using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogService.Model;
using WatchDogService.Services.Interfaces;

namespace WatchDogService.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        public IEnumerable<StatusWatcher> GetAllWatchers()
        {
            var ca1 = new LogAction();
            var ca2 = new ConsoleAction();

            var wsw1 = new WindowsServiceWatcher("Temas");
            wsw1.AddContingenceAction(ca1);
            wsw1.AddContingenceAction(ca2);

            var wsw2 = new WindowsServiceWatcher("asdasd");
            wsw2.AddContingenceAction(ca1);
            wsw2.AddContingenceAction(ca2);

            var wsw3 = new WindowsServiceWatcher("jejejeje");
            wsw3.AddContingenceAction(ca2);


            return new List<StatusWatcher>() { wsw1, wsw2, wsw3 };
        }
    }
}
