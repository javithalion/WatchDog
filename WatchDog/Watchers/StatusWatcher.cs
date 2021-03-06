﻿using System;
using System.Collections.Generic;
using System.Threading;
using WatchDog.Actions;
using System.Linq;

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
        private ICollection<Actions.Action> _actions = new List<Actions.Action>();

        public StatusWatcher(int refreshPeriodInSeconds, string name = "Watcher")
        {
            Identifier = Guid.NewGuid();
            Name = name;
            RefreshPeriod = TimeSpan.FromSeconds(refreshPeriodInSeconds);
            CreationDate = DateTime.Now;
        }

        protected abstract Status CheckStatus(object state);

        public void Start()
        {
            _monitorTimer = new Timer(
                (e) =>
                {
                    Status = CheckStatus(e);
                    LastCheck = DateTime.Now;

                    foreach (var action in _actions.Where(x => x.TriggerWhen == Status))
                        action.Execute();
                });

            _monitorTimer.Change(1000, (int)RefreshPeriod.TotalMilliseconds);
        }

        public void AddAction(Actions.Action action)
        {
            _actions.Add(action);
        }

        public void AddActions(IList<Actions.Action> actionsForWatcher)
        {
            foreach (var newAction in actionsForWatcher)
            {
                AddAction(newAction);
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
                _actions.Clear();

                _monitorTimer = null;
                _actions = null;
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
