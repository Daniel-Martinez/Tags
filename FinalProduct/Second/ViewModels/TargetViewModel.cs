using SADgui.Models;
using Second.Data;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace SADgui.ViewModels
{
    public class TargetViewModel : ViewModelBase, IComparable<TargetViewModel>
    {

        //variables to hold values for types
        private Target m_target;
        
        public TargetViewModel(Target target)
        {
            m_target = target;
        }

        public TargetViewModel()
        {
            
        }

        //This method sorts List on Phi Value
        public int CompareTo(TargetViewModel other)
        {
            return this.Phi.CompareTo(other.CoordinatesX);

        }


        public void SetPhiTheata(ObservableCollection<TargetViewModel> targetList)
        {
            double radius = 0;
            double mPhi = 0;
            double mTheta = 0;



            for (int i = 0; i < targetList.Count; i++) 
            {
                radius = Math.Sqrt(Math.Pow(targetList[i].CoordinatesX, 2) + Math.Pow((targetList[i].CoordinatesY - 3), 2) + Math.Pow(targetList[i].CoordinatesZ, 2));  //cartetion product


                mTheta = (180 / Math.PI) * (Math.Acos(targetList[i].CoordinatesZ / radius));       //calculating theta which is our up/down
                targetList[i].Theta =  mTheta;
               // MessageBox.Show("theta" + mTheta);
                mPhi = (180 / Math.PI) * (Math.Atan(targetList[i].CoordinatesY / targetList[i].CoordinatesX));          //calculateing phi which is left/right
                targetList[i].Phi = mPhi;
              //  MessageBox.Show("phi" + mPhi);


                
                if (targetList[i].CoordinatesZ == 0)
                {
                    mTheta = 0;
                    targetList[i].Theta = mTheta;

                }
             }
          }


        //get and set Phi
        public double Phi
        {
            get
            {
                return m_target.phi;
            }

            set
            {
                m_target.phi = value;
                OnPropertyChanged("Phi"); //may be lower case
            }
        }

        public double Theta
        {
            get
            {
                return m_target.theta;
            }

            set
            {
                m_target.theta = value;
                OnPropertyChanged("points"); //may be lower case
            }
        }

        //returns the value of our score
        public double Score
        {
            get
            {
                return m_target.points;
            }

            set
            {
                m_target.points = value;
                OnPropertyChanged("points"); //may be lower case
            }
        }

        //Shows the value of Status field
        public int Status
        {
            get
            {
                return m_target.status;
            }

            set
            {
                m_target.status = value;
                OnPropertyChanged("status"); //may be lower case
            }
        }

        //Shows the value of ID field
        public int Id
        {
            get
            {
                return m_target.id;
            }

            set
            {
                m_target.id = value;
                OnPropertyChanged("Status"); //may be lower case
            }
        }

        //Shows value of Z coordinate
        public double CoordinatesZ
        {
            get
            {
                return m_target.z;

            }

            set
            {

                m_target.z = value;
                OnPropertyChanged("Zcord");
            }
        }

        //Shows value of led field
        public int Flash
        {
            get
            {
                return m_target.led;

            }

            set
            {

                m_target.led = value;
                OnPropertyChanged("led");
            }
        }

        //Shows value of Y coordinate
        public double CoordinatesY
        {
            get
            {
                return m_target.y;

            }

            set
            {

                m_target.y = value;
                OnPropertyChanged("Ycord");
            }
        }

        //Shows value of X coordinate
        public double CoordinatesX
        {
            get
            {
                return m_target.x;

            }

            set
            {

                m_target.x = value;
                OnPropertyChanged("Xcord");
            }
        }
        
        //Returns the target name
        public string Name
        {
            get
            {
                return m_target.name;
            }

            set
            {

                m_target.name = value;
                OnPropertyChanged("Name");
            }
        }

        //Returns if target was hit
        public int Hit
        {
            get
            {
                return m_target.hit;
            }

            set
            {

                m_target.hit = value;
                OnPropertyChanged("hit");
            }
        }

        //Returns if the target is moving
        public bool MovingState
        {
            get
            {
                return m_target.movingState;
            }

            set
            {

                m_target.movingState = value;
                OnPropertyChanged("movingState");
            }
        }

        //Returns the rate target is returned and ready to fire
        public double SpawnRate
        {
            get
            {
                return m_target.spawnRate;
            }

            set
            {

                m_target.spawnRate = value;
                OnPropertyChanged("Name");
            }
        }

        //Returns true if Target is moving
        public bool IsMoving
        {
            get
            {
                return m_target.isMoving;
            }

            set
            {

                m_target.isMoving = value;
                OnPropertyChanged("Name");
            }
        }


        //Returns the points for target
        public double Points
        {
            get
            {
                return m_target.points;
            }

            set
            {

                m_target.points = value;
                OnPropertyChanged("Name");
            }
        }

        //Returns the time game was initiated
        public double StartTime
        {
            get
            {
                return m_target.startTime;
            }

            set
            {

                m_target.startTime = value;
                OnPropertyChanged("Name");
            }
        }

        //Returns the input, not sure wht this does
        public int Input
        {
            get
            {
                return m_target.input;
            }

            set
            {

                m_target.input = value;
                OnPropertyChanged("Name");
            }
        }

        //Returns the dutyCycle, not sure what this does.
        public double DutyCycle
        {
            get
            {
                return m_target.dutyCycle;
            }

            set
            {

                m_target.dutyCycle = value;
                OnPropertyChanged("Name");
            }
        }
    }
}
