using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItem : ContentPage
    {
        AddItemViewModel viewModel;
        public AddItem()
        {
            InitializeComponent();
            BindingContext = viewModel = new AddItemViewModel();
            viewModel.User_ID = int.Parse(Preferences.Get("ExpenseT_UserID", "40"));

         PopulateCategoryPicker();
      }
      public AddItem(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new AddItemViewModel();
            viewModel.User_ID = int.Parse(Preferences.Get("ExpenseT_UserID", "40"));
         viewModel.Account_ID = accountID;
         PopulateCategoryPicker();
      }
      public AddItem(string accountType, string accountName)
      {
         InitializeComponent();
         BindingContext = viewModel = new AddItemViewModel();
            viewModel.User_ID = int.Parse(Preferences.Get("ExpenseT_UserID", "40"));
         viewModel.AccountType = accountType;
         viewModel.AccountName = accountName;
         PopulateCategoryPicker();

      }

      private void PopulateCategoryPicker()
      {
         viewModel.IsBusy = true;
         viewModel.DataQuery.expenseSelect = "Select * From " + viewModel.AccountType + "Category ";
         viewModel.DataQuery.expenseWhere = " where user_Id = 40 or user_id = " + viewModel.User_ID;
         ObservableCollection<Exp_Inc_Category> picker = viewModel.DataQuery.ExecuteAQuery<Exp_Inc_Category>();
         ObservableCollection<string> pickerList = new ObservableCollection<string>();
         foreach (Exp_Inc_Category cat in picker)
         {
            pickerList.Add(cat.CategoryName);
         }
         viewModel.Category = pickerList;
         viewModel.IsBusy = false;
      }
      async void OnSaveButtonClicked(object sender, EventArgs e)
      {


         int AccountSelect = 0;
         DateTime myDateTime = DateTime.Now;
         string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd");

         if (viewModel.AccountType == "Income")
         {
            AccountSelect = 1;
         }
         else if (viewModel.AccountType == "Expense")
         {
            AccountSelect = 2;
         }



         try
         {

            viewModel.IsBusy = true;
            /*if (saveButton.Text == "Save")
            {
               viewModel.DataQuery.expenseSelect = "INSERT INTO " + viewModel.AccountType + " VALUES (" + AccountSelect + ", '" + viewModel.AccountName + 
                  "', '" + viewModel.AccountDesc + "', '" + sqlFormattedDate + "', " + userID + ")";
               viewModel.DataQuery.expenseWhere = ;
               int result = viewModel.DataQuery.AlterDataQuery();

               viewModel.IsBusy = false;
            }
            else if (saveButton.Text == "Update")
            {
               viewModel.DataQuery.expenseSelect = "UPDATE[dbo].[Account] SET [AccountName] = '" + viewModel.AccountName + "', [Description] = '"
                  + viewModel.AccountDesc + "', [AccountType_ID] = " + AccountSelect;
               viewModel.DataQuery.expenseWhere = " Where ID = " + accountID;
               int result = viewModel.DataQuery.AlterDataQuery();
               if (result == 1)
                  DependencyService.Get<IToast>().Show(viewModel.AccountName + " was successfully updated.");

            }*/
         }
         catch (Exception ex)
         {
            await DisplayAlert("Adding account failed", ex.Message, "OK");
            viewModel.IsBusy = false;
         }

         await Navigation.PopAsync();
      }

      async void OnCancelButtonClicked(object sender, EventArgs e)
      {
         await Navigation.PopAsync();
      }

      private void PopulateOnEdit()
      {
         try
         {
            /*viewModel.IsBusy = true;
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

            viewModel.IsBusy = false;*/

         }
         catch (Exception ex)
         {
            DisplayAlert("Adding account failed", ex.Message, "OK");
            viewModel.IsBusy = false;
         }
      }
   }
}