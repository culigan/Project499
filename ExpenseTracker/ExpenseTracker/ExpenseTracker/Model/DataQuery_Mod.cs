using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Essentials;
using System.Reflection;
using Newtonsoft.Json;

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

      public string expenseWhere = "";
      public string expenseSelect = "SELECT ex.[ID], ex.[ExpenseAmount], ex.[User_ID], ac.AccountName, Ac1.AccountName, " +
	      "ex.[ExpenseDate], ex.[Repeat], Rp.RepeatPeriod FROM[dbo].[Expense] ex join Account Ac on Ex.Account_ID = Ac.ID " +
         "inner join Account Ac1 on ex.IncomeAccount_ID = Ac1.ID inner join RepeatPeriod Rp on ex.RepeatPeriod_ID = Rp.ID";
     
      public DataQuery_Mod()
      {
         connectionString = Resource1.ResourceManager.GetString("connections") + "User ID=" + Resource1.ResourceManager.GetString("value1") +
            "; Password=\"" + Resource1.ResourceManager.GetString("value2") + "\";";
      }

      private ObservableCollection<T> DataReaderMapToList<T>(IDataReader dr)
      {
         ObservableCollection<T> list = new ObservableCollection<T>();
         T obj = default(T);
         while (dr.Read())
         {
            obj = Activator.CreateInstance<T>();
            foreach (PropertyInfo prop in obj.GetType().GetProperties())
            {
               if (!object.Equals(dr[prop.Name], DBNull.Value))
               {
                  prop.SetValue(obj, dr[prop.Name], null);
               }
            }
            list.Add(obj);
         }
         return list;
      }
      public ObservableCollection<T> ExecuteAQuery<T>()
      {
         try
         {
            string queryString = expenseSelect + " " + expenseWhere;
            //string returnString = "{";
            using (SqlConnection connection = new SqlConnection(
                  connectionString))
            {
               connection.Open();

               SqlCommand command = new SqlCommand(queryString, connection);
               SqlDataReader reader = command.ExecuteReader();
               ObservableCollection<T> expenseEntries = DataReaderMapToList<T>(reader);
               
               reader.Close();
               connection.Close();

               return expenseEntries;
            }
            
         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
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
