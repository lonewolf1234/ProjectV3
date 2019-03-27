using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Shapes;
using VHDLGenerator.Models;
using VHDLGenerator.Templates;
using VHDLGenerator.ViewModels;
using VHDLGenerator.ViewModels.Commands;
using VHDLGenerator.Views;

namespace VHDLGenerator.ViewModels
{
    class MainViewModel : INotifyPropertyChanged
    {
        #region Private
        private DataPathModel _dataPath;
        /// <summary>
        /// Path Location of the Debug Folder in Visual Studio IDE
        /// </summary>
        private string _debugPath;

        /// <summary>
        /// Path Loaction of the Generated Code Folder
        /// </summary>
        private string _newFolderPath;

        /// <summary>
        /// ID that is assigned on the creation of a new component. It is used in the Port Mapping Process
        /// </summary>
        private int _id;

        private List<PointData> DataPoints;

        private bool _DatapathEnable;
        private bool _CompEnable;
        private bool _CopyCompEnable;
        private bool _SignalEnable;

        private ObservableCollection<TreeViewItem> _ProjectChildren;
        private ObservableCollection<TreeViewItem> _CodeChildren;
        #endregion

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
            _dataPath = new DataPathModel();
            OpenDatapathViewCommand = new OpenCommand(Btn_Datapath_Click);

            _debugPath = (string)System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);     //Path to the location of the executable
            CreateFolder();                                                                                     //Path to the location of the executable moved one folder up
            _id = 1;

            this.DatapathEnable = true;
            this.CompEnable = false;
            this.CopyCompEnable = false;
            this.SignalEnable = false;

            this.ProjectChildren = new ObservableCollection<TreeViewItem>();
            this._CodeChildren = new ObservableCollection<TreeViewItem>();

