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

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtName.Text != null & txtInTopic != null & txtOutTopic != null)
            {
                String pSQL = "INSERT INTO devices ([deviceName], [deviceDateSubscription], [deviceActiv], [deviceUsrId], [deviceTyp], [inTopic], [outTopic]) VALUES ('@deviceName', '@deviceDateSubscription', 1,1, '@inTopic', ' @outTopic')";
                pSQL = pSQL.Replace("@deviceName", txtName.Text);
                pSQL = pSQL.Replace("@inTopic", txtInTopic.Text);
                pSQL = pSQL.Replace("@outTopic", txtOutTopic.Text);
                pSQL = pSQL.Replace("@deviceDateSubscription", onmotica.convertD2IDateTime(DateTime.Now));
                onmotica.fetchReader(pSQL);
                Response.Redirect("/dashboard");
            }
        }
    }
}