using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UsbLibrary;

namespace SADgui.Models
{
    public abstract class AbstractMissileLauncher
    {
        public bool DevicePresent;
        public string name = "";
        public int missiles = 4;
        public byte[] UP;
        public byte[] RIGHT;
        public byte[] LEFT;
        public byte[] DOWN;
        public byte[] FIRE;
        public byte[] STOP;
        public byte[] LED_OFF;
        public byte[] LED_ON;
        public UsbHidPort USB;
    }
}
