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
			if(!Page.IsPostBack)
		    { 
			    String pSQL = "SELECT * FROM devices WHERE deviceUsrId = " + Session["UsrID"].ToString();
			    SqlDataReader dr = onmotica.fetchReader(pSQL);
			    while (dr.Read())
			    {
				    if (dr.HasRows)
				    {
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
			HtmlGenericControl li = new HtmlGenericControl("li");
			menu.Controls.Add(li);

			Button btnOpenDevice = new Button();
			btnOpenDevice.Click += new EventHandler(button_Click);
			btnOpenDevice.CssClass = btnID;
			btnOpenDevice.Text = text;
			btnOpenDevice.Width = 200;
			
			li.Controls.Add(btnOpenDevice);         
		}
		protected void button_Click(object sender, EventArgs e)
		{
			Button button = sender as Button;
			var deviceID = Int32.Parse(button.ID);
			Session["deviceID"] = deviceID;
			// identify which button was clicked and perform necessary actions
		}

	}
}