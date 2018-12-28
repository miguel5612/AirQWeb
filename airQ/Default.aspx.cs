using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using airQ.App_Code;

namespace airQ
{
    public partial class _Default : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            onmotica.isLogged(Session, Response,"default");
        }
    }
}