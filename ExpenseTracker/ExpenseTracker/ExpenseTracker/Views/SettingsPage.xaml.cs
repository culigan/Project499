using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Essentials;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SettingsPage : ContentPage
   {
      ViewModels.SettingsViewModel viewModel;

      public SettingsPage()
      {
         InitializeComponent();
         this.BindingContext = viewModel = new ViewModels.SettingsViewModel();
      }

        async void OnAdd(object sender, EventArgs e)
        {
            String userID = Preferences.Get("ExpenseT_UserID", "NULL");
            viewModel.IsBusy = true;
            try
            {
                if (Connectivity.NetworkAccess == NetworkAccess.Internet)
                {
                    viewModel.DataQuery.expenseSelect = "INSERT INTO ExpenseCategory VALUES ";
                    viewModel.DataQuery.expenseWhere = "('" + viewModel.CategoryName + "', 'New User Category', '" + userID + "')";
                    viewModel.ExpenseCategoryInfo = viewModel.DataQuery.ExecuteAQuery<Exp_Inc_Category>();
                    DependencyService.Get<IToast>().Show("New Category " + viewModel.CategoryName + " Created");
                }
            }
            catch (Exception ex)
            {
                await DisplayAlert("Adding account failed", ex.Message, "OK");
                viewModel.IsBusy = false;
            }
        }

      private void OnSave(object sender, EventArgs e)
      {
         Preferences.Set("start_date", viewModel.PickerStartDate);
         if (viewModel.PickerEndDate != DateTime.Now)
            Preferences.Set("end_date", viewModel.PickerEndDate);
         else
         {
            Preferences.Remove("end_date");
            
         }
         Navigation.PopAsync();
      }

      private void OnCancel(object sender, EventArgs e)
      {
         Navigation.PopAsync();
      }
   }
}