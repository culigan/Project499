using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Text;
using System.Runtime;
using System.Windows.Input;
using Xamarin.Forms;

namespace ExpenseTracker
{
    public class RunProgram : INotifyPropertyChanged
    {

      private ICommand _TestButton;
      public ICommand TestButton
      {
         get { return _TestButton; }
         set { _TestButton = value; OnPropertyChanged(); }
      }

      private ObservableCollection<ExpenseList> _ExpenseList;
      public ObservableCollection<ExpenseList> ItemList
      {
         get { return _ExpenseList; }
         set { _ExpenseList = value; OnPropertyChanged(); }
      }

      private string _ValueTemp = "Test";
      public string ValueTemp
      {
         get { return _ValueTemp; }
         set { _ValueTemp = value; }
      }
      public RunProgram()
      {
         

         ItemList = new ObservableCollection<ExpenseList>();

         ExpenseList expenseList = new ExpenseList();
         expenseList.TestValue1 = "testing1";
         expenseList.TestValue2 = "testing2";
         expenseList.TestValue3 = "testing3";
         expenseList.TestButton = new Command(ButtonClick);
         ItemList.Add(expenseList);
         ItemList.Add(expenseList);
         ItemList.Add(expenseList);
         ItemList.Add(expenseList);
      }
      public RunProgram(List<string> DataValue)
      {
         ItemList = new ObservableCollection<ExpenseList>();

         foreach (string s in DataValue)
         {
            ExpenseList expenseList = new ExpenseList();
            string[] userInfo = s.Split(',');
            for (int i = 0; i < 3; i++)
            {
               switch (i) {
                  case 0:
                     expenseList.TestValue1 = userInfo[i];
                     break;
                  case 1:
                     expenseList.TestValue2 = userInfo[i];
                     break;
                  case 2:
                     expenseList.TestValue3 = userInfo[i];
                     break;
                  default:
                     break;
                    
            }
            }
            ItemList.Add(expenseList);
         }
      }

      public void ButtonClick()
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
