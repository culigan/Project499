using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

using Xamarin.Essentials;

namespace ExpenseTracker.ViewModels
{
   public class SettingsViewModel : INotifyPropertyChanged
   {
      Model.DataQuery_Mod DataQuery;
      private ObservableCollection<Exp_Inc_Category> _Categories;
      public ObservableCollection<Exp_Inc_Category> Categories 
      { 
         get 
         {
            DataQuery.expenseSelect = "SELECT * FROM [dbo].[ExpenseCategory]";
            DataQuery.expenseWhere = "WHere User_ID = 40 or User_ID = " + Preferences.Get("ExpenseT_UserID", " -1");
            
            _Categories = DataQuery.ExecuteAQuery<Exp_Inc_Category>();
            
            return _Categories; 
         }
         set { _Categories = value; }
      }
      private ObservableCollection<Exp_Inc_Category> _CategoriesI;
      public ObservableCollection<Exp_Inc_Category> CategoriesI
      {
         get
         {
            DataQuery.expenseSelect = "SELECT * FROM [dbo].[IncomeCategory]";
            DataQuery.expenseWhere = "WHere User_ID = 40 or User_ID = " + Preferences.Get("ExpenseT_UserID", " -1");

            _Categories = DataQuery.ExecuteAQuery<Exp_Inc_Category>();

            return _CategoriesI;
         }
         set { _CategoriesI = value; }
      }


      public DateTime StartDate { 
         get 
         { 
            var dat = DateTime.Now;
            dat = dat.AddMonths(-1);
            return Preferences.Get("start_date", dat); 
         } 
         set 
         {
            Preferences.Set("start_date", value); 
            OnPropertyChanged(nameof(StartDate)); 
         } 
      }

      private DateTime _EndDate;
      public DateTime EndDate
      {
         get
         {
            var dat = DateTime.Now;
            return Preferences.Get("end_date", dat);
         }
         set
         {
            Preferences.Set("end_date", value);
            OnPropertyChanged(nameof(EndDate));
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
