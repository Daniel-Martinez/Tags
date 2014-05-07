using SADgui.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;


namespace SADgui.ViewModels
{
    public class MissileLauncherViewModel : ViewModelBase
    {
        private IMissileLauncher m_launcher;

        //initial values for missile launceher, assuming fully loaded and in start position
        private int missiles = 4;       
        private double phi = 0;
        private double theta = 0;

        //Default constructor setting launcher
        public MissileLauncherViewModel(IMissileLauncher launcher)
        {

            m_launcher = launcher;

            //The below are definitions for DelegateCommand objects

            Action fire = FireMissiles;
            Action up = MoveUp;
            Action down = MoveDown;
            Action left = MoveLeft;
            Action right = MoveRight;
            Action reload = Reload;
            Action calibrate = Calibrate;

            FireCommand = new DelegateCommand(fire);
            UpCommand = new DelegateCommand(up);
            DownCommand = new DelegateCommand(down);
            LeftCommand = new DelegateCommand(left);
            RightCommand = new DelegateCommand(right);
            ReloadCommand = new DelegateCommand(reload);
            CalibrateCommand = new DelegateCommand(calibrate);


        }

        //Returns value for theta to Viewmodel
        public double Theta
        {
            get
            {
                return theta;
            }

            set
            {
                theta = value;
                OnPropertyChanged("theta");
            }
        }

        //Returns value for phi to ViewModel
        public double Phi
        {
            get
            {
                return phi;
            }

            set
            {
                phi = value;
                OnPropertyChanged("phi");
            }
        }

        //Retuns value for missiles to ViewModel
        public int Missiles
        {
            get
            {
                return missiles;
            }

            set
            {
                missiles = value;
                OnPropertyChanged("Missiles");
            }
        }








        //Returns missile Launcher to phi=0 and theta=0 position
        public void Calibrate()
        {
            Thread thread = new Thread(CalibrateTread);
            thread.Start();

        }


        public void CalibrateTread()
        {
            m_launcher.command_reset();
            phi = 0;
            OnPropertyChanged("phi");
            theta = 0;
            OnPropertyChanged("theta");
            m_launcher.reload();
            missiles = 4;
            OnPropertyChanged("missiles");
        }

        //Reloads the misile launcher and returns missile count to 4
        private void Reload()
        {
            m_launcher.reload();
            missiles = 4;
            OnPropertyChanged("missiles");
        }

        //Manual control, will move missile launcher 1 degree to the right
        private void MoveRight()
        {
            m_launcher.command_Right(10);
            phi = phi + 10;
            OnPropertyChanged("phi");
        }

        //Overloaded control for use by targeting system, will move right by value phi
        private void MoveRight(int m)
        {
            m_launcher.command_Right(m);
            //phi = phi + m;
            OnPropertyChanged("phi");
        }

        //Manual control, will move missile launcher 1 degree to the left
        private void MoveLeft()
        {
            m_launcher.command_Left(10);
            phi = phi - 10;
            OnPropertyChanged("phi");
        }

        //Overloaded control for use by targeting system, will move left by value phi
        private void MoveLeft(int m)
        {
            m_launcher.command_Left(m);
            //phi = phi - m;
            OnPropertyChanged("phi");
        }

        //Manual control, will move missile launcher 1 degree down
        private void MoveDown(int m)
        {
            m_launcher.command_Down(m);
            if (theta > 0)
            {
                //theta = theta - m;
                OnPropertyChanged("theta");
            }
        }

        //Overloaded control for use by targeting system, will move down by value theta
        private void MoveDown()
        {
            m_launcher.command_Down(10);
            if (theta > 0)
            {
                theta = theta - 10;
                OnPropertyChanged("theta");
            }
        }
        
        //Manual control, will move missile launcher 1 degree up
        private void MoveUp()
        {
            m_launcher.command_Up(10);
            if (theta <= 21)
            {
                theta = theta + 10;
                OnPropertyChanged("theta");
            }
        }


        //Overloaded control for use by targeting system, will move up by value theta
        private void MoveUp(int m)
        {
            m_launcher.command_Up(m);
            //theta = theta + m;
            OnPropertyChanged("theta");
        }

 
        //Will fire one misile
        private void FireMissiles()
        {
            
            if (missiles >= 1)
            {
                missiles--;
                OnPropertyChanged("missiles");
            }
            m_launcher.command_Fire();

        }

        //This function is called from the TargetViewModel to handel the Missile Launcher movment and fireing once a target
        //is determinied to be an enemy.  The function is set up to keep track on Phi and Theta as it attackes muliple targets
        public void Position(TargetViewModel targetList, int to_kill)
        {
            //converts to int because our missleLauncher only understands integers
            int t = Convert.ToInt32(targetList.Phi);
            int t2 = Convert.ToInt32(targetList.Theta);
            

            if (t < 0)
            {
                t = (t + 90);
                t = -t;
            }
            else
            {
                t = (90 - t);
            }

            //for theta value
            if(t2 != 0)
            {
                t2 = 90 - t2;
            }
         

            if (t <= phi)
            {
                double left = phi + t;
                int positive = Math.Abs(Convert.ToInt32(left));
                MoveLeft(positive);
                phi = (phi - positive);
                OnPropertyChanged("phi");
            }

            else
            {
                double right = t - phi;
                int positive = Math.Abs(Convert.ToInt32(right));
                MoveRight(positive);
                phi = phi + positive;
                OnPropertyChanged("phi");
            }

            //if entered value is less that zero it is a negative number and will move down.
            if (t2 <= theta) 
            {
                int temp2 = Convert.ToInt32(theta - t2);
                int positive = Math.Abs(temp2);
                MoveDown(positive);
                theta = theta - positive;
                OnPropertyChanged("theta");
            }

            //if entered value is greater that zero it is a positive number and will move up.
            else
            {
                int temp2 = Convert.ToInt32(t2 - theta);
                int positive = Math.Abs(temp2);
                MoveUp(positive);
                theta = theta + positive;
                OnPropertyChanged("theta");
            }


            //Depending on enemy selected fire missile.

            if (targetList.Status == to_kill)
            {
                FireMissiles();
            }
            else if(to_kill == 2)
            {
                FireMissiles();
            }
        }
            
        

        public ICommand FireCommand { get; set; }
        public ICommand UpCommand { get; private set; }
        public ICommand DownCommand { get; private set; }
        public ICommand LeftCommand { get; private set; }
        public ICommand RightCommand { get; private set; }
        public ICommand ReloadCommand { get; private set; }
        public ICommand CalibrateCommand { get; private set; }


    }
    

}
