using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VHDLGenerator.Models;

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

    //    private DataPathModel Data = new DataPathModel();
    //    private List<string> _Title = new List<string>();
    //    private TreeViewData _treeViewData = new TreeViewData();

    //    //public DataPathModel TreeviewData { get; set; }
    //    public List<string> Title { get { return this._Title; } }

    //    private DataPathModel _MainWinData { get; set; }
    //    public DataPathModel MainWinData { get { return _MainWinData; } set { this._MainWinData = value; OnPropertyChanged("TreeviewData"); } }

    //    public TreeViewData TreeviewData
    //    {
    //        get { return _treeViewData; }
    //        set
    //        {
    //            this._treeViewData = value;

    //            if(MainWinData.Name != null)
    //            {
    //                _treeViewData.Title = Data.Name;
    //            }

    //            if(MainWinData.Ports != null)
    //            {
    //                TreeViewData tv = new TreeViewData();
    //                tv.Title = "Ports";
    //                _treeViewData.Items.Add(tv);
    //            }

    //            if(MainWinData.Signals != null)
    //            {
    //                TreeViewData tv = new TreeViewData();
    //                tv.Title = "Signal";
    //                _treeViewData.Items.Add(tv);
    //            }
    //        }
    //    }

    //    public MainViewModel(DataPathModel data)
    //    {
    //        this.Data = data;
    //    }
    //}

    //public class TreeViewData
    //{
    //    public TreeViewData()
    //    {
    //        this.Items = new List<TreeViewData>();
    //    }
    //    public string Title { get; set; }
    //    public List<TreeViewData> Items { get; set; }
    }

}
