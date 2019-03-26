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

namespace VHDLGenerator.Views
{
    /// <summary>
    /// Interaction logic for Window_CopyComp.xaml
    /// </summary>
    public partial class CopyCompView : Window
    {
        CopyCompViewModel model;

        public CopyCompView(DataPathModel data)
        {
            InitializeComponent();
            model = new CopyCompViewModel(data);
            this.DataContext = model;

        }

        public ComponentModel GetCompCopy { get { return this.model.GetComponent; } }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();                               //Closes instance of window when Cancel is selected 
        }

        private void Finish_Click(object sender, RoutedEventArgs e)
        {
            this.DialogResult = true;                   //Set dialogResult to True to signify that data entry is finished
            this.Close();                               //Closes instance of window when Finish is selected
        }
    }
}
