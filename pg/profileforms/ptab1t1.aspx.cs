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
    public partial class ptab1t1 : System.Web.UI.Page
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
            ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_CITY");
            pTab1ModalSelectBcity.DataSource = ds.Tables[0];
            pTab1ModalSelectBcity.DataTextField = "NAME";
            pTab1ModalSelectBcity.DataValueField = "ID";
            pTab1ModalSelectBcity.DataBind();
            pTab1ModalSelectAddresscity.DataSource = ds.Tables[0];
            pTab1ModalSelectAddresscity.DataTextField = "NAME";
            pTab1ModalSelectAddresscity.DataValueField = "ID";
            pTab1ModalSelectAddresscity.DataBind();
            ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_NAT");
            pTab1ModalSelectNat.DataSource = ds.Tables[0];
            pTab1ModalSelectNat.DataTextField = "NAME";
            pTab1ModalSelectNat.DataValueField = "ID";
            pTab1ModalSelectNat.DataBind();
            ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_EDUTP");
            pTab1ModalSelectEdutp.DataSource = ds.Tables[0];
            pTab1ModalSelectEdutp.DataTextField = "NAME";
            pTab1ModalSelectEdutp.DataValueField = "ID";
            pTab1ModalSelectEdutp.DataBind();
            ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_SOCPOS");
            pTab1ModalSelectSocpos.DataSource = ds.Tables[0];
            pTab1ModalSelectSocpos.DataTextField = "NAME";
            pTab1ModalSelectSocpos.DataValueField = "ID";
            pTab1ModalSelectSocpos.DataBind();
            ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_OCCTYPE");
            pTab1ModalSelectOcctp.DataSource = ds.Tables[0];
            pTab1ModalSelectOcctp.DataTextField = "NAME";
            pTab1ModalSelectOcctp.DataValueField = "ID";
            pTab1ModalSelectOcctp.DataBind();
            ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_RELATION");
            pTab1ModalSelectRelRelation.DataSource = ds.Tables[0];
            pTab1ModalSelectRelRelation.DataTextField = "NAME";
            pTab1ModalSelectRelRelation.DataValueField = "ID";
            pTab1ModalSelectRelRelation.DataBind();

            string strVal = myObj.OracleExecuteScalar("SELECT MNAME||'~'||LNAME||'~'||FNAME||'~'||GENDER||'~'||BDATE||'~'||EDUTP_ID||'~'||OCCTYPE_ID||'~'||OCCNAME||'~'||BCITY_ID||'~'||BDIST_ID||'~'||BPLACE||'~'||NAT_ID||'~'||SOCPOS_ID||'~'||ADDRCITY_ID||'~'||ADDRDIST_ID||'~'||ADDRESSNAME||'~'||TEL||'~'||TEL2||'~'||EMAIL||'~'||RELNAME||'~'||RELATION_ID||'~'||RELADDRESSNAME||'~'||RELTEL||'~'||RELTEL2||'~'||RELEMAIL||'~'||MACID FROM ST_STAFFS WHERE ID=" + Request.QueryString["id"]).ToString();
            //section1
            pTab1ModalInputMName.Value = strVal.Split('~')[0];
            pTab1ModalInputLName.Value = strVal.Split('~')[1];
            pTab1ModalInputFName.Value = strVal.Split('~')[2];
            if (strVal.Split('~')[3] == "1") pTab1ModalSelectGenderMale.Attributes["checked"] = "checked";
            else pTab1ModalSelectGenderFemale.Attributes["checked"] = "checked";
            pTab1ModalInputBdate.Value = strVal.Split('~')[4];
            pTab1ModalSelectEdutp.SelectedIndex = pTab1ModalSelectEdutp.Items.IndexOf(pTab1ModalSelectEdutp.Items.FindByValue(strVal.Split('~')[5]));
            pTab1ModalSelectOcctp.SelectedIndex = pTab1ModalSelectOcctp.Items.IndexOf(pTab1ModalSelectOcctp.Items.FindByValue(strVal.Split('~')[6]));
            pTab1ModalInputOccname.Value = strVal.Split('~')[7];
            //section2
            pTab1ModalSelectBcity.SelectedIndex = pTab1ModalSelectBcity.Items.IndexOf(pTab1ModalSelectBcity.Items.FindByValue(strVal.Split('~')[8]));
            if (strVal.Split('~')[8] != "")
            {
                ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_DIST WHERE BCITY_ID=" + strVal.Split('~')[8] + "");
                pTab1ModalSelectBdist.DataSource = ds.Tables[0];
                pTab1ModalSelectBdist.DataTextField = "NAME";
                pTab1ModalSelectBdist.DataValueField = "ID";
                pTab1ModalSelectBdist.DataBind();
                pTab1ModalSelectBdist.Disabled = false;
                if (strVal.Split('~')[9] != "") pTab1ModalSelectBdist.SelectedIndex = pTab1ModalSelectBdist.Items.IndexOf(pTab1ModalSelectBdist.Items.FindByValue(strVal.Split('~')[9]));
            }
            pTab1ModalInputBplace.Value = strVal.Split('~')[10];
            pTab1ModalSelectNat.SelectedIndex = pTab1ModalSelectNat.Items.IndexOf(pTab1ModalSelectNat.Items.FindByValue(strVal.Split('~')[11]));
            pTab1ModalSelectSocpos.SelectedIndex = pTab1ModalSelectSocpos.Items.IndexOf(pTab1ModalSelectSocpos.Items.FindByValue(strVal.Split('~')[12]));
            //section3
            pTab1ModalSelectAddresscity.SelectedIndex = pTab1ModalSelectAddresscity.Items.IndexOf(pTab1ModalSelectAddresscity.Items.FindByValue(strVal.Split('~')[13]));
            if (strVal.Split('~')[13] != "")
            {
                ds = myObj.OracleExecuteDataSet("SELECT null as ID, TO_CHAR('- Сонго -') as NAME FROM DUAL UNION ALL SELECT ID, TO_CHAR(NAME) FROM STN_DIST WHERE BCITY_ID=" + strVal.Split('~')[13] + "");
                pTab1ModalSelectAddressdist.DataSource = ds.Tables[0];
                pTab1ModalSelectAddressdist.DataTextField = "NAME";
                pTab1ModalSelectAddressdist.DataValueField = "ID";
                pTab1ModalSelectAddressdist.DataBind();
                pTab1ModalSelectAddressdist.Disabled = false;
                if (strVal.Split('~')[14] != "") pTab1ModalSelectAddressdist.SelectedIndex = pTab1ModalSelectAddressdist.Items.IndexOf(pTab1ModalSelectAddressdist.Items.FindByValue(strVal.Split('~')[14]));
            }
            pTab1ModalInputAddressname.Value = strVal.Split('~')[15];
            pTab1ModalInputTel.Value = strVal.Split('~')[16];
            pTab1ModalInputTel2.Value = strVal.Split('~')[17];
            pTab1ModalInputEmail.Value = strVal.Split('~')[18];
            //section4
            pTab1ModalInputRelName.Value = strVal.Split('~')[19];
            pTab1ModalSelectRelRelation.SelectedIndex = pTab1ModalSelectRelRelation.Items.IndexOf(pTab1ModalSelectRelRelation.Items.FindByValue(strVal.Split('~')[20]));
            pTab1ModalInputRelAddress.Value = strVal.Split('~')[21];
            pTab1ModalInputRelTel.Value = strVal.Split('~')[22];
            pTab1ModalInputRelTel2.Value = strVal.Split('~')[23];
            pTab1ModalInputRelEmail.Value = strVal.Split('~')[24];
            MacID.Value = strVal.Split('~')[25];
        }
    }
}