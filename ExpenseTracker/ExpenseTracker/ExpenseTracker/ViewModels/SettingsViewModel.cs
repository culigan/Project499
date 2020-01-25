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


      private DateTime _StartDate;
      public DateTime StartDate { get { return _StartDate; } set { _StartDate = value; OnPropertyChanged(nameof(StartDate)); } }

      private DateTime _EndDate;
      public DateTime EndDate { get { return _EndDate; } set { _EndDate = value; OnPropertyChanged(nameof(EndDate)); } }

      private string _CategoryName;
      public string CategoryName { get { return _CategoryName; } set { _CategoryName = value; OnPropertyChanged(nameof(CategoryName)); } }

      private string _CategoryDesc;
      public string CategoryDesc { get { return _CategoryDesc; } set { _CategoryDesc = value; OnPropertyChanged(nameof(CategoryDesc)); } }

      private int _NumberTrans;
      public int NumberTrans { get { return _NumberTrans; } set { _NumberTrans = value; OnPropertyChanged(nameof(NumberTrans)); } }

      private Exp_Inc_Category _CatSelected;
      public Exp_Inc_Category CatSelected { get { return _CatSelected; } set { _CatSelected = value; OnPropertyChanged(nameof(CatSelected)); } }

      private Exp_Inc_Category _CatSelectedI;
      public Exp_Inc_Category CatSelectedI { get { return _CatSelectedI; } set { _CatSelectedI = value; OnPropertyChanged(nameof(CatSelectedI)); } }

      private string _AddEditName = "";
      public string AddEditName { get { return _AddEditName; } set { _AddEditName = value; OnPropertyChanged(nameof(AddEditName)); } }

      private string _AddEditNameI = "";
      public string AddEditNameI { get { return _AddEditNameI; } set { _AddEditNameI = value; OnPropertyChanged(nameof(AddEditNameI)); } }

      private string _AddEditDesc = "";
      public string AddEditDesc { get { return _AddEditDesc; } set { _AddEditDesc = value; OnPropertyChanged(nameof(AddEditDesc)); } }

      private string _AddEditDescI = "";
      public string AddEditDescI { get { return _AddEditDescI; } set { _AddEditDescI = value; OnPropertyChanged(nameof(AddEditDescI)); } }

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
