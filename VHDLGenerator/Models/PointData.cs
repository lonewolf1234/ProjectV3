using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VHDLGenerator.Models
{
    class PointData
    {

        public PointData(string componentname,string portname, Point point)
        {
            this.ComponentName = componentname;
            this.PortName = portname;
            this.Point = point;
        }

        string ComponentName { get; set; }
        string PortName { get; set; }
        Point Point { get; set; }
    }

    
}
