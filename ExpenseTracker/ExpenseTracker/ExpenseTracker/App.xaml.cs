using System;
using System.IO;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using Xamarin.Essentials;


[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace ExpenseTracker
{
   public partial class App : Application
   {
        public static string FolderPath { get; private set; }
      public App()
      {
         InitializeComponent();
         FolderPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData));
         
         if (Preferences.Get("ExpenseT_UserID", "NotFound") != "NotFound")
         {
            var accountsPage = new NavigationPage(new Views.AccountsPage() { Title = "Accounts Page" });
            NavigationPage.SetHasBackButton(accountsPage, true);
            MainPage = accountsPage; 
         }
         else
         {
            var accountsPage = new NavigationPage(new LoginPage() { Title = "Login" });
            NavigationPage.SetHasBackButton(accountsPage, true);
            MainPage = accountsPage;
         }
      }

      protected override void OnStart()
      {
         // Handle when your app starts
      }

      protected override void OnSleep()
      {
         // Handle when your app sleeps
      }

      protected override void OnResume()
      {
         // Handle when your app resumes
      }
   }
}
