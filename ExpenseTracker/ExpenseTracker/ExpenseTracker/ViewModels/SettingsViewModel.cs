﻿using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Essentials;
using Xamarin.Forms;

namespace ExpenseTracker.ViewModels
{
   public class SettingsViewModel : INotifyPropertyChanged
   {
        // Model.DataQuery_Mod DataQuery;
        public Model.DataQuery_Mod DataQuery;
        private bool _IsBusy = false;
      public bool IsBusy { get { return _IsBusy; } set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); } }

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

        private ObservableCollection<Exp_Inc_Category> _ExpenseCategoryInfo;
        public ObservableCollection<Exp_Inc_Category> ExpenseCategoryInfo
        {
            get { return _ExpenseCategoryInfo; }
            set
            {
                _ExpenseCategoryInfo = value;
                OnPropertyChanged(nameof(ExpenseCategoryInfo));
            }
        }

        private ObservableCollection<Exp_Inc_Category> _Categories;
      public ObservableCollection<Exp_Inc_Category> Categories 
      { 
         get 
         {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
               DataQuery.expenseSelect = "SELECT * FROM [dbo].[ExpenseCategory]";
               DataQuery.expenseWhere = "WHere User_ID = 40 or User_ID = " + Preferences.Get("ExpenseT_UserID", " -1");

               _Categories = DataQuery.ExecuteAQuery<Exp_Inc_Category>();
            }
            else
            {
               DependencyService.Get<IToast>().Show("No Internet Connection.");
               _Categories = new ObservableCollection<Exp_Inc_Category>();
            }
            return _Categories; 
         }
         set { _Categories = value; }
      }
      private ObservableCollection<Exp_Inc_Category> _CategoriesI;
      public ObservableCollection<Exp_Inc_Category> CategoriesI
      {
         get
         {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
               DataQuery.expenseSelect = "SELECT * FROM [dbo].[IncomeCategory]";
               DataQuery.expenseWhere = "WHere User_ID = 40 or User_ID = " + Preferences.Get("ExpenseT_UserID", " -1");

               _Categories = DataQuery.ExecuteAQuery<Exp_Inc_Category>();
            }
            else
            {
               DependencyService.Get<IToast>().Show("No Internet Connection.");
               _CategoriesI = new ObservableCollection<Exp_Inc_Category>();
            }
            return _CategoriesI;
         }
         set { _CategoriesI = value; }
      }


      public DateTime PickerStartDate { 
         get 
         { 
            var dat = DateTime.Now;
            dat = dat.AddMonths(-1);            
            return Preferences.Get("start_date", dat); 
         } 
         set 
         {
            if (value.ToString("d") == DateTime.Now.ToString("d"))
               Preferences.Remove("start_date");
            else
               Preferences.Set("start_date", value); 
            OnPropertyChanged(nameof(PickerStartDate)); 
         } 
      }

      
      public DateTime PickerEndDate
      {
         get
         {
            var dat = DateTime.Now;
            return Preferences.Get("end_date", dat);
         }
         set
         {
            if (value.ToString("d") == DateTime.Now.ToString("d"))
               Preferences.Remove("end_date");
            else
               Preferences.Set("end_date", value);
            OnPropertyChanged(nameof(PickerEndDate));
         }
      }

        private string _CategoryName;
        public string CategoryName
        {
            get { return _CategoryName; }
            set
            {
                _CategoryName = value;

                OnPropertyChanged(nameof(CategoryName));
            }
        }

        private string _CategoryPicker;
        public string CategoryPicker
        {
            get { return _CategoryPicker; }
            set
            {
                _CategoryPicker = value;

                OnPropertyChanged(nameof(CategoryPicker));
            }
        }


        public int NumberTrans { get { return Preferences.Get("trans_num", 1000); } set { Preferences.Set("trans_num", value); OnPropertyChanged(nameof(NumberTrans)); } }

      
      public SettingsViewModel()
      {
         DataQuery = new Model.DataQuery_Mod();
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
