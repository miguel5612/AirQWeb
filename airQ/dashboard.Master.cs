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
			    String pSQL = "SELECT * FROM devices WHERE deviceUsrId = " + Session["UsrID"].ToString();
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
                            firstFlag = false;
                        }
					    AddMenuItem(dr["deviceName"].ToString(), dr["deviceID"].ToString());
				    }
			    }
			}
            if(Session["deviceID"] != null)
            {
                if (Int32.Parse(Session["deviceID"].ToString()) > 0)
                {
                    String pSQL = "SELECT TOP (1) *  FROM  measurements WHERE activ = 1 AND deviceId = " + Session["deviceID"].ToString();
                    SqlDataReader dr = onmotica.fetchReader(pSQL);
                    while (dr.Read())
                    {
                        if (dr.HasRows)
                        {
                            fillInitialData(dr["data"].ToString());
                        }
                    }
                }
            }
		}
        private void fillInitialData(string data)
        {
            if(data.Length>0)
            {
               dynamic dataIn = JsonConvert.DeserializeObject(data);
                txtData1.Value = int.Parse(dataIn.data1) >= 0 ? dataIn.data1 : 0;
                txtData2.Value = int.Parse(dataIn.data2) >= 0 ? dataIn.data2 : 0;
                txtData3.Value = int.Parse(dataIn.data3) >= 0 ? dataIn.data3 : 0;
                txtData4.Value = int.Parse(dataIn.data4) >= 0 ? dataIn.data4 : 0;
                txtData5.Value = int.Parse(dataIn.data5) >= 0 ? dataIn.data5 : 0;
                txtData6.Value = int.Parse(dataIn.data6) >= 0 ? dataIn.data6 : 0;
                txtData7.Value = int.Parse(dataIn.data7) >= 0 ? dataIn.data7 : 0;
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