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

using System.Data.SqlClient;
using System.Globalization;
using Newtonsoft.Json;

namespace airQ.App_Code
{
    //onmotica is an Miguel Califa creation :) 2018 C

    public class onmotica
    {

        public static string getBrokerAddress()
        {
            return "68.183.31.237";
        }
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
        public static void saveIntoDB(string msg, string topic)
        {
            NumberFormatInfo nfi = new NumberFormatInfo();
            nfi.NumberDecimalSeparator = ".";

            var pSQL = "INSERT INTO [measurements] ([topic], [data], [registerAt], [activ], @otherFields) VALUES ('@topic', '@data', @registerAt, '@activ', @otherValues)";

            try
            {
                dynamic jsonMesssage = JsonConvert.DeserializeObject(msg);
                var data = "";
                pSQL = pSQL.Replace("@topic", topic);
                pSQL = pSQL.Replace("@registerAt", "GETDATE()");
                pSQL = pSQL.Replace("@activ", "1");

                data += Convert.ToDouble(jsonMesssage.D1).ToString(nfi);//temperatura - Cama caliente
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D2).ToString(nfi); //humedad - Extrusor
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D3).ToString(nfi); //Presion atmosferica - Motor 1
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D4).ToString(nfi); //Alcoholes - Motor 2
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D5).ToString(nfi); //TVOC - Motor 3
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D6).ToString(nfi); //CO2 - Motor 4
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D7).ToString(nfi); //Gas metano - Motor 5
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D8).ToString(nfi); //Latitud - Corriente
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D9).ToString(nfi); //Longitud - Voltaje
                data += ",";
                data += jsonMesssage.D10; //Fecha - Potencia electrica

                if (topic.Contains("dron") & Convert.ToInt32(jsonMesssage.D1) > 0)
                {
                    //airQ

                    var otherFields = "[temperatura], [humedad], [presionAtmosferica], [Alcohol], [TVOC], [CO2], [NH4], [Latitud], [Longitud], [fecha]";
                    pSQL = pSQL.Replace("@otherFields", otherFields);
                    var otherValues = data;
                    pSQL = pSQL.Replace("@otherValues", otherValues);

                    pSQL = pSQL.Replace("@data", data);
                    executeSQLAirQ(pSQL);

                }
                else if (topic.Contains("printer") & Convert.ToInt32(jsonMesssage.D1) > 0)
                {
                    //3DPrinterSupervisionSys
                    var otherFields = "[tempHotBed], [TempExt], [M1], [M2], [M3], [M4], [M5], [Corriente], [Voltaje], [PotenciaElectrica]";
                    pSQL = pSQL.Replace("@otherFields", otherFields);
                    var otherValues = data;
                    pSQL = pSQL.Replace("@otherValues", otherValues);

                    pSQL = pSQL.Replace("@data", data);
                    executeSQLMonitor3D(pSQL);
                }
            }
            catch (Exception err)
            {
                //("Error insertando el registro -- ", err);
            }
            finally
            {

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