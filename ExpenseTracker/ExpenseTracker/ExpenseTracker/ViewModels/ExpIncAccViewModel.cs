using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
//using Plugin.Toast;
using Newtonsoft.Json;
using Xamarin.Essentials;
using Xamarin.Forms;


namespace ExpenseTracker.ViewModels
{
   public class ExpIncAccViewModel : INotifyPropertyChanged
   {
      Model.DataQuery_Mod DataQuery;
      private ObservableCollection<Account> _ItemListA;
      public ObservableCollection<Account> ItemListA
      {
         get 
         {
            try
            {
               string idString = "";
               DataQuery.expenseSelect = "SELECT acc.[ID], [AccountType_ID], [AccountName], [Description], [DateCreated], [User_ID], " +
                        "(SELECT ExpenseAmount FROM[dbo].[Expense] where ID = 9) as AccountBalance FROM [dbo].[Account] acc inner join " + 
                        "AccountType acct on acc.AccountType_ID = acct.ID ";
               DataQuery.expenseWhere = "WHere User_ID = " + Preferences.Get("ExpenseT_UserID", " - 1");

               if (accountType.ToUpper() == "EXPENSEACCOUNT")
               {
                  DataQuery.expenseWhere = " and acc.AccountType_ID = 2";
                  idString = "Select SUM(ExpenseAmount) From Expense ";
               }
               else if (accountType.ToUpper() == "INCOMEACCOUNT")
               {
                  DataQuery.expenseWhere = " and acc.AccountType_ID = 1";
                  idString = "Select SUM(IncomeAmount) From Income ";
               }

               _ItemListA =  DataQuery.ExecuteAQuery<Account>();

               string[] item = new string[_ItemListA.Count];
               for(int i = 0; i < _ItemListA.Count; i++)
               {
                  DataQuery.expenseWhere = "Where Account_id = " + _ItemListA[i].ID;
                  DataQuery.expenseSelect = idString;
                  _ItemListA[i].AccountBalance = double.Parse(DataQuery.ExecuteAQuery());

               }

               return _ItemListA;
            }
            catch(Exception ex)
            {
               //if(Xamarin.Forms.Device.RuntimePlatform != "UWP")
              //    CrossToastPopUp.Current.ShowToastMessage(ex.Message);
               DependencyService.Get<IToast>().Show(ex.Message);

               return null;
               
            }
         }
         set { _ItemListA = value; OnPropertyChanged(nameof(ItemListA)); }
      }

      private ObservableCollection<ExpenseEntry> _ItemListE;
      public ObservableCollection<ExpenseEntry> ItemListE
      {
         get
         {
            try
            {
               if (accountType.ToUpper() == "EXPENSEACCOUNT")
               {
                  DataQuery.expenseWhere = " and acc.AccountType_ID = 2";
               }
               else if (accountType.ToUpper() == "INCOMEACCOUNT")
               { 
                  DataQuery.expenseWhere = " and acc.AccountType_ID = 1";
                  }

               //A query needs to be created for this. The query below is just a filler.
               DataQuery.expenseSelect = "SELECT acc.[ID], [AccountType_ID], [AccountName], [Description], [DateCreated], [User_ID] " +
                        " (select FROM [dbo].[Account] acc inner join AccountType acct on acc.AccountType_ID = acct.ID ";
               DataQuery.expenseWhere = "WHere User_ID = " + Preferences.Get("ExpenseT_UserID", " - 1");
               

               _ItemListE = DataQuery.ExecuteAQuery<ExpenseEntry>();
               return _ItemListE;
            }
            catch (Exception ex)
            {
               //if (Xamarin.Forms.Device.RuntimePlatform != "UWP")
              //    CrossToastPopUp.Current.ShowToastMessage(ex.Message);
               DependencyService.Get<IToast>().Show(ex.Message);
               return null;
            }
         }
         set { _ItemListE = value; OnPropertyChanged(nameof(ItemListE)); }
      }
      string accountType;
      public ExpIncAccViewModel(string accountType)
      {
         DataQuery = new Model.DataQuery_Mod();
         this.accountType = accountType;
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
