using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WatchDogService.Watchers;

namespace WatchDogService.ContingenceActions
{
    public class LogFileAction : ContingenceAction
    {
        private readonly string _fileName;

        public LogFileAction(StatusWatcher statusWatcher, string fileName, string name = "LogFileAction")
            :base(statusWatcher,name)
        {
            this._fileName = fileName;
        }

        public override void Execute()
        {
            using (var sw = new StreamWriter(_fileName, true))
            {
                sw.WriteLine(
                            string.Format("{0}: {1} is not working",
                            DateTime.Now.ToString(),
                            _statusWatcher.GetWathcDescription()));
            }
        }
    }
}
