using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Linq;
using System.Text;

using Foundation;
using ExpenseTracker.iOS;
using UIKit;

[assembly:Xamarin.Forms.Dependency(typeof(Toast_IOS))]
namespace ExpenseTracker.iOS
{
   public class Toast_IOS : IToast
   {
      const double LONG_DAY = 3.5;

      NSTimer alertDelay;
      UIAlertController alert;

      public void Show(string message)
      {
         ShowAlert(message, LONG_DAY);
      }

      void ShowAlert(string message, double seconds)
      {
         alertDelay = NSTimer.CreateScheduledTimer(seconds, (obj) =>
         {
            dismissMessage();
         });
         alert = UIAlertController.Create(null, message, UIAlertControllerStyle.Alert);
         UIApplication.SharedApplication.KeyWindow.RootViewController.PresentViewController(alert, true, null);
      }
       
      void dismissMessage()
      {
         if (alert != null)
            alert.DismissViewController(true, null);
         if (alertDelay != null)
            alertDelay.Dispose();
      }
   }
}