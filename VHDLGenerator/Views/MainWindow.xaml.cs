using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using VHDLGenerator.Models;
using Newtonsoft.Json;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using VHDLGenerator.Templates;
using VHDLGenerator.ViewModels;

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Private Variables
        /// <summary>
        /// Main Data Produced by the windows
        /// </summary>
        /// 
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

        //private Point startPoint;
        //private Rectangle rect;
        //private bool _loaded;
        #endregion

        #region Main Code

        #region Window Methods

        public MainWindow()
        {
            InitializeComponent();
            _dataPath = new DataPathModel();
            _debugPath = (string)System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);     //Path to the location of the executable
            CreateFolder();                                                                                     //Path to the location of the executable moved one folder up
            _id = 1;

            Btn_Component.IsEnabled = false;
            Btn_Signal.IsEnabled = false;
            Btn_Datapath.IsEnabled = true;

            //Data = new MainViewModel(DataPath);
            //this.DataContext = Data;
            //_loaded = false;
        }

        private void Btn_Datapath_Click(object sender, RoutedEventArgs e)
        {
            Window_Datapath window_Datapath = new Window_Datapath();

            if (window_Datapath.ShowDialog() == true)
            {
                try
                {
                    _dataPath = window_Datapath.GetDataPathModel;           //Gets the DataPath Object from the Datapath menu and passes it to _datapath

                    Btn_Component.IsEnabled = true;                         //Disables the Datapath button and enables the other buttons
                    Btn_Signal.IsEnabled = false;
                    Btn_Datapath.IsEnabled = false;
                    GenerateDatapath(_dataPath);                            //DataPath Code Generation

                    LoadFileTree();                                         //Loads text into the Project file tree view using info in _dataPath
                    LoadCodeTree();                                         //Loads generated code file names into the tree view using the _newfolderPath
                }
                catch (Exception) { }
            }
        }
        private void Btn_Component_Click(object sender, RoutedEventArgs e)
        {
            Window_Component window_Component = new Window_Component(_dataPath);     //Creates new instance of component window 
            ComponentModel model = new ComponentModel();

            if (window_Component.ShowDialog() == true)                      //Waits till the window is closed
            {
                try
                {
                    model = window_Component.GetComponentModel;             //Gets the component model produced by the component menu
                    model.ID = _id.ToString();                              //Sets the component ID to an int _id
                    _id++;                                                  //Increments _id everytime a component is created
                    //models.Add(model);
                    _dataPath.Components.Add(model);                       //Adds the Model data to _dataPath

                    GenerateDatapath(_dataPath);                            //Regenerates Datapath Code File
                    GenerateComponents(_dataPath);                          //Generates Component Code File

                    LoadFileTree();                                         //Loads text into the Project file tree view using info in _dataPath
                    LoadCodeTree();                                         //Loads generated code file names into the tree view using the _newfolderPath
                    Btn_Signal.IsEnabled = true;
                    #region Debug
                    //var newDP_ResultJSON = JsonConvert.SerializeObject(_dataPath, Formatting.Indented);
                    //File.WriteAllText(System.IO.Path.Combine(_newFolderPath, "DatapathJSON.txt"), newDP_ResultJSON);
                    #endregion
                }
                catch (Exception) { }
            }
        }
        private void Btn_Signal_Click(object sender, RoutedEventArgs e)
        {
            Window_Signal window_Signal = new Window_Signal(_dataPath);     //Creates a new Window instance upon button click and passes the _dataPath into it

            if (window_Signal.ShowDialog() == true)                         //Waits till the window is closed
            {
                try
                {
                    _dataPath.Signals.Add(window_Signal.GetSignalModel);    //Gets the Signal model from the signal Menu and addds it to the _dataPath

                    GenerateDatapath(_dataPath);                            //Regenerates the Datapath Code using the new data from the signal menu (Port Mapping)

                    LoadFileTree();                                         //Loads text into the Project file tree view using info in _dataPath
                    LoadCodeTree();                                         //Loads generated code file names into the tree view using the _newfolderPath

                    #region Debug
                    //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "SignalJSON.txt"), window_Signal.GetSignalJSON);
                    //var newDP_ResultJSON = JsonConvert.SerializeObject(_dataPath, Formatting.Indented);
                    //System.IO.File.WriteAllText(System.IO.Path.Combine(DebugPath, "newDatapathwsJSON.txt"), newDP_ResultJSON);
                    //File.WriteAllText(System.IO.Path.Combine(_newFolderPath, "DatapathJSON.txt"), newDP_ResultJSON);
                    #endregion
                }
                catch (Exception) { }
            }
        }

        private void Btn_OpenFile_Click(object sender, RoutedEventArgs e)
        {
            if (CodeTreeView.SelectedItem != null)                                  //Checks to see if anything is selected in the CodeTreeView
            {
                var item = CodeTreeView.SelectedItem as TreeViewData;               //Casts the selected item into a TreeViewData Class
                var itempath = System.IO.Path.Combine(_newFolderPath, item.Title);  //combines the item name to the folder path to create the path for the code file
                Process.Start(itempath);                                            //Opens the code file
            }
        }
        #endregion

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
                foreach (ComponentModel comp in Data.Components)                                            //Cycles through all components in data that was passed into the function
                {
                    ComponentTemplate CompTemplate = new ComponentTemplate(comp);                           //Creates an instance of the Component template using the comp passed into the function
                    String CompText = CompTemplate.TransformText();                                         //Generates the code into a string using the Component Template file
                    File.WriteAllText(System.IO.Path.Combine(_newFolderPath,comp.Name+ ".txt"),CompText);   //Combines the folder path and comp name to create the code file and writes the string to it
                }
            }
        }

        /// <summary>
        /// Automatically populates the Project File TreeView based on the data contained _dataPath
        /// </summary>
        public void LoadFileTree()
        {
            CustomTreeView.Items.Clear();                       //Clears all items from the existing TreeView

            TreeViewData maintv = new TreeViewData();

            if (_dataPath.Name != null)
            {
                TreeViewData tv = new TreeViewData();
                maintv.Title = _dataPath.Name;                  //Adds the main tree named after the datapath name                                               
            }

            if (_dataPath.Ports != null)
            {
                TreeViewData tv = new TreeViewData();
                tv.Title = "Ports";                             //Add section named ports
                foreach (PortModel port in _dataPath.Ports)
                {
                    TreeViewData tv1 = new TreeViewData();
                    tv1.Title = port.Name;                      //Add ports as children to the section
                    tv.Items.Add(tv1);
                }
                maintv.Items.Add(tv);
            }

            if (_dataPath.Components != null)
            {
                TreeViewData tv = new TreeViewData();
                tv.Title = "Components";                        //Add section named components
                foreach (ComponentModel comp in _dataPath.Components)
                {
                    TreeViewData tv1 = new TreeViewData();
                    tv1.Title = comp.Name;                      //Add components as children to the section
                    tv.Items.Add(tv1);
                }
                maintv.Items.Add(tv);
            }

            if (_dataPath.Signals != null)
            {
                TreeViewData tv = new TreeViewData();
                tv.Title = "Signal";                            //Add section named signalss
                maintv.Items.Add(tv);
            }

            CustomTreeView.Items.Add(maintv);
        }

        /// <summary>
        /// Automatically populates the Generated Code File TreeView based on what is contained in the Generated Code Folder
        /// </summary>
        public void LoadCodeTree()
        {
            CodeTreeView.Items.Clear();                                                         //Clears all items in the CodeTreeView

            List<string> FileNames = new List<string>(Directory.GetFiles(_newFolderPath));      //Gets all file names in the Generated Code Folder

            foreach (string Path in FileNames)
            {
                string FileName = "";
                Uri uri = new Uri(Path);                                                        //Gets the path of the item
                FileName = uri.Segments[uri.Segments.Length - 1];                               //Takes the last segment of the item URI (File Name)
                TreeViewData tv = new TreeViewData();
                tv.Title = FileName;                                                            //Adds the file name to the CodeTreeView
                CodeTreeView.Items.Add(tv);
            }
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

        #endregion
    }


}
