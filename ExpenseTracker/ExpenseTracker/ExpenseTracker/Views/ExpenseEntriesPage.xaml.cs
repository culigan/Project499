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
   public partial class ExpenseEntriesPage : ContentPage
   {
      Model.DataQuery_Mod DataQuery;
      ViewModels.ExpenseEntriesViewModel viewModel;
      bool focusFlag = false;
      public ExpenseEntriesPage(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpenseEntriesViewModel(accountID);
         viewModel.Span = 3;

         DataQuery = new Model.DataQuery_Mod();
         this.ToolbarItems.Add(new ToolbarItem()
         {
            IconImageSource = "hamburger.png",
            Command = new Command(() =>
            {
               DisplayMenu();
            })
         });

         base.Appearing += ExpenseEntriesPage_Appearing; ;
      }

      private void ExpenseEntriesPage_Appearing(object sender, EventArgs e)
      {
         if(focusFlag)
         {
            focusFlag = false;
            List<ExpenseEntry> tempList = viewModel.ItemListE.ToList();
            viewModel.ItemListE = null;
            viewModel.ItemListE = new ObservableCollection<ExpenseEntry>(tempList);
         }

         var amount = 0.0;
         foreach (ExpenseEntry exp in listView.ItemsSource)
         {
            amount += exp.ExpenseAmount;
         }
         this.Title = this.Title.Remove(this.Title.IndexOf("$") + 1) + amount.ToString("0.00");
      }

      public void DisplayMenu()
      {
         if (viewModel.MenuVisible)
            viewModel.MenuVisible = false;
         else
            viewModel.MenuVisible = true;
      }

      public void OnSwipeLeft(object sender, SwipedEventArgs e)
      {
         var frame = ((sender as Grid).Parent as Frame);
         viewModel.Span = 1;
         var button = ((sender as Grid).Parent.Parent as Grid).Children[1];
         var button1 = ((sender as Grid).Parent.Parent as Grid).Children[2];
         button.IsVisible = true;
         button1.IsVisible = true;

         
      }
      public void OnSwipeRight(object sender, SwipedEventArgs e)
      {
         var button = (sender as Grid).Children[4];
         var button1 = (sender as Grid).Children[5];
         button.IsVisible = false;
         button1.IsVisible = false;
      }


      async public void OnAddClick(object sender, EventArgs e)
      {

         var parent = this.Parent as NavigationPage;
         focusFlag = true;
         await parent.PushAsync(new AddItem("Expense", this.Title.Remove(this.Title.IndexOf("$") - 1)) { Title = "Add Expense" });
            
        }

      async public void OnExpenseTap(object sender, EventArgs e)
      {
         var parent = this.Parent as NavigationPage;
         var grid = (sender as Grid);
         var account = (ExpenseEntry)((TapGestureRecognizer)grid.GestureRecognizers[0]).CommandParameter;
         focusFlag = true;
         await parent.PushAsync(new AddItem(account, null) { Title = "Edit Expense" });

      }

      async public void OnDeleteClick(object sender, EventArgs e)
      {
         try
         {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
                     DataQuery.expenseSelect = "Delete From totals";
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
                     DataQuery.expenseSelect = "Delete From totals";
                     DataQuery.expenseWhere = "where account_id = " + deleteID.ID;
                     results = DataQuery.AlterDataQuery();
                     DataQuery.expenseSelect = "Delete From Account";
                     DataQuery.expenseWhere = "where id = " + deleteID.ID;
                     results = DataQuery.AlterDataQuery();
                  }

                  focusFlag = true;
               }
            }
            else
            {
               DependencyService.Get<IToast>().Show("No Internet Connection.");
            }
         }
         catch (Exception ex)
         {
            DependencyService.Get<IToast>().Show(ex.Message);
         }
      }

      public void OnEditClick(object sender, EventArgs e)
      {
         try
         {

            Account deleteID = null;
            if (sender is MenuItem)
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


      async public void OnSettings(object sender, EventArgs e)
      {
         viewModel.MenuVisible = false;
         var parent = this.Parent as NavigationPage;
         await parent.PushAsync(new SettingsPage() { Title = "Settings" });
      }

      public void OnLogOut(object sender, EventArgs e)
      {
         Preferences.Clear();
         var accountsPage = new NavigationPage(new LoginPage() { Title = "Login" });
         NavigationPage.SetHasBackButton(accountsPage, true);
         App.Current.MainPage = accountsPage;
      }
   }
}