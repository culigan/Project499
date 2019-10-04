using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;

namespace ExpenseTracker
{
    public class RunProgram : INotifyPropertyChanged
    {
      ObservableCollection<ExpenseList> _ExpenseList;
      ObservableCollection<ExpenseList> ItemList
      {
         get { return _ExpenseList; }
         set { _ExpenseList = value; }
      }

      private string _ValueTemp = "Test";
      public string ValueTemp
      {
         get { return _ValueTemp; }
         set { _ValueTemp = value; OnPropertyChanged("ItemList"); }
      }
      public RunProgram()
      {
         ItemList = new ObservableCollection<ExpenseList>();

         ExpenseList expenseList = new ExpenseList();
         expenseList.TestValue1 = "testing1";
         expenseList.TestValue2 = "testing2";
         expenseList.TestValue3 = "testing3";
         ItemList.Add(expenseList);
         ItemList.Add(expenseList);
         ItemList.Add(expenseList);
         ItemList.Add(expenseList);
      }

      public event PropertyChangedEventHandler PropertyChanged;

      protected void OnPropertyChanged(string name)
      {
         PropertyChangedEventHandler handler = PropertyChanged;
         if (handler != null)
         {
            handler(this, new PropertyChangedEventArgs(name));
         }
      }

   }
}
