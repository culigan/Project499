using System;
using System.IO;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.Model;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class LoginPage : ContentPage
   {
      LoginViewModel viewModel;


      public LoginPage()
      {
         InitializeComponent();
         BindingContext = viewModel = new LoginViewModel();

      }

      async void OnLoginButtonClicked(object sender, EventArgs e)
      {
         try
         {
            viewModel.IsBusy = true;
            viewModel.DataQuery.expenseSelect = "Select * From Users ";
            viewModel.DataQuery.expenseWhere = "where Username = '" + viewModel.Username + "' and Password = '" + viewModel.PasswordHash + "'";
            viewModel.UsersInfo = viewModel.DataQuery.ExecuteAQuery<Users>();
            if (viewModel.UsersInfo.Count == 1)
            {
               Preferences.Set("ExpenseT_UserID", viewModel.UsersInfo[0].ID.ToString());
               viewModel.User_ID = viewModel.UsersInfo[0].ID;
               var accountsPage = new NavigationPage(new Views.AccountsPage() { Title = "Accounts Page" });
               NavigationPage.SetHasBackButton(accountsPage, true);
               App.Current.MainPage = accountsPage;
            }
            viewModel.IsBusy = false;

         }
         catch (Exception ex)
         {
            await DisplayAlert("Login Failed!", ex.Message, "OK");
            viewModel.IsBusy = false;
         }
      }

      



        async void OnNewUserButtonClicked(object sender, EventArgs e)
        {
            var parent = this.Parent as NavigationPage;
            await parent.PushAsync(new NewUser() { Title = "Create New User" });

        }
    }
  
}