using System;
using WatchDog.Watchers;

namespace WatchDog.Actions
{
    public class ConsoleAction : Action
    {
        private readonly string _messageLayout;

        public ConsoleAction(StatusWatcher statusWatcher, string messageLayout, Status triggerWhen, string name = "ConsoleAction")
            : base(statusWatcher, triggerWhen, name)
        {
            _messageLayout = messageLayout;
        }

        public override void Execute()
        {
            var result = _messageLayout;
            result = result.Replace(@"%date", DateTime.Now.ToString());
            result = result.Replace(@"%name", _statusWatcher.Name);

            Console.WriteLine(result);
        }        
    }
}
