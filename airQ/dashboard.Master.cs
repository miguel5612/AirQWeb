using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using airQ.App_Code;
using System.Data.SqlClient;
using Newtonsoft.Json;
using airQ.App_Code;

namespace airQ
{
	public partial class dashboard : System.Web.UI.MasterPage
	{
		protected void Page_Load(object sender, EventArgs e)
		{
            if (Request.QueryString["device"] != null)
            {
                if(int.Parse(Request.QueryString["device"].ToString())>0)
                {
                    Session["deviceID"] = Request.QueryString["device"].ToString();
                }
            }
			if(!Page.IsPostBack)
		    {
                Session["unSubscribeTopics"] = "";

                String pSQL = "SELECT * FROM devices WHERE deviceActiv = 1 AND deviceUsrId = " + Session["UsrID"].ToString();
			    SqlDataReader dr = onmotica.fetchReader(pSQL);
                bool firstFlag = true;
			    while (dr.Read())
			    {
				    if (dr.HasRows)
				    {
                        if(firstFlag & Request.QueryString["device"] == null)
                        {
                            Session["deviceID"] = dr["deviceID"].ToString();
                            Session["deviceName"] = dr["deviceName"].ToString();
                            Session["inTopic"] = dr["inTopic"].ToString();
                            firstFlag = false;
                        }
                        else if(firstFlag & dr["deviceID"].ToString() == Request.QueryString["device"])
                        {
                            Session["deviceID"] = dr["deviceID"].ToString();
                            Session["deviceName"] = dr["deviceName"].ToString();
                            Session["inTopic"] = dr["inTopic"].ToString();
                            firstFlag = false;
                        }
                        else
                        { 
                            Session["unSubscribeTopics"] = Session["unSubscribeTopics"].ToString() + dr["inTopic"].ToString();
                        }
                        AddMenuItem(dr["deviceName"].ToString(), dr["deviceID"].ToString());
				    }
			    }
			}
            if(Session["deviceID"] != null)
            {
                txtData9.Value = Session["inTopic"].ToString(); // inTopic
                if (Int32.Parse(Session["deviceID"].ToString()) > 0)
                {
                    String pSQL = "SELECT TOP (1) *  FROM  measurements WHERE activ = 1 AND topic = '" + Session["inTopic"].ToString() + "' ORDER BY registerAt DESC";
                    SqlDataReader dr = onmotica.fetchReader(pSQL);
                    while (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            var temperatura = dr["Temperatura"].ToString();
                            var humedad = dr["Humedad"].ToString();
                            var presionAtmosferica = dr["presionAtmosferica"].ToString();
                            var Alcohol = dr["Alcohol"].ToString();
                            var TVOC = dr["TVOC"].ToString();
                            var CO2 = dr["CO2"].ToString();
                            var NH4 = dr["NH4"].ToString();
                            var Metano = dr["Metano"].ToString();

                            var tempDouble = onmotica.string2Double(temperatura);
                            var humDouble = onmotica.string2Double(humedad);
                            var PresAtDouble = onmotica.string2Double(presionAtmosferica);
                            var AlcoholDouble = onmotica.string2Double(Alcohol);
                            var TVOCDouble = onmotica.string2Double(TVOC);
                            var CO2Double = onmotica.string2Double(CO2);
                            var NH4Double = onmotica.string2Double(NH4);
                            var MetanoDouble = onmotica.string2Double(Metano);


                            txtData1.Value = tempDouble <= 0 ? "0" : temperatura;
                            txtData2.Value = humDouble <= 0 ? "0" : humedad;
                            txtData3.Value = PresAtDouble <= 0 ? "0" : presionAtmosferica;
                            txtData4.Value = AlcoholDouble <= 0 ? "0" : Alcohol;
                            txtData5.Value = TVOCDouble <= 0 ? "0" : TVOC;
                            txtData6.Value = CO2Double <= 0 ? "0" : CO2;
                            txtData7.Value = NH4Double <= 0 ? "0" : NH4;
                            txtData8.Value = MetanoDouble <= 0 ? "0" : Metano;
                        }
                    }
                }
            }
		}

        private void AddMenuItem(string text, string btnID)
		{
            Session["deviceName" + btnID] = text;

            HtmlGenericControl li = new HtmlGenericControl("li");
			menu.Controls.Add(li);

            HyperLink openDeviceLink = new HyperLink();
			openDeviceLink.CssClass = btnID;
            openDeviceLink.NavigateUrl = "~/dashboard?device=" + btnID;
            openDeviceLink.Text = text;
			openDeviceLink.Width = 200;
            if (btnID == Session["deviceID"].ToString())
            {
                openDeviceLink.Style.Add("background-color", "darkred");
                openDeviceLink.Style.Add("color", "white");                
            }
			
			li.Controls.Add(openDeviceLink);         
		}
}
}