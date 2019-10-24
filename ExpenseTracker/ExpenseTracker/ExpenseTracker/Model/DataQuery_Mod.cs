using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseTracker.Model
{
   public class DataQuery_Mod
   {
      private bool _IsConnected;
      public bool IsConnected
      {
         get { return _IsConnected; }
         set { _IsConnected = value; }
      }
      public DataQuery_Mod()
      {

      }
      public static List<string> CreateCommand(string queryString,  string connectionString)
      {
         List<string> returnData = new List<string>();
         using (SqlConnection connection = new SqlConnection(
               connectionString))
         {
            connection.Open();

            SqlCommand command = new SqlCommand(queryString, connection);
            SqlDataReader reader = command.ExecuteReader();
            
            while (reader.Read())
            {
               string rows = "";
               for(int i = 0; i < reader.FieldCount; i++)
               {
                  rows += reader[i].ToString() + ",";
               }
               returnData.Add(rows.ToString());
            }
            reader.Close();
            connection.Close();
         }
         return returnData;
      }

   }
}
