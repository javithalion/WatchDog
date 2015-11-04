using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogService.Watchers;
using WatchDogService.Services.Interfaces;
using WatchDogService.ContingenceActions;
using WatchDogService.Configuration;
using System.Reflection;
using System.Globalization;

namespace WatchDogService.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        public IEnumerable<StatusWatcher> GetAllWatchers()
        {

            var result = new List<StatusWatcher>();
            var config = (WatchDogConfigurationSection)System.Configuration.ConfigurationManager.GetSection("watchDogConfigurationSection");

            for (int i = 0; i < config.Watchers.Count; i++)
            {
                var configWatcher = config.Watchers[i];

                var type = Type.GetType(configWatcher.Type);
                ConstructorInfo ctor = type.GetConstructors().FirstOrDefault();

                var listOfParameters = new List<object>();
                var properties = configWatcher.GetType().GetProperties();
                foreach (var parameter in ctor.GetParameters())
                {
                    listOfParameters.Add(
                        Convert.ChangeType(
                        properties.FirstOrDefault(x => x.Name.ToLower() == parameter.Name.ToLower()).GetValue(configWatcher),
                        parameter.ParameterType,
                        CultureInfo.InvariantCulture)
                        );
                }

                StatusWatcher watcher = Activator.CreateInstance(type, listOfParameters.ToArray()) as StatusWatcher;


                for (int j = 0; j < configWatcher.ContingenceActions.Count; j++)
                {
                    var contingenceActionFromConfig = configWatcher.ContingenceActions[j];

                    type = Type.GetType(contingenceActionFromConfig.Type);
                    ctor = type.GetConstructors().FirstOrDefault();

                    listOfParameters = new List<object>();
                    listOfParameters.Add(watcher);

                    properties = contingenceActionFromConfig.GetType().GetProperties();
                    foreach (var parameter in ctor.GetParameters())
                    {
                        if (parameter.Name.ToLower() != "statuswatcher")
                            listOfParameters.Add(
                                Convert.ChangeType(
                                properties.FirstOrDefault(x => x.Name.ToLower() == parameter.Name.ToLower()).GetValue(contingenceActionFromConfig),
                                parameter.ParameterType,
                                CultureInfo.InvariantCulture)
                                );
                    }

                    ContingenceAction action = Activator.CreateInstance(type, listOfParameters.ToArray()) as ContingenceAction;
                    watcher.AddContingenceAction(action);
                }

                result.Add(watcher);
            }
            return result;
        }


    }
}
