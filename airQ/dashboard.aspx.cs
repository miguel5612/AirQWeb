using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;

namespace airQ
{
    public partial class dashboard1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            onmotica.isLogged(Session, Response,"dashboard");
        }
        void Page_LoadComplete(object sender, EventArgs e)
        {
            if (Session["deviceName"] != null & Session["deviceId"] != null)
            {
                lblTittle.Text = Session["deviceName" + Session["deviceId"].ToString()].ToString();
                divMeters.Visible = true;
            }
            else
            {
                lblTittle.Text = "No tienes dispositivos registrados, haz clic en registrar nuevo dispositivo...";
                divMeters.Visible = false;
            }
        }
    }
}