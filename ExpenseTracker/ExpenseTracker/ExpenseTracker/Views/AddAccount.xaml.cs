using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAccount : ContentPage
    {
        AddAccountViewModel viewModel;
        public AddAccount()
        {
            InitializeComponent();
            BindingContext = viewModel = new AddAccountViewModel();
        }

        async void OnSaveAccountButtonClicked(object sender, EventArgs e)
        {
            Preferences.Set("ExpenseT_UserID", viewModel.UsersInfo[0].ID.ToString());
            viewModel.User_ID = viewModel.UsersInfo[0].ID;

            int AccountSelect = 0;
            DateTime myDateTime = DateTime.Now;
            string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd");

            if (viewModel.AccountTypePicker == "Income")
            {
                AccountSelect = 1;
            }
            else if (viewModel.AccountTypePicker == "Expenses")
            {
                AccountSelect = 2;
            }

            try
            {
                viewModel.IsBusy = true;
                viewModel.DataQuery.expenseSelect = "INSERT INTO account VALUES ";
                viewModel.DataQuery.expenseWhere = "(" + AccountSelect + ", " + viewModel.AccountName + ", " + viewModel.AccountDesc + ", " + sqlFormattedDate + ", " + viewModel.User_ID + ")";
                viewModel.UsersInfo = viewModel.DataQuery.ExecuteAQuery<Account>();
                
                viewModel.IsBusy = false;

            }
            catch (Exception ex)
            {
                await DisplayAlert("Login Failed!", ex.Message, "OK");
                viewModel.IsBusy = false;
            }
            await Navigation.PopModalAsync();
        }

        async void OnDeleteAccountButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}