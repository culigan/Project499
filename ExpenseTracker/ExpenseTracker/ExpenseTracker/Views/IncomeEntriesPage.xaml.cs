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
   public partial class IncomeEntriesPage : ContentPage
   {
      bool focusFlag = false;
      double balance = 0.0;
      ViewModels.IncomeEntriesViewModel viewModel;
      
      public IncomeEntriesPage(int accountID)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.IncomeEntriesViewModel(accountID);
         
         this.ToolbarItems.Add(new ToolbarItem()
         {
            IconImageSource = "hamburger.png",
            Command = new Command(() =>
            {
               DisplayMenu();
            })
         });

         base.Appearing += IncomeEntriesPage_Appearing;
      }

      private void IncomeEntriesPage_Appearing(object sender, EventArgs e)
      {
         if (focusFlag)
         {
            focusFlag = false;
            List<IncomeEntry> tempList = viewModel.ItemListE.ToList();
            viewModel.ItemListE = null;
            viewModel.ItemListE = new ObservableCollection<IncomeEntry>(tempList);

            var amount = 0.0;
            foreach(IncomeEntry inc in listView.ItemsSource)
            {
               amount += inc.IncomeAmount;
            }
            this.Title = this.Title.Remove(this.Title.IndexOf("$") + 1) + amount.ToString("0.00");
         }
      }

      public void DisplayMenu()
      {
         if (viewModel.MenuVisible)
            viewModel.MenuVisible = false;
         else
            viewModel.MenuVisible = true;
      }

      async public void OnAddClick(object sender, EventArgs e)
      {
            var parent = this.Parent as NavigationPage;
         focusFlag = true;
            await parent.PushAsync(new AddItem("Income", this.Title.Remove(this.Title.IndexOf("$") - 1)) { Title = "Add Income" });
        }

      async public void OnIncomeTap(object sender, EventArgs e)
      {
         var parent = this.Parent as NavigationPage;
         var grid = (sender as Grid);
         var account = (IncomeEntry)((TapGestureRecognizer)grid.GestureRecognizers[0]).CommandParameter;
         focusFlag = true;
         await parent.PushAsync(new AddItem(null, account) { Title = "Edit Income" });

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