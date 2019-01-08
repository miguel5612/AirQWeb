using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.Sql;
using System.Data.SqlClient;
using airQ.App_Code;

namespace airQ
{
    public partial class info : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            var devId = Request.QueryString["devId"];
            if(devId!=null)
            {
                if (Convert.ToInt32(devId) > 0)
                {
                    var pSQL = "SELECT TOP(1)* FROM measurements INNER JOIN devices ON devices.inTopic = measurements.topic WHERE deviceID = @devId ORDER BY registerAt DESC";
                    pSQL = pSQL.Replace("@devId", devId);
                    SqlDataReader dr = onmotica.fetchReader(pSQL);

                    DataTable pResult = new DataTable();

                    pResult.Columns.Add("Nombre", typeof(String));
                    pResult.Columns.Add("Resultado", typeof(String));
                    pResult.Columns.Add("Recomendacion", typeof(String));

                    double temp = 0, hum = 0, presAt = 0, Alcohol = 0, CO2 = 0, TVOC = 0, Metano = 0, NH4 = 0;
                    double tempL = 0, humL = 0, presAtL = 0, AlcoholL = 0, CO2L = 0, TVOCL = 0, MetanoL = 0, NH4L = 0;
                    string fecha = "";
                    while (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            temp = onmotica.NZDBNum(dr["Temperatura"].ToString());
                            hum = onmotica.NZDBNum(dr["Humedad"].ToString());
                            presAt = onmotica.NZDBNum(dr["PresionAtmosferica"].ToString());
                            Alcohol = onmotica.NZDBNum(dr["Alcohol"].ToString());
                            CO2 = onmotica.NZDBNum(dr["TVOC"].ToString());
                            TVOC = onmotica.NZDBNum(dr["CO2"].ToString());
                            Metano = onmotica.NZDBNum(dr["Metano"].ToString());
                            NH4 = onmotica.NZDBNum(dr["NH4"].ToString());
                            fecha = dr["Fecha"].ToString();
                        }

                    }

                    pSQL = "SELECT TOP(1)* FROM limits ORDER BY limitId DESC";

                    pSQL = pSQL.Replace("@devId", devId);
                    dr = onmotica.fetchReader(pSQL);

                    while (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            tempL = onmotica.NZDBNum(dr["temperatura"].ToString());
                            humL = onmotica.NZDBNum(dr["humedad"].ToString());
                            presAtL = onmotica.NZDBNum(dr["presionAtmosferica"].ToString());
                            AlcoholL = onmotica.NZDBNum(dr["Alcohol"].ToString());
                            CO2L = onmotica.NZDBNum(dr["TVOC"].ToString());
                            TVOCL = onmotica.NZDBNum(dr["CO2"].ToString());
                            MetanoL = onmotica.NZDBNum(dr["Metano"].ToString());
                            NH4L = onmotica.NZDBNum(dr["NH4"].ToString());
                        }
                    }

                    if(temp>=tempL)
                    {
                        pResult.Rows.Add("Alerta en temperatura", "La temperatura ha superado los limites (" + tempL + ")", "Mantengase hidratado");
                    }
                    else
                    {
                        pResult.Rows.Add("Temperatura normal.", "La temperatura esta dentro de los limites normales", "");
                    }
                    if (hum >= humL)
                    {
                        pResult.Rows.Add("Alerta en Humedad", "La humedad ha superado los limites (" + humL + ")", "Mantengase hidratado");
                    }
                    else
                    {
                        pResult.Rows.Add("Humedad normal", "La humedad esta dentro de los limites normales", "");
                    }
                    if (presAt >= presAtL)
                    {
                        pResult.Rows.Add("Alerta en presion atmosferica.", "La presion atmosferica ha superado los limites (" + presAtL + ")", "");
                    }
                    else
                    {
                        pResult.Rows.Add("Presion atmosferica normal", "La presion atmosferica esta dentro de los limites normales", "");
                    }
                    if (Alcohol >= AlcoholL)
                    {
                        pResult.Rows.Add("Alerta en niveles de alcohol (PPM)", "los niveles de alcohol en PPM han superado los limites (" + AlcoholL + ")", "Contacte al administrador");
                    }
                    else
                    {
                        pResult.Rows.Add("Niveles de alcohol PPM correctos", "Los niveles no superan el humbral de alerta", "");
                    }
                    if (CO2 >= CO2L)
                    {
                        pResult.Rows.Add("Alerta en niveles de CO2 (PPM)", "los niveles de CO2 en PPM han superado los limites (" + CO2L + ")", "Contacte al administrador");
                    }
                    else
                    {
                        pResult.Rows.Add("Niveles de CO2 PPM correctos", "Los niveles no superan el humbral de alerta", "");
                    }

                    if (TVOC >= TVOCL)
                    {
                        pResult.Rows.Add("Alerta en niveles de TVOC (PPM)", "los niveles de TVOC en PPM han superado los limites (" + TVOCL + ")", "Contacte al administrador");
                    }
                    else
                    {
                        pResult.Rows.Add("Niveles de TVOC PPM correctos", "Los niveles no superan el humbral de alerta", "");
                    }
                    if (Metano >= MetanoL)
                    {
                        pResult.Rows.Add("Alerta en niveles de Metano (PPM)", "los niveles de Metano en PPM han superado los limites (" + MetanoL + ")", "Contacte al administrador");
                    }
                    else
                    {
                        pResult.Rows.Add("Niveles de Metano PPM correctos", "Los niveles no superan el humbral de alerta", "");
                    }
                    if (NH4 >= NH4L)
                    {
                        pResult.Rows.Add("Alerta en niveles de NH4 (PPM)", "los niveles de NH4 en PPM han superado los limites (" + NH4L + ")", "Contacte al administrador");
                    }
                    else
                    {
                        pResult.Rows.Add("Niveles de NH4 PPM correctos", "Los niveles no superan el humbral de alerta", "");
                    }
                    this.GVResults.Visible = true;

                    GVResults.DataSource = pResult;

                    GVResults.DataBind();

                    lblFecha.Text = fecha;
                }
            }
        }
    }
}