using SADgui.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second.Data
{

    //This class allows us to sort the list by points.
    class SortByPoints: IComparer<TargetViewModel>
    {
        public int Compare(TargetViewModel x, TargetViewModel y)
        {
            return x.Points.CompareTo(y.Points);
        }
    }
}
