using System;
using WatchDogService.Watchers;

namespace WatchDogService.ContingenceActions
{
    public class ConsoleAction : ContingenceAction
    {
        private readonly string _formatString;

        public ConsoleAction(StatusWatcher statusWatcher, string formatString ,string name = "ConsoleAction")
            :base(statusWatcher,name)
        {
            _formatString = formatString;
        }

        public override void Execute()
        {
            var result = _formatString.ToLower();
            result = result.Replace(@"%date", DateTime.Now.ToString());
            result = result.Replace(@"%name", _statusWatcher.GetWathcDescription());

            Console.WriteLine(result);
        }
    }
}
