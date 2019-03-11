using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;
using Newtonsoft.Json;
using System.ComponentModel;
using System.Text.RegularExpressions;

namespace VHDLGenerator.ViewModels
{
    class SignalViewModel : INotifyPropertyChanged , IDataErrorInfo
    {
        #region Property Changed Interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if(handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        #region Private Variables
        private SignalModel Signal = new SignalModel();
        private DataPathModel _Datapath = new DataPathModel();
        private List<string> SPorts = new List<string>();
        private List<string> TPorts = new List<string>();
        private bool _GridEnable { get; set; }
        #endregion

        public SignalViewModel(DataPathModel _datapath)
        {
           _Datapath = _datapath;
        }

        #region Properties
        public string SigEntityNameTxt
        {
            get { return Signal.Name; }
            set { Signal.Name = value; }
        }
        public string MsbTxt
        {
            get {return Signal.MSB; }
            set {Signal.MSB = value; }
        }
        public string LsbTxt
        {
            get { return Signal.LSB; }
            set { Signal.LSB = value; }
        }
        public bool GridEnable
        {
            get
            {
                if (this.SCompName == _Datapath.Name || this.TCompName == _Datapath.Name)
                {
                    if (ErrorCollection.ContainsKey("SigEntityNameTxt"))
                    {
                        ErrorCollection["SigEntityNameTxt"] = null;
                    }
                    if (ErrorCollection.ContainsKey("MsbTxt"))
                    {
                        ErrorCollection["MsbTxt"] = null;
                    }
                    if (ErrorCollection.ContainsKey("LsbTxt"))
                    {
                        ErrorCollection["LsbTxt"] = null;
                    }
                    OnPropertyChanged("ErrorCollection");
                    this._GridEnable = false;
                }
                else
                    this._GridEnable = true;
                return this._GridEnable;
            }
            set{ this._GridEnable = value;}
        }
        public bool SigBusSel
        {
            get { return this.Signal.Bus; }
            set
            {
                this.Signal.Bus = value;
                OnPropertyChanged("BitsEnable");
                OnPropertyChanged("MsbTxt");
                OnPropertyChanged("LsbTxt");
            }
        }

        //item source - components
        //use as source for the combobox items
        public List<string> ComponentNames
        {
            get
            {
                return GetCompName();
            }
        }
        public SignalModel GetSignal
        {
            get { return this.Signal; }
        }

        //item selected
        //for selected item in source catsx
        public string SCompName
        {
            get { return Signal.Source_Comp; }
            set
            {
                this.Signal.Source_Comp = value;

                this.SPorts = GetPortNames(this.Signal.Source_Comp, "source");
                OnPropertyChanged("SCompPorts");
                OnPropertyChanged("GridEnable");
            }
        }
        //for selected item in traget cat
        public string TCompName
        {
            get { return Signal.Target_Comp; }
            set
            {
                this.Signal.Target_Comp = value;
                this.TPorts = GetPortNames(this.Signal.Target_Comp, "target");
                OnPropertyChanged("TCompPorts");
                OnPropertyChanged("GridEnable");
            }
        }

        //item source - source ports
        public List<string> SCompPorts
        {
            get
            {
                return this.SPorts;
            }
        }
        //item source - target ports
        public List<string> TCompPorts
        {
            get
            {
                return this.TPorts;
            }
        }

        //for selected iten in source cat - port
        public string SCompPortName
        {
            get { return Signal.Source_port; }
            set { this.Signal.Source_port = value; OnPropertyChanged("FinishEnable"); }
        }
        //for selected item in traget cat - port
        public string TCompPortName
        {
            get { return Signal.Target_port; }
            set { this.Signal.Target_port = value; OnPropertyChanged("FinishEnable"); }
        }
        #endregion

        #region Methods
        private List<string> GetCompName()
        {
            List<string> names = new List<string>();
            try
            {
                foreach(ComponentModel comp in _Datapath.Components)
                {
                    names.Add(comp.Name);
                }
                names.Add(_Datapath.Name);
            }
            catch(Exception) { }
            
            return names;
        }

        private List<string> GetPortNames(string selectedComponent, string filter)
        {

            List<string> names = new List<string>();

            try
            {
                if (_Datapath.Name == selectedComponent)
                {
                    foreach (PortModel port in _Datapath.Ports)
                    {
                        if (filter == "source" && port.Direction == "in")
                        {
                            names.Add(port.Name);
                        }
                        else if (filter == "target" && (port.Direction == "out" || port.Direction == "inout"))
                        {
                            names.Add(port.Name);
                        }
                    }
                }
                else
                {
                    foreach (ComponentModel comp in _Datapath.Components)
                    {
                        if (comp.Name == selectedComponent)
                        {
                            foreach (PortModel port in comp.Ports)
                            {
                                if (filter == "source" && (port.Direction == "out" || port.Direction == "inout"))
                                {
                                    names.Add(port.Name);
                                }
                                else if (filter == "target" && port.Direction == "in")
                                {
                                    names.Add(port.Name);
                                }
                            }
                        }
                    }
                }

                
            }
            catch (Exception) { };
            
            return names;
        }
        #endregion

        #region Validation
        //this needs to be changed
        private bool _FinishEnable { get; set; }
        public bool FinishEnable
        {
            get
            {
                if (SCompName != null && TCompName != null && SCompPortName != null && TCompPortName != null)
                {
                    if (!SignalExist(Signal, _Datapath))
                    {
                        if (GridEnable == true && BitsEnable == true)
                        {
                            if (ErrorCollection["SigEntityNameTxt"] == null && ErrorCollection["MsbTxt"] == null && ErrorCollection["LsbTxt"] == null)
                                this._FinishEnable = true;
                            else
                                this._FinishEnable = false;
                        }
                        else if (GridEnable == true)
                        {
                            if (ErrorCollection["SigEntityNameTxt"] == null)
                                this._FinishEnable = true;
                            else
                                this._FinishEnable = false;
                        }
                        else
                        {
                            this._FinishEnable = true;
                        }
                    }
                    else
                        this._FinishEnable = false;
                }
                else
                    this._FinishEnable = false;

                return this._FinishEnable;
            }
            set { this._FinishEnable = value; }
            
        }


