using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Watchers;
using WatchDog.Services.Interfaces;
using WatchDog.ContingenceActions;
using WatchDog.Configuration;
using System.Reflection;
using System.Globalization;

namespace WatchDog.Services.Implementations
{
    public class ConfigurationService : IConfigurationService
    {
        public IEnumerable<StatusWatcher> GetAllWatchers()
        {
            try
            {
                var result = new List<StatusWatcher>();
                var config = (WatchDogConfigurationSection)System.Configuration.ConfigurationManager.GetSection("watchDogConfigurationSection");

                for (int i = 0; i < config.Watchers.Count; i++)
                {
                    var configWatcher = config.Watchers[i];
                    var watcher = BuildWatcherFromConfiguration(configWatcher);

                    var actionsForWatcher = BuildActionsForWatcher(watcher, configWatcher);
                    watcher.AddContingenceActions(actionsForWatcher);

                    result.Add(watcher);
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new Exception("There was a problem configuring WatchDogService. See inner exception for futher details.", ex);
            }
        }

        private StatusWatcher BuildWatcherFromConfiguration(WatcherSection configWatcher)
        {
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
            return Activator.CreateInstance(type, listOfParameters.ToArray()) as StatusWatcher;
        }

        private IList<ContingenceAction> BuildActionsForWatcher(StatusWatcher watcher, WatcherSection configWatcher)
        {
            var result = new List<ContingenceAction>();
            IList<object> listOfParameters;
            IList<PropertyInfo> properties;

            for (int j = 0; j < configWatcher.ContingenceActions.Count; j++)
            {
                var contingenceActionFromConfig = configWatcher.ContingenceActions[j];

                var type = Type.GetType(contingenceActionFromConfig.Type);
                var ctor = type.GetConstructors().FirstOrDefault();

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
                result.Add(action);
            }
            return result;
        }
    }
}
