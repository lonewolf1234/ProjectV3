using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;

namespace VHDLGenerator.Templates
{
    public partial class DataPathTemplate
    {

        public DataPathTemplate(DataPathModel data)
        {
            this.Name = data.Name;
            this.ArchName = data.ArchName;
            this.Ports = data.Ports;
            this.Components = data.Components;
            this.Signals = data.Signals;
        }

        public string Name { get; set; }

        public string ArchName { get; set; }

        public List<PortModel> Ports { get; set; }

        public List<ComponentModel> Components { get; set; }

        public List<SignalModel> Signals { get; set; }

        public List<string> MainPortsData
        {
            get{return PortTranslation(this.Ports);}
        }

        public List<string> SignalData
        {
            get{ return SignalTranslation(this.Signals); }
        }

        private List<string> PortTranslation(List<PortModel> ports)
        {
            List<string> templist = new List<string>();

            if (ports != null)
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
                        templist.Add( temp + ");");
                }
            }

            return templist;
        }

        private List<string> SignalTranslation(List<SignalModel> signals)
        {
            List<string> templist = new List<string>();


            if (signals != null)
            {
                foreach (SignalModel sig in signals)
                {
                    string tempsig = string.Empty;
                    if (sig.Name != null)
                    {
                        if (sig.Bus == true)
                        {
                            tempsig = $"signal {sig.Name} : STD_LOGIC_VECTOR({sig.MSB} downto {sig.LSB});";
                        }
                        else
                        {
                            tempsig = $"signal {sig.Name} : STD_LOGIC;";
                        }
                        templist.Add(tempsig);
                    }
                }
            }
            else
                templist = null; 

            return templist;
        }

        private List<string> PortMappingProcess(ComponentModel comp, List<SignalModel> signals, string datapathname)
        {
            List<string> Mapping = new List<string>();
            string temp = "";
            if (comp != null)
            {
                if (comp.Ports != null)
                {
                    foreach (PortModel port in comp.Ports)
                    {
                        if (signals.Count > 0 && comp.Name != datapathname)
                        {
                            foreach (SignalModel signal in signals)
                            {
                                #region commented code
                                //if ((comp.Name == signal.Source_Comp || comp.Name == signal.Target_Comp) && (port.Name == signal.Source_port || port.Name == signal.Target_port) && comp.Name != datapathname)
                                //{
                                //    if(signal.Source_Comp == datapathname)
                                //    {
                                //        temp = $"{port.Name} => {signal.Source_port}";
                                //    }
                                //    else if(signal.Target_Comp == datapathname)
                                //    {
                                //        temp = $"{port.Name} => {signal.Target_port}";
                                //    }
                                //    else
                                //    {
                                //        temp = $"{port.Name} => {signal.Name}";
                                //    }

                                //    if (comp.Ports.First() == port)
                                //    {
                                //        Mapping.Add(temp + ",");
                                //    }
                                //    else if (comp.Ports.Last() == port)
                                //    {
                                //        Mapping.Add("\t" + temp + ");");
                                //    }
                                //    else
                                //    {
                                //        Mapping.Add("\t" + temp + ",");
                                //    }

                                //    //Mapping.Add(temp);
                                //}
                                #endregion

                               
                                if (comp.Name == signal.Source_Comp && port.Name == signal.Source_port)
                                {
                                    if (signal.Target_Comp == datapathname)
                                    {
                                        temp = $"{port.Name} => {signal.Target_port}";
                                        break;
                                    }
                                    else
                                    {
                                        temp = $"{port.Name} => {signal.Name}";
                                        break;
                                    }
                                }
                                else if (comp.Name == signal.Target_Comp && port.Name == signal.Target_port)
                                {
                                    if (signal.Source_Comp == datapathname)
                                    {
                                        temp = $"{port.Name} => {signal.Source_port}";
                                        break;
                                    }
                                    else
                                    {
                                        temp = $"{port.Name} => {signal.Name}";
                                        break;
                                    }
                                }
                                else
                                {
                                    temp = $"{port.Name} =>     ";
                                        
                                }
                               
                            }
                           
                            if (comp.Ports.Count > 1)
                            {
                                if (comp.Ports.First() == port)
                                {
                                    Mapping.Add(temp + ",");
                                }
                                else if (comp.Ports.Last() == port)
                                {
                                    Mapping.Add("\t" + temp + ");");
                                }
                                else
                                {
                                    Mapping.Add("\t" + temp + ",");
                                }
                            }
                            else
                                Mapping.Add( temp + ");");
                           
                        }
                        else
                        {
                            temp = $"{port.Name} =>     ";
                            if (comp.Ports.Count > 1)
                            {
                                if (comp.Ports.First() == port)
                                {
                                    Mapping.Add(temp + ",");
                                }
                                else if (comp.Ports.Last() == port)
                                {
                                    Mapping.Add("\t" + temp + ");");
                                }
                                else
                                {
                                    Mapping.Add("\t" + temp + ",");
                                }
                            }
                            else
                                Mapping.Add(temp + ");");
                        }
                    }
                }
            }
            return Mapping;
        }
    }
}
