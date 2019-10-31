using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
   public class Sync
   {
      public int ID { get; set; }
      public int Users_ID { get; set; }
      public DateTime LastSyncDate { get; set; }
      public int LastExpense_ID { get; set; }
      public int LastIncome_ID { get; set; }
      public int LastAccount_ID { get; set; }
   }
}
