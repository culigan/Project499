﻿using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
   public class Account
   {
      public int ID { get; set; }
      public int AcountType_ID { get; set; }
      public string AccountName { get; set; }
      public string Description { get; set; }
      public DateTime DateCreated { get; set; }
   }
}
