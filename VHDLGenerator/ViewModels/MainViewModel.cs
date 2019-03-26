using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;
using VHDLGenerator.ViewModels;
using VHDLGenerator.ViewModels.Commands;
using VHDLGenerator.Views;

namespace VHDLGenerator.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
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

        public MainViewModel()
        {

        }

        #region Open DatapathView Command
        public OpenCommand OpenDatapathViewCommand { get; private set; }
        private void Btn_Datapath_Click()
        {
            DatapathView window_Datapath = new DatapathView();
            List<PointData> datapoints = new List<PointData>();
            if (window_Datapath.ShowDialog() == true)
            {
                try
                {
                    _dataPath = window_Datapath.GetDataPathModel;           //Gets the DataPath Object from the Datapath menu and passes it to _datapath

                    Btn_Component.IsEnabled = true;                         //Disables the Datapath button and enables the other buttons
                    Btn_Signal.IsEnabled = false;
                    Btn_Datapath.IsEnabled = false;
                    Btn_Copy_Component.IsEnabled = false;

                    GenerateDatapath(_dataPath);                            //DataPath Code Generation

                    LoadFileTree(_dataPath);                                         //Loads text into the Project file tree view using info in _dataPath
                    LoadCodeTree(_dataPath);                                         //Loads generated code file names into the tree view using the _newfolderPath

                    Canvas canvas = new Canvas();
                    canvas = this.DrawingCanvas;

                    datapoints = DrawDatapath(_dataPath, canvas);
                    foreach (PointData data in datapoints)
                    {
                        DataPoints.Add(data);
                    }
                }
                catch (Exception) { }
            }
        }
        #endregion

    }

}
