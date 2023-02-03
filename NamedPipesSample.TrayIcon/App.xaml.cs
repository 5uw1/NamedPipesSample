using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace NamedPipesSample.TrayIcon
{
   /// <summary>
   /// Interaction logic for App.xaml
   /// </summary>
   public partial class App : Application
   {
      private TaskbarIcon notifyIcon;

      public App()
      {
         NamedPipesClient.Instance.InitializeAsync().ContinueWith(t =>
            MessageBox.Show($"Error while connecting to pipe server: {t.Exception}"),
            TaskContinuationOptions.OnlyOnFaulted);
      }

      protected override void OnStartup(StartupEventArgs e)
      {
         base.OnStartup(e);
         notifyIcon = (TaskbarIcon)FindResource("NotifyIcon");
      }

      protected override void OnExit(ExitEventArgs e)
      {
         NamedPipesClient.Instance.Dispose();
         notifyIcon.Dispose();
         base.OnExit(e);
      }
   }
}
