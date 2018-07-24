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

namespace ElctionWPF
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {

        
        public ElectionDb.ElectionViewModel ElectionViewModel { get; set; }

     
        public Window1()
        {
            InitializeComponent();
        }

        private void window_Loaded(object sender, RoutedEventArgs e)
        {
            ElectionViewModel = new ElectionDb.ElectionViewModel();
            DataContext = ElectionViewModel;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
           
            ElectionViewModel.Update();
          
                      

        }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }
    }
}
