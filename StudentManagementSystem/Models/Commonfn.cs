using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
using System.Linq;
using System.Web;

namespace StudentManagementSystem.Models
{
    public class Commonfn
    {
        public class Commonfnx
        {
            MySqlConnection _connection=new MySqlConnection(ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString);
            public void Query(string query, params MySqlParameter[] parameters)
            {
                using (MySqlConnection conn = new MySqlConnection(
                    ConfigurationManager.ConnectionStrings["SchoolCS"].ConnectionString))
                {
                    conn.Open();
                    using (MySqlCommand cmd = new MySqlCommand(query, conn))
                    {
                        if (parameters != null)
                        {
                            cmd.Parameters.AddRange(parameters);
                        }
                        cmd.ExecuteNonQuery();
                    }
                }
            }

            public DataTable Fetch(string query)
            {
                DataTable dt = new DataTable();

                try
                {
                    if (_connection.State == ConnectionState.Closed)
                    {
                        _connection.Open();
                    }
                    MySqlCommand cmd = new MySqlCommand(query, _connection);
                    MySqlDataAdapter sda = new MySqlDataAdapter(cmd);
                    sda.Fill(dt);
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex.Message);
                }
                finally
                {
                    _connection.Close();
                }

                return dt;
            }
            
        }
    }
}