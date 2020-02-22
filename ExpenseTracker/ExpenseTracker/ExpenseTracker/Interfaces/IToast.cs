using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
   public interface IToast
   {
      void Show(string message);
   }
}
