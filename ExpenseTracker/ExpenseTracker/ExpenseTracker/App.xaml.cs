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
            MainPage = new Views.AccountsPage
            {
               Title = "Accounts",
               Children = {
                  new Views.ExpIncAccPage("ExpenseAccount"),
                  new Views.ExpIncAccPage("IncomeAccount")
               }
            };
         }
         else 
            MainPage = new NavigationPage(new LoginPage());
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
