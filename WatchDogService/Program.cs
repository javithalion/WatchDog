using System;
using System.ServiceProcess;

namespace WatchDog
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main()
        {
            if (Environment.UserInteractive)
            {
                var service = new WatchDogService();
                service.Start();
                Console.ReadLine();
            }
            else
            {
                ServiceBase[] ServicesToRun;
                ServicesToRun = new ServiceBase[]
                {
                new WatchDogService()
                };
                ServiceBase.Run(ServicesToRun);
            }
        }
    }
}
