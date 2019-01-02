using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;
using System.Collections;
using System.Data;
using System.Data.SqlClient;


namespace airQ
{
    public partial class editDevice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            onmotica.isLogged(Session, Response,"editDevices");
            if (Request.QueryString["deviceIDArchiv"] != null & !Page.IsPostBack)
            {
               var pSQL = "UPDATE devices SET deviceActiv = 0 WHERE deviceID = " + Request.QueryString["deviceIDArchiv"];
               onmotica.executeSQL(pSQL);
               Response.Redirect("/editDevice");
            }
            if (Request.QueryString["deviceID"] != null & !Page.IsPostBack)
            {
                //edit mode
                gvDevices.Visible = false;
                editDiv.Visible = true;
                var pSQL = "SELECT * FROM devices WHERE deviceID = " + Request.QueryString["deviceID"];
                SqlDataReader dr = onmotica.fetchReader(pSQL);
                while (dr.Read())
                {
                    if (dr.HasRows)
                    {
                        txtName.Text = dr["deviceName"].ToString();
                        txtInTopic.Text = dr["inTopic"].ToString();
                        txtOutTopic.Text = dr["outTopic"].ToString();
                        deviceActiv.Checked = dr["deviceActiv"].ToString() == "True"?true:false;
                    }
                    else
                    {
                        Response.Redirect("/editDevice");
                    }
                }
            }
            else
            {
                //edit mode
                gvDevices.Visible = true;
                editDiv.Visible = false;
            }
        }
        public override void VerifyRenderingInServerForm(Control control)
        {
            //base.VerifyRenderingInServerForm(control);
        }
        protected void btnDescartar_click(object sender, EventArgs e)
        {
            Response.Redirect("/editDevice");
        }
        protected void btnEnviar_click(object sender, EventArgs e)
        {
            try
            {
                var pSQL = "UPDATE devices SET deviceName = '@deviceName', inTopic = '@inTopic', outTopic = '@outTopic', deviceActiv = @deviceActiv WHERE deviceID = @deviceID";
                pSQL = pSQL.Replace("@deviceName", txtName.Text);
                pSQL = pSQL.Replace("@inTopic", txtInTopic.Text);
                pSQL = pSQL.Replace("@outTopic", txtOutTopic.Text);
                pSQL = pSQL.Replace("@deviceActiv", (deviceActiv.Checked==true?1:0).ToString());
                pSQL = pSQL.Replace("@deviceID", Request.QueryString["deviceID"]);
                onmotica.executeSQL(pSQL);
            }
            catch(Exception err)
            {

            }
            finally
            {
                Response.Redirect("/editDevice");
            }
        }
    }
}