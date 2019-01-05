﻿using System;
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
    public partial class monthReport : System.Web.UI.Page
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
                if (Session["month"]!=null)
                {
                    txtDate.Text = "1" + "/" + Session["month"].ToString() + "/" + now.Year;
                    Session["month"] = Session["month"];
                }
                else
                {
                    txtDate.Text = "1" + "/" + now.Month.ToString() + "/" + now.Year;
                }
                dateString = txtDate.Text;
                dateValue = DateTime.Parse(dateString.ToString());
                Session["month"] = dateValue.ToString("MM");
                Session["year"] = dateValue.ToString("yyyy");
                Session["day"] = dateValue.ToString("dd");
            }
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            dateString = txtDate.Text;
            dateValue = DateTime.Parse(dateString);
            Session["month"] = dateValue.ToString("MM");
            Session["year"] = dateValue.ToString("yyyy");
            Session["day"] = dateValue.ToString("dd");

            var pSQL = "SELECT * FROM measurements WHERE Fecha = " + onmotica.convertD2IDate(DateTime.Parse(txtDate.Text));
            SqlDataReader dr = onmotica.fetchReader(pSQL);
            
            DataTable pResult = new DataTable();

            pResult.Columns.Add("Nombre", typeof(Double));
            pResult.Columns.Add("Resultado", typeof(Double));
            pResult.Columns.Add("Recomendacion", typeof(Double));

            double temp = 0, hum = 0, presAt = 0, Alcohol = 0, CO2 = 0, TVOC = 0, NH4 = 0;
            int numMuestras = 0;

            while (dr.Read())
            {
                if (dr.HasRows)
                {
                    temp += double.Parse(dr["Temperatura"].ToString());
                    hum += double.Parse(dr["Humedad"].ToString());
                    presAt += double.Parse(dr["PresionAtmosferica"].ToString());
                    Alcohol += double.Parse(dr["Alcohol"].ToString());
                    CO2 += double.Parse(dr["TVOC"].ToString());
                    TVOC += double.Parse(dr["CO2"].ToString());
                    NH4 += double.Parse(dr["NH4"].ToString());
                    numMuestras++;
                }
                if (temp > 0) { temp = temp / numMuestras; };
                if (hum > 0) { hum = hum / numMuestras; };
                if (presAt > 0) { presAt = presAt / numMuestras; };
                if (Alcohol > 0) { Alcohol = Alcohol / numMuestras; };
                if (CO2 > 0) { CO2 = CO2 / numMuestras; };
                if (TVOC > 0) { TVOC = TVOC / numMuestras; };
                if (NH4 > 0) { NH4 = NH4 / numMuestras; };

                if(temp == 0)
                {
                    pResult.Rows.Add("Alerta en temperatura", "La temperatura promedio es igual a 0ºC", "Verifique la conexion del sensor de temperatura");
                }
                else
                {
                    pResult.Rows.Add("La temperatura promedio es: " + temp + " ºC", "Este valor se obtuvo promediando " + numMuestras, "");
                }
                if (hum == 0)
                {
                    pResult.Rows.Add("Alerta en hum", "La humedad promedio es igual a 0%", "Verifique la conexion del sensor de humedad");
                }
                else
                {
                    pResult.Rows.Add("La humedad promedio es: " + hum + " %", "Este valor se obtuvo promediando " + numMuestras, "");
                }
                if (presAt == 0)
                {
                    pResult.Rows.Add("Alerta en presion atmosferica", "La presion atmosferica promedio es igual a 0mB", "Verifique la conexion del sensor de de presion atmosferica");
                }
                else
                {
                    pResult.Rows.Add("La presion atmosferica promedio es: " + presAt + " mB", "Este valor se obtuvo promediando " + numMuestras, "");
                }
                if (Alcohol == 0)
                {
                    pResult.Rows.Add("Alerta en Alcohol", "Los niveles de Alcohol promedio es igual a 0ppb", "Verifique la conexion del sensor de temperatura");
                }
                else
                {
                    pResult.Rows.Add("Los niveles de Alcohol promedio es: " + Alcohol + " ppb", "Este valor se obtuvo promediando " + numMuestras, "");
                }
                if (CO2 == 0)
                {
                    pResult.Rows.Add("Alerta en CO2", "Los niveles de CO2 promedio es igual a 0", "Verifique la conexion del sensor de CO2");
                }
                else
                {
                    pResult.Rows.Add("Los niveles promedio de CO2 es: " + CO2 + " ppb", "Este valor se obtuvo promediando " + numMuestras, "");
                }
                if (TVOC == 0)
                {
                    pResult.Rows.Add("Alerta en TVOC", "Los niveles de TVOC promedio es igual a 0", "Verifique la conexion del sensor de TVOC");
                }
                else
                {
                    pResult.Rows.Add("Los niveles de TVOC promedio es: " + TVOC + " ppb", "Este valor se obtuvo promediando " + numMuestras, "");
                }
                if (NH4 == 0)
                {
                    pResult.Rows.Add("Alerta en gas Metano", "Los niveles promedio es igual a 0", "Verifique la conexion del sesnor de gas metano");
                }
                else
                {
                    pResult.Rows.Add("Los niveles de Gas Metano promedio es: " + NH4 + " ppm", "Este valor se obtuvo promediando " + numMuestras, "");
                }

            }
        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            dateString = txtDate.Text;
            dateValue = DateTime.Parse(dateString);
            Session["month"] = dateValue.ToString("MM");
            Session["year"] = dateValue.ToString("yyyy");
            Session["day"] = dateValue.ToString("dd");
        }
    }
}