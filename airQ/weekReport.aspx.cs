using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;
using System.Data;

namespace airQ
{
    public partial class weekReport : System.Web.UI.Page
    {
        CultureInfo enUS = new CultureInfo("en-US");
        string dateString;
        DateTime dateValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            onmotica.isLogged(Session, Response, "monthReport");
            if(!Page.IsPostBack)
            { 
                var now = DateTime.Now;
                if (Session["month"]==null)                
                {
                    txtDate.Text = DateTime.Now.Date.ToString("dd/MM/yyyy");
                }
                dateString = txtDate.Text == "" ? DateTime.Now.Date.ToString():txtDate.Text;
                dateValue = DateTime.Parse(dateString.ToString());
                Session["month"] = dateValue.ToString("MM");
                Session["year"] = dateValue.ToString("yyyy");
                Session["day"] = dateValue.ToString("dd");

                Session["endDay"] = dateValue.AddDays(7).ToString("dd");
                calculatePromsAndResultates();
            }
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            dateString = txtDate.Text;
            dateValue = DateTime.Parse(dateString);
            Session["month"] = dateValue.ToString("MM");
            Session["year"] = dateValue.ToString("yyyy");
            Session["day"] = dateValue.ToString("dd");

            Session["endDay"] = dateValue.AddDays(7).ToString("dd");

            calculatePromsAndResultates();
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            dateString = txtDate.Text == "" ? DateTime.Now.Date.ToString() : txtDate.Text;

            dateValue = DateTime.Parse(dateString);
            Session["month"] = dateValue.ToString("MM");
            Session["year"] = dateValue.ToString("yyyy");
            Session["day"] = dateValue.ToString("dd");

            Session["endDay"] = dateValue.AddDays(7).ToString("dd");

            calculatePromsAndResultates();
        }

