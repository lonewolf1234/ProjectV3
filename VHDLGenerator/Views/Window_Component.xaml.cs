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
using System.Windows.Shapes;
using VHDLGenerator.Models;
using VHDLGenerator.ViewModels;
using Newtonsoft.Json;

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for Window_Component.xaml
    /// Sets the DataContext of the Window to that of the Component ViewModel
    /// To allow for Bindings
    /// </summary>
    public partial class Window_Component : Window
    {
        #region Private Varible
        private ComponentViewModel Data;
        #endregion

        #region Properties
        //Allows for the MainWindow to retrive the Component Model created by the Component Menu
        public ComponentModel GetComponentModel { get { return this.Data.GetComponent; } }
        #endregion

        #region Methods
        //constructor
        public Window_Component(DataPathModel data)
        {
            InitializeComponent();
            Data = new ComponentViewModel(data);            //Creates an instance for the ComponentViewModel
            this.DataContext = Data;                    //Sets the DataContext of the this Window to that of the ComponentViewModel
                                                        //to allow for Binding of the VM properties to the XAML
        }

        private void AddPort_Click(object sender, RoutedEventArgs e)
        {
            
            //PortDataGrid.Items.Add(Data.GetPortData);   //Adds the Port created to the Datagrid 
            Data.AddPortSel = true;                     //Set the AddPortSel prop to true when the AddPort button is clicked

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();                               //Closes instance of window when Cancel is selected 
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;                   //Set dialogResult to True to signify that data entry is finished
            this.Close();                               //Closes instance of window when Finish is selected
        }

        private void EditPort_Click(object sender, RoutedEventArgs e)
        {

        }

        private void DeletePort_Click(object sender, RoutedEventArgs e)
        {

        }
        #endregion
    }
}
