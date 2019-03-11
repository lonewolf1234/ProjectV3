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
using System.Text.RegularExpressions;
using VHDLGenerator.ViewModels;
using VHDLGenerator.Models;
using Newtonsoft.Json;

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for Window_Signal.xaml
    /// </summary>
    public partial class Window_Signal : Window
    {
        #region Properties
        public SignalModel GetSignalModel { get { return this._Data.GetSignal; } }
        #endregion

        #region Private Varibles
        private SignalViewModel _Data;
        #endregion

        #region Window Methods
        //constructor that accepts a DatapathModel object to be used in the combo Boxes
        public Window_Signal(DataPathModel _DataPath)
        {
            InitializeComponent();
            _Data = new SignalViewModel(_DataPath);          //creates an instance of the SignalViewModel and passes the datapath data from the window to the viewmodel
            this.DataContext = _Data;                        //Sets the Window DataContext to that of the SignalViewModel to allow for binding
        }

        private void Bus_CB_Checked(object sender, RoutedEventArgs e)
        {

        }
    
        private void Bus_CB_Unchecked(object sender, RoutedEventArgs e)
        {

        }

        private void MSB_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Validate_Int(e);        //if result is true it accepts the text being entered
        }

        private void LSB_TB_PreviewTextInput(object sender, TextCompositionEventArgs e)
        {
            e.Handled = Validate_Int(e);        //if result is true it accepts the text being entered
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();                       //Closes instance of window when Cancel is selected 
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;           //Set dialogResult to True to signify that data entry is finished
            this.Close();                       //Closes instance of window when Finish is selected
        }
        #endregion

        #region Validation Method
        //Checks to determinde if the text entered in the textbox is and Int (0 to 9)
        private bool Validate_Int(TextCompositionEventArgs e)
        {
            Regex regex = new Regex("[^0-9]+");
            return (regex.IsMatch(e.Text));         //if it matches 0 to 9 it returns true
        }
        #endregion

    }
}
