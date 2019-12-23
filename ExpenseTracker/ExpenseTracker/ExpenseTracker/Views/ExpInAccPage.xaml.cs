using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Views
{

   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ExpIncAccPage : ContentPage
   {
      bool focusFlag = false;
      Model.DataQuery_Mod DataQuery;
      string accountType = "";
      ViewModels.ExpIncAccViewModel viewModel;
      public ExpIncAccPage(string accountType)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpIncAccViewModel(accountType);
         this.accountType = accountType;

         this.ToolbarItems.Add(new ToolbarItem("LogOut", "", () =>
         {
            OnLogOut();
         }));
         DataQuery = new Model.DataQuery_Mod();

         this.Title = "Accounts";

         if(Device.RuntimePlatform == Device.UWP)
         {
              
         }
         base.Appearing += ExpIncAccPage_Appearing;
         
      }

      private void ExpIncAccPage_PropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
      {
         throw new NotImplementedException();
      }

      public ExpIncAccPage(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpIncAccViewModel(accountID);

         this.ToolbarItems.Add(new ToolbarItem("LogOut", "menu-button.png", () =>
         {
            OnLogOut();
         }));
         DataQuery = new Model.DataQuery_Mod();

         this.Title = "Accounts";

         base.Appearing += ExpIncAccPage_Appearing; ;
      }

      private void ExpIncAccPage_Appearing(object sender, EventArgs e)
      {
         var parent = this.Parent as AccountsPage;
         
         if (focusFlag || parent.pageChanged)
         {
            parent.pageChanged = false;
            focusFlag = false;
            List<Account> tempList = viewModel.ItemListA.ToList();
            viewModel.ItemListA = null;
            viewModel.ItemListA = new ObservableCollection<Account>(tempList);
         }
      }

      async public void OnAddClick(object sender, EventArgs e)
      {
         var parent = this.Parent.Parent as NavigationPage;
         focusFlag = true;
         await parent.PushAsync(new AddAccount());         
      }

      async public void OnAccountTap(object sender, EventArgs e)
      {
         var grid = sender as Grid;
         var cell = (grid.Parent.Parent.Parent as ViewCell).ContextActions[0] as MenuItem;
         
         var tap = (TapGestureRecognizer)grid.GestureRecognizers[0];
         var account = (Account)((TapGestureRecognizer)grid.GestureRecognizers[0]).CommandParameter;

         focusFlag = true;

         if (account.AccountType_ID == 1)
         {
            var parent = this.Parent.Parent as NavigationPage;

            await parent.PushAsync(new IncomeEntriesPage(account.ID)
            { Title = account.AccountName + " $" + account.AccountBalance.ToString("0.00") }) ;
            //await Navigation.PushModalAsync(new NavigationPage(new IncomeEntriesPage(account.ID, account.AccountName, account.AccountBalance)));
         }
         else if (account.AccountType_ID == 2)
         {
            var parent = this.Parent.Parent as NavigationPage;

            await parent.PushAsync(new ExpenseEntriesPage(account.ID) 
            { Title = account.AccountName + " $" + account.AccountBalance.ToString("0.00") });
         }
         else
         {
            var parent = this.Parent.Parent as NavigationPage;

            await parent.PushAsync(new ExpenseEntriesPage(account.ID) { Title = "ERROR" });
         }
         
      }

      public void OnLogOut()
      {
         Preferences.Clear();
         var accountsPage = new NavigationPage(new LoginPage() { Title = "Login" });
         NavigationPage.SetHasBackButton(accountsPage, true);
         App.Current.MainPage = accountsPage;
      }

      async public void OnDeleteClick (object sender, EventArgs e)
      {
         try
         {
            bool choice = await DisplayAlert("ALERT", "This will delete all entries for this account. Are you sure you want to delete?", "Delete", "Cancel");
            if (choice)
            {
               var deleteID = (Account)((sender as MenuItem).CommandParameter);
               if (deleteID.AccountType_ID == 1)
               {
                  DataQuery.expenseSelect = "Delete From Expense";
                  DataQuery.expenseWhere = "where incomeaccount_id = " + deleteID.ID;
                  int results = DataQuery.AlterDataQuery();
                  DataQuery.expenseSelect = "Delete From Income";
                  DataQuery.expenseWhere = "where account_id = " + deleteID.ID;
                  results = DataQuery.AlterDataQuery();
                  DataQuery.expenseSelect = "Delete From Account";
                  DataQuery.expenseWhere = "where id = " + deleteID.ID;
                  results = DataQuery.AlterDataQuery();
               }
               else if (deleteID.AccountType_ID == 2)
               {
                  DataQuery.expenseSelect = "Delete From Expense";
                  DataQuery.expenseWhere = "where account_id = " + deleteID.ID;
                  int results = DataQuery.AlterDataQuery();
                  DataQuery.expenseSelect = "Delete From Account";
                  DataQuery.expenseWhere = "where id = " + deleteID.ID;
                  results = DataQuery.AlterDataQuery();
               }

               focusFlag = true;
               ExpIncAccPage_Appearing(sender, e);
            }
         }
         catch (Exception ex)
         {
            DependencyService.Get<IToast>().Show(ex.Message);
         }
      }

      public void OnEditClick (object sender, EventArgs e)
      {
         try
         {

            Account deleteID = null;
            if(sender is MenuItem)
               deleteID = (Account)((sender as MenuItem).CommandParameter);
            else if (sender is Button)
               deleteID = (Account)((sender as Button).CommandParameter);
            Navigation.PushAsync(new AddAccount(deleteID.ID) { Title = "Edit " + deleteID.AccountName + " Account" });
            focusFlag = true;
         }
         catch (Exception ex)
         {
            DependencyService.Get<IToast>().Show(ex.Message);
         }
      }

      public void OnSwipeLeft(object sender, SwipedEventArgs e)
      {
         var grid = sender as Grid;
         grid.Children[1].IsVisible = false;

         var slide = grid.Parent.Parent as Grid;
         var button1 = slide.Children[1] as Button;
         button1.IsVisible = true;
         var button2 = slide.Children[2] as Button;
         button2.IsVisible = true;
      }
      public void OnSwipeRight(object sender, SwipedEventArgs e)
      {
         var grid = sender as Grid;
         grid.Children[1].IsVisible = true;

         var slide = grid.Parent.Parent as Grid;
         var button1 = slide.Children[1] as Button;
         button1.IsVisible = false;
         var button2 = slide.Children[2] as Button;
         button2.IsVisible = false;
      }
   }
}