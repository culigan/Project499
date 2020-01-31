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
   public class IncomeEntriesViewModel : INotifyPropertyChanged
   {
      Model.DataQuery_Mod DataQuery;
      
      private ObservableCollection<IncomeEntry> _ItemListE;
      public ObservableCollection<IncomeEntry> ItemListE
      {
         get
         {
            try
            {
               var dat = DateTime.Now;
               dat = dat.AddMonths(-1);
               string startDateString = "";
               string endDateString = "";
               if (Preferences.Get("start_date", dat) == dat)
                  startDateString = dat.ToString();
               else
                  startDateString = Preferences.Get("start_date", dat).ToString();
               if (Preferences.Get("end_date", DateTime.Now) == DateTime.Now)
                  endDateString = DateTime.Now.ToString();
               else
                  endDateString = Preferences.Get("end_date", DateTime.Now).ToString();

               DataQuery.expenseSelect = "SELECT ex.[ID], ex.[User_ID], acc1.AccountName, ex.[IncomeAmount], ex.[IncomeDate]"
                     + " ,ec.CategoryName as IncomeCategory, ex.[Repeat], rp.RepeatPeriod, ex.[IncomeName] FROM [dbo].[Income] ex inner join Account acc1 on ex.Account_ID = acc1.ID"
                     + " inner join IncomeCategory ec on ex.IncomeCategory_ID = ec.ID"
                     + " inner join RepeatPeriod rp on ex.RepeatPeriod_ID = rp.ID";


                  DataQuery.expenseWhere = " where ex.user_id = " + Preferences.Get("ExpenseT_UserID", "0") + " and (IncomeDate between '" + startDateString + "' and '" + endDateString + "') and ex.Account_ID = " + accountID;
                  _ItemListE = DataQuery.ExecuteAQuery<IncomeEntry>();

               
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
      public IncomeEntriesViewModel(int accountID)
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
