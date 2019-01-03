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
                txtData8.Value = Session["inTopic"].ToString(); // inTopic
                if (Int32.Parse(Session["deviceID"].ToString()) > 0)
                {
                    String pSQL = "SELECT TOP (1) *  FROM  measurements WHERE activ = 1 AND topic = '" + Session["inTopic"].ToString() + "' ORDER BY registerAt DESC";
                    SqlDataReader dr = onmotica.fetchReader(pSQL);
                    while (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            txtData1.Value = int.Parse(dr["temperatura"].ToString()) <= 0 ? "0" : dr["temperatura"].ToString();
                            txtData2.Value = int.Parse(dr["humedad"].ToString()) <= 0 ? "0" : dr["humedad"].ToString();
                            txtData3.Value = int.Parse(dr["presionAtmosferica"].ToString()) <= 0 ? "0" : dr["presionAtmosferica"].ToString();
                            txtData4.Value = int.Parse(dr["Alcohol"].ToString()) <= 0 ? "0" : dr["Alcohol"].ToString();
                            txtData5.Value = int.Parse(dr["TVOC"].ToString()) <= 0 ? "0" : dr["TVOC"].ToString();
                            txtData6.Value = int.Parse(dr["CO2"].ToString()) <= 0 ? "0" : dr["CO2"].ToString();
                            txtData7.Value = int.Parse(dr["NH4"].ToString()) <= 0 ? "0" : dr["NH4"].ToString();
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