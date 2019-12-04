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
      ViewModels.ExpenseEntriesViewModel viewModel;
      
      public ExpenseEntriesPage(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpenseEntriesViewModel(accountID);
         
         this.ToolbarItems.Add(new ToolbarItem("LogOut", "", () =>
         {
            OnLogOut();
         }));

      }

      async public void OnAddClick(object sender, EventArgs e)
      {
         await Navigation.PushModalAsync(new AddAccount());
      }

      async public void OnExpenseTap(object sender, EventArgs e)
      {
         
      }

      public void OnLogOut()
      {
         Preferences.Clear();
         var accountsPage = new NavigationPage(new LoginPage() { Title = "Login" });
         NavigationPage.SetHasBackButton(accountsPage, true);
         App.Current.MainPage = accountsPage;
      }
   }
}