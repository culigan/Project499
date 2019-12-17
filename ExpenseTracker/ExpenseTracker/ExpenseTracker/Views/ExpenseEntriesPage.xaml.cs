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
         
            var parent = this.Parent as NavigationPage;
            await parent.PushAsync(new AddItem("Expense", this.Title.Remove(this.Title.IndexOf("$") - 1)) { Title = "Add Expense" });

        }

      async public void OnExpenseTap(object sender, EventArgs e)
      {
         var parent = this.Parent as NavigationPage;
         var grid = (sender as Grid);
         var account = (ExpenseEntry)((TapGestureRecognizer)grid.GestureRecognizers[0]).CommandParameter;
         await parent.PushAsync(new AddItem(account, null) { Title = "Edit Expense" });

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