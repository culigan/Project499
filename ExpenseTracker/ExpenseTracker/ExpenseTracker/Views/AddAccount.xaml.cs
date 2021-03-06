﻿using System;
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
      private int accountType;
        AddAccountViewModel viewModel;

      public AddAccount(int accountType)
      {
         InitializeComponent();
         BindingContext = viewModel = new AddAccountViewModel();
         this.accountType = accountType;
         PopulatePicker();

      }
      public AddAccount(int accountID, int accountType)
      {
         InitializeComponent();
         BindingContext = viewModel = new AddAccountViewModel();

         this.accountID = accountID;
         this.accountType = accountType;
         PopulatePicker();
         PopulateOnEdit();
         saveButton.Text = "Update";
      }

      
      private void PopulatePicker()
      {
         try
         {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
               viewModel.IsBusy = true;
               viewModel.DataQuery.expenseSelect = "Select * From accounttype  ";
               viewModel.DataQuery.expenseWhere = "order by id";
               picker = viewModel.DataQuery.ExecuteAQuery<AccountType>();
               ObservableCollection<string> pickerList = new ObservableCollection<string>();
               foreach (AccountType accType in picker)
               {
                  pickerList.Add(accType.TypeName);
               }
               AccountTypePicker.ItemsSource = pickerList;
               AccountTypePicker.SelectedIndex = accountType - 1;
               viewModel.IsBusy = false;
            }
            else
            {
               DependencyService.Get<IToast>().Show("No Internet Connection.");
            }
         }
         catch (Exception ex)
         {
            DisplayAlert("Query failed ", ex.Message, "OK");
            viewModel.IsBusy = false;
         }
      }
      private void PopulateOnEdit()
      {
         try
         {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
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
            else
            {
               DependencyService.Get<IToast>().Show("No Internet Connection.");
            }
         }
         catch (Exception ex)
         {
            DisplayAlert("Adding account failed", ex.Message, "OK");
            viewModel.IsBusy = false;
         }
      }
      async void OnSaveAccountButtonClicked(object sender, EventArgs e)
      {
         if (viewModel.AccountName == "")
         {
            await DisplayAlert("Missing Entry", "You must enter an Account Name.", "Ok");
            return;
         }

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
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {

               if (viewModel.AccountName.Contains("'"))
               {
                   viewModel.AccountName = viewModel.AccountName.Replace("'", "''");
               }

               if (viewModel.AccountDesc.Contains("'"))
               {
                   viewModel.AccountDesc = viewModel.AccountDesc.Replace("'", "''");
               }

               viewModel.DataQuery.expenseSelect = "Select * From account  ";
               viewModel.DataQuery.expenseWhere = "where AccountName = '" + viewModel.AccountName + "' and user_ID = " + userID;
               ObservableCollection<Account> UniqueEntry = viewModel.DataQuery.ExecuteAQuery<Account>();

               if (saveButton.Text == "Save" && UniqueEntry.Count == 0)
               {
                  viewModel.DataQuery.expenseSelect = "INSERT INTO account VALUES ";
                  viewModel.DataQuery.expenseWhere = "(" + AccountSelect + ", '" + viewModel.AccountName + "', '" + viewModel.AccountDesc + "', '" + DateTime.Now + "', " + userID + ")";
                  viewModel.UsersInfo = viewModel.DataQuery.ExecuteAQuery<Account>();
                  viewModel.DataQuery.expenseSelect = "INSERT INTO [Totals] ([Account_ID], [Total]) VALUES " +
                     "((Select ID from Account where AccountName = '" + viewModel.AccountName + "' and User_ID = '" + userID + "'), 0.00) ";
                  viewModel.DataQuery.expenseWhere = "";
                  int count = viewModel.DataQuery.AlterDataQuery();


               }
               else if (saveButton.Text == "Update" && UniqueEntry.Count == 1 && UniqueEntry[0].ID == accountID)
               {
                  viewModel.DataQuery.expenseSelect = "UPDATE[dbo].[Account] SET [AccountName] = '" + viewModel.AccountName + "', [Description] = '"
                     + viewModel.AccountDesc + "', [AccountType_ID] = " + AccountSelect;
                  viewModel.DataQuery.expenseWhere = " Where ID = " + accountID;
                  int result = viewModel.DataQuery.AlterDataQuery();

                  if (result == 1)
                     DependencyService.Get<IToast>().Show(viewModel.AccountName + " was successfully updated.");

               }
               else
               {
                  await DisplayAlert("Duplicate Entry", "You already have an account with that name.", "OK");
                  return;
               }
               viewModel.IsBusy = false;
            }
            else
            {
               DependencyService.Get<IToast>().Show("No Internet Connection.");
            }
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