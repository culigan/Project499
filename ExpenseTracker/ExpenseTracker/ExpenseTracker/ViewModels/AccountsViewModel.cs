using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace ExpenseTracker.ViewModels
{
   public class AccountsViewModel : INotifyPropertyChanged
   {

      private string _Title = "Accounts";
      public string Title
      {
         get { return _Title; }
         set { _Title = value; OnPropertyChanged(nameof(Title)); }
      }
      public AccountsViewModel()
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
