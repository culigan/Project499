using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Text;
using System.Security.Cryptography;
using ExpenseTracker.Model;

namespace ExpenseTracker.ViewModels
{
   public class LoginViewModel : INotifyPropertyChanged
   {
      
      public DataQuery_Mod DataQuery;
      private bool _IsBusy = false;
      public bool IsBusy { get { return _IsBusy; } set { _IsBusy = value; } }
      public int User_ID { get; set; }
      private string _Username;
      public string Username { get { return _Username; }  set { _Username = value; OnPropertyChanged(nameof(Username)); } }
      private string _Password;
      public string Password 
      { 
         get { return _Password; } 
         set 
         { 
            using (MD5 md5Hash = MD5.Create())
            {
               _Password = HashClass.GetMd5Hash(md5Hash, value);               
            }
            OnPropertyChanged(nameof(Password)); } }
      public LoginViewModel()
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
