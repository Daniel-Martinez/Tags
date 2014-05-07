using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.Expression.Encoder.Devices;
using WebcamControl;
using System.IO;
using System.Drawing.Imaging;
using System.Threading;
using System.Windows.Threading;

namespace Second.Windows
{
    /// <summary>
    /// Interaction logic for TargetViewControl.xaml
    /// </summary>
    public partial class TargetViewControl : UserControl
    {
        const string BUTTON_TEXT_START = "start";
        const string BUTTON_TEXT_STOP = "stop";
        private bool isRunning;
        Binding test;
        public TargetViewControl()
        {
            InitializeComponent();

            Binding binding_1 = new Binding("SelectedValue");
            test = binding_1;
            binding_1.Source = VideoDevicesComboBox;
            //WebcamCtrl.SetBinding(Webcam.VideoDeviceProperty, binding_1);
            Thread workerThread = new Thread(ThreadJob);
            workerThread.Start();




            
            // Set some properties of the Webcam control

            WebcamCtrl.FrameRate = 60;
            WebcamCtrl.FrameSize = new System.Drawing.Size(640, 480);

            // Find available a/v devices for use with Laptop Cam or other USB.
            
            var vidDevices = EncoderDevices.FindDevices(EncoderDeviceType.Video);
            VideoDevicesComboBox.ItemsSource = vidDevices;
            VideoDevicesComboBox.SelectedIndex = 0;
         }

        private void StartCaptureButton_Click(object sender, RoutedEventArgs e)
        {


            try
            {
                // Display webcam video

                WebcamCtrl.StartCapture();
            }
            catch (Microsoft.Expression.Encoder.SystemErrorException ex)
            {
                MessageBox.Show("Device is in use by another application");
            }
        }

        private void StopCaptureButton_Click(object sender, RoutedEventArgs e)
        {
            // Stop the display of webcam video.
            WebcamCtrl.StopCapture();
            
        }

        private void ThreadJob()
        {
           
                // Display webcam video

                Dispatcher.Invoke(new Action(()=>WebcamCtrl.SetBinding(Webcam.VideoDeviceProperty, test)));

        }
    }
}

