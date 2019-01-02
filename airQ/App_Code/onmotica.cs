using System;
using System.Collections.Generic;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Text.RegularExpressions;
using System.IO;

namespace airQ.App_Code
{
    //onmotica is an Miguel Califa creation :) 2018 C
    public class onmotica
    {
        public static void isLogged(System.Web.SessionState.HttpSessionState Session, HttpResponse Response, String location)
        {
            if (Session["UsrID"] != null & Session["UsrName"] != null)
            {
                if (Convert.ToInt32(Session["UsrID"]) > 0 & Session["UsrName"].ToString() != "" & (location == "default" || location == "login"))
                {
                    Response.Redirect("/dashboard");
                }
            }
            else if(location!="login" & location!="default")
            {
                Response.Redirect("/login");
            }
        }
        public static void executeSQL(string query)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            myConn.Open();
            cmd.CommandText = query;
            cmd.Connection = myConn;
            cmd.ExecuteNonQuery();
            myConn.Close();
        }
        public static void executeSQLMonitor3D(string query)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["3DMonitorConnectionString"].ConnectionString);
            myConn.Open();
            cmd.CommandText = query;
            cmd.Connection = myConn;
            cmd.ExecuteNonQuery();
            myConn.Close();
        }
        public static void executeSQLAirQ(string query)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            myConn.Open();
            cmd.CommandText = query;
            cmd.Connection = myConn;
            cmd.ExecuteNonQuery();
            myConn.Close();
        }
        public static SqlDataReader fetchReader(string query)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            myConn.Open();
            SqlCommand myCmd = new SqlCommand(query, myConn);
            SqlDataReader dr = myCmd.ExecuteReader(CommandBehavior.CloseConnection);
            return dr;
        }

        public static DataSet fetchData(string query)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            SqlDataAdapter myAdapter = new SqlDataAdapter(query, myConn);
            DataSet myData = new DataSet();
            myAdapter.Fill(myData);
            return myData;
        }

        public static object fetchScalar(string query)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            SqlCommand myCmd = new SqlCommand(query, myConn);
            myConn.Open();
            object scalar = myCmd.ExecuteScalar();
            myConn.Close();
            return scalar;
        }

        public static object fetchScalar(string query, int timeToWait)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            SqlCommand myCmd = new SqlCommand(query, myConn);
            myConn.Open();
            myCmd.CommandTimeout = timeToWait;
            object scalar = myCmd.ExecuteScalar();
            myConn.Close();
            return scalar;
        }

        public static SqlDataReader fetchReader(SqlCommand myCmd)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            myCmd.Connection = myConn;
            myConn.Open();
            return myCmd.ExecuteReader(CommandBehavior.CloseConnection);
        }

        public static DataSet fetchData(SqlCommand myCmd)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            myCmd.Connection = myConn;
            SqlDataAdapter myAdapter = new SqlDataAdapter(myCmd);
            DataSet myData = new DataSet();
            myAdapter.Fill(myData);
            return myData;
        }

        public static object fetchScalar(SqlCommand myCmd)
        {
            SqlConnection myConn = new SqlConnection(ConfigurationManager.ConnectionStrings["AirQConnectionString"].ConnectionString);
            myCmd.Connection = myConn;
            myConn.Open();
            myCmd.CommandTimeout = 32400; //32400 sec = 9 hours
            object scalar = myCmd.ExecuteScalar();
            myConn.Close();
            return scalar;
        }
        public static string convertD2IDate(DateTime datum)
        {
            return DateTime.Parse(datum.ToString()).ToString("yyyyMMdd");
        }

        public static string convertD2IDateTime(DateTime datum)
        {
            return DateTime.Parse(datum.ToString()).ToString("yyyyMMdd HH:mm:ss");
        }

        public static string convertD2PGDate(string datum)
        {
            return DateTime.Parse(datum).ToString("dd.MM.yyyy");
        }

        public static string convertD2PGDateTime(string datum)
        {
            return DateTime.Parse(datum).ToString("dd.MM.yyyy HH:mm:ss");
        }

        public static string convertG2IDate(string datum)
        {
            return DateTime.Parse(datum).ToString("yyyyMMdd");
        }

    }
}