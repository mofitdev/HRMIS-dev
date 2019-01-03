using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRWebApp.pg
{
    public partial class error500 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["HRMIS_UserID"] == null) Response.Redirect("~/login");
            else
            {
                if (Session["error500"] != null && Session["errorMessage"] != null)
                {
                    if (Session["errorMessage"].ToString() == "SessionDied" || Session["errorMessage"].ToString() == "Invalid operation. The connection is closed.") Response.Redirect("~/login");
                    else errorname.InnerHtml = Session["error500"].ToString();
                }
            }
        }
    }
}