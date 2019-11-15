using System;
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Text;
using System.Security.Cryptography;
using ExpenseTracker.Model;

namespace ExpenseTracker.ViewModels
{
   public class LoginViewModel : INotifyPropertyChanged
   {
      
      public DataQuery_Mod DataQuery;
      private bool _IsBusy = false;
      public bool IsBusy { get { return _IsBusy; } set { _IsBusy = value; OnPropertyChanged(nameof(IsBusy)); } }
      public int User_ID { get; set; }
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

      private string _Username;
      public string Username
      {
         get { return _Username; }
         set
         {
            _Username = value;
            
            OnPropertyChanged(nameof(Username));
         }
      }
      public string PasswordHash;
      private string _Password;
      public string Password 
      { 
         get { return _Password; } 
         set 
         {
            _Password = value;
            using (MD5 md5Hash = MD5.Create())
            {
               PasswordHash = HashClass.GetMd5Hash(md5Hash, value);               
            }
            OnPropertyChanged(nameof(Password)); } 
      }
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
