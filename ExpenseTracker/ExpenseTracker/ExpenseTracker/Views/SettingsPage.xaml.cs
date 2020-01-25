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

      private void OnSelect(object sender, EventArgs e)
      {
         viewModel.AddEditName = viewModel.CatSelected.CategoryName;
         viewModel.AddEditDesc = viewModel.CatSelected.CategoryDesc;
      }

      private void OnSelectI(object sender, EventArgs e)
      {
         viewModel.AddEditNameI = viewModel.CatSelectedI.CategoryName;
         viewModel.AddEditDescI = viewModel.CatSelectedI.CategoryDesc;
      }

      private void OnDelete(object sender, EventArgs e)
      {

      }

      private void OnSave(object sender, EventArgs e)
      {
         viewModel.CatSelected = null;
      }

      private void OnCancel(object sender, EventArgs e)
      {
         Navigation.PopAsync();
      }
   }
}