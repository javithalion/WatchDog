using System;
using WatchDog.Watchers;

namespace WatchDog.Actions
{
    public abstract class Action
    {
        protected readonly StatusWatcher _statusWatcher;

        public readonly Status TriggerWhen;
        public readonly Guid Identifier;
        public readonly string Name;        

        public Action(StatusWatcher statusWatcher, Status triggerWhen = Status.NotWorking, string name = "ContingenceAction")
        {
            Identifier = Guid.NewGuid();
            Name = name;
            TriggerWhen = triggerWhen;
            _statusWatcher = statusWatcher;
        }

        public abstract void Execute();        
    }

    public enum TriggerMode
    {
        WhenWorking,
        WhenNotWorking
    }
}
