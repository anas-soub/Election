using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace ElctionWPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private System.Windows.Data.CollectionViewSource personViewSource;       

        private ElectionDb.Search _currentSearch=new ElectionDb.Search();
        public ElectionDb.Search CurrentSearch
        {
            get { return _currentSearch; }
            set
            {
                if (_currentSearch == value) return;

                _currentSearch = value;
                OnPropertyChanged("CurrentSearch");

            }
        }

            private string _searchText;

     
        public string SearchText
        {
            get { return _searchText; }
            set
            {
                if (_searchText == value) return;
                _searchText = value;

                OnPropertyChanged("SearchText");

            }
        }
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;

        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {

            personViewSource = ((CollectionViewSource)(this.FindResource("personViewSource")));
         
          
            SearchText = "انس";

           // personViewSource.Source = searchForName();




            // Load data by setting the CollectionViewSource.Source property:
            // searchViewSource.Source = [generic data source]
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
          

            personViewSource.Source = searchForName();
        }
        private ObservableCollection<ElectionDb.Person> searchForName()
        {

            try
            {
                ElectionDb.ElectionDb db = new ElectionDb.ElectionDb();
                db.FilterElctorByName(SearchText);
                db.FilterByGender("2");
                db.FilterByElectorByGovernate("الكرك");
                db.SortByAge(false);
                var results = db.GetResults();
                return new ObservableCollection<ElectionDb.Person>(results);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return null;
            }
        }
    }
}
