﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Essentials;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class AccountsPage : TabbedPage
   {
      ViewModels.AccountsViewModel viewModel;
      public AccountsPage()
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.AccountsViewModel();
         NavigationPage navigationPage = new NavigationPage(new ExpIncAccPage("ExpenseAccount"));
         navigationPage.Title = "Expenses";

         NavigationPage navigationPage1 = new NavigationPage(new ExpIncAccPage("IncomeAccount"));
         navigationPage1.Title = "Income";

         Children.Add(navigationPage);
         Children.Add(navigationPage1);
      }

      async public void OnLogOut(object sender, EventArgs e)
      {
         Preferences.Clear();
         await Navigation.PopAsync();
      }
   }


}