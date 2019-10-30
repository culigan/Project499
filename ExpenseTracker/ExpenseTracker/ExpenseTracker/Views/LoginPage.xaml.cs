using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
         string ID = Preferences.Get("ExpenseT_UserID", "didn'tWork");//this works for local storage
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {         
            await Navigation.PushAsync(new MainPage());
        }

        async void OnNewUserButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage());
        }
    }
    

    
}