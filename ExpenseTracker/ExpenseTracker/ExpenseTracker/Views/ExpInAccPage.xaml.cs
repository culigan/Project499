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
         await Navigation.PushModalAsync(new AddAccount());
      }

      async public void OnAccountTap(object sender, EventArgs e)
      {
         var grid = sender as Grid;
         var account = (Account)((TapGestureRecognizer)grid.GestureRecognizers[0]).CommandParameter;
         NavigationPage navigationPage = new NavigationPage();
         navigationPage.Title = account.AccountName;
         if (account.AccountType_ID == 1)
         {
            await navigationPage.PushAsync(new IncomeEntriesPage(account.ID, account.AccountName));
         }
         else if (account.AccountType_ID == 2)
         {
            await navigationPage.PushAsync(new ExpenseEntriesPage(account.ID, account.AccountName));
         }
         else
         {
            await navigationPage.PushAsync(new ExpenseEntriesPage(0, "ERROR"));
         }
         
         await Navigation.PushModalAsync(navigationPage);
      }

      public void OnLogOut()
      {
         Preferences.Clear();
         Application.Current.MainPage = new LoginPage();
      }
   }
}