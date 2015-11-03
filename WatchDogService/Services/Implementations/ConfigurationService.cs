using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogService.Watchers;
using WatchDogService.Services.Interfaces;
using WatchDogService.ContingenceActions;

namespace WatchDogService.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        public IEnumerable<StatusWatcher> GetAllWatchers()
        {
           

            var wsw1 = new WindowsServiceWatcher("Temas", 30);

            var logAction = new LogFileAction(wsw1, @"C:\Users\jcerve14\Desktop\log1.txt");

            wsw1.AddContingenceAction(logAction);

            var wsw2 = new WindowsServiceWatcher("asdasd", 10);

            var logAction2 = new LogFileAction(wsw2, @"C:\Users\jcerve14\Desktop\log2.txt");
            var consoleAction2 = new ConsoleAction(wsw2);

            wsw2.AddContingenceAction(logAction2);
            wsw2.AddContingenceAction(consoleAction2);

            var wsw3 = new WindowsServiceWatcher("jejejeje", 15);

            var logAction3 = new LogFileAction(wsw3, @"C:\Users\jcerve14\Desktop\log3.txt");
            var consoleAction3 = new ConsoleAction(wsw3);

            wsw3.AddContingenceAction(logAction3);
            wsw3.AddContingenceAction(consoleAction3);



            return new List<StatusWatcher>() { wsw1, wsw2, wsw3 };
        }


    }
}
