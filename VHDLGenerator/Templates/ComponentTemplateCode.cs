using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;
using VHDLGenerator.Templates;

namespace VHDLGenerator.Templates
{
    public partial class ComponentTemplate
    {
        public ComponentTemplate(ComponentModel data)
        {
            this.Name = data.Name;
            this.ArchName = data.ArchName;
            this.Ports = data.Ports;
        }

        public string Name { get; set; }

        public string ArchName { get; set; }

        public List<PortModel> Ports { get; set; }

        public List<string> CompPortsData
        {
            get { return PortTranslation(this.Ports); }
           
        }

        private List<string> PortTranslation(List<PortModel> ports)
        {
            List<string> templist = new List<string>();

            if (ports.Count != 0)
            {
                foreach (PortModel port in ports)
                {
                    string temp = "";
                    if (port.Bus == true)
                    {
                        temp = $"{port.Name} : {port.Direction} STD_LOGIC_VECTOR({port.MSB} downto {port.LSB})";
                    }
                    else
                    {
                        temp = $"{port.Name} : {port.Direction} STD_LOGIC";
                    }

                    if (ports.Count > 1)
                    {
                        if (ports.First() == port)
                        {
                            templist.Add(temp + ";");
                        }
                        else if (ports.Last() == port)
                        {
                            templist.Add("\t" + temp + ");");
                        }
                        else
                        {
                            templist.Add("\t" + temp + ";");
                        }
                    }
                    else
                        templist.Add(temp + ");");
                }
            }

            return templist;
        }

    }
}
