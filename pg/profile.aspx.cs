using HRWebApp.cs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OracleClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace HRWebApp.pg
{
    public partial class profile : System.Web.UI.Page
    {
        string strMyVal = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["HRMIS_UserID"] == null) Response.Redirect("~/login");
            else setDatas();
        }
        protected void setDatas()
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            if (Request.QueryString["id"] != null)
            {
                if ("0" != myObj.OracleExecuteScalar("SELECT COUNT(1) FROM ST_STAFFS WHERE ID=" + Request.QueryString["id"]).ToString())
                {
                    try
                    {
                        DataSet ds = myObj.OracleExecuteDataSet("SELECT a.IMAGE, CASE WHEN i.ACTIVE=1 AND i.SHOW=1 THEN TO_CHAR(h.NAME) WHEN i.ACTIVE=1 AND i.SHOW=0 THEN 'Идэвхтэй' ELSE TO_CHAR(j.NAME) END AS TP, i.COLOR, CASE WHEN a.GENDER=1 THEN '<i class=\"fa fa-male\"></i> Эрэгтэй<br />' ELSE '<i class=\"fa fa-female\"></i> Эмэгтэй<br />' END as GENDER,  NVL(TO_CHAR(trunc(months_between(sysdate,to_date(a.BDATE,'YYYY-MM-DD'))/12)),'-') as AGE,  a.FNAME, a.LNAME, CASE WHEN c.ID=c.FATHER_ID THEN c.INITNAME ELSE d.INITNAME||' - '||c.INITNAME END as NEGJ, f.NAME as POS_NAME, a.TEL||CASE WHEN TEL is not null THEN ', '||TEL2 END as TEL, a.EMAIL, g.NAME||NVL2(g.NAME,', ','')||h.NAME||NVL2(h.NAME,', ','')||a.ADDRESSNAME as ADDRESSNAME, a.REGNO, a.CITNO, a.SOCNO, a.HEALNO, a.DOCPERMISSIONID FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID=b.STAFFS_ID AND b.ISACTIVE=1 INNER JOIN ST_BRANCH c ON b.BRANCH_ID=c.ID INNER JOIN ST_BRANCH d ON c.FATHER_ID=d.ID INNER JOIN STN_POS f ON b.POS_ID=f.ID LEFT JOIN STN_CITY g ON a.ADDRCITY_ID=g.ID LEFT JOIN STN_DIST h ON a.ADDRDIST_ID=h.ID INNER JOIN STN_MOVE i ON b.MOVE_ID=i.ID INNER JOIN STN_MOVETYPE j ON i.MOVETYPE_ID=j.ID WHERE a.ID=" + Request.QueryString["id"]);
                        if (ds.Tables[0].Rows.Count != 0)
                        {
                            foreach (DataRow dr in ds.Tables[0].Rows)
                            {
                                if (dr[0].ToString() != "") profileMainImage.Src = "../files/staffs/" + dr[0].ToString();
                                profileMainDivStafftype.InnerHtml = "<span class=\"label " + dr[2].ToString() + "\">" + dr[1].ToString() + "</span><br /><br />" + dr[3].ToString() + "<span>" + dr[4].ToString() + " нас</span>";
                                profileMainH1Names.InnerHtml = "<span class=\"semi-bold\">" + dr[5].ToString() + "</span> " + dr[6].ToString() + "<br><small>" + dr[7].ToString() + " | " + dr[8].ToString() + "</small>";
                                if (dr[9].ToString() != "") profileMainSpanTels.InnerHtml = dr[9].ToString();
                                if (dr[10].ToString() != "")
                                {
                                    profileMainSpanEmail.InnerHtml = dr[10].ToString();
                                    profileMainSpanEmail.HRef = "mailto:" + dr[10].ToString();
                                }
                                if (dr[11].ToString() != "") profileMainSpanAddress.InnerHtml = dr[11].ToString();
                                profileMainInputRegno.Value = dr[12].ToString();
                                profileMainInputCitno.Value = dr[13].ToString();
                                profileMainInputSocno.Value = dr[14].ToString();
                                profileMainInputHealno.Value = dr[15].ToString();
                                profileMainSelectDocRole.SelectedIndex = profileMainSelectDocRole.Items.IndexOf(profileMainSelectDocRole.Items.FindByValue(dr["DOCPERMISSIONID"].ToString()));
                            }
                        }
                        myObj.DBDisconnect();

                        if (!myObjGetTableData.checkRoles(Session["HRMIS_UserID"].ToString(), "1,4"))
                        {
                            pTab2Li.Attributes["class"] = "hide";
                            profileMainSelectDocRole.Disabled = true;
                        }
                        else
                        {
                            if (Request.QueryString["ismove"] != null)
                            {
                                if (Request.QueryString["ismove"] == "1")
                                {
                                    pTab1Li.Attributes["class"] = "";
                                    pTab2Li.Attributes["class"] = "active";
                                    pTab1.Attributes["class"] = "tab-pane fade";
                                    pTab2.Attributes["class"] = "tab-pane fade in active";
                                }
                            }
                            profileMainSelectDocRole.Disabled = false;
                        }

                        if (!myObjGetTableData.checkRoles(Session["HRMIS_UserID"].ToString(), "1,13"))
                        {
                            profileMainSelectDocRole.Disabled = true;
                        }
                        else
                        {
                            profileMainSelectDocRole.Disabled = false;
                        }
                    }
                    catch (NullReferenceException ex)
                    {

                    }
                    catch (cs.HRMISException ex)
                    {
                        HttpContext.Current.Session["error500"] = "Message: " + ex.Message + "<br/>StackTrace: " + ex.StackTrace;
                        //cs.WriteLogForEx.WriteLog(ex);
                        throw ex;
                    }
                    catch (Exception ex)
                    {
                        HttpContext.Current.Session["error500"] = "Message: " + ex.Message + "<br/>StackTrace: " + ex.StackTrace;
                        //cs.WriteLogForEx.WriteLog(ex);
                        throw ex;
                    }
                }
                else
                {
                    Session["error500"] = "БУРУУ ХАНДАЛТ";
                    Response.Redirect("~/#pg/error500.aspx", false);
                }
            }
            else
            {
                Session["error500"] = "БУРУУ ХАНДАЛТ";
                Response.Redirect("~/#pg/error500.aspx", false);
            }
        }
    }
}