using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace ExpenseTracker
{
   public partial class MainPage : ContentPage
   {

      List<string> _ExpenseList;
      List<string> ExpenseList
      {
         get { return _ExpenseList; }
         set { _ExpenseList = value; }
      }

      public MainPage()
      {
         InitializeComponent();
         BindingContext = new RunProgram();
         
      }
   }
}
