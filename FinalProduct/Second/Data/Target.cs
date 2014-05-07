using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Second.Data
{
    public class Target
    {
        public int id { get; set; }
        public int status { get; set; }
        public int hit { get; set; }
        public bool movingState { get; set; }
        public int led { get; set; }
        public string name { get; set; }
        public double spawnRate { get; set; }
        public bool isMoving { get; set; }
        public double points { get; set; }
        public double startTime { get; set; }
        public double x { get; set; }
        public double y { get; set; }
        public double z { get; set; }
        public int input { get; set; }
        public double dutyCycle { get; set; }
        public double phi { get; set; }
        public double theta { get; set; }
    }
}
