using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class AccountsPage : TabbedPage
   {
      ViewModels.AccountsViewModel viewModel;
      public AccountsPage()
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.AccountsViewModel();
         /*NavigationPage navigationPage = new NavigationPage();
         navigationPage.Title = "Expenses";

         NavigationPage navigationPage1 = new NavigationPage();
         navigationPage1.Title = "Income";
         */
         Children.Add(new ExpIncAccPage("ExpenseAccount") { Title = "Expenses" });
         Children.Add(new ExpIncAccPage("IncomeAccount") { Title = "Income" });
      }

      async public void OnLogOut(object sender, EventArgs e)
      {
         Preferences.Clear();
         var accountsPage = new NavigationPage(new LoginPage() { Title = "Login" });
         NavigationPage.SetHasBackButton(accountsPage, true);
         App.Current.MainPage = accountsPage;
      }
   }


}