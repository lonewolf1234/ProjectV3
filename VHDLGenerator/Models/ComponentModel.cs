using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    public class ComponentModel
    {
        public ComponentModel() { }

        public string ID { get; set; }

        public string Name { get; set; }

        public string ArchName { get; set; }

        public List<PortModel> Ports { get; set; }
    }
}
