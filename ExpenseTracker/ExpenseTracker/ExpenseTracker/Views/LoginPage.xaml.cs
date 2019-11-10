using System;
using System.IO;
using System.Collections.Generic;
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
            viewModel.User_ID = int.Parse(Preferences.Get("ExpenseT_UserID", "-1"));//this works for local storage

      }

      async void OnLoginButtonClicked(object sender, EventArgs e)
        {
         try
         {      
            viewModel.IsBusy = true;
            viewModel.DataQuery.ExecuteAQuery("Select ID From Users where Username = '" + viewModel.Username + "'");
            if (viewModel.DataQuery.QueryResults.Count == 1)
            {
               viewModel.DataQuery.ExecuteAQuery("Select ID From Users where Username = '" + viewModel.Username + "' and Password = '" + viewModel.PasswordHash + "'");
               if (viewModel.DataQuery.QueryResults.Count == 1)
               {
                  Preferences.Set("ExpenseT_UserID", viewModel.DataQuery.QueryResults[0].ToString());
                  viewModel.User_ID = int.Parse(viewModel.DataQuery.QueryResults[0]);
                  await Navigation.PushAsync(new MainPage());
               }
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