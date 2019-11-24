using System;
using System.Collections.Generic;
using System.Text;
using ExpenseTracker.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ExpenseTracker.ViewModels
{
    class AddAccountViewModel : INotifyPropertyChanged
    {
        public DataQuery_Mod DataQuery;
        private bool _IsBusy = false;
        public bool IsBusy { get { return _IsBusy; } set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); } }
        public int User_ID { get; set; }
        private ObservableCollection<Users> _UsersInfo;
        public ObservableCollection<Users> UsersInfo
        {
            get { return _UsersInfo; }
            set
            {
                _UsersInfo = value;
                OnPropertyChanged(nameof(UsersInfo));
            }
        }

        public AddAccountViewModel()
        {
            DataQuery = new DataQuery_Mod();
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([System.Runtime.CompilerServices.CallerMemberName] string name = "")
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(name));
            }
        }

        private string _Username;
        public string Username
        {
            get { return _Username; }
            set
            {
                _Username = value;

                OnPropertyChanged(nameof(Username));
            }
        }
    }
}
