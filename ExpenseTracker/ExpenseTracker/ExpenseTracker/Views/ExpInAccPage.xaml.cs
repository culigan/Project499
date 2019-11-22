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
   public partial class ExpIncAccPage : ContentPage
   {
      ViewModels.ExpIncAccViewModel viewModel;
      public ExpIncAccPage(string accountType)
      {
         InitializeComponent();
         BindingContext = viewModel = new ViewModels.ExpIncAccViewModel(accountType);


         this.ToolbarItems.Add(new ToolbarItem("LogOut", "menu-button.png", () =>
         {
            OnLogOut();
         }));

         this.Title = "Accounts";
      }

      async public void OnAddClick(object sender, EventArgs e)
      {
         await Navigation.PushModalAsync(new AddAccount());
      }

      public void OnLogOut()
      {
         Preferences.Clear();
         Application.Current.MainPage = new LoginPage();
      }
   }
}