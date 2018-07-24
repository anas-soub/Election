using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionDb
{
    public class ElectionViewModel: INotifyPropertyChanged
    {
       

        private PersonDetails personDetails;

        public PersonDetails PersonDetails
        {
            get { return personDetails; }
            set {
                if (personDetails == value) return;
                personDetails = value;
                OnPropertyChanged("PersonDetails");
            }
        }

        private Person person;

        public Person Person
        {
            get { return person; }
            set
            {
                if (person == value) return;
                person = value;
                PersonDetails = new PersonDetails(person);
                OnPropertyChanged("Person");
                
            }
        }


        private Search _currentSearch = new Search();
        public Search CurrentSearch
        {
            get { return _currentSearch; }
            set
            {
                if (_currentSearch == value) return;

                _currentSearch = value;
                OnPropertyChanged("CurrentSearch");

            }
        }

        private string searchInfo;

        public string SearchInfo
        {
            get { return searchInfo; }
            set
            {
                if (searchInfo == value) return;
                searchInfo = value;
                OnPropertyChanged("SearchInfo");
            }
        }

        private ObservableCollection<Person> currentPersonsList;     

        public ObservableCollection<Person> CurrentPersonsList
        {
            get { return currentPersonsList; }
            set
            {
                if (currentPersonsList == value) return;

                currentPersonsList = value;
                OnPropertyChanged("CurrentPersonsList");
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public ElectionViewModel()
        {
            DateTime dt1 = DateTime.Now;
            //try
            //{
            ElectionDb db = new ElectionDb();
                db.FilterElctorByName("انس");
                db.FilterByGender("2");
                db.FilterByElectorByGovernate("الكرك");
                db.SortByAge(false);
            var results = db.GetResults();
                CurrentPersonsList = new ObservableCollection<Person>(results);
            DateTime dt2 = DateTime.Now;
            double diff = dt2.Ticks - dt1.Ticks;
            double ms = Math.Round( diff / 10000.0,2);
            SearchInfo = string.Format("{0} Records in {1} ms", CurrentPersonsList.Count, ms) ;
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message);
            //    CurrentPersonsList= null;
            //}

        }
        public void Update()
        {
            //try
            //{
            DateTime dt1 = DateTime.Now;
            ElectionDb db = new ElectionDb();
                db.FilterElctorByName(CurrentSearch.Name);
                if (CurrentSearch.Gender != Gender.NOT_SET)
                {
                    string g = CurrentSearch.Gender == Gender.Mail ? "1" : "2";
                    db.FilterByGender(g);
                }
                db.FilterByElectorByGovernate("الكرك");
                db.SortByAge(false);
                var results = db.GetResults();
                CurrentPersonsList = new ObservableCollection<Person>(results);
            DateTime dt2 = DateTime.Now;
            double diff = dt2.Ticks - dt1.Ticks;
            double seconds = Math.Round(diff / 10000000.0, 2);
            SearchInfo = string.Format("{0} Records in   {1} s", CurrentPersonsList.Count, seconds);
            //}
            //catch (Exception ex)
            //{
            //    System.Windows.MessageBox.Show(ex.Message);
            //    CurrentPersonsList = null;
            //}
        }
        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null) PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }


    }

    public enum Gender
    {
        [Description(" ")]
        NOT_SET = 0,

        [Description("ذكر")]
        Mail,

        [Description("انثى")]
        Femail
    }
}
