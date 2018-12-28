using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data;
using System.Data.SqlClient;
using airQ.App_Code;
using System.Drawing;

namespace airQ
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            onmotica.isLogged(Session, Response,"login");
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("/register");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string pSQL = "SELECT * FROM users WHERE UserName = '" + txtUser.Text + "' AND pass = '" + txtPassword.Text + "'";
            SqlDataReader dr = onmotica.fetchReader(pSQL);
            while (dr.Read())
            {
                if (!dr.HasRows || (dr.IsDBNull(0)))
                {
                    lblError.Text = "Username or password error, try again";
                    lblError.ForeColor = Color.Red;
                }
                else
                {
                    Session["UsrID"] = dr["IDUser"].ToString();
                    Session["UsrName"] = dr["UserName"].ToString();
                    Response.Redirect("/dashboard");
                }
            }
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}