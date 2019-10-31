using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
   public class IncomeEntry
   {
      public int ID { get; set; }
      public int User_ID { get; set; }
      public int Account_ID { get; set; }
      public float IncomeAmount { get; set; }
      public int IncomeCategory_ID { get; set; }
      public DateTime ExpenseDate { get; set; }
      public Boolean Repeat { get; set; }
      public int RepeatPeriod_ID { get; set; }
   }
}
