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
   }
}