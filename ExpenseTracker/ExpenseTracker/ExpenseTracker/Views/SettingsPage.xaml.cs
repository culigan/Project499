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