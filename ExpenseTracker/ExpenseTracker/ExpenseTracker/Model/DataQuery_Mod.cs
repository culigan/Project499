using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace ExpenseTracker.Model
{
   public class DataQuery_Mod
   {
      protected string connectionString;
      private bool _IsConnected;
      public bool IsConnected
      {
         get { return _IsConnected; }
         set { _IsConnected = value; }
      }

      private string _QueryString;
      public string QuerySTring
      {
         get { return _QueryString; }
         set { _QueryString = value; }
      }

      public DataQuery_Mod()
      {
         connectionString = "Server=tcp:sqlexpensetracker.database.windows.net,1433;Initial Catalog=ExpenseTracker" +
            ";Persist Security Info=False;User ID=" + Resource1.ResourceManager.GetString("value1") +
            "; Password=\"" + Resource1.ResourceManager.GetString("value2") +
            "\";MultipleActiveResultSets=False;Encrypt=True;" +
            "TrustServerCertificate=False;Connection Timeout=30;";
      }

      private void ConnectToDatabase()
      {
         

         Model.DataQuery_Mod.CreateCommand("Select * from users", );

         
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
