using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;

namespace Assignment
{
    public class ProcessDatabase : System.Web.Services.WebService
    {
        static string str = "Data Source=DESKTOP-INLGI8J\\SQLEXPRESS;Initial Catalog="+"ASSIGNMENT"+";Integrated Security=True";
        static SqlConnection con = new SqlConnection(str);
        public static void connectData()
        {
            if(con.State != ConnectionState.Open)
            {
                con.Open();
            }
        }
        public static void disconnectData()
        {
            if(con.State != ConnectionState.Closed)
            {
                con.Close();
            }
        }

        public static DataTable getData(string str)
        {
            connectData();
            SqlDataAdapter adapter = new SqlDataAdapter(str, con);
            DataTable temp = new DataTable();
            adapter.Fill(temp);
            disconnectData();
            return temp;
        }

        public static void addData(string str)
        {
            connectData();
            SqlCommand comd = new SqlCommand(str, con);
            try
            {
                comd.ExecuteNonQuery();
            }catch(System.Exception ex)
            {
                
            }
            disconnectData();
        }

        public static void UpdateData(string str)
        {
            connectData();
            SqlCommand comd = new SqlCommand(str, con);
            try
            {
                comd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
            disconnectData();
        }
        public static void deleteData(string str)
        {
            connectData();
            SqlCommand comd = new SqlCommand(str, con);
            try
            {
                comd.ExecuteNonQuery();
            }
            catch (System.Exception ex)
            {

            }
            disconnectData();
        }

    }
}