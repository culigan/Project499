using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using ExpenseTracker.ViewModels;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddItem : ContentPage
    {
        AddItemViewModel viewModel;
        public AddItem()
        {
            InitializeComponent();
            BindingContext = viewModel = new AddItemViewModel();
            viewModel.TransType = "Test";
            viewModel.Account = "Test";
        }
        async void OnSaveButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }

        async void OnCancelButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopAsync();
        }
    }
}