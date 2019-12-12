﻿using System;
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
      public string expenseSelect = "";
     
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
               if (prop.Name != "AccountBalance" && !object.Equals(dr[prop.Name], DBNull.Value))
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
            DependencyService.Get<IToast>().Show(ex.Message);
            throw new Exception(ex.Message);
         }
      }

      public string ExecuteAQuery()
      {
         try
         {
            string queryString = expenseSelect + " " + expenseWhere;

            using (SqlConnection connection = new SqlConnection(
                  connectionString))
            {
               connection.Open();

               SqlCommand command = new SqlCommand(queryString, connection);
               SqlDataReader reader = command.ExecuteReader();
               List<string> returnString = new List<string>();
               while (reader.Read())
               {
                  returnString.Add(reader.GetValue(0).ToString());
               }
               reader.Close();
               connection.Close();

               return string.Join(",", returnString);
            }

         }
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }


      public int AlterDataQuery()
      {
         try
         {
            string queryString = expenseSelect + " " + expenseWhere;
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
         catch (Exception ex)
         {
            throw new Exception(ex.Message);
         }
      }

   }
}
