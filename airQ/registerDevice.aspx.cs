using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;

namespace airQ
{
    public partial class registerDevice : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            onmotica.isLogged(Session, Response, "dashboard");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text != null & txtInTopic.Text != null & txtOutTopic.Text != null)
            {
                String pSQL = "INSERT INTO devices ([deviceName], [deviceDateSubscription], [deviceActiv], [deviceUsrId], [deviceTyp], [inTopic], [outTopic]) VALUES ('@deviceName', '@deviceDateSubscription', @deviceActiv, @deviceUsrId, @deviceTyp, '@inTopic', '@outTopic')";
                pSQL = pSQL.Replace("@deviceName", txtName.Text.Trim());
                pSQL = pSQL.Replace("@inTopic", txtInTopic.Text.Trim());
                pSQL = pSQL.Replace("@outTopic", txtOutTopic.Text.Trim());
                pSQL = pSQL.Replace("@deviceUsrId", Session["UsrID"].ToString());

                pSQL = pSQL.Replace("@deviceActiv", "1");
                pSQL = pSQL.Replace("@deviceTyp", "1");

                pSQL = pSQL.Replace("@deviceDateSubscription", onmotica.convertD2IDateTime(DateTime.Now));
                onmotica.executeSQL(pSQL);
                Response.Redirect("/dashboard");
            }
        }
    }
}