

namespace SupermarketsChain.LoadToMySQL
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Data;
    using System.Drawing;
    using System.Linq;
    using System.Text;
    using MySql.Data.MySqlClient;
    using SupermarketsChain.Models;
    public class Program
    {
        public static void Main()
        {
            string cs = "Server = localhost; Database = test; Uid = root; Pwd = 12345;";

            MySqlConnection conn = null;

            try
            {
                conn = new MySqlConnection(cs);
                conn.Open();
                Console.WriteLine("MySQL version : {0}", conn.ServerVersion);

            }
            catch (MySqlException ex)
            {
                Console.WriteLine("Error: {0}", ex.ToString());

            }
            finally
            {
                if (conn != null)
                {
                    conn.Close();
                }
            }



        }

    }
}
