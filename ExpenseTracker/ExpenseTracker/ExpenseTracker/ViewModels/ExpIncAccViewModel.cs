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
      ObservableCollection<Account> incomeSum = new ObservableCollection<Account>();

      private double _ChangeFont = 12;
      public double ChangeFont
      {
         get { return _ChangeFont; }
         set { _ChangeFont = value; OnPropertyChanged(nameof(ChangeFont)); }
      }
      private bool _MenuVisible = false;
      public bool MenuVisible 
      {
         get
         {
            return _MenuVisible;
         }
         set 
         {
            _MenuVisible = value; OnPropertyChanged(nameof(MenuVisible));
         } 
      }
      public bool EditVisible { get; set; } = false;
      public bool DeleteVisible { get; set; } = false;

      private bool _Busy = true;
      public bool Busy { get { return _Busy; } set { _Busy = value; OnPropertyChanged(nameof(Busy)); } }

      private ObservableCollection<Account> _ItemListA;
      public ObservableCollection<Account> ItemListA
      {
         get 
         {
            try
            {
               if (Connectivity.NetworkAccess == NetworkAccess.Internet)
               {
                  Busy = true;
                  string idString = "";
                  DataQuery.expenseSelect = "SELECT acc.[ID], [AccountType_ID], [AccountName], [Description], [DateCreated], [User_ID], " +
                           "(SELECT ExpenseAmount FROM[dbo].[Expense] where ID = 9) as AccountBalance FROM [dbo].[Account] acc inner join " +
                           "AccountType acct on acc.AccountType_ID = acct.ID ";
                  DataQuery.expenseWhere = "WHere User_ID = " + Preferences.Get("ExpenseT_UserID", " -1");


                  if (accountType.ToUpper() == "EXPENSEACCOUNT")
                  {
                     DataQuery.expenseWhere += " and acc.AccountType_ID = 2";
                     _ItemListA = DataQuery.ExecuteAQuery<Account>();

                     for (int i = 0; i < _ItemListA.Count; i++)
                     {
                        string accountB = "";
                        DataQuery.expenseSelect = "Select * From Totals ";
                        DataQuery.expenseWhere = "Where Account_id = " + _ItemListA[i].ID;
                        ObservableCollection<Totals> totals = DataQuery.ExecuteAQuery<Totals>();
                        if (totals.Count == 0)
                        {
                           DataQuery.expenseWhere = "Where user_id = " + Preferences.Get("ExpenseT_UserID", "0") + " and Account_id = " + _ItemListA[i].ID;
                           DataQuery.expenseSelect = "Select SUM(ExpenseAmount) From Expense ";
                           accountB = DataQuery.ExecuteAQuery();
                           if (accountB == "")
                              accountB = "0.00";
                           DataQuery.expenseSelect = "INSERT INTO [Totals] ([Account_ID], [Total]) VALUES (" + _ItemListA[i].ID + ", " + float.Parse(accountB) + ") ";
                           DataQuery.expenseWhere = "";
                           int count = DataQuery.AlterDataQuery();

                        }
                        else
                           accountB = totals[0].Total.ToString("0.00");

                        _ItemListA[i].AccountBalance = accountB == "" ? 0.00 : (double.Parse(accountB));
                     }
                  }
                  else if (accountType.ToUpper() == "INCOMEACCOUNT")
                  {
                     DataQuery.expenseWhere += " and acc.AccountType_ID = 1";
                     _ItemListA = DataQuery.ExecuteAQuery<Account>();

                     for (int i = 0; i < _ItemListA.Count; i++)
                     {
                        string accountB = "";
                        double expenseSum = 0.00;
                        DataQuery.expenseSelect = "Select * From Totals ";
                        DataQuery.expenseWhere = "Where Account_id = " + _ItemListA[i].ID;
                        ObservableCollection<Totals> totals = DataQuery.ExecuteAQuery<Totals>();
                        if (totals.Count == 0)
                        {
                           DataQuery.expenseWhere = "Where user_id = " + Preferences.Get("ExpenseT_UserID", "0") + " and IncomeAccount_id = " + _ItemListA[i].ID;
                           DataQuery.expenseSelect = "Select SUM(ExpenseAmount) From Expense ";
                           accountB = DataQuery.ExecuteAQuery();
                           expenseSum = accountB == "" ? 0.00 : (double.Parse(accountB));
                           DataQuery.expenseWhere = "Where user_id = " + Preferences.Get("ExpenseT_UserID", "0") + " and Account_id = " + _ItemListA[i].ID;
                           DataQuery.expenseSelect = "Select SUM(IncomeAmount) From Income ";
                           accountB = DataQuery.ExecuteAQuery();
                           if (accountB == "")
                              accountB = "0.00"; DataQuery.expenseSelect = "INSERT INTO [Totals] ([Account_ID], [Total]) VALUES (" + _ItemListA[i].ID + ", " + float.Parse(accountB) + ") ";
                           DataQuery.expenseWhere = "";
                           int count = DataQuery.AlterDataQuery();
                        }
                        else
                           accountB = totals[0].Total.ToString("0.00");

                        _ItemListA[i].AccountBalance = accountB == "" ? 0.00 : (double.Parse(accountB) - expenseSum);
                     }
                  }
               }
               else
               {
                  DependencyService.Get<IToast>().Show("No Internet Connection.");
                  _ItemListA = new ObservableCollection<Account>();
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
            Busy = false;
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
               if (Connectivity.NetworkAccess == NetworkAccess.Internet)
               {
                  DataQuery.expenseSelect = "SELECT ex.[ID], ex.[User_ID], acc1.AccountName, ex.[ExpenseAmount], acc2.AccountName, ex.[ExpenseDate]"
                     + " ,ec.CategoryName, ex.[Repeat], rp.RepeatPeriod FROM [dbo].[Expense] ex inner join Account acc1 on ex.Account_ID = acc1.ID"
                     + " inner join Account acc2 on ex.IncomeAccount_ID = acc2.ID inner join ExpenseCategory ec on ex.ExpenseCategory_ID = ec.ID"
                     + " inner join RepeatPeriod rp on ex.RepeatPeriod_ID = rp.ID";


                  DataQuery.expenseWhere = " where user_id = " + Preferences.Get("ExpenseT_UserID", "0") + " and Account_ID = " + accountID;
                  _ItemListE = DataQuery.ExecuteAQuery<ExpenseEntry>();
               }
               else
               {
                  DependencyService.Get<IToast>().Show("No Internet Connection.");
                  _ItemListE = new ObservableCollection<ExpenseEntry>();
               }

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
      int accountID = 0;
      public ExpIncAccViewModel(string accountType)
      {
         DataQuery = new Model.DataQuery_Mod();
         this.accountType = accountType;
      }

      public ExpIncAccViewModel(int accountID)
      {
         DataQuery = new Model.DataQuery_Mod();
         this.accountID = accountID;
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
