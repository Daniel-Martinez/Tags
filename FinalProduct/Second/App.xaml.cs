using SADgui.Models;
using SADgui.ViewModels;
using Second.Models;
//using Second.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace SADgui
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
          private void Application_Startup(object sender, StartupEventArgs e)
        {
            MainWindow window = new MainWindow();
              
            //Creating types of Missile Launchers that will be passed into our MainViewodel

            var launcherTypes = new List<LauncherTypes>();
            launcherTypes.Add(LauncherTypes.Real);
            launcherTypes.Add(LauncherTypes.Mock);

            //creating new server types to pass to MainViewModel
            var gameServerTypes = new List<GameServerType>();
            gameServerTypes.Add(GameServerType.Mock);
            gameServerTypes.Add(GameServerType.WebClient);

            //Creating an instance of MainVieModel and passing Launcher Type and Target arguments.

            MainViewModel viewModel = new MainViewModel(launcherTypes, gameServerTypes);  

            window.DataContext = viewModel;
            window.ShowDialog();
        }
        }
    }

