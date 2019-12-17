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
        public int Account_ID { get; set; }

      private bool _InAccVisible;
      public bool InAccVisible
      {
         get { return _InAccVisible; }
         set { _InAccVisible = value; OnPropertyChanged(nameof(InAccVisible)); }
      }

      private string _TransName;
      public string TransName
      {
         get { return _TransName; }
         set { _TransName = value; OnPropertyChanged(nameof(TransName)); }
      }

      private ObservableCollection<string> _IncomeAccountList;
      public ObservableCollection<string> IncomeAccountList
      {
         get { return _IncomeAccountList; }
         set { _IncomeAccountList = value; OnPropertyChanged(nameof(IncomeAccountList)); }
      }
      
      private string _IncomeAccount;
      public string IncomeAccount
      {
         get { return _IncomeAccount; }
         set { _IncomeAccount = value; OnPropertyChanged(nameof(IncomeAccount)); }
      }

        private string _AccountType;
        public string AccountType
        {
            get { return _AccountType; }
            set
            {
            _AccountType = value;

                OnPropertyChanged(nameof(AccountType));
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

      private ObservableCollection<string> _CategoryList;
      public ObservableCollection<string> CategoryList
      {
         get { return _CategoryList; }
         set
         {
            _CategoryList = value;

            OnPropertyChanged(nameof(CategoryList));
         }
      }

      private string _Category;
      public string Category
      {
         get { return _Category; }
         set
         {
            _Category = value;

            OnPropertyChanged(nameof(Category));
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
