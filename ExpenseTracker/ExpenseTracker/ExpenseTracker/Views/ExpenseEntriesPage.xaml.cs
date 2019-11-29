using System;
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
      string accountType = "";
      ViewModels.ExpenseEntriesViewModel viewModel;
      
      public ExpenseEntriesPage(int accountID, string accountName)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpenseEntriesViewModel(accountID);

         this.ToolbarItems.Add(new ToolbarItem("LogOut", "menu-button.png", () =>
         {
            OnLogOut();
         }));

         this.Title = accountName;
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
         Application.Current.MainPage = new LoginPage();
      }
   }
}