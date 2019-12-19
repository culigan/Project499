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
      private string _AddNameButton = "Add";
      public string AddNameButton { get { return _AddNameButton; } set { _AddNameButton = value; OnPropertyChanged(nameof(AddNameButton)); } }
      public int User_ID { get; set; }
        private ObservableCollection<Account> _UsersInfo;
        public ObservableCollection<Account> UsersInfo
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

      private bool _PickerVisible;
      public bool PickerVisible
      {
         get { return _PickerVisible; }
         set
         {
            _PickerVisible = value;

            OnPropertyChanged(nameof(PickerVisible));
         }
      }

      private bool _EntryVisible;
      public bool EntryVisible
      {
         get { return _EntryVisible; }
         set
         {
            _EntryVisible = value;

            OnPropertyChanged(nameof(EntryVisible));
         }
      }

      private string _AccountNameP;
      public string AccountNameP
      {
         get { return _AccountNameP; }
         set
         {
            _AccountNameP = value;

            OnPropertyChanged(nameof(AccountNameP));
         }
      }

      private string _AccountTypePicker;
        public string AccountTypePicker
        {
            get { return _AccountTypePicker; }
            set
            {
                _AccountTypePicker = value;

                OnPropertyChanged(nameof(AccountTypePicker));
            }
        }

        private string _AccountName;
        public string AccountName
        {
            get { return _AccountName; }
            set
            {
                _AccountName = value;

                OnPropertyChanged(nameof(AccountName));
            }
        }

        private string _AccountDesc;
        public string AccountDesc
        {
            get { return _AccountDesc; }
            set
            {
                _AccountDesc = value;

                OnPropertyChanged(nameof(AccountDesc));
            }
        }
    }
}
