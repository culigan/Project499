using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

      private void OnAdd(object sender, EventArgs e)
      {

      }

      private void OnDelete(object sender, EventArgs e)
      {

      }

      private void OnSave(object sender, EventArgs e)
      {

      }

      private void OnCancel(object sender, EventArgs e)
      {
         Navigation.PopAsync();
      }
   }
}