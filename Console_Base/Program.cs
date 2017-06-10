using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Console_Base
{
    class Program
    {
        static string connection_string;
        static string login;
        static string pass;

        static void Main(string[] args)
        {
            connection_string = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\oleg\Documents\Base_Azure.mdf;Integrated Security=True;Connect Timeout=30";
            authorization();
        }

        static void books_read()
        {
            string query2 = "SELECT * FROM Books";
            SqlConnection conn = new SqlConnection(connection_string);
            conn.Open();
            SqlCommand cmd2 = new SqlCommand(query2, conn);
            SqlDataReader dr = cmd2.ExecuteReader();

            while (dr.Read())
            {
                Console.WriteLine(dr["title"] + "; Автор: " + dr["author"]);
            }
            conn.Close();
        }

        static void authorization()
        {
            Console.WriteLine("Введите логин");
            login = Console.ReadLine();
            Console.WriteLine("Введите пароль");
            pass = Console.ReadLine();
            try
            {
                string query1 = String.Format("SELECT pass FROM Users WHERE login =  (N'{0}')", login);
                SqlConnection conn = new SqlConnection(connection_string);
                conn.Open();
                SqlCommand cmd1 = new SqlCommand(query1, conn);
                string x = (string)cmd1.ExecuteScalar();
                conn.Close();
                if (x == pass)
                {
                    books_read();
                }
                else Console.WriteLine("НЕправильный логин или пароль!!!");
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

    }
}
