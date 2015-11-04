using System;
using System.IO;
using WatchDogService.Watchers;

namespace WatchDogService.ContingenceActions
{
    public class LogFileAction : ContingenceAction
    {
        private readonly string _fileName;
        private readonly string _formatString;

        public LogFileAction(StatusWatcher statusWatcher, string fileName, string formatString, string name = "LogFileAction")
            :base(statusWatcher,name)
        {
            _fileName = fileName;
            _formatString = formatString;
        }

        public override void Execute()
        {
            var result = _formatString.ToLower();
            result = result.Replace(@"%date", DateTime.Now.ToString());
            result = result.Replace(@"%name", _statusWatcher.GetWathcDescription());

            using (var sw = new StreamWriter(_fileName, true))
            {
                sw.WriteLine(result);
            }
        }
    }
}
