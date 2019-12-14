using System;
using System.Collections.Generic;
using System.Text;
using ExpenseTracker.Model;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ExpenseTracker.ViewModels
{
    class AddItemViewModel : INotifyPropertyChanged
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

        public int Account_ID { get; set; }
        private ObservableCollection<Users> _AccountInfo;
        public ObservableCollection<Users> AccountInfo
        {
            get { return _AccountInfo; }
            set
            {
                _AccountInfo = value;
                OnPropertyChanged(nameof(AccountInfo));
            }
        }

        private string _TransType;
        public string TransType
        {
            get { return _TransType; }
            set
            {
                _TransType = value;

                OnPropertyChanged(nameof(TransType));
            }
        }

        private string _Account;
        public string Account
        {
            get { return _Account; }
            set
            {
                _Account = value;

                OnPropertyChanged(nameof(Account));
            }
        }

        private string _TransAmount;
        public string TransAmount
        {
            get { return _TransAmount; }
            set
            {
                _TransAmount = value;

                OnPropertyChanged(nameof(TransAmount));
            }
        }

        private string _TransName;
        public string TransName
        {
            get { return _TransName; }
            set
            {
                _TransName = value;

                OnPropertyChanged(nameof(TransName));
            }
        }

        public AddItemViewModel()
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

    }

}
