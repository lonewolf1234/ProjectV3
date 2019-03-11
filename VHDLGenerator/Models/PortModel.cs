using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VHDLGenerator.Models
{
    public class PortModel
    {
        public PortModel() { }

        public string Name { get; set; }

        public string Direction { get; set; }

        public bool Bus { get; set; }

        public string MSB { get; set; }

        public string LSB { get; set; }

        public void Clear()
        {
            this.Name = string.Empty;
            this.Direction = string.Empty;
            this.Bus = false;
            this.MSB = string.Empty;
            this.LSB = string.Empty;
        }

    }
}
