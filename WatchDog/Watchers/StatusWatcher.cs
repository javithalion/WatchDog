using System;
using System.Collections.Generic;
using System.Threading;
using WatchDog.ContingenceActions;

namespace WatchDog.Watchers
{
    public abstract class StatusWatcher : IDisposable
    {
        public Guid Identifier { get; private set; }
        public string Name { get; private set; }
        public DateTime CreationDate { get; private set; }
        public DateTime LastCheck { get; private set; }
        public TimeSpan RefreshPeriod { get; private set; }
        public Status Status { get; private set; }

        private Timer _monitorTimer;
        private ICollection<ContingenceAction> _contingenceActions = new List<ContingenceAction>();

        public StatusWatcher(int refreshPeriodInSeconds, string name = "Watcher")
        {
            Identifier = Guid.NewGuid();
            Name = name;
            RefreshPeriod = TimeSpan.FromSeconds(refreshPeriodInSeconds);
            CreationDate = DateTime.Now;
        }

        protected abstract Status CheckStatus(object state);

        public abstract string GetWathcDescription();

        public void Start()
        {
            _monitorTimer = new Timer(
                (e) =>
                {
                    Status = CheckStatus(e);
                    LastCheck = DateTime.Now;

                    if (Status != Status.Working)
                        foreach (var action in _contingenceActions)
                            action.Execute();
                });

            _monitorTimer.Change(1000, (int)RefreshPeriod.TotalMilliseconds);
        }

        public void AddContingenceAction(ContingenceAction action)
        {
            _contingenceActions.Add(action);
        }

        public void AddContingenceActions(IList<ContingenceAction> actionsForWatcher)
        {
            foreach (var newAction in actionsForWatcher)
            {
                AddContingenceAction(newAction);
            }
        }     

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _monitorTimer.Dispose();
                _monitorTimer = null;                
            }           
        }
    }

    public enum Status
    {
        Working,
        NotWorking,
        Unknown
    }
}
