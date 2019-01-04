using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;
using System.Data.Sql;
using System.Data.SqlClient;

namespace airQ
{
    public partial class monthReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            txtDate.Text = DateTime.Now.ToString("MM/01/yyyy");
            onmotica.isLogged(Session, Response, "monthReport");
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            var pSQL = "SELECT * FROM measurements WHERE Fecha = " + onmotica.convertD2IDate(DateTime.Parse(txtDate.Text));
            SqlDataReader dr =  onmotica.fetchReader(pSQL);

        }
    }
}