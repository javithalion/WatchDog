﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace WatchDogService.Model
{
    public abstract class StatusWatcher
    {
        public Guid Identifier { get; protected set; }
        public string Name { get; protected set; }
        public DateTime CreationDate { get; protected set; }
        public DateTime LastCheck { get; protected set; }
        public TimeSpan RefreshPeriod { get; protected set; }
        public Status Status { get; protected set; }

        private Timer _monitorTimer;
        private ICollection<ContingenceAction> _contingenceActions = new List<ContingenceAction>();

        public void Start()
        {
            _monitorTimer = new Timer(
                (e) =>
                {
                    CheckStatus(e);
                    if (Status != Status.Working)
                        foreach (var action in _contingenceActions)
                            action.Execute(this);
                });

            _monitorTimer.Change(1000, (int)RefreshPeriod.TotalMilliseconds);
        }
        
        public void AddContingenceAction(ContingenceAction action)
        {
            _contingenceActions.Add(action);
        }

        protected abstract void CheckStatus(object state);
        public abstract string GetWatcherInformation();
    }

    public enum Status
    {
        Working,
        NotWorking,
        Unknown
    }
}
