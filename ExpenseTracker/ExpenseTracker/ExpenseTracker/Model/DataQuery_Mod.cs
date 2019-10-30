using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;

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

      private List<string> _QueryResults;
      public List<string> QueryResults
      {
         get { return _QueryResults; }
         set { _QueryResults = value; }
      }

      public DataQuery_Mod()
      {
         connectionString = Resource1.ResourceManager.GetString("connections") + "User ID=" + Resource1.ResourceManager.GetString("value1") +
            "; Password=\"" + Resource1.ResourceManager.GetString("value2") + "\";";
      }

      public void ExecuteAQuery(string queryString)
      {
         Task.Run(() =>
         {
            QueryResults = new List<string>();
            using (SqlConnection connection = new SqlConnection(
                  connectionString))
            {
               connection.Open();

               SqlCommand command = new SqlCommand(queryString, connection);
               SqlDataReader reader = command.ExecuteReader();

               while (reader.Read())
               {
                  string rows = "";
                  for (int i = 0; i < reader.FieldCount; i++)
                  {
                     rows += reader[i].ToString() + ",";
                  }
                  QueryResults.Add(rows.ToString());
               }
               reader.Close();
               connection.Close();
            }
         });
      }

      public int AlterDataQuery(string queryString)
      {
         int rowAffected = 0;
         using (SqlConnection connection = new SqlConnection(
               connectionString))
         {
            connection.Open();

            SqlCommand command = new SqlCommand(queryString, connection);
            rowAffected = command.ExecuteNonQuery();

            
            connection.Close();
         }
         return rowAffected;
      }

   }
}
