using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
   public class IncomeEntry
   {
      public int ID { get; set; }
      public int User_ID { get; set; }
      public string AccountName { get; set; }
      public double IncomeAmount { get; set; }
      public string IncomeCategory { get; set; }
      public DateTime IncomeDate { get; set; }
      public Boolean Repeat { get; set; }
      public string RepeatPeriod { get; set; }
   }
}
