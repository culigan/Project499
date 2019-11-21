using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using ExpenseTracker.Droid;

[assembly: Xamarin.Forms.Dependency(typeof(Toast_Android))]
namespace ExpenseTracker.Droid
{
   public class Toast_Android : IToast
   {
      public void Show(string message)
      {
         Android.Widget.Toast.MakeText(Android.App.Application.Context, message, ToastLength.Long).Show();
      }
   }
}