        public bool _BitsEnable { get; set; }
        public bool BitsEnable
        {
            get
            {
                if (SigBusSel == true)
                {
                    this._BitsEnable = true;
                }
                else
                {
                    this.Signal.MSB = null;
                    this.Signal.LSB = null;

                    if (ErrorCollection.ContainsKey("MsbTxt"))
                    {
                        ErrorCollection["MsbTxt"] = null;
                    }
                    if (ErrorCollection.ContainsKey("LsbTxt"))
                    {
                        ErrorCollection["LsbTxt"] = null;
                    }
                    OnPropertyChanged("ErrorCollection");
                    this._BitsEnable = false;
                }
                return this._BitsEnable;
            }
            set
            {
                this._BitsEnable = value;

            }
        }

        public Dictionary<string, string> ErrorCollection { get; private set; } = new Dictionary<string, string>();
        public List<string> ReservedWords = new List<string>
        {
            "abs", "access","after","alias","all","and","architecture","array","assert","attribute",
            "begin","block","body","buffer","bus",
            "case","component","configuration","constant",
            "disconnect","downto",
            "else","elsif","end","entity","exit",
            "file","for","function",
            "generate","generic","group","guarded",
            "if","impure","in","internal","inout","is",
            "label","library","linkage","loop",
            "map","mod",
            "nand","new","next","nor","not","null",
            "of","on","open","or","others","out",
            "package","port","postponed","procedure","process","pure",
            "range","record","register","reject","rem","report","return","rol","ror",
            "select","severity","signal","shared","sla","sll","sra","srl","subtype",
            "then","to","transport","type",
            "unaffected","units","use",
            "variable",
            "wait","when","while","with",
            "xnor","xor"
        };
        public string Error { get; }
        public string this[string propertyname]
        {
            get
            {
                string result = null;
                switch (propertyname)
                {
                    case "SigEntityNameTxt":
                        if (string.IsNullOrWhiteSpace(SigEntityNameTxt))
                            result = "Entity Name cannot be empty";
                        else if (IsBeginWNum(SigEntityNameTxt))
                            result = "Cannot begin with a number";
                        else if (IsValidName(SigEntityNameTxt))
                            result = "Not a valid name. Only Letters, Numbers or underscore are allowed";
                        else if (IsReservedWord(SigEntityNameTxt))
                            result = "This is a Reserved Word";
                        break;

                    case "MsbTxt":
                        if (BitsEnable == true)
                        {
                            if (string.IsNullOrWhiteSpace(MsbTxt))
                                result = "MSB cannot be empty";
                            else if (IsInterger(MsbTxt))
                                result = "Only Integers are allowed";
                            break;
                        }
                        else
                            break;

                    case "LsbTxt":
                        if (BitsEnable == true)
                        {
                            if (string.IsNullOrWhiteSpace(LsbTxt))
                                result = "LSB cannot be empty";
                            else if (IsInterger(LsbTxt))
                                result = "Only Integers are allowed";
                            break;
                        }
                        else
                            break;
                }

                if (ErrorCollection.ContainsKey(propertyname))
                    ErrorCollection[propertyname] = result;
                else if (result != null)
                    ErrorCollection.Add(propertyname, result);

                OnPropertyChanged("ErrorCollection");
                OnPropertyChanged("FinishEnable");
                //OnPropertyChanged("AddPortEnable");
                return result;
            }
        }

        public bool IsReservedWord(string word)
        {
            bool result = false;

            foreach (string s in ReservedWords)
            {
                if (s.ToLower() == word.ToLower())
                {
                    result = true;
                }
            }

            return result;
        }

        public bool IsInterger(string word)
        {
            Regex regex = new Regex("[^0-9]+");
            return (regex.IsMatch(word));
        }

        public bool IsValidName(string word)
        {
            Regex regex = new Regex("[^A-Za-z0-9_]+");
            return (regex.IsMatch(word));
        }

        public bool IsBeginWNum(string word)
        {
            Regex regex = new Regex("^[0-9]");
            return (regex.IsMatch(word));
        }

        public bool IsDirectionSel(string word)
        {
            if (word == null)
                return false;
            else
                return true;
        }

        public bool SignalExist(SignalModel signal, DataPathModel data)
        {
            bool result = false;

            if (signal != null && data.Signals != null)
            {
                foreach (SignalModel sig in data.Signals)
                {
                    bool name = false;
                    bool SComp = false;
                    bool SCompPort = false;
                    bool TComp = false;
                    bool TCompPort = false;

                    if (signal.Name == sig.Name)
                        name = true;
                    if (signal.Source_Comp == sig.Source_Comp)
                        SComp = true;
                    if (signal.Source_port == sig.Source_port)
                        SCompPort = true;
                    if (signal.Target_Comp == sig.Target_Comp)
                        TComp = true;
                    if (signal.Target_port == sig.Target_port)
                        TCompPort = true;

                    if (signal.Name == null && sig.Name == null)
                    {
                        if (SComp && SCompPort && TComp && TCompPort)
                        {
                            result = true;
                        }
                        else
                            result = false;
                    }
                    else if (name || (SComp && SCompPort && TComp && TCompPort))
                    {
                        result = true;
                    }
                }
            }
            return result;
        }

        #endregion

    }
}
