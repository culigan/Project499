using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExpenseTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.PlatformConfiguration;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAccount : ContentPage
    {
      ObservableCollection<AccountType> picker;
      private int accountID;
        AddAccountViewModel viewModel;
        public AddAccount()
        {
            InitializeComponent();
            BindingContext = viewModel = new AddAccountViewModel();

         PopulatePicker();

      }
      public AddAccount(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new AddAccountViewModel();

         this.accountID = accountID;
         PopulatePicker();
         PopulateOnEdit();
         saveButton.Text = "Update";
      }

      
      private void PopulatePicker()
      {
         viewModel.IsBusy = true;
         viewModel.DataQuery.expenseSelect = "Select * From accounttype  ";
         viewModel.DataQuery.expenseWhere = "";
         picker = viewModel.DataQuery.ExecuteAQuery<AccountType>();
         ObservableCollection<string> pickerList = new ObservableCollection<string>();
         foreach(AccountType accType in picker)
         {
            pickerList.Add(accType.TypeName);
         }
         AccountTypePicker.ItemsSource = pickerList;
         AccountTypePicker.SelectedIndex = 0;
         viewModel.IsBusy = false;
      }
      private void PopulateOnEdit()
      {
         try
         {
            viewModel.IsBusy = true;
            viewModel.DataQuery.expenseSelect = "Select * From account  ";
            viewModel.DataQuery.expenseWhere = "where id = " + accountID;
            viewModel.UsersInfo = viewModel.DataQuery.ExecuteAQuery<Account>();
            viewModel.AccountDesc = viewModel.UsersInfo[0].Description;
            viewModel.AccountName = viewModel.UsersInfo[0].AccountName;
            int index = 0;
            foreach (AccountType accType in picker)
            {
               if (viewModel.UsersInfo[0].AccountType_ID == picker[index].ID)
                  viewModel.AccountTypePicker = picker[index].TypeName;
               index++;
            }

            viewModel.IsBusy = false;

         }
         catch (Exception ex)
         {
            DisplayAlert("Adding account failed", ex.Message, "OK");
            viewModel.IsBusy = false;
         }
      }
      async void OnSaveAccountButtonClicked(object sender, EventArgs e)
        {
            String userID = Preferences.Get("ExpenseT_UserID", "NULL");
               viewModel.IsBusy = true;


            int AccountSelect = 0;

            if (viewModel.AccountTypePicker == "Income")
            {
                AccountSelect = 1;
            }
            else if (viewModel.AccountTypePicker == "Expense")
            {
                AccountSelect = 2;
            }

         
            
            try
            {
            
            if (saveButton.Text == "Save")
            {
               viewModel.DataQuery.expenseSelect = "INSERT INTO account VALUES ";
               viewModel.DataQuery.expenseWhere = "(" + AccountSelect + ", '" + viewModel.AccountName + "', '" + viewModel.AccountDesc + "', '" + DateTime.Now + "', " + userID + ")";
               viewModel.UsersInfo = viewModel.DataQuery.ExecuteAQuery<Account>();

            }
            else if(saveButton.Text == "Update")
            {
               viewModel.DataQuery.expenseSelect = "UPDATE[dbo].[Account] SET [AccountName] = '" + viewModel.AccountName + "', [Description] = '"
                  + viewModel.AccountDesc + "', [AccountType_ID] = " + AccountSelect;
               viewModel.DataQuery.expenseWhere = " Where ID = " + accountID;
                int result = viewModel.DataQuery.AlterDataQuery();
               if(result == 1)
                  DependencyService.Get<IToast>().Show(viewModel.AccountName + " was successfully updated.");

            }
               viewModel.IsBusy = false;

         }
         catch (Exception ex)
            {
                await DisplayAlert("Adding account failed", ex.Message, "OK");
                viewModel.IsBusy = false;
            }

         while (viewModel.IsBusy) ;
            await Navigation.PopAsync();
        }

       async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}