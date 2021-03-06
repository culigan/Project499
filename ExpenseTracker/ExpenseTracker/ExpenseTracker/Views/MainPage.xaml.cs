﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
//Testing to make sure I can update the repository. -JS

namespace ExpenseTracker
{
   public partial class MainPage : ContentPage
   {

      List<string> _ExpenseList;
      List<string> ExpenseList
      {
         get { return _ExpenseList; }
         set { _ExpenseList = value; }
      }

      public MainPage()
      {
         InitializeComponent();
         BindingContext = new RunProgram();
         
         
      }
      public MainPage(List<string>DisplayData)
      {
         InitializeComponent();
         BindingContext = new RunProgram(DisplayData);
         DependencyService.Get<IToast>().Show("Toast Message");
      }

      async public void OnLogOut(object sender, EventArgs e)
      {
         Preferences.Clear();
         await Navigation.PopAsync();
      }
   }
}
