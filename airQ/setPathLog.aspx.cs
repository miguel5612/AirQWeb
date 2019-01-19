using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;

namespace airQ
{
    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string pathFolder = HttpContext.Current.Request.PhysicalApplicationPath; //HttpContext.Current.Server.MapPath("~");

            onmotica.updateAppFolder(pathFolder);

            Exception ex = new Exception("Este es un mensaje de prueba - Significa que se esta guardando bien el log :)");
            onmotica.saveInLogMQTT(ex);
        }
    }
}