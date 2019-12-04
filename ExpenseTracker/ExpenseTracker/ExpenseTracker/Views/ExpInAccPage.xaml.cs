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
      string accountType = "";
      ViewModels.ExpIncAccViewModel viewModel;
      public ExpIncAccPage(string accountType)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpIncAccViewModel(accountType);
         this.accountType = accountType;

         this.ToolbarItems.Add(new ToolbarItem("LogOut", "menu-button.png", () =>
         {
            OnLogOut();
         }));

         this.Title = "Accounts";
      }

      public ExpIncAccPage(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpIncAccViewModel(accountID);

         this.ToolbarItems.Add(new ToolbarItem("LogOut", "menu-button.png", () =>
         {
            OnLogOut();
         }));

         this.Title = "Accounts";
      }

      async public void OnAddClick(object sender, EventArgs e)
      {
         var parent = this.Parent.Parent as NavigationPage;

         await parent.PushAsync(new AddAccount());
      }

      async public void OnAccountTap(object sender, EventArgs e)
      {
         var grid = sender as Grid;      
         var account = (Account)((TapGestureRecognizer)grid.GestureRecognizers[0]).CommandParameter;
         
         if (account.AccountType_ID == 1)
         {
            var parent = this.Parent.Parent as NavigationPage;
            
            await parent.PushAsync(new IncomeEntriesPage(account.ID)
            { Title = account.AccountName + " $" + account.AccountBalance.ToString("0.00") });
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

            await parent.PushAsync(new ExpenseEntriesPage(0) { Title = "ERROR" });
         }
         
      }

      public void OnLogOut()
      {
         Preferences.Clear();
         var accountsPage = new NavigationPage(new LoginPage() { Title = "Login" });
         NavigationPage.SetHasBackButton(accountsPage, true);
         App.Current.MainPage = accountsPage;
      }
   }
}