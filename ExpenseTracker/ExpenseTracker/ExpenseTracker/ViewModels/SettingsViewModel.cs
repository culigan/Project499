using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace ExpenseTracker.ViewModels
{
   public class SettingsViewModel : INotifyPropertyChanged
   {
      private ObservableCollection<string> _Categories;
      public ObservableCollection<string> Categories { get { return _Categories; } set { _Categories = value; OnPropertyChanged(nameof(Categories)); } }
      
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

      private string _CatSelected;
      public string CatSelected { get { return _CatSelected; } set { _CatSelected = value; OnPropertyChanged(nameof(CatSelected)); } }

      public SettingsViewModel()
      {

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
