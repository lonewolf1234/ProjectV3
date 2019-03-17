using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VHDLGenerator.Models
{
    public class PointData
    {

        public PointData(string componentID,string componentname,string portname, Point point)
        {
            this.ComponentID = componentID;
            this.ComponentName = componentname;
            this.PortName = portname;
            this.Point = point;
        }
        public string ComponentID { get; set; }
        public string ComponentName { get; set; }
        public string PortName { get; set; }
        public Point Point { get; set; }
    }

    
}
