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
            //return "68.183.31.237"; 157.230.174.83
            return "157.230.174.83"; 
        }
        public static string getAppFolder()
        {
            string folderPath = "";
            SqlDataReader dr = fetchReader("SELECT TOP(1) AppFolderPath FROM ServerInfo ORDER BY ServerInfoID DESC");
            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    folderPath = dr["AppFolderPath"].ToString();
                }                
            }
            return folderPath;
        }
        public static void updateAppFolder(string Path)
        {
            executeSQL("UPDATE ServerInfo SET AppFolderPath = '"+ Path + "' WHERE ServerInfoID = 1");
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
                Session.RemoveAll();
                Response.Redirect("/login");
            }
        }
        public static Double string2Double(string numero)
        {
            if (numero != "")
            {
                return Convert.ToDouble(numero.Replace(".", ","));
            }
            else
            {
                return 0;
            }
        }
        public static double NZDBNum(string pIn)
        {
            if(pIn == null)
            {
                return 0;
            }
            else
            {
                return string2Double(pIn);
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
                pSQL = pSQL.Replace("@registerAt", "CONVERT(datetime, '" + convertD2IDateTime(DateNow()) + "')");
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
                data += Convert.ToDouble(jsonMesssage.D8).ToString(nfi); //NH4 - 
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D9).ToString(nfi); //Latitud - Corriente
                data += ",";
                data += Convert.ToDouble(jsonMesssage.D10).ToString(nfi); //Longitud - Voltaje

                var data2InserSQL = data; //Copio la data para insertar la fecha en el formato que deseo
                if (topic.Contains("dron"))
                {
                    data += ",";
                    string fecha = jsonMesssage.D11;
                    if (Int32.Parse(fecha) > 0)
                    {
                        try
                        {

                        /*
                        var length = fecha.Length; // 5 70119 2 3
                        var daylen = 0;
                        if (length > 5) daylen = 2; 
                        else daylen = 1;
                        var year = Convert.ToInt32(fecha.Substring(length - 2, 2)) + 2000;
                        var Month = Convert.ToInt32(fecha.Substring(length - 4, 2));
                        var day = Convert.ToInt32(fecha.Substring(length - 5, daylen));
                        DateTime dt = new DateTime(year, Month, day);
                        data += dt.Date.ToString(); //Fecha
                        */
                        var length = fecha.Length; // 5 70119 2 3
                        if (length == 5) fecha = "0" + fecha;

                        var year = Convert.ToInt32(fecha.Substring(4, 2)) + 2000;
                        var Month = Convert.ToInt32(fecha.Substring(2, 2));
                        var day = Convert.ToInt32(fecha.Substring(0, 2));

                        DateTime dt = new DateTime(year, Month, day);
                        data += dt.Date.ToShortDateString(); //Fecha

                        }
                        catch(Exception ex)
                        {
                            saveInLogMQTT(ex);
                        }
                        finally
                        {
                            data += DateNow().ToString();
                        }
                    }
                    else data += DateNow().ToString();

                    //airQ

                    var otherFields = "[temperatura], [humedad], [presionAtmosferica], [Alcohol], [TVOC], [CO2], [Metano], [NH4], [Latitud], [Longitud], [fecha]";
                    pSQL = pSQL.Replace("@otherFields", otherFields);

                    data2InserSQL += ",";
                    data2InserSQL += "CONVERT(datetime, '" + convertD2IDateTime(DateNow()) + "')";
                    var otherValues = data2InserSQL;
                    pSQL = pSQL.Replace("@otherValues", otherValues);


                    pSQL = pSQL.Replace("@data", data);
                    executeSQLAirQ(pSQL);

                }
                else if (topic.Contains("printer"))
                {
                    data += ",";
                    data += jsonMesssage.D10; // Potencia electrica

                    data += ",";
                    data += convertD2SQLDate(DateNow()); //Fecha
                    //3DPrinterSupervisionSys
                    var otherFields = "[tempHotBed], [TempExt], [M1], [M2], [M3], [M4], [M5], [Corriente], [Voltaje], [PotenciaElectrica], [Fecha]";
                    pSQL = pSQL.Replace("@otherFields", otherFields);

                    data2InserSQL += ",";
                    data2InserSQL += "CONVERT(datetime, '" + convertD2IDateTime(DateNow()) + "')";
                    var otherValues = data2InserSQL;
                    pSQL = pSQL.Replace("@otherValues", otherValues);


                    pSQL = pSQL.Replace("@data", data);
                    executeSQLMonitor3D(pSQL);
                }
            }
            catch (Exception ex)
            {
                //("Error insertando el registro -- ", err);
                saveInLogMQTT(ex, msg);
            }
            finally
            {
            }
        }
        public static void saveInLogMQTT(Exception ex)
        {
            try
            {
                string pathFolder = @getAppFolder();
                string pathLog = @"Logs";
                string strFileName = "mosquitto.txt"; 

                string path = string.Format("{0}\\{1}\\{2}", pathFolder, pathLog, strFileName);
                FileStream objFilestream = new FileStream(path, FileMode.Append, FileAccess.Write);
            using (StreamWriter sw = new StreamWriter((Stream)objFilestream))
            {
                sw.WriteLine("=============Error Logging ===========");
                sw.WriteLine("===========Start============= " + DateTime.Now);
                sw.WriteLine("Error Message: " + ex.Message);
                sw.WriteLine("Stack Trace: " + ex.StackTrace);
                sw.WriteLine("Path App: " + path);
                sw.WriteLine("===========End============= " + DateTime.Now);

            }
            }
            catch (Exception Err)
            {

            }
        }
        public static void saveInLogMQTT(Exception ex, String jsonMsg)
        {
            try
            {
                string pathFolder = @getAppFolder();
                string pathLog = @"Logs";
                string strFileName = "mosquitto.txt";

                string path = string.Format("{0}\\{1}\\{2}", pathFolder, pathLog, strFileName);
                FileStream objFilestream = new FileStream(path, FileMode.Append, FileAccess.Write);
                using (StreamWriter sw = new StreamWriter((Stream)objFilestream))
                {
                    sw.WriteLine("=============Error Logging ===========");
                    sw.WriteLine("===========Start============= " + DateTime.Now);
                    sw.WriteLine("Error Message: " + ex.Message);
                    sw.WriteLine("Stack Trace: " + ex.StackTrace);
                    sw.WriteLine("Json Msg: " + jsonMsg);
                    sw.WriteLine("Path App: " + path);
                    sw.WriteLine("===========End============= " + DateTime.Now);

                }
            }
            catch (Exception Err)
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
        public static DateTime DateNow()
        {
            DateTime utcTime = DateTime.UtcNow;
            TimeZoneInfo myZone = TimeZoneInfo.CreateCustomTimeZone("COLOMBIA", new TimeSpan(-5, 0, 0), "Colombia", "Colombia");
            DateTime custDateTime = TimeZoneInfo.ConvertTimeFromUtc(utcTime, myZone);
            return custDateTime;
        }
        public static string convertD2SQLDate(DateTime dt)
        {
            return dt.ToString("yyyy/MM/ddThh:mm:ss", CultureInfo.InvariantCulture);
        }
        public static string convertD2IDate(DateTime datum)
        {
            return DateTime.Parse(datum.ToString()).ToString("yyyyMMdd");
        }

        public static string convertD2IDateTime(DateTime datum)
        {
            return DateTime.Parse(datum.ToString()).ToString("yyyy-MM-dd HH:mm:ss");
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