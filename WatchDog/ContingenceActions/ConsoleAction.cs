using System;
using WatchDog.Watchers;

namespace WatchDog.ContingenceActions
{
    public class ConsoleAction : ContingenceAction
    {
        private readonly string _messageLayout;

        public ConsoleAction(StatusWatcher statusWatcher, string messageLayout, string name = "ConsoleAction")
            :base(statusWatcher,name)
        {
            _messageLayout = messageLayout;
        }

        public override void Execute()
        {
            var result = _messageLayout.ToLower();
            result = result.Replace(@"%date", DateTime.Now.ToString());
            result = result.Replace(@"%name", _statusWatcher.GetWathcDescription());

            Console.WriteLine(result);
        }
    }
}
