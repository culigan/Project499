using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.PlatformConfiguration.AndroidSpecific;
using Xamarin.Forms.PlatformConfiguration.WindowsSpecific;

namespace ExpenseTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class AccountsPage : Xamarin.Forms.TabbedPage
   {
      ViewModels.AccountsViewModel viewModel;
      public bool pageChanged = false;
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
         this.On<Android>().DisableSwipePaging();

         this.CurrentPageChanged += AccountsPage_CurrentPageChanged;
      }

      private void AccountsPage_CurrentPageChanged(object sender, EventArgs e)
      {
         pageChanged = true;
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