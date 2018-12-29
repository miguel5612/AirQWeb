using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;
using System.Data.SqlClient;
using System.Drawing;

namespace airQ
{
    public partial class register : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            Response.Redirect("/login");
        }

        protected void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            bool bandera = true;
            var usuario = txtUserName.Text;
            var password = txtPassword.Text;
            if (usuario!= null & password != null)
            {
                if (usuario!= "" & password != "")
                {
                    string pSQL = "SELECT * FROM users WHERE UserName = '" + txtUserName.Text + "'";
                    SqlDataReader dr = onmotica.fetchReader(pSQL);
                    while (dr.Read())
                    {
                        if (dr.HasRows || (dr.IsDBNull(0)))
                        {
                            if (dr["UserName"] != null)
                            {
                                if (dr["UserName"].ToString() != "")
                                {
                                    lblError.Text = "Usuario ya registrado, prueba con otro nombre de usuario";
                                    lblError.ForeColor = Color.Red;
                                    bandera = false;
                                }
                            }
                        }
                        else
                        {
                            lblError.Visible = false;
                        }
                    }
                    if(bandera)
                    {
                        pSQL = "INSERT INTO [users]  ([UserName], [Pass], [RegisterDate], [IsActiv]) VALUES ('@UserName','@Pass','@RegisterDate',@IsActiv)";
                        pSQL = pSQL.Replace("@UserName", usuario);
                        pSQL = pSQL.Replace("@Pass", password);
                        pSQL = pSQL.Replace("@RegisterDate", onmotica.convertD2IDateTime(DateTime.Now));
                        pSQL = pSQL.Replace("@IsActiv", "1");
                        onmotica.executeSQL(pSQL);
                        login(Session, Response, usuario, password);
                    }
                }

            }

        }
        public static void login(System.Web.SessionState.HttpSessionState Session, HttpResponse Response, string username, string password)
        {
            string pSQL = "SELECT * FROM users WHERE UserName = '" + username + "' AND pass = '" + password + "'";
            SqlDataReader dr = onmotica.fetchReader(pSQL);
            while (dr.Read())
            {
                    Session["UsrID"] = dr["IDUser"].ToString();
                    Session["UsrName"] = dr["UserName"].ToString();
                    Response.Redirect("/dashboard");
            }
        }
    }    
}