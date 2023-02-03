using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace NamedPipesSample.TrayIcon
{
   public class NotifyIconViewModel
   {
      public ICommand ShowWindowCommand
      {
         get
         {
            return new DelegateCommand
            {
               CanExecuteFunc = () => Application.Current.MainWindow == null || !Application.Current.MainWindow.IsVisible,
               CommandAction = () =>
               {
                  Application.Current.MainWindow = new MainWindow();
                  Application.Current.MainWindow.Show();
               }
            };
         }
      }

      public ICommand HideWindowCommand { 
         get
         {
            return new DelegateCommand
            {
               CommandAction = () => Application.Current.MainWindow.Close(),
               CanExecuteFunc = () => Application.Current.MainWindow != null && Application.Current.MainWindow.IsVisible
            };
         }
      }

      public ICommand ExitApplicationCommand
      {
         get
         {
            return new DelegateCommand { CommandAction = () => Application.Current.Shutdown() };
         }
      }
   }
}
