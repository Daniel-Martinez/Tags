using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SADgui.Models
{
    public interface IMissileLauncher
    {
        void command_Right(int degrees);
        void command_Left(int degrees);
        void command_Up(int degrees);
        void command_Down(int degrees);
        void command_Fire();
        void command_reset();
        void reload();
        string getName();
        void setName(string value);
        int getMissles();
    }
}