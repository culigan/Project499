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
      string catPick = "";
      string incPick = "";
        public AddItem()
        {
            InitializeComponent();
            BindingContext = viewModel = new AddItemViewModel();
            viewModel.User_ID = int.Parse(Preferences.Get("ExpenseT_UserID", "40"));

         PopulateCategoryPicker();
      }
      public AddItem(ExpenseEntry expenseEntry, IncomeEntry incomeEntry)
      {
         InitializeComponent();
         BindingContext = viewModel = new AddItemViewModel();
            viewModel.User_ID = int.Parse(Preferences.Get("ExpenseT_UserID", "40"));
         if (expenseEntry != null)
         {
            viewModel.AccountType = "Expense";
            viewModel.Account_ID = expenseEntry.ID;
            viewModel.AccountName = expenseEntry.AccountName;
            viewModel.TransAmount = expenseEntry.ExpenseAmount.ToString("0.00");
            viewModel.TransName = expenseEntry.ExpenseName;
            viewModel.IncomeAccount = expenseEntry.IncomeAccountName;
            viewModel.InAccVisible = true;
            catPick = expenseEntry.ExpenseCategory;
            incPick = expenseEntry.IncomeAccountName;
         }
         else if (incomeEntry != null)
         {
            viewModel.AccountType = "Income";
            viewModel.Account_ID = incomeEntry.ID;
            viewModel.AccountName = incomeEntry.AccountName;
            viewModel.Category = incomeEntry.IncomeCategory;
            viewModel.TransAmount = incomeEntry.IncomeAmount.ToString("0.00");
            viewModel.TransName = incomeEntry.IncomeName;
            catPick = incomeEntry.IncomeCategory;
         }

         saveButton.Text = "Update";
         PopulateCategoryPicker();
         PopulateIncomeAccountPicker();
      }
      public AddItem(string accountType, string accountName)
      {
         InitializeComponent();
         BindingContext = viewModel = new AddItemViewModel();
            viewModel.User_ID = int.Parse(Preferences.Get("ExpenseT_UserID", "40"));
         if (accountType == "Expense")
            viewModel.InAccVisible = true;
         else
            viewModel.InAccVisible = false;

         viewModel.AccountType = accountType;
         viewModel.AccountName = accountName;
         PopulateCategoryPicker();
         PopulateIncomeAccountPicker();
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
         viewModel.CategoryList = pickerList;
         viewModel.Category = catPick;
         viewModel.IsBusy = false;
      }
      private void PopulateIncomeAccountPicker()
      {
         viewModel.IsBusy = true;
         viewModel.DataQuery.expenseSelect = "Select * From Account ";
         viewModel.DataQuery.expenseWhere = " where (user_id = " + viewModel.User_ID + " or user_id = 40) and AccountType_ID = 1";
         ObservableCollection<Account> picker = viewModel.DataQuery.ExecuteAQuery<Account>();
         ObservableCollection<string> pickerList = new ObservableCollection<string>();
         foreach (Account acc in picker)
         {
            pickerList.Add(acc.AccountName);
         }
         viewModel.IncomeAccountList = pickerList;
         viewModel.IncomeAccount = incPick;
         viewModel.IsBusy = false;
      }
      async void OnSaveButtonClicked(object sender, EventArgs e)
      {

         DateTime myDateTime = DateTime.Now;
         string sqlFormattedDate = myDateTime.ToString("yyyy-MM-dd");
         

         try
         {

            viewModel.IsBusy = true;
            if (saveButton.Text == "Save")
            {
               if (viewModel.AccountType == "Income")
               {
                  viewModel.DataQuery.expenseSelect = "INSERT INTO " + viewModel.AccountType + " ([User_ID],[Account_ID],[IncomeAmount]," +
                     "[IncomeDate],[IncomeCategory_ID],[Repeat],[RepeatPeriod_ID],[IncomeName]) VALUES (" + viewModel.User_ID + ", " +
                     "(select id from Account where AccountName = '" + viewModel.AccountName + "' and User_ID = " + viewModel.User_ID + "), " +
                        viewModel.TransAmount + ", '" + DateTime.Now + "', " + "(select ID from IncomeCategory where CategoryName = '" + viewModel.Category + "')," +
                       " 0, 4, '" + viewModel.TransName + "')";
               }
               else if (viewModel.AccountType == "Expense")
               {
                  viewModel.DataQuery.expenseSelect = "INSERT INTO " + viewModel.AccountType + " ([User_ID],[IncomeAccount_ID],[Account_ID],[ExpenseAmount]," +
                     "[ExpenseDate],[ExpenseCategory_ID],[Repeat],[RepeatPeriod_ID],[ExpenseName]) VALUES (" + viewModel.User_ID + ", (select id from Account where AccountName = '"
                     + viewModel.IncomeAccount + "' and user_id = " + viewModel.User_ID + "), (select id from Account where AccountName = '" + viewModel.AccountName + "' and User_ID = " + viewModel.User_ID + "), " +
                        viewModel.TransAmount + ", '" + DateTime.Now + "', " + "(select ID from ExpenseCategory where (user_id = " + viewModel.User_ID + " or user_id = 40) and  CategoryName = '" + 
                        viewModel.Category + "'), 0, 4, '" + viewModel.TransName + "')";
               }
               viewModel.DataQuery.expenseWhere = "";
               int result = viewModel.DataQuery.AlterDataQuery();

               viewModel.IsBusy = false;
            }
            else if (saveButton.Text == "Update")
            {

               if (viewModel.AccountType == "Expense")
               {

                  viewModel.DataQuery.expenseSelect = "UPDATE [dbo].[Expense] SET [IncomeAccount_ID] = (select Account_ID from Account where user_id = " + viewModel.User_ID + 
                     " and AccountName = '" + viewModel.IncomeAccount + "'), [ExpenseAmount] = '" + viewModel.TransAmount + "', [ExpenseCategory_ID] = " +
                     "(select ID from ExpenseCategory where (user_id = " + viewModel.User_ID + " or user_id = 40) and CategoryName = '" + viewModel.Category + "'), [ExpenseName] = '" + viewModel.TransName + "'"; 
                  viewModel.DataQuery.expenseWhere = " Where ID = " + viewModel.Account_ID;
               }
               else if (viewModel.AccountType == "Income")
               {

                  viewModel.DataQuery.expenseSelect = "UPDATE [dbo].[Income] SET [IncomeAmount] = " + viewModel.TransAmount + ", [IncomeCategory_ID] = " +
                     "(select ID from IncomeCategory where (user_id = " + viewModel.User_ID + " or user_id = 40) and CategoryName = '" + viewModel.Category + "'), [IncomeName] = '" + viewModel.TransName + "'";
                  viewModel.DataQuery.expenseWhere = " Where ID = " + viewModel.Account_ID;
               }
               int result = viewModel.DataQuery.AlterDataQuery();
               if (result == 1)
                  DependencyService.Get<IToast>().Show(viewModel.AccountName + " was successfully updated.");

            }
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