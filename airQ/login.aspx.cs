using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace airQ
{
    public partial class login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            Response.Redirect("/register");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string pSQL = "SELECT * FROM users WHERE UserName = '" + txtUser.Text + "' AND pass = '" + txtPassword.Text + "'";
        }

        protected void TextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}