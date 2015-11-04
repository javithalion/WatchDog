using System;
using System.IO;
using WatchDog.Watchers;

namespace WatchDog.ContingenceActions
{
    public class LogFileAction : ContingenceAction
    {
        private readonly string _fileName;
        private readonly string _messageLayout;

        public LogFileAction(StatusWatcher statusWatcher, string fileName, string messageLayout, string name = "LogFileAction")
            :base(statusWatcher,name)
        {
            _fileName = fileName;
            _messageLayout = messageLayout;
        }

        public override void Execute()
        {
            var result = _messageLayout.ToLower();
            result = result.Replace(@"%date", DateTime.Now.ToString());
            result = result.Replace(@"%name", _statusWatcher.GetWathcDescription());

            using (var sw = new StreamWriter(_fileName, true))
            {
                sw.WriteLine(result);
            }
        }
    }
}
