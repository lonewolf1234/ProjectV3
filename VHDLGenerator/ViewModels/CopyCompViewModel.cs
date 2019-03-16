using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;

namespace VHDLGenerator.ViewModels
{
    class CopyCompViewModel : INotifyPropertyChanged
    {
        DataPathModel _data = new DataPathModel();
        ComponentModel Component = new ComponentModel();

        #region Property Changed Interface
        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }
        #endregion

        public CopyCompViewModel(DataPathModel data)
        {
            _data = data;
        }

        public List<string> CompNames { get { return GetNames(_data); }}

        public ComponentModel GetComponent { get { return Component; } }

        private string _compSelected { get; set; }
        public string CompSelected
        {
            get { return this._compSelected; }
            set { this._compSelected = value; OnPropertyChanged("CompSelected"); CopyComponent(CompSelected, _data); }
        }

        private List<string> GetNames(DataPathModel data)
        {
            List<string> names = new List<string>();

            if(data.Components.Count > 0)
            {
                foreach(ComponentModel comp in data.Components)
                {
                    if(!names.Exists(x => x == comp.Name))
                    {
                        names.Add(comp.Name);
                    }
                }
            }
            return names;
        }

        private void CopyComponent(string compname, DataPathModel data)
        {
            ComponentModel copycomp = new ComponentModel();
            int id;

            if (data.Components.Count > 0)
            {
                ComponentModel tempcomp = new ComponentModel();
                id = data.Components.Count + 1;
                tempcomp = data.Components.Find(x => x.Name == compname);

                copycomp.Name = tempcomp.Name;
                copycomp.ID = id.ToString();
                copycomp.ArchName = tempcomp.ArchName;
                copycomp.Ports = tempcomp.Ports;

                Component = copycomp;
            }

           
        }
    }
}
