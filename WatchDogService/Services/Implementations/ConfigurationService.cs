using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogService.Watchers;
using WatchDogService.Services.Interfaces;
using WatchDogService.ContingenceActions;
using WatchDogService.Configuration;

namespace WatchDogService.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        public IEnumerable<StatusWatcher> GetAllWatchers()
        {

            var result = new List<StatusWatcher>();
            var config = (WatchDogConfigurationSection)System.Configuration.ConfigurationManager.GetSection("watchDogConfigurationSection");

            WindowsServiceWatcher serviceWatcherAux;
            ContingenceAction contingenceActionAux;
            for (int i = 0; i < config.WindowsServiceWatchers.Count; i++)
            {
                serviceWatcherAux =
                    new WindowsServiceWatcher(
                    config.WindowsServiceWatchers[i].ServiceName,
                    Convert.ToInt32(config.WindowsServiceWatchers[i].RefreshTimeInSeconds),
                    config.WindowsServiceWatchers[i].Name);

                for (int j = 0; j < config.WindowsServiceWatchers[i].ContingenceActions.Count; j++)
                {
                    var contingenceActionFromConfig = config.WindowsServiceWatchers[i].ContingenceActions[j];

                    switch (contingenceActionFromConfig.Type)
                    {
                        case "LogFileAction":
                            contingenceActionAux = CreateLogAction(serviceWatcherAux, contingenceActionFromConfig);
                            break;
                        case "ConsoleAction":
                            contingenceActionAux = CreateConsoleAction(serviceWatcherAux, contingenceActionFromConfig);
                            break;
                        default:
                            throw new Exception();
                    }

                    serviceWatcherAux.AddContingenceAction(contingenceActionAux);
                }
                result.Add(serviceWatcherAux);
            }

            return result;
        }

        private static ContingenceAction CreateConsoleAction(WindowsServiceWatcher serviceWatcherAux, ContingenceActionSection contingenceActionFromConfig)
        {
            return new ConsoleAction(serviceWatcherAux, contingenceActionFromConfig.MessageLayout, contingenceActionFromConfig.Name);
        }

        private static ContingenceAction CreateLogAction(WindowsServiceWatcher serviceWatcherAux, ContingenceActionSection contingenceActionFromConfig)
        {
            return new LogFileAction(serviceWatcherAux, contingenceActionFromConfig.FileName, contingenceActionFromConfig.MessageLayout, contingenceActionFromConfig.Name);
        }
    }
}
