﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Views
{

   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ExpenseEntriesPage : ContentPage
   {
      Model.DataQuery_Mod DataQuery;
      ViewModels.ExpenseEntriesViewModel viewModel;
      bool focusFlag = false;
      public ExpenseEntriesPage(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpenseEntriesViewModel(accountID);
         viewModel.Span = 3;
         if (Device.RuntimePlatform == Device.UWP || DeviceDisplay.MainDisplayInfo.Density <= 2)
            viewModel.ChangeFont = 20.0;

         DataQuery = new Model.DataQuery_Mod();
         this.ToolbarItems.Add(new ToolbarItem()
         {
            IconImageSource = "hamburger.png",
            Command = new Command(() =>
            {
               DisplayMenu();
            })
         });

         base.Appearing += ExpenseEntriesPage_Appearing; ;
      }

      private void ExpenseEntriesPage_Appearing(object sender, EventArgs e)
      {
         if(focusFlag)
         {
            focusFlag = false;
            List<ExpenseEntry> tempList = viewModel.ItemListE.ToList();
            viewModel.ItemListE = null;
            viewModel.ItemListE = new ObservableCollection<ExpenseEntry>(tempList);
         }

         var amount = 0.0;
         foreach (ExpenseEntry exp in listView.ItemsSource)
         {
            amount += exp.ExpenseAmount;
         }
         this.Title = this.Title.Remove(this.Title.IndexOf("$") + 1) + amount.ToString("0.00");
      }

      public void DisplayMenu()
      {
         if (viewModel.MenuVisible)
            viewModel.MenuVisible = false;
         else
            viewModel.MenuVisible = true;
      }

      public void OnSwipeLeft(object sender, SwipedEventArgs e)
      {
         var stack = (((sender as Grid).Parent.Parent as Grid).Children[1] as StackLayout);
         stack.IsVisible = true;

         var frame = ((sender as Grid).Parent as Frame);
         var grid = sender as Grid;
         if (Device.RuntimePlatform == Device.UWP || DeviceDisplay.MainDisplayInfo.Density <= 2)
         {
            Grid.SetColumnSpan(frame, 4);
            Grid.SetColumnSpan(stack, 1);
            Grid.SetColumn(stack, 4);
         }
         else
         {
            grid.ColumnDefinitions[1].Width = new GridLength(.4, GridUnitType.Star);
            grid.ColumnDefinitions[2].Width = new GridLength(.5, GridUnitType.Star);
            grid.ColumnDefinitions[3].Width = new GridLength(0, GridUnitType.Absolute);
            grid.ColumnDefinitions[4].Width = new GridLength(0, GridUnitType.Absolute);
            Grid.SetColumnSpan(frame, 1);
            var label = grid.Children[2];
            label.IsVisible = false;
         }

      }
      public void OnSwipeRight(object sender, SwipedEventArgs e)
      {
         var stack = (((sender as Grid).Parent.Parent as Grid).Children[1] as StackLayout);
         stack.IsVisible = false;

         var displayInfo = DeviceDisplay.MainDisplayInfo;
         var frame = ((sender as Grid).Parent as Frame);
         var grid = sender as Grid;
         if (Device.RuntimePlatform != Device.UWP)
         {
            grid.ColumnDefinitions[1].Width = new GridLength(.20, GridUnitType.Star);
            grid.ColumnDefinitions[2].Width = new GridLength(.35, GridUnitType.Star);
            grid.ColumnDefinitions[3].Width = new GridLength(.1, GridUnitType.Star);
            grid.ColumnDefinitions[4].Width = new GridLength(.25, GridUnitType.Star);
         }
         var label = grid.Children[2];
         label.IsVisible = true;

         Grid.SetColumnSpan(frame, 3);
         if (Device.RuntimePlatform == Device.UWP || DeviceDisplay.MainDisplayInfo.Density <= 2)
         {
            Grid.SetColumnSpan(stack, 2);
            Grid.SetColumn(stack, 1);
         }
      }


      async public void OnAddClick(object sender, EventArgs e)
      {

         var parent = this.Parent as NavigationPage;
         focusFlag = true;
         await parent.PushAsync(new AddItem("Expense", this.Title.Remove(this.Title.IndexOf("$") - 1)) { Title = "Add Expense Transaction" });
            
        }

      async public void OnExpenseTap(object sender, EventArgs e)
      {
         var parent = this.Parent as NavigationPage;
         var grid = (sender as Grid);
         var account = (ExpenseEntry)((TapGestureRecognizer)grid.GestureRecognizers[0]).CommandParameter;
         focusFlag = true;
         await parent.PushAsync(new AddItem(account, null) { Title = "Edit Expense Transaction" });

      }

      async public void OnDeleteClick(object sender, EventArgs e)
      {
         try
         {
            if (Connectivity.NetworkAccess == NetworkAccess.Internet)
            {
               bool choice = await DisplayAlert("ALERT", "Are you sure you want to delete?", "Delete", "Cancel");
               if (choice)
               {
                  var deleteID = (ExpenseEntry)((sender as Button).CommandParameter);

                  DataQuery.expenseSelect = "Select * From Totals ";
                  DataQuery.expenseWhere = "Where Account_id = (Select top (1) ID from Account where AccountName = '" + deleteID.AccountName + "' and User_ID = '" + int.Parse(Preferences.Get("ExpenseT_UserID", "")) + "')";
                  ObservableCollection<Totals> totals = DataQuery.ExecuteAQuery<Totals>();
                  totals[0].Total -= (float)deleteID.ExpenseAmount;

                  DataQuery.expenseSelect = "UPDATE [dbo].[Totals] SET [Total] = " + totals[0].Total.ToString("0.00");
                  DataQuery.expenseWhere = " Where ID = " + totals[0].ID;
                  int count = DataQuery.AlterDataQuery();

                  DataQuery.expenseSelect = "Select * From Totals ";
                  DataQuery.expenseWhere = "Where Account_id = (Select top (1) ID from Account where AccountName = '" + deleteID.IncomeAccountName + "' and User_ID = '" + int.Parse(Preferences.Get("ExpenseT_UserID", "")) + "')";
                  totals = DataQuery.ExecuteAQuery<Totals>();
                  totals[0].Total -= (float)deleteID.ExpenseAmount;

                  DataQuery.expenseSelect = "UPDATE [dbo].[Totals] SET [Total] = " + totals[0].Total.ToString("0.00");
                  DataQuery.expenseWhere = " Where ID = " + totals[0].ID;
                  count = DataQuery.AlterDataQuery();

                  DataQuery.expenseSelect = "Delete From Expense";
                  DataQuery.expenseWhere = "where id = " + deleteID.ID;
                  int results = DataQuery.AlterDataQuery();
                                         

                  focusFlag = true;
                  var parent = this.Parent as NavigationPage;
                  await parent.PopAsync();
               }
            }
            else
            {
               DependencyService.Get<IToast>().Show("No Internet Connection.");
            }
         }
         catch (Exception ex)
         {
            DependencyService.Get<IToast>().Show(ex.Message);
         }
      }

      public void OnEditClick(object sender, EventArgs e)
      {
         try
         {
            ExpenseEntry deleteID = null;
            if (sender is MenuItem)
               deleteID = (ExpenseEntry)((sender as Button).CommandParameter);
            else if (sender is Button)
               deleteID = (ExpenseEntry)((sender as Button).CommandParameter);
            Navigation.PushAsync(new AddItem(deleteID, null) { Title = "Edit " + deleteID.AccountName + " Account" });
            focusFlag = true;
         }
         catch (Exception ex)
         {
            DependencyService.Get<IToast>().Show(ex.Message);
         }
      }


      async public void OnSettings(object sender, EventArgs e)
      {
         viewModel.MenuVisible = false;
         var parent = this.Parent as NavigationPage;
         await parent.PushAsync(new SettingsPage() { Title = "Settings" });
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