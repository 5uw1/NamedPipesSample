using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceProcess;

namespace NamedPipesSample.WindowsService
{
   internal class ServiceHost : ServiceBase
   {
      private static Thread serviceThread;
      private static bool stopping;

      private static NamedPipesServer pipesServer;

      public ServiceHost() {
         ServiceName = "Named Pipes Sample Service";
      }

      protected override void OnStart(string[] args)
      {
         Run(args);
      }

      protected override void OnStop()
      {
         Abort();
      }

      protected override void OnShutdown()
      {
         Abort();
      }

      public static void Run(string[] args)
      {
         serviceThread = new Thread(InitializeServiceThread)
         {
            Name = "Named Pipes Sample Service Thread",
            IsBackground = true
         };
      }

      public static void Abort()
      {
         stopping= true;
      }

      private static void InitializeServiceThread()
      {
         pipesServer = new NamedPipesServer();
         pipesServer.InitializeAsync().GetAwaiter().GetResult();

         while(!stopping) { 
            Task.Delay(100).GetAwaiter().GetResult();
         }
      }
   }
}