            DataPoints = new List<PointData>();
        }

        #region Button Enable Properties
        public bool DatapathEnable
        {
            get { return _DatapathEnable; }
            private set { _DatapathEnable = value; OnPropertyChanged("DatapathEnable"); }
        }
        public bool CompEnable
        {
            get { return _CompEnable; }
            private set { _CompEnable = value; OnPropertyChanged("CompEnable"); }
        }
        public bool CopyCompEnable
        {
            get { return _CopyCompEnable; }
            private set { _CopyCompEnable = value; OnPropertyChanged("CopyCompEnable"); }
        }
        public bool SignalEnable
        {
            get { return _SignalEnable; }
            private set { _SignalEnable = value; OnPropertyChanged("SignalEnable"); }
        }
        #endregion

        #region
        public ObservableCollection<TreeViewItem> ProjectChildren
        {
            get { return _ProjectChildren; }
            private set { _ProjectChildren = value; OnPropertyChanged("ProjectChildren"); }
        }

        public ObservableCollection<TreeViewItem> CodeChildren
        {
            get { return _CodeChildren; }
            private set { _CodeChildren = value; OnPropertyChanged("CodeChildren"); }
        }
        #endregion

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

                    this.DatapathEnable = false;
                    this.CompEnable = true;
                    this.CopyCompEnable = true;
                    this.SignalEnable = true;
                   
                    GenerateDatapath(_dataPath);                            //DataPath Code Generation

                    LoadFileTree(_dataPath);                                         //Loads text into the Project file tree view using info in _dataPath
                    LoadCodeTree(_dataPath);                                         //Loads generated code file names into the tree view using the _newfolderPath

                    //Canvas canvas = new Canvas();
                    //canvas = this.DrawingCanvas;

                    //datapoints = DrawDatapath(_dataPath, canvas);
                    //foreach (PointData data in datapoints)
                    //{
                    //    DataPoints.Add(data);
                    //}
                }
                catch (Exception) { }
            }
        }
        #endregion

        public List<PointData> DrawDatapath(DataPathModel _data, Canvas _canvas)
        {
            List<PointData> pointDatas = new List<PointData>();

            Point startpoint = new Point(90, 30);
            Rectangle Datapath = new Rectangle()
            {
                Stroke = Brushes.PaleVioletRed,
                StrokeThickness = 2,
                Width = _canvas.ActualWidth - startpoint.X * 2,
                Height = _canvas.ActualHeight - startpoint.Y * 2
            };


            Canvas.SetTop(Datapath, startpoint.Y);
            Canvas.SetLeft(Datapath, startpoint.X);
            _canvas.Children.Add(Datapath);


            TextBlock DatapathName = new TextBlock()
            {
                Text = _data.Name,
                FontSize = 20
            };
            Point NamePoint = new Point((Datapath.Width / 2) - 50, 2);
            Canvas.SetTop(DatapathName, NamePoint.Y);
            Canvas.SetLeft(DatapathName, NamePoint.X);
            _canvas.Children.Add(DatapathName);

            #region addtion of port labels
            int counterin = 1;
            int counterout = 1;

            foreach (PortModel port in _data.Ports)
            {
                if (port.Direction == "in")
                {
                    TextBlock PortName = new TextBlock()
                    {
                        Text = port.Name
                    };
                    Point PortPoint = new Point(startpoint.X - (PortName.Text.Length * 5) - 10, (startpoint.Y * counterin) + 20);

                    Canvas.SetTop(PortName, PortPoint.Y);
                    Canvas.SetLeft(PortName, PortPoint.X);

                    PortPoint.X = startpoint.X;

                    PointData pointData = new PointData(null, _data.Name, port.Name, PortPoint);
                    pointDatas.Add(pointData);

                    _canvas.Children.Add(PortName);
                    counterin++;
                }
                else
                {
                    Point PortPoint = new Point(startpoint.X + Datapath.Width, (startpoint.Y * counterout) + 20);
                    Label PortName = new Label()
                    {
                        Content = port.Name
                    };
                    Canvas.SetTop(PortName, PortPoint.Y);
                    Canvas.SetLeft(PortName, PortPoint.X);

                    PointData pointData = new PointData(null, _data.Name, port.Name, PortPoint);
                    pointDatas.Add(pointData);

                    _canvas.Children.Add(PortName);
                    counterout++;
                }
            }
            #endregion 
            return pointDatas;
        }

        public List<PointData> DrawComponents(DataPathModel _data, Canvas _canvas)
        {
            List<PointData> pointDatas = new List<PointData>();

            Point[] StartPoints = new Point[6];
            Point StartPoint = new Point(90, 30);

            StartPoints[0] = new Point(StartPoint.X + 100, StartPoint.Y + 100);
            for (int i = 1; i < 6; i++)
            {
                if (i < 3)
                    StartPoints[i] = new Point(StartPoints[i - 1].X + 380, StartPoints[i - 1].Y);
                else
                    StartPoints[i] = new Point(StartPoints[i - 3].X, StartPoints[i - 3].Y + 300);
            }

            for (int i = 0; i < _data.Components.Count; i++)
            {

                Rectangle Component = new Rectangle()
                {
                    Stroke = Brushes.Blue,
                    StrokeThickness = 2,
                    Width = 220,
                    Height = 150
                };


                Canvas.SetTop(Component, StartPoints[i].Y);
                Canvas.SetLeft(Component, StartPoints[i].X);
                _canvas.Children.Add(Component);


                Label ComponentName = new Label()
                {
                    Content = _data.Components[i].Name,
                    FontSize = 20
                };
                Point NamePoint = new Point(((Component.Width / 2) + StartPoints[i].X) - 50, StartPoints[i].Y - 30);
                Canvas.SetTop(ComponentName, NamePoint.Y);
                Canvas.SetLeft(ComponentName, NamePoint.X);
                _canvas.Children.Add(ComponentName);

                #region addtion of port labels
                int counterin = 1;
                int counterout = 1;

                foreach (PortModel port in _data.Components[i].Ports)
                {
                    if (port.Direction == "in")
                    {
                        TextBlock PortName = new TextBlock()
                        {
                            Text = port.Name
                        };

                        Point PortPoint = new Point(StartPoints[i].X + 5, StartPoints[i].Y + (counterin * 20));

                        Canvas.SetTop(PortName, PortPoint.Y);
                        Canvas.SetLeft(PortName, PortPoint.X);

                        PortPoint.X = StartPoints[i].X;
                        PointData pointData = new PointData(_data.Components[i].ID, _data.Components[i].Name, port.Name, PortPoint);
                        pointDatas.Add(pointData);

                        _canvas.Children.Add(PortName);
                        counterin++;
                    }
                    else
                    {
                        TextBlock PortName = new TextBlock()
                        {
                            Text = port.Name
                        };
                        Point PortPoint = new Point(StartPoints[i].X + Component.Width - (PortName.Text.Length * 5) - 10, StartPoints[i].Y + (counterout * 20));

                        Canvas.SetTop(PortName, PortPoint.Y);
                        Canvas.SetLeft(PortName, PortPoint.X);

                        PortPoint.X = StartPoints[i].X + Component.Width;
                        PointData pointData = new PointData(_data.Components[i].ID, _data.Components[i].Name, port.Name, PortPoint);
                        pointDatas.Add(pointData);

                        _canvas.Children.Add(PortName);
                        counterout++;
                    }
                }
                #endregion
            }
            return pointDatas;

        }

        public void DrawSignals(DataPathModel _data, Canvas _canvas, List<PointData> ConnectionPoints)
        {
            foreach (SignalModel signal in _data.Signals)
            {
                Point ConnectionStart = new Point();
                Point ConnectionEnd = new Point();

                ConnectionStart = ConnectionPoints.Find(x => x.ComponentID == signal.Source_Comp_ID && x.PortName == signal.Source_port).Point;
                ConnectionEnd = ConnectionPoints.Find(x => x.ComponentID == signal.Target_Comp_ID && x.PortName == signal.Target_port).Point;

                Line line = new Line()
                {
                    X1 = ConnectionStart.X,
                    Y1 = ConnectionStart.Y,
                    X2 = ConnectionEnd.X,
                    Y2 = ConnectionEnd.Y,
                    Stroke = Brushes.DarkMagenta,
                    StrokeThickness = 1
                };

                _canvas.Children.Add(line);
            }
        }


        #region Other Methods

        /// <summary>
        /// Generates VHDL Code using a DataPathModel and DataPathTemplate 
        /// </summary>
        private void GenerateDatapath(DataPathModel Data)
        {
            try
            {
                if (Data != null && Data.Name != null)
                {
                    DataPathTemplate DPTemplate = new DataPathTemplate(Data);                               //Creates an instance of the datapath templates using the Data passed into the function
                    String DPText = DPTemplate.TransformText();                                             //Generates the code into a string using the DataPath Template file

                    File.WriteAllText(System.IO.Path.Combine(_newFolderPath, Data.Name + ".txt"), DPText);  //Combines the folder path and datapath name to create the code file and writes the string to it
                }
            }
            catch (Exception) { }
        }

        /// <summary>
        /// Generates VHDL Code using the ComponentModel List in DataPathModel and ComponentTemplate
        /// </summary>
        public void GenerateComponents(DataPathModel Data)
        {
            if (Data.Components != null)
            {
                List<string> generated = new List<string>();

                foreach (ComponentModel comp in Data.Components)                                            //Cycles through all components in data that was passed into the function
                {
                    if (!generated.Exists(x => x == comp.Name))
                    {
                        ComponentTemplate CompTemplate = new ComponentTemplate(comp);                           //Creates an instance of the Component template using the comp passed into the function
                        String CompText = CompTemplate.TransformText();                                         //Generates the code into a string using the Component Template file
                        File.WriteAllText(System.IO.Path.Combine(_newFolderPath, comp.Name + ".txt"), CompText);   //Combines the folder path and comp name to create the code file and writes the string to it
                        generated.Add(comp.Name);
                    }
                }
            }
        }

        /// <summary>
        /// Automatically populates the Project File TreeView based on the data contained _dataPath
        /// </summary>
        public void LoadFileTree(DataPathModel data)
        {
            //CustomTreeView.Items.Clear();                       //Clears all items from the existing TreeView

            //TreeViewData maintv = new TreeViewData();
            //if (data.Name != null)
            //{
            //    TreeViewData tv = new TreeViewData();
            //    maintv.Title = data.Name;                  //Adds the main tree named after the datapath name                                               
            //}

            TreeViewItem mainitem = new TreeViewItem();
            if(data.Name != null)
            {
                mainitem.Header = data.Name;
            }

            //if (data.Components != null)
            //{
            //    //TreeViewData tv = new TreeViewData();
            //    //tv.Title = "Components";                        //Add section named components
            //    foreach (ComponentModel comp in data.Components)
            //    {
            //        TreeViewData tv1 = new TreeViewData();
            //        tv1.Title = "cop" + comp.ID + ": " + comp.Name;                      //Add components as children to the section
            //        maintv.Items.Add(tv1);
            //    }
            //    //maintv.Items.Add(tv);
            //}

            if(data.Components.Count > 0)
            {
                foreach(ComponentModel comp in data.Components)
                {
                    TreeViewItem item = new TreeViewItem();
                    item.Header = "cop" + comp.ID + ": " + comp.Name;                      //Add components as children to the section
                    mainitem.Items.Add(item);
                }
            }


            //CustomTreeView.Items.Add(maintv);
            ProjectChildren.Add(mainitem);

           
        }

        /// <summary>
        /// Automatically populates the Generated Code File TreeView based on what is contained in the Generated Code Folder
        /// </summary>
        public void LoadCodeTree(DataPathModel data)
        {
            //CodeTreeView.Items.Clear();                                                         //Clears all items in the CodeTreeView

            List<string> PathNames = new List<string>(Directory.GetFiles(_newFolderPath));      //Gets all file names in the Generated Code Folder
            List<string> FileNames = new List<string>();
            FileNames = PathtoName(PathNames);

            if (data.Name != null)
            {
                TreeViewItem DataPathtv = new TreeViewItem()
                {
                    Header = "Datapath"
                };

                foreach (string name in FileNames)
                {
                    if (name == data.Name + ".txt")
                    {
                        TreeViewItem tv = new TreeViewItem()
                        {
                            Header = name
                        };
                        DataPathtv.Items.Add(tv);
                    }
                }

                CodeChildren.Add(DataPathtv);
            }

            if (data.Components.Count > 0)
            {
                TreeViewItem Componenttv = new TreeViewItem()
                {
                    Header = "Components"
                };

                foreach (string name in FileNames)
                {
                    if (name != data.Name + ".txt")
                    {
                        TreeViewItem tv = new TreeViewItem()
                        {
                            Header = name
                        };
                        Componenttv.Items.Add(tv);
                    }
                }
                CodeChildren.Add(Componenttv);
            }

            //foreach (string Path in FileNames)
            //{
            //    string FileName = "";
            //    Uri uri = new Uri(Path);                                                        //Gets the path of the item
            //    FileName = uri.Segments[uri.Segments.Length - 1];                               //Takes the last segment of the item URI (File Name)
            //    TreeViewData tv = new TreeViewData();
            //    tv.Title = FileName;                                                            //Adds the file name to the CodeTreeView
            //    CodeTreeView.Items.Add(tv);
            //}
        }

        public List<string> PathtoName(List<string> paths)
        {
            List<string> Names = new List<string>();

            if (paths != null)
            {
                foreach (string Path in paths)
                {
                    string FileName = "";
                    Uri uri = new Uri(Path);                                                        //Gets the path of the item
                    FileName = uri.Segments[uri.Segments.Length - 1];                               //Takes the last segment of the item URI (File Name)
                    Names.Add(FileName);
                }
            }

            return Names;
        }

        /// <summary>
        /// Creates an empty folder to store the generated code outside the folder that contains the application's executable file
        /// </summary>
        public void CreateFolder()
        {
            string temp = "";
            string FolderPath = "";

            Uri uri = new Uri((string)System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location));   //get the path of the executable application file
            temp = uri.Segments[uri.Segments.Length - 1];                                                           //stores the last segment of the path
            FolderPath = _debugPath.Substring(0, _debugPath.Length - temp.Length - 1);                              //grabs everything in the path except the last segment and the backslash
            string pathString = System.IO.Path.Combine(FolderPath, "GeneratedCode");                                //combines the path with "Generated Code"
            _newFolderPath = pathString;                                                                            //Sets the _newFolderPath to that of the desired folder path

            if (Directory.Exists(pathString))                                                                       //checks to see if the folder "GenerateCode exists"
            {
                Directory.Delete(pathString, true);                                                                 //delets folder and creates a new empty one
                Directory.CreateDirectory(pathString);
            }
            else
                Directory.CreateDirectory(pathString);                                                              //creates a new folder if it doesnot exist
        }
        #endregion

    }

}
