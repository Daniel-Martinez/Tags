using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADgui.Models
{
    public enum LauncherTypes
    {
        Mock,
        Real
    }

    public class MissileLauncherFactory
    {
        public static IMissileLauncher Create(LauncherTypes type)
        {
            IMissileLauncher launcher = null;
            switch (type)
            {
                case LauncherTypes.Mock:
                    launcher = new MockLauncher();
                    break;
                case LauncherTypes.Real:
                    launcher = new MissileLauncher();
                    break;
                default:
                    break;
            }

            return launcher;
        }
    }
}
