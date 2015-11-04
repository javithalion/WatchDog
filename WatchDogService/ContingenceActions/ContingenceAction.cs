using System;
using WatchDogService.Watchers;

namespace WatchDogService.ContingenceActions
{
    public abstract class ContingenceAction
    {
        protected readonly StatusWatcher _statusWatcher;

        public readonly Guid Identifier;
        public readonly string Name;        

        public ContingenceAction(StatusWatcher statusWatcher, string name = "ContingenceAction")
        {
            Identifier = Guid.NewGuid();
            Name = name;
            _statusWatcher = statusWatcher;
        }

        public abstract void Execute();
    }
}
