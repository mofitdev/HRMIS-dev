using HRWebApp.cs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRWebApp.pg.profileforms
{
    public partial class ptab1t8 : System.Web.UI.Page
    {
        DataSet ds;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["HRMIS_UserID"] == null) Response.Redirect("~/login.html");
            else setDatas();
        }
        protected void setDatas()
        {
            ModifyDB myObj = new ModifyDB();
            ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_ORGTYPE");
            pTab1T8Section1ModalSelectOrgtype.DataSource = ds.Tables[0];
            pTab1T8Section1ModalSelectOrgtype.DataTextField = "NAME";
            pTab1T8Section1ModalSelectOrgtype.DataValueField = "ID";
            pTab1T8Section1ModalSelectOrgtype.DataBind();
            ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_ISWORKING");
            pTab1T8Section1ModalSelectIsworking.DataSource = ds.Tables[0];
            pTab1T8Section1ModalSelectIsworking.DataTextField = "NAME";
            pTab1T8Section1ModalSelectIsworking.DataValueField = "ID";
            pTab1T8Section1ModalSelectIsworking.DataBind();
            ds = myObj.OracleExecuteDataSet("SELECT ISGAVE, ISSWEAR, TESTDATE FROM ST_STATES WHERE STAFFS_ID="+ Session["HRMIS_UserID"].ToString());
            if (ds.Tables[0].Rows.Count != 0)
            {
                if (ds.Tables[0].Rows[0]["ISGAVE"].ToString() == "1")
                {
                    ptab1t8IsgaveT1.Checked = true;
                    ptab1t8IsgaveT0.Checked = false;
                }
                else if (ds.Tables[0].Rows[0]["ISGAVE"].ToString() == "0"){
                    ptab1t8IsgaveT1.Checked = false;
                    ptab1t8IsgaveT0.Checked = true;
                }
                else {
                    ptab1t8IsgaveT1.Checked = false;
                    ptab1t8IsgaveT0.Checked = false;
                }
                if (ds.Tables[0].Rows[0]["ISSWEAR"].ToString() == "1")
                {
                    ptab1t8IsswearT1.Checked = true;
                    ptab1t8IsswearT0.Checked = false;
                }
                else if (ds.Tables[0].Rows[0]["ISSWEAR"].ToString() == "0"){
                    ptab1t8IsswearT1.Checked = false;
                    ptab1t8IsswearT0.Checked = true;
                }
                else
                {
                    ptab1t8IsswearT1.Checked = false;
                    ptab1t8IsswearT0.Checked = false;
                }
                ptab1t8Testdate.Value = ds.Tables[0].Rows[0]["TESTDATE"].ToString();
            }
        }
    }
}