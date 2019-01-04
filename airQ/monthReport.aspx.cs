using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Globalization;

namespace airQ
{
    public partial class monthReport : System.Web.UI.Page
    {
        CultureInfo enUS = new CultureInfo("en-US");
        string dateString;
        DateTime dateValue;

        protected void Page_Load(object sender, EventArgs e)
        {
            onmotica.isLogged(Session, Response, "monthReport");
            var now = DateTime.Now;
            if (Session["month"]!=null)
            {
                txtDate.Text = "1" + "/" + Session["month"].ToString() + "/" + now.Year;
                Session["month"] = Session["month"];
            }
            else
            {
                txtDate.Text = "1" + "/" + now.Month.ToString() + "/" + now.Year;
            }
            dateString = txtDate.Text;
            dateValue = DateTime.Parse(dateString);
            Session["month"] = dateValue.ToString("MM");
            Session["year"] = dateValue.ToString("yyyy");
            Session["day"] = dateValue.ToString("dd");
        }

        protected void btnCalcular_Click(object sender, EventArgs e)
        {
            var pSQL = "SELECT * FROM measurements WHERE Fecha = " + onmotica.convertD2IDate(DateTime.Parse(txtDate.Text));
            SqlDataReader dr =  onmotica.fetchReader(pSQL);

        }

        protected void txtDate_TextChanged(object sender, EventArgs e)
        {
            dateString = txtDate.Text;
            DateTime.TryParseExact(dateString, "g", enUS,
                                 DateTimeStyles.AllowLeadingWhite, out dateValue);
            Session["month"] = dateValue.ToString("MM");
            Session["year"] = dateValue.ToString("yyyy");
            Session["day"] = dateValue.ToString("dd");
            Response.Redirect("/monthReport");
        }
    }
}