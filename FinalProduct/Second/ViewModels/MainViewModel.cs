using SADgui.Models;
using Second.Data;
using Second.Models;
using Second.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SADgui.ViewModels
{
    public class MainViewModel : ViewModelBase
    {
        public event PropertyChangedEventHandler PropertyChanged;


        private MissileLauncherViewModel m_selectedLauncher;

        private GameServerViewModel m_selectedServer;

        private LauncherTypes m_selectedType;

        private GameServerType m_gameservertype;

        private TargetViewModel m_targetView;
        private string m_gameChoice;
        private double score = 0;

        private int timer = 0;


        //to return List of targets
        private IGameServer m_tempServer;

        
        public MainViewModel(IEnumerable<LauncherTypes> launchers, IEnumerable<GameServerType> servers)  
        {
            Targets = new ObservableCollection<TargetViewModel>();
            Targets_Hit_Status = new ObservableCollection<TargetViewModel>();
            Games = new ObservableCollection<string>();

            //Creates List type of available launchers passed in on App startup
            Launchers = new ObservableCollection<LauncherTypes>();
            foreach (var launcher in launchers)
            {
                Launchers.Add(launcher);
            }


            //Creates List type of available Serve types passed in on App startup
            Servers = new ObservableCollection<GameServerType>();
            foreach (var server in servers)
            {
                Servers.Add(server);
            }


            //Delegate Commands used for Binding
           
            GameChioceCommand = new DelegateCommand(GameChoice);
            LeftToRightCommand = new DelegateCommand(LeftToRight);
            ByPointsCommand = new DelegateCommand(All_Targets);
            StopCommand = new DelegateCommand(Stop);
            KillFriends = new DelegateCommand(Kill_Friends);
            StartCommand = new DelegateCommand(Start);
           

            //used to pass function parameters that have to do with target managment
            m_targetView = new TargetViewModel();     
        }

        public ObservableCollection<LauncherTypes> Launchers { get; private set; }
        public ObservableCollection<TargetViewModel> Targets { get; private set; }
        public ObservableCollection<TargetViewModel> Targets_Hit_Status { get; private set; }
        public ObservableCollection<GameServerType> Servers { get; private set; }
        public ObservableCollection<string> Games { get; private set; }


        
        //The NewList function creates a list of Targets  by the user selecting a game from Game window.
        private void GameChoice()
        {
            if (Targets != null)
            {
                Targets.Clear();
            }
            if (m_gameChoice != null)
            {
  
                    var targets = m_tempServer.RetrieveTargetList(m_gameChoice);
                    foreach (var target in targets)
                    Targets.Add(new TargetViewModel(target));
               
            }
        }

        //This Stops the game.
        private void Stop()
        {
            if (m_tempServer != null)
            {
                m_tempServer.StopRunningGame();
                MessageBox.Show("Game Over");
                Thread.Sleep(5000);
                score = 0;
                OnPropertyChanged("score");
            }
        }

        public void Timer()
        {
            Thread threadTime = new Thread(TimerThread);
            threadTime.Start();

        }

        public void TimerThread()
        {
            while(timer < 61)
            {
                Thread.Sleep(1000);
                ++timer;
                
            }
            Stop();

        }

        private void Start()
        {
            if (m_tempServer != null)
            {
                m_tempServer.StartGame(m_gameChoice);
                Timer();
                               
            }
        }

        //Function to set users selection of file to open
        public string FilePath
        {
            get
            {
                return m_gameChoice;
            }

            set
            {
                m_gameChoice = value;
                OnPropertyChanged("m_path");
            }
        }


        //This targets the List from Left to right.
        private void LeftToRight()
        {
            m_targetView.SetPhiTheata(Targets);
            List<TargetViewModel> temp = Targets.ToList();

            temp.Sort();
            temp.Sort();

            
            if (m_selectedLauncher != null)
            {
               
                foreach (TargetViewModel target in Targets)
                {
                    m_selectedLauncher.Position(target, 0);

                }

                //This block will aquire Target list to include Hit status and update GUI
                GameChoice();
                score = 0;

                //This block will aquire Target list to include Hit status and update GUI

                foreach (var target in Targets)
                {

                    double temp_score = target.Hit * target.Points;
                    score = score + temp_score;
                    OnPropertyChanged("score");
                }
            }

        }


        //Code to sort the list by points High to low for High Score.
        private void All_Targets()
        {
            m_targetView.SetPhiTheata(Targets);
            List<TargetViewModel> temp = Targets.ToList();

            temp.Sort();
            temp.Sort();


            if (m_selectedLauncher != null)
            {
                
                foreach (TargetViewModel target in Targets)
                {
                    m_selectedLauncher.Position(target, 2);  //value 2 represent kill all

                }
                
                    GameChoice();
                    score = 0;

                //This block will aquire Target list to include Hit status and update GUI

                foreach (var target in Targets)
                {
                    
                    double temp_score = target.Hit * target.Points;
                    score = score + temp_score;
                    OnPropertyChanged("score");
                }
            }

        }

        private void Kill_Friends()
        {
            m_targetView.SetPhiTheata(Targets);
            List<TargetViewModel> temp = Targets.ToList();

            temp.Sort();
            temp.Sort();


            if (m_selectedLauncher != null)
            {
                
                foreach (TargetViewModel target in Targets)
                {
                    m_selectedLauncher.Position(target, 1);  //value 1 represent kill Friends
                }

                //This block will aquire Target list to include Hit status and update GUI
              
                GameChoice();
                score = 0;

                //This block will aquire Target list to include Hit status and update GUI

                foreach (var target in Targets)
                {

                    double temp_score = target.Hit * target.Points;
                    score = score + temp_score;
                    OnPropertyChanged("score");
                }
            }

        }



        public double Score
        {
            get
            {
                return score;
            }

            set
            {
                score = value;
                OnPropertyChanged("Score");
            }
        }

 


        //Gets the viewModel for Missile Launcher
        public MissileLauncherViewModel MissileLaunchers
        {
            get
            {
                return m_selectedLauncher;
            }
            private set
            {
                m_selectedLauncher = value;
                OnPropertyChanged("MissileLaunchers");
            }
        }
        
        //Selects the Launcher Type; Mock or Real
        public LauncherTypes SelectedType
        {
            get
            {
                return m_selectedType;
            }
            set
            {
                if (m_selectedType == value)
                    return;

                m_selectedType = value;
                var launcher = MissileLauncherFactory.Create(value);
                MissileLaunchers = new MissileLauncherViewModel(launcher);
            }
        }


        //Sets Game server, Mock or WebCient
        public GameServerViewModel SelectedServers
        {
            get
            {
                return m_selectedServer;
            }
            private set
            {
                m_selectedServer = value;
                OnPropertyChanged("SelectedServers");
            }
        }



       //not sure if I will keep this here just testing the creation method
       String teamName = "TEAMGlatos";
       String ipAddress = "10.0.0.5";
       int port = 3000;
            

            //Create our server of Mock or Web
            public GameServerType ServerType
            {
                get
                {
                    return m_gameservertype;
                }
                set
                {
                    if (m_gameservertype == value)
                        return;

                    //Clear Games from previous server
                    Games.Clear();
                    Targets.Clear();

                    m_gameservertype = value;
                    var server = GameServerFactory.Create(value, teamName, ipAddress, port);
                    m_tempServer = server;
                    SelectedServers = new GameServerViewModel(server);

                    score = 0;
                    //Adds to Games ObservableCollection for display on GUI
                    var data = m_selectedServer.GetList();
                    foreach (string game in data)
                    {
                        Games.Add(game);
                    }
                }
            }

        //ICommands to use in DelegateCommand class for Binding
        public ICommand ClearCommand { get; private set; }
        public ICommand GameChioceCommand { get; private set; }
        public ICommand LeftToRightCommand { get; private set; }
        public ICommand ByPointsCommand { get; private set; }
        public ICommand StopCommand { get; private set; }
        public ICommand KillFriends { get; private set; }
        public ICommand StartCommand { get; private set; }
      
    }

}
