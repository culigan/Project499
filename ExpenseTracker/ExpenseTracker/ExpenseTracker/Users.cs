using System;
using System.Collections.Generic;
using System.Text;

namespace ExpenseTracker
{
   public class Users
   {
      public int ID { get; set; }
      public string Username { get; set; }
      public string FirstName { get; set; }
      public string LastName { get; set; }
      public string Password { get; set; }
      public DateTime CreatedDate { get; set; }
   }
}
