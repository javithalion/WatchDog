using System;
using System.IO;
using WatchDog.Watchers;

namespace WatchDog.Actions
{
    public class LogFileAction : Action
    {
        private readonly string _fileName;
        private readonly string _messageLayout;

        public LogFileAction(StatusWatcher statusWatcher, string fileName, string messageLayout, Status triggerWhen, string name = "LogFileAction")
            : base(statusWatcher, triggerWhen, name)
        {
            _fileName = fileName;
            _messageLayout = messageLayout;
        }

        public override void Execute()
        {
            var result = _messageLayout;
            result = result.Replace(@"%date", DateTime.Now.ToString());
            result = result.Replace(@"%name", _statusWatcher.Name);

            using (var sw = new StreamWriter(_fileName, true))
            {
                sw.WriteLine(result);
            }
        }
    }
}
