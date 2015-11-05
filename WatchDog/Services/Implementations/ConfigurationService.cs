using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDog.Watchers;
using WatchDog.Services.Interfaces;
using WatchDog.Actions;
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
                    watcher.AddActions(actionsForWatcher);

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
            ConstructorInfo constructorInformation = type.GetConstructors().FirstOrDefault();

            var listOfParameters = new List<object>();
            var properties = configWatcher.GetType().GetProperties();
            foreach (var parameter in constructorInformation.GetParameters())
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

        private IList<Actions.Action> BuildActionsForWatcher(StatusWatcher watcher, WatcherSection configWatcher)
        {
            var result = new List<Actions.Action>();
            IList<object> listOfParameters;
            IList<PropertyInfo> properties;

            for (int j = 0; j < configWatcher.ContingenceActions.Count; j++)
            {
                var contingenceActionFromConfig = configWatcher.ContingenceActions[j];

                var type = Type.GetType(contingenceActionFromConfig.Type);
                var constructorInformation = type.GetConstructors().FirstOrDefault();

                listOfParameters = new List<object>();
                listOfParameters.Add(watcher);

                properties = contingenceActionFromConfig.GetType().GetProperties();
                foreach (var parameter in constructorInformation.GetParameters())
                {
                    if (parameter.Name.ToLower() != "statuswatcher")
                        listOfParameters.Add(
                            Convert.ChangeType(
                            properties.FirstOrDefault(x => x.Name.ToLower() == parameter.Name.ToLower()).GetValue(contingenceActionFromConfig),
                            parameter.ParameterType,
                            CultureInfo.InvariantCulture)
                            );
                }

                Actions.Action action = Activator.CreateInstance(type, listOfParameters.ToArray()) as Actions.Action;
                result.Add(action);
            }
            return result;
        }
    }
}
