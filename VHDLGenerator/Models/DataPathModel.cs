using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    public class DataPathModel
    {
        public DataPathModel()
        {
            this.Ports = new List<PortModel>();
            this.Components = new List<ComponentModel>();
            this.Signals = new List<SignalModel>();

        }

        public string Name { get; set; }

        public string ArchName { get; set; }
        
        public List<PortModel> Ports { get; set; }

        public List<ComponentModel> Components { get; set; }

        public List<SignalModel> Signals { get; set; }
    }
}
