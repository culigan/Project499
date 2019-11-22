using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace ExpenseTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class ExpIncAccPage : ContentPage
   {
      
      public ExpIncAccPage(string accountType)
      {
         InitializeComponent();
         BindingContext = new ViewModels.ExpIncAccViewModel(accountType);
         if (accountType.ToUpper() == "EXPENSEACCOUNT")
         {
            this.Title = "Expenses";
         }
         else if (accountType.ToUpper() == "INCOMEACCOUNT")
         {
            this.Title = "Income";
         }
      }

      async public void OnAddClick(object sender, EventArgs e)
      {
         await Navigation.PushModalAsync(new AddAccount());
      }
   }
}