using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginPage : ContentPage
    {
        public LoginPage()
        {
            InitializeComponent();
            
        }

        async void OnLoginButtonClicked(object sender, EventArgs e)
        {
         List<string> userData = Model.DataQuery_Mod.CreateCommand("Select * from users", "Server=tcp:" +
            "sqlexpensetracker.database.windows.net,1433;Initial Catalog=ExpenseTracker" +
            ";Persist Security Info=False;User ID=TeamAdmin;Password=\"Gy773Hv123;b\";" +
            "MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            await Navigation.PushAsync(new MainPage(userData));
        }

        async void OnNewUserButtonClicked(object sender, EventArgs e)
        {
            await Navigation.PushAsync(new MainPage { });
        }
    }
    

    
}