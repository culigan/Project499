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
   public partial class IncomeEntriesPage : ContentPage
   {
      ViewModels.IncomeEntriesViewModel viewModel;
      
      public IncomeEntriesPage(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.IncomeEntriesViewModel(accountID);

         this.ToolbarItems.Add(new ToolbarItem("LogOut", "menu-button.png", () =>
         {
            OnLogOut();
         }));

      }

      async public void OnAddClick(object sender, EventArgs e)
      {
         
      }

      async public void OnIncomeTap(object sender, EventArgs e)
      {
         var parent = this.Parent as NavigationPage;

         await parent.PushAsync(new AddItem() { Title = "Edit Income" });
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