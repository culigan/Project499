using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
   public class ExpenseEntry
   {
      public int ID { get; set; }
      public int User_ID { get; set; }
      public string AccountName { get; set; }
      public float ExpenseAmount { get; set; }
      public string IncomeAccountName { get; set; }
      private string _ExpenseDate;
      public string ExpenseDate { get { return _ExpenseDate; } set { _ExpenseDate = value; } }
      public string ExpenseCategory { get; set; }
      public Boolean Repeat { get; set; }
      public string RepeatPeriod { get; set; }
   }
}
