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
            viewModel.DataQuery.expenseSelect = "Select ID From Users ";
            viewModel.DataQuery.expenseWhere = "where Username = '" + viewModel.Username + "' and Password = '" + viewModel.PasswordHash + "'";
            viewModel.UsersInfo = viewModel.DataQuery.ExecuteAQuery<Users>();
            if (viewModel.UsersInfo.Count == 1)
            {
               Preferences.Set("ExpenseT_UserID", viewModel.UsersInfo[0].ID);
               viewModel.User_ID = viewModel.UsersInfo[0].ID;
               await Navigation.PushAsync(new MainPage());
            }
            viewModel.IsBusy = false;

         }
         catch (Exception ex)
         {
            await DisplayAlert("Login Failed!", ex.Message, "OK");
         }
      }

      
        async void OnNewUserButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new NewUser());
        }
    }
  
}