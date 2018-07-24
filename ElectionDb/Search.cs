using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ElectionDb
{
    public class Search : INotifyPropertyChanged
    {
        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                if (value == name)
                    return;

                name = value;
                OnPropertyChanged("Name");
            }
        }
        private Gender gender;

        public Gender Gender
        {
            get { return gender; }
            set
            {
                if (value == gender) return;
                gender = value;
                OnPropertyChanged("Gender");
            }
        }
        public Search()
        {
            gender = Gender.NOT_SET;
        }

        public event PropertyChangedEventHandler PropertyChanged;


        void OnPropertyChanged(string name)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(name));
        }

    }
}