        protected void calculatePromsAndResultates()
        {
            dateString = txtDate.Text == "" ? DateTime.Now.Date.ToString() : txtDate.Text;

            var campoFecha = DateTime.Parse(dateString); //Fecha de inicio
            var fechaFin = campoFecha.AddDays(7);
            var pSQL = "SELECT temperatura, humedad, presionAtmosferica, Alcohol, TVOC, CO2, NH4, Metano FROM measurements WHERE registerAt >= '" + onmotica.convertD2IDate(campoFecha) + "' AND registerAt < '" + onmotica.convertD2IDate(fechaFin) + "'";
            SqlDataReader dr = onmotica.fetchReader(pSQL);

            DataTable pResult = new DataTable();

            DataTable promResult = new DataTable();

            pResult.Columns.Add("Nombre", typeof(String));
            pResult.Columns.Add("Resultado", typeof(String));
            pResult.Columns.Add("Recomendacion", typeof(String));

            promResult.Columns.Add("Temperatura", typeof(Double));
            promResult.Columns.Add("Humedad", typeof(Double));
            promResult.Columns.Add("PresionAtmosferica", typeof(Double));
            promResult.Columns.Add("Alcohol", typeof(Double));
            promResult.Columns.Add("TVOC", typeof(Double));
            promResult.Columns.Add("CO2", typeof(Double));
            promResult.Columns.Add("Metano", typeof(Double));
            promResult.Columns.Add("NH4", typeof(Double));

            double temp = 0, hum = 0, presAt = 0, Alcohol = 0, CO2 = 0, TVOC = 0, Metano = 0, NH4 = 0;
            int numMuestras = 0;

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    temp += onmotica.NZDBNum(dr["Temperatura"].ToString());
                    hum += onmotica.NZDBNum(dr["Humedad"].ToString());
                    presAt += onmotica.NZDBNum(dr["PresionAtmosferica"].ToString());
                    Alcohol += onmotica.NZDBNum(dr["Alcohol"].ToString());
                    CO2 += onmotica.NZDBNum(dr["TVOC"].ToString());
                    TVOC += onmotica.NZDBNum(dr["CO2"].ToString());
                    Metano += onmotica.NZDBNum(dr["Metano"].ToString());
                    NH4 += onmotica.NZDBNum(dr["NH4"].ToString());
                    numMuestras++;
                }

            }

            if (temp > 0) { temp = temp / numMuestras; };
            if (hum > 0) { hum = hum / numMuestras; };
            if (presAt > 0) { presAt = presAt / numMuestras; };
            if (Alcohol > 0) { Alcohol = Alcohol / numMuestras; };
            if (CO2 > 0) { CO2 = CO2 / numMuestras; };
            if (TVOC > 0) { TVOC = TVOC / numMuestras; };
            if (Metano > 0) { Metano = Metano / numMuestras; };
            if (NH4 > 0) { NH4 = NH4 / numMuestras; };

            promResult.Rows.Add(temp, hum, presAt, Alcohol, CO2, TVOC, Metano, NH4);

            if (temp == 0)
            {
                pResult.Rows.Add("Alerta en temperatura", "La temperatura promedio es igual a 0ºC", "Verifique la conexion del sensor de temperatura");
            }
            else
            {
                pResult.Rows.Add("La temperatura promedio es: " + temp.ToString() + " ºC", "Este valor se obtuvo promediando " + numMuestras.ToString() + " Mediciones individuales", "");
            }
            if (hum == 0)
            {
                pResult.Rows.Add("Alerta en humedad", "La humedad promedio es igual a 0%", "Verifique la conexion del sensor de humedad");
            }
            else
            {
                pResult.Rows.Add("La humedad promedio es: " + hum.ToString() + " %", "Este valor se obtuvo promediando " + numMuestras.ToString() + " Mediciones individuales", "");
            }
            if (presAt == 0)
            {
                pResult.Rows.Add("Alerta en presion atmosferica", "La presion atmosferica promedio es igual a 0mB", "Verifique la conexion del sensor de de presion atmosferica");
            }
            else
            {
                pResult.Rows.Add("La presion atmosferica promedio es: " + presAt.ToString() + " mB", "Este valor se obtuvo promediando " + numMuestras.ToString() + " Mediciones individuales", "");
            }
            if (Alcohol == 0)
            {
                pResult.Rows.Add("Alerta en Alcohol", "Los niveles de Alcohol promedio es igual a 0ppb", "Verifique la conexion del sensor de Alcohol");
            }
            else
            {
                pResult.Rows.Add("Los niveles de Alcohol promedio es: " + Alcohol.ToString() + " ppb", "Este valor se obtuvo promediando " + numMuestras.ToString() + " Mediciones individuales", "");
            }
            if (CO2 == 0)
            {
                pResult.Rows.Add("Alerta en CO2", "Los niveles de CO2 promedio es igual a 0", "Verifique la conexion del sensor de CO2");
            }
            else
            {
                pResult.Rows.Add("Los niveles promedio de CO2 es: " + CO2.ToString() + " ppb", "Este valor se obtuvo promediando " + numMuestras.ToString() + " Mediciones individuales", "");
            }
            if (TVOC == 0)
            {
                pResult.Rows.Add("Alerta en TVOC", "Los niveles de TVOC promedio es igual a 0", "Verifique la conexion del sensor de TVOC");
            }
            else
            {
                pResult.Rows.Add("Los niveles de TVOC promedio es: " + TVOC.ToString() + " ppb", "Este valor se obtuvo promediando " + numMuestras.ToString() + " Mediciones individuales", "");
            }
            if (NH4 == 0)
            {
                pResult.Rows.Add("Alerta en gas NH4", "Los niveles promedio es igual a 0 ppm", "Verifique la conexion del sensor de gas NH4");
            }
            else
            {
                pResult.Rows.Add("Los niveles de Gas NH4 promedio es: " + NH4.ToString() + " ppm", "Este valor se obtuvo promediando " + numMuestras.ToString() + " Mediciones individuales", "");
            }
            if (Metano == 0)
            {
                pResult.Rows.Add("Alerta en gas Metano", "Los niveles promedio es igual a 0 ppm", "Verifique la conexion del sensor de gas Metano");
            }
            else
            {
                pResult.Rows.Add("Los niveles de Gas Metano promedio es: " + NH4.ToString() + " ppm", "Este valor se obtuvo promediando " + numMuestras.ToString() + " Mediciones individuales", "");
            }

            this.GVProms.Visible = true;

            GVProms.DataSource = promResult;

            GVProms.DataBind();

            this.GVResults.Visible = true;

            GVResults.DataSource = pResult;

            GVResults.DataBind();
        }
    }
}