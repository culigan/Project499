using System;
using System.Collections.Generic;
using System.Text;
using ExpenseTracker.Model;
using System.Security.Cryptography;
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace ExpenseTracker.ViewModels
{
    class NewUserViewModel : INotifyPropertyChanged
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

        private string _Firstname;
        public string Firstname
        {
            get { return _Firstname; }
            set
            {
                _Firstname = value;

                OnPropertyChanged(nameof(Firstname));
            }
        }

        private string _Lastname;
        public string Lastname
        {
            get { return _Lastname; }
            set
            {
                _Lastname = value;

                OnPropertyChanged(nameof(Lastname));
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

        private string _FirstPassword;
        public string FirstPasswordHash;
        public string FirstPassword
        {
            get { return _FirstPassword; }
            set
            {
                _FirstPassword = value;
                using (MD5 md5Hash = MD5.Create())
                {
                    FirstPasswordHash = HashClass.GetMd5Hash(md5Hash, value);
                }
                OnPropertyChanged(nameof(FirstPassword));
            }
        }

        private string _SecondPassword;
        public string SecondPasswordHash;
        public string SecondPassword
        {
            get { return _SecondPassword; }
            set
            {
                _SecondPassword = value;
                using (MD5 md5Hash = MD5.Create())
                {
                    SecondPasswordHash = HashClass.GetMd5Hash(md5Hash, value);
                }
                OnPropertyChanged(nameof(SecondPassword));
            }
        }

        public NewUserViewModel()
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
