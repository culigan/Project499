using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AddAccount : ContentPage
    {
        public AddAccount()
        {
            InitializeComponent();
        }

        async void OnSaveAccountButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }

        async void OnDeleteAccountButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PopModalAsync();
        }
    }
}