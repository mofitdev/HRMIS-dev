using HRWebApp.cs;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace HRWebApp.webservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "ServiceMain" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select ServiceMain.svc or ServiceMain.svc.cs at the Solution Explorer and start debugging.
    public class ServiceMain : IServiceMain
    {
        public string GazarListForDDL()
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet("SELECT ID, INITNAME, NAME FROM ST_BRANCH WHERE BRANCH_TYPE_ID IN (1,3) AND ISACTIVE=1 ORDER BY SORT");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string HeltesListForDDL(string gazarId)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            DataSet ds = null;
            string strQry = "";
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                if ("0" != myObj.OracleExecuteScalar("SELECT COUNT(1) FROM ST_BRANCH WHERE BRANCH_TYPE_ID=2 AND ISACTIVE=1 AND FATHER_ID=" + gazarId).ToString())
                {
                    strQry = @"SELECT ID, INITNAME, NAME FROM ST_BRANCH WHERE BRANCH_TYPE_ID=3 AND ISACTIVE=1 AND ID="+gazarId+@"
UNION ALL
SELECT ID, INITNAME, NAME FROM ST_BRANCH WHERE BRANCH_TYPE_ID=2 AND ISACTIVE=1 AND FATHER_ID=" + gazarId;
                }
                else
                {
                    strQry = "SELECT ID, INITNAME, NAME FROM ST_BRANCH WHERE BRANCH_TYPE_ID IN (1,3) AND ISACTIVE=1 AND FATHER_ID=" + gazarId;
                }
                ds = myObj.OracleExecuteDataSet(strQry);
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string StaffListForDDL(string gazarId, string heltesId)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            string strQry = "";
            if (gazarId != "") gazarId = " AND f.FATHER_ID=" + gazarId;
            //if (heltesId != "") heltesId = " AND b.BRANCH_ID=" + heltesId;
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                if (heltesId == "")
                    strQry = @"SELECT a.ID as ST_ID, SUBSTR(a.LNAME,0,1)||'.'||SUBSTR(a.FNAME,0,1)||LOWER(SUBSTR(a.FNAME,2)) as ST_NAME, d.NAME as POS_NAME, f.ID as BR_ID, CASE WHEN f.ID=f.FATHER_ID THEN f.INITNAME ELSE g.INITNAME||'-'||f.INITNAME END as DOMAIN_ORG 
FROM ST_STAFFS a 
INNER JOIN ST_STBR b ON a.ID=b.STAFFS_ID AND b.ISACTIVE=1 
INNER JOIN STN_MOVE c ON b.MOVE_ID=c.ID AND c.ACTIVE=1 
LEFT JOIN STN_POS d ON b.POS_ID=d.ID 
INNER JOIN ST_BRANCH f ON b.BRANCH_ID=f.ID AND f.ISACTIVE=1 
INNER JOIN ST_BRANCH g ON f.FATHER_ID=g.ID AND g.ISACTIVE=1 
WHERE 1=1" + gazarId + @" 
ORDER BY g.SORT, f.SORT, d.SORT";
                else strQry = @"SELECT h.ST_ID, SUBSTR(a.LNAME,0,1)||'.'||SUBSTR(a.FNAME,0,1)||LOWER(SUBSTR(a.FNAME,2)) as ST_NAME, d.NAME as POS_NAME, f.ID as BR_ID, CASE WHEN f.ID=f.FATHER_ID THEN f.INITNAME ELSE g.INITNAME||'-'||f.INITNAME END as DOMAIN_ORG 
FROM(
  SELECT ST_ID
  FROM(
    SELECT a.ID as ST_ID
    FROM ST_STAFFS a
    INNER JOIN ST_STBR b ON a.ID = b.STAFFS_ID AND b.ISACTIVE = 1
    INNER JOIN STN_MOVE c ON b.MOVE_ID = c.ID AND c.ACTIVE = 1
    INNER JOIN ST_BRANCH f ON b.BRANCH_ID = f.ID AND f.ISACTIVE = 1
    WHERE f.FATHER_ID = (SELECT FATHER_ID FROM ST_BRANCH WHERE ID = " + heltesId + @" AND ISACTIVE = 1) AND b.POS_ID = 2010201
    UNION ALL
    SELECT a.ID as ST_ID
    FROM ST_STAFFS a
    INNER JOIN ST_STBR b ON a.ID = b.STAFFS_ID AND b.ISACTIVE = 1
    INNER JOIN STN_MOVE c ON b.MOVE_ID = c.ID AND c.ACTIVE = 1
    INNER JOIN ST_BRANCH f ON b.BRANCH_ID = f.ID AND f.ISACTIVE = 1
    WHERE f.ID = " + heltesId + @"
  )
  GROUP BY ST_ID
) h
INNER JOIN ST_STAFFS a ON h.ST_ID = a.ID
INNER JOIN ST_STBR b ON a.ID = b.STAFFS_ID AND b.ISACTIVE = 1
INNER JOIN STN_MOVE c ON b.MOVE_ID = c.ID AND c.ACTIVE = 1
LEFT JOIN STN_POS d ON b.POS_ID = d.ID
INNER JOIN ST_BRANCH f ON b.BRANCH_ID = f.ID AND f.ISACTIVE = 1
INNER JOIN ST_BRANCH g ON f.FATHER_ID = g.ID AND g.ISACTIVE = 1
ORDER BY g.SORT, f.SORT, d.SORT";
                DataSet ds = myObj.OracleExecuteDataSet(strQry);
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string StaffListForSelect2(string selectedList)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet("SELECT b.BRANCH_ID , CASE WHEN d.ID = d.FATHER_ID THEN d.INITNAME ELSE f.INITNAME || '-' || d.INITNAME END AS NEGJNAME , a.ID as STAFFS_ID , SUBSTR(a.LNAME, 0, 1) || '.' || SUBSTR(a.FNAME, 1, 1) || LOWER(SUBSTR(a.FNAME, 2)) as STAFFS_NAME , g.NAME as POS_NAME , NVL2(h.STAFFS_ID, ' selected=\"selected\"', null) as ISSELECTED FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID = b.STAFFS_ID AND b.ISACTIVE = 1 INNER JOIN STN_MOVE c ON b.MOVE_ID = c.ID AND c.ACTIVE = 1 INNER JOIN ST_BRANCH d ON b.BRANCH_ID = d.ID AND d.ISACTIVE = 1 INNER JOIN ST_BRANCH f ON d.FATHER_ID = f.ID AND d.ISACTIVE = 1 INNER JOIN STN_POS g ON b.POS_ID = g.ID LEFT JOIN( select regexp_substr('" + selectedList + "', '[^,]+', 1, level) AS STAFFS_ID from dual connect by regexp_substr('" + selectedList + "', '[^,]+', 1, level) is not null ) h ON a.ID = h.STAFFS_ID ORDER BY f.SORT, d.SORT, g.SORT, a.ID");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string OracleExecuteScalar(string qry)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                return myObj.OracleExecuteScalar(qry).ToString();
            }
            catch (NullReferenceException ex)
            {
                return "";
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void OracleExecuteNonQuery(string qry)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                myObj.OracleExecuteNonQuery(qry).ToString();
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

        //*****property.aspx*****//
        public string PropertyTab1Datatable(string staffid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet(@"SELECT ROWNUM as ROWNO, a.*
FROM (
  SELECT a.RECEIVEDDT, a.PROPERTYLIST_ID, b.CODE, b.PROPERTYTYPELIST_ID, c.NAME as PROPERTYTYPELIST_NAME, b.UNITLIST_ID, d.NAME as UNITLIST_NAME, b.UNITPRICE, b.DESCRIPTION
  FROM ST_PROPERTYUSE a
  INNER JOIN ST_PROPERTYLIST b ON a.PROPERTYLIST_ID=b.ID
  INNER JOIN STN_PROPERTYTYPELIST c ON b.PROPERTYTYPELIST_ID=c.ID
  INNER JOIN STN_UNITLIST d ON b.UNITLIST_ID=d.ID
  WHERE a.STAFFS_ID="+ staffid + @" AND a.HANDADDATE is null
  ORDER BY a.RECEIVEDDT, b.CODE
) a");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string PropertyTab1PropertyTypeListForDDL()
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet("SELECT ID, NAME FROM STN_PROPERTYTYPELIST ORDER BY NAME");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string PropertyTab1UnitListForDDL()
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet("SELECT ID, NAME FROM STN_UNITLIST ORDER BY NAME");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void PROPERTYUSE_INSERT(string P_RECEIVEDDT, string P_CODE, string P_PROPERTYTYPELIST_ID, string P_UNITLIST_ID, string P_UNITPRICE, string P_DESCRIPTION, string P_STAFFS_ID, string P_MOD_STAFFS_ID)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string[] ParamName = new string[8], ParamValue = new string[8];
                ParamName[0] = "P_RECEIVEDDT";
                ParamName[1] = "P_CODE";
                ParamName[2] = "P_PROPERTYTYPELIST_ID";
                ParamName[3] = "P_UNITLIST_ID";
                ParamName[4] = "P_UNITPRICE";
                ParamName[5] = "P_DESCRIPTION";
                ParamName[6] = "P_STAFFS_ID";
                ParamName[7] = "P_MOD_STAFFS_ID";
                ParamValue[0] = P_RECEIVEDDT;
                ParamValue[1] = P_CODE;
                ParamValue[2] = P_PROPERTYTYPELIST_ID;
                ParamValue[3] = P_UNITLIST_ID;
                ParamValue[4] = P_UNITPRICE;
                ParamValue[5] = P_DESCRIPTION;
                ParamValue[6] = P_STAFFS_ID;
                ParamValue[7] = P_MOD_STAFFS_ID;
                myObj.SP_OracleExecuteNonQuery("PROPERTYUSE_INSERT", ParamName, ParamValue);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void PROPERTYUSE_UPDATE(string P_PROPERTYLIST_ID, string P_RECEIVEDDT, string P_CODE, string P_PROPERTYTYPELIST_ID, string P_UNITLIST_ID, string P_UNITPRICE, string P_DESCRIPTION, string P_STAFFS_ID, string P_MOD_STAFFS_ID)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string[] ParamName = new string[9], ParamValue = new string[9];
                ParamName[0] = "P_PROPERTYLIST_ID";
                ParamName[1] = "P_RECEIVEDDT";
                ParamName[2] = "P_CODE";
                ParamName[3] = "P_PROPERTYTYPELIST_ID";
                ParamName[4] = "P_UNITLIST_ID";
                ParamName[5] = "P_UNITPRICE";
                ParamName[6] = "P_DESCRIPTION";
                ParamName[7] = "P_STAFFS_ID";
                ParamName[8] = "P_MOD_STAFFS_ID";
                ParamValue[0] = P_PROPERTYLIST_ID;
                ParamValue[1] = P_RECEIVEDDT;
                ParamValue[2] = P_CODE;
                ParamValue[3] = P_PROPERTYTYPELIST_ID;
                ParamValue[4] = P_UNITLIST_ID;
                ParamValue[5] = P_UNITPRICE;
                ParamValue[6] = P_DESCRIPTION;
                ParamValue[7] = P_STAFFS_ID;
                ParamValue[8] = P_MOD_STAFFS_ID;
                myObj.SP_OracleExecuteNonQuery("PROPERTYUSE_UPDATE", ParamName, ParamValue);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string PropertyTab2Datatable()
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet("SELECT ROWNUM as ROWNO, a.* FROM ( SELECT ID, NAME FROM STN_PROPERTYTYPELIST ORDER BY NAME ) a");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

        //*****srv.aspx*****//
        public string SrvTab1Datatable(string tp, string staffid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            if (tp == "1") tp = " WHERE TO_DATE(a.BEGINDT, 'YYYY-MM-DD') <= SYSDATE AND TO_DATE(a.ENDDT, 'YYYY-MM-DD') >= SYSDATE";
            else if (tp == "2") tp = " WHERE TO_DATE(a.ENDDT, 'YYYY-MM-DD') < SYSDATE";
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet(@"SELECT ROWNUM as ROWNO, a.* 
FROM ( SELECT a.ID, TITLE, a.BEGINDT, a.ENDDT, a.FILENAME, CASE WHEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') <= SYSDATE AND TO_DATE(a.ENDDT, 'YYYY-MM-DD') >= SYSDATE THEN 1 ELSE 0 END as ISACTIVE, CASE WHEN SRVANSWER_ID is not null THEN 1 ELSE 0 END as ISANSWERED
FROM ST_SRVQUESTION a
LEFT JOIN ST_SRVRESULT b ON a.ID=b.SRVQUESTION_ID AND b.STAFFS_ID=" + staffid+ tp + @"
ORDER BY a.BEGINDT, a.ENDDT ) a");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string SrvTab1AnswerList(string questionid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet(@"SELECT ID, NAME FROM ST_SRVANSWER WHERE SRVQUESTION_ID="+questionid+" ORDER BY ID");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

        //*****amralt.aspx*****//
        public string AmraltTab1GetAmraltDays(string yr, string staffid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet(@"SELECT 15 as DEFAULT_DAY, a.NEMEGDEL_DAY, NVL(b.AMRALTDAY,0) as AMARSAN_DAY
FROM ( 
  SELECT " + staffid + @"  as STAFF_ID, CASE WHEN 6 > YRCNT THEN 0 WHEN 6 <= YRCNT AND 11 > YRCNT THEN 3 WHEN 11 <= YRCNT AND 16 > YRCNT THEN 5 WHEN 16 <= YRCNT AND 21 > YRCNT THEN 7 WHEN 21 <= YRCNT AND 26 > YRCNT THEN 9 WHEN 26 <= YRCNT AND 32 > YRCNT THEN 11 WHEN 32 <= YRCNT THEN 14 END as NEMEGDEL_DAY 
  FROM( 
    SELECT SUM(YRCNT) as YRCNT 
    FROM( 
      SELECT ROUND(SUM(TOTALTM)/365,1) as YRCNT 
      FROM ( 
        SELECT SUM(CC) as TOTALTM, 0 as ISGOVTM, 0 as ISMYORG 
        FROM ( 
          SELECT SUM(NVL(TO_DATE(a.TODATE,'YYYY-MM-DD'),SYSDATE)-TO_DATE(a.FROMDATE,'YYYY-MM-DD') + 1) as CC, b.ISGOV, 0 as ISMYORG 
          FROM ST_EXPHISTORY a 
          INNER JOIN STN_ORGTYPE b ON a.ORGTYPE_ID=b.ID 
          WHERE a.STAFFS_ID=" + staffid + @"  
          GROUP BY b.ISGOV 
          UNION ALL 
          SELECT SUM(ROUND(NVL(TO_DATE(a.ENDDT, 'YYYY-MM-DD'), SYSDATE) - TO_DATE(a.DT, 'YYYY-MM-DD') + 1)) as CC, 1 as ISGOV, 1 as ISMYORG 
          FROM ST_STBR a 
          INNER JOIN STN_MOVE b ON a.MOVE_ID = b.ID 
          WHERE (b.ACTIVE = 1 OR b.MOVETYPE_ID=2) AND a.POS_ID != 2020102 AND a.STAFFS_ID = " + staffid + @"  
        ) 
        UNION ALL 
        SELECT 0 as TOTALTM, SUM(CC) as ISGOVTM, 0 as ISMYORG 
        FROM ( 
          SELECT SUM(NVL(TO_DATE(a.TODATE,'YYYY-MM-DD'),SYSDATE)-TO_DATE(a.FROMDATE,'YYYY-MM-DD') + 1) as CC, b.ISGOV, 0 as ISMYORG 
          FROM ST_EXPHISTORY a 
          INNER JOIN STN_ORGTYPE b ON a.ORGTYPE_ID=b.ID 
          WHERE a.STAFFS_ID=" + staffid + @"  
          GROUP BY b.ISGOV 
          UNION ALL 
          SELECT SUM(ROUND(NVL(TO_DATE(a.ENDDT, 'YYYY-MM-DD'), SYSDATE) - TO_DATE(a.DT, 'YYYY-MM-DD') + 1)) as CC, 1 as ISGOV, 1 as ISMYORG 
          FROM ST_STBR a 
          INNER JOIN STN_MOVE b ON a.MOVE_ID = b.ID 
          WHERE (b.ACTIVE = 1 OR b.MOVETYPE_ID=2) AND a.POS_ID != 2020102 AND a.STAFFS_ID = " + staffid + @"  
        ) 
        WHERE ISGOV=1 
        UNION ALL 
        SELECT 0 as TOTALTM, 0 as ISGOVTM, SUM(CC) as ISMYORG 
        FROM ( 
          SELECT SUM(NVL(TO_DATE(a.TODATE,'YYYY-MM-DD'),SYSDATE)-TO_DATE(a.FROMDATE,'YYYY-MM-DD') + 1) as CC, b.ISGOV, 0 as ISMYORG 
          FROM ST_EXPHISTORY a 
          INNER JOIN STN_ORGTYPE b ON a.ORGTYPE_ID=b.ID 
          WHERE a.STAFFS_ID=" + staffid + @"  
          GROUP BY b.ISGOV 
          UNION ALL 
          SELECT SUM(ROUND(NVL(TO_DATE(a.ENDDT, 'YYYY-MM-DD'), SYSDATE) - TO_DATE(a.DT, 'YYYY-MM-DD') + 1)) as CC, 1 as ISGOV, 1 as ISMYORG 
          FROM ST_STBR a 
          INNER JOIN STN_MOVE b ON a.MOVE_ID = b.ID 
          WHERE (b.ACTIVE = 1 OR b.MOVETYPE_ID=2) AND a.POS_ID != 2020102 AND a.STAFFS_ID = " + staffid + @" 
        ) 
        WHERE ISMYORG=1
      )
    )
  ) 
) a
LEFT JOIN (
  SELECT " + staffid + @"  as STAFF_ID, COUNT(a.STAFFS_ID) as AMRALTDAY 
  FROM ( 
    SELECT a.STAFFS_ID, a.BEGINDT||' - '||a.ENDDT as TM, b.DT 
    FROM ST_AMRALT a 
    INNER JOIN ( 
      SELECT a.DT
      FROM ( 
        SELECT DT 
        FROM ( 
          SELECT DT 
          FROM( 
            SELECT(TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') - 1) + ROWNUM DT 
            FROM DUAL 
            CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + @"-12-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') 
          ) 
          WHERE MOD(TO_CHAR(DT, 'J'), 7) + 1 IN(1, 2, 3, 4, 5) 
          UNION ALL 
          SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT 
          FROM ST_HOLIDAYISWORK 
          WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" 
        ) 
        GROUP BY DT 
      ) a
      LEFT JOIN ( 
        SELECT DT 
        FROM ( 
          SELECT TO_DATE('" + yr + @"-' || HOLMONTH || '-' || HOLDAY, 'YYYY-MM-DD') as DT 
          FROM ST_HOLIDAYOFFICIAL 
          UNION ALL 
          SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT 
          FROM ST_HOLIDAYUNOFFICIAL 
          WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" 
        ) 
        GROUP BY DT 
      ) b ON a.DT=b.DT 
      WHERE b.DT IS NULL 
    ) b ON TO_DATE(a.BEGINDT, 'yyyy-mm-dd') <= b.DT AND TO_DATE(a.ENDDT, 'yyyy-mm-dd') >= b.DT 
    WHERE TO_NUMBER(TO_CHAR(TO_DATE(a.BEGINDT,'YYYY-MM-DD'),'YYYY'))=" + yr + @" AND a.STAFFS_ID="+staffid+@" 
    ORDER BY a.STAFFS_ID, b.DT 
  ) a 
  GROUP BY a.STAFFS_ID
) b ON a.STAFF_ID=b.STAFF_ID");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string AmraltTab2t1Datatable(string yr, string gazarid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            if (gazarid != "") gazarid = " AND d.FATHER_ID=" + gazarid;
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet(@"SELECT ROWNUM as ROWNO, a.* FROM ( 
SELECT 
  a.ID, 
  CASE WHEN d.ID=d.FATHER_ID THEN d.INITNAME ELSE f.INITNAME||'-'||d.INITNAME END as NEGJ,
  SUBSTR(b.LNAME,0,1)||'.'||b.FNAME as STAFFNAME, 
  g.NAME as POS_NAME, 
  h.NEMEGDEL_DAY as AMRAH_HONOG, 
  i.AMRALTDAY as AMARSAN_HONOG, 
  j.TMLIST, 
  NVL(h.NEMEGDEL_DAY,0)-NVL(i.AMRALTDAY, 0) as DUTUU_HONOG
FROM ST_AMRALT a 
INNER JOIN ST_STAFFS b ON a.STAFFS_ID=b.ID 
INNER JOIN ST_STBR c ON b.ID=c.STAFFS_ID AND c.ISACTIVE=1 
INNER JOIN ST_BRANCH d ON c.BRANCH_ID=d.ID 
INNER JOIN ST_BRANCH f ON d.FATHER_ID=f.ID 
LEFT JOIN STN_POS g ON c.POS_ID=g.ID 
LEFT JOIN (
  SELECT STAFFS_ID, 
    CASE 
      WHEN 6 > YRCNT THEN 0 
      WHEN 6 <= YRCNT AND 11 > YRCNT THEN 3 
      WHEN 11 <= YRCNT AND 16 > YRCNT THEN 5
      WHEN 16 <= YRCNT AND 21 > YRCNT THEN 7 
      WHEN 21 <= YRCNT AND 26 > YRCNT THEN 9 
      WHEN 26 <= YRCNT AND 32 > YRCNT THEN 11 
      WHEN 32 <= YRCNT THEN 14 
    END+15 as NEMEGDEL_DAY 
  FROM( 
    SELECT STAFFS_ID, SUM(YRCNT) as YRCNT 
    FROM( 
      SELECT STAFFS_ID, ROUND(SUM(TOTALTM)/365,1) as YRCNT 
      FROM ( 
        SELECT STAFFS_ID, SUM(CC) as TOTALTM, 0 as ISGOVTM, 0 as ISMYORG 
        FROM ( 
          SELECT a.STAFFS_ID, SUM(NVL(TO_DATE(a.TODATE,'YYYY-MM-DD'),SYSDATE)-TO_DATE(a.FROMDATE,'YYYY-MM-DD') + 1) as CC, b.ISGOV, 0 as ISMYORG 
          FROM ST_EXPHISTORY a 
          INNER JOIN STN_ORGTYPE b ON a.ORGTYPE_ID=b.ID 
          GROUP BY b.ISGOV, a.STAFFS_ID
          UNION ALL 
          SELECT a.STAFFS_ID, SUM(ROUND(NVL(TO_DATE(a.ENDDT, 'YYYY-MM-DD'), SYSDATE) - TO_DATE(a.DT, 'YYYY-MM-DD') + 1)) as CC, 1 as ISGOV, 1 as ISMYORG 
          FROM ST_STBR a 
          INNER JOIN STN_MOVE b ON a.MOVE_ID = b.ID 
          WHERE (b.ACTIVE = 1 OR b.MOVETYPE_ID=2) AND a.POS_ID != 2020102
          GROUP BY a.STAFFS_ID
        ) 
        GROUP BY STAFFS_ID
        UNION ALL 
        SELECT STAFFS_ID, 0 as TOTALTM, SUM(CC) as ISGOVTM, 0 as ISMYORG 
        FROM ( 
          SELECT a.STAFFS_ID, SUM(NVL(TO_DATE(a.TODATE,'YYYY-MM-DD'),SYSDATE)-TO_DATE(a.FROMDATE,'YYYY-MM-DD') + 1) as CC, b.ISGOV, 0 as ISMYORG 
          FROM ST_EXPHISTORY a 
          INNER JOIN STN_ORGTYPE b ON a.ORGTYPE_ID=b.ID 
          GROUP BY a.STAFFS_ID, b.ISGOV
          UNION ALL 
          SELECT a.STAFFS_ID, SUM(ROUND(NVL(TO_DATE(a.ENDDT, 'YYYY-MM-DD'), SYSDATE) - TO_DATE(a.DT, 'YYYY-MM-DD') + 1)) as CC, 1 as ISGOV, 1 as ISMYORG 
          FROM ST_STBR a 
          INNER JOIN STN_MOVE b ON a.MOVE_ID = b.ID 
          WHERE (b.ACTIVE = 1 OR b.MOVETYPE_ID=2) AND a.POS_ID != 2020102
          GROUP BY a.STAFFS_ID
        ) 
        WHERE ISGOV=1 
        GROUP BY STAFFS_ID
        UNION ALL 
        SELECT STAFFS_ID, 0 as TOTALTM, 0 as ISGOVTM, SUM(CC) as ISMYORG 
        FROM ( 
          SELECT a.STAFFS_ID, SUM(NVL(TO_DATE(a.TODATE,'YYYY-MM-DD'),SYSDATE)-TO_DATE(a.FROMDATE,'YYYY-MM-DD') + 1) as CC, b.ISGOV, 0 as ISMYORG 
          FROM ST_EXPHISTORY a 
          INNER JOIN STN_ORGTYPE b ON a.ORGTYPE_ID=b.ID 
          GROUP BY a.STAFFS_ID, b.ISGOV 
          UNION ALL 
          SELECT a.STAFFS_ID, SUM(ROUND(NVL(TO_DATE(a.ENDDT, 'YYYY-MM-DD'), SYSDATE) - TO_DATE(a.DT, 'YYYY-MM-DD') + 1)) as CC, 1 as ISGOV, 1 as ISMYORG 
          FROM ST_STBR a 
          INNER JOIN STN_MOVE b ON a.MOVE_ID = b.ID 
          WHERE (b.ACTIVE = 1 OR b.MOVETYPE_ID=2) AND a.POS_ID != 2020102
          GROUP BY a.STAFFS_ID
        ) 
        WHERE ISMYORG=1
        GROUP BY STAFFS_ID
      )
      GROUP BY STAFFS_ID
    )
    GROUP BY STAFFS_ID
  )
) h ON a.STAFFS_ID=h.STAFFS_ID
LEFT JOIN (
  SELECT 
    a.STAFFS_ID, COUNT(a.STAFFS_ID) as AMRALTDAY
  FROM (
    SELECT 
      a.STAFFS_ID, 
      a.BEGINDT||' - '||a.ENDDT as TM, 
      b.DT
    FROM ST_AMRALT a 
    INNER JOIN (
      SELECT a.DT
      FROM (
        SELECT DT
        FROM (
          SELECT DT
          FROM(
            SELECT(TO_DATE('"+yr+ @"-1-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
            FROM DUAL
            CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + @"-12-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + @"-1-01', 'yyyy-mm-dd')
          )
          WHERE MOD(TO_CHAR(DT, 'J'), 7) + 1 IN(1, 2, 3, 4, 5)
          UNION ALL
          SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT FROM ST_HOLIDAYISWORK WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @"
        )
        GROUP BY DT
      ) a
      LEFT JOIN (
        SELECT DT
        FROM (
          SELECT TO_DATE('" + yr + @"-' || HOLMONTH || '-' || HOLDAY, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYOFFICIAL
          UNION ALL
          SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYUNOFFICIAL
          WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @"
        ) 
        GROUP BY DT
      ) b ON a.DT=b.DT
      WHERE b.DT IS NULL
    ) b ON TO_DATE(a.BEGINDT, 'yyyy-mm-dd') <= b.DT AND TO_DATE(a.ENDDT, 'yyyy-mm-dd') >= b.DT
    WHERE a.TZISRECEIVED=1 AND TO_NUMBER(TO_CHAR(TO_DATE(a.BEGINDT,'YYYY-MM-DD'),'YYYY'))=" + yr + @" 
    ORDER BY a.STAFFS_ID, b.DT
  ) a
  GROUP BY a.STAFFS_ID
) i ON a.STAFFS_ID=i.STAFFS_ID
LEFT JOIN ( 
  SELECT 
    STAFFS_ID, 
    RTRIM(xmlagg(xmlelement(e, TM || ', ') ORDER BY TM).extract('//text()'), ', ') as TMLIST
  FROM (
    SELECT 
      a.STAFFS_ID, 
      a.BEGINDT||' - '||a.ENDDT as TM 
    FROM ST_AMRALT a 
    INNER JOIN ST_STAFFS b ON a.STAFFS_ID=b.ID 
    INNER JOIN ST_STBR c ON b.ID=c.STAFFS_ID AND c.ISACTIVE=1 
    INNER JOIN ST_BRANCH d ON c.BRANCH_ID=d.ID 
    INNER JOIN ST_BRANCH f ON d.FATHER_ID=f.ID 
    LEFT JOIN STN_POS g ON c.POS_ID=g.ID 
    WHERE a.TZISRECEIVED=1 AND TO_NUMBER(TO_CHAR(TO_DATE(a.BEGINDT,'YYYY-MM-DD'),'YYYY'))=" + yr + @" 
    GROUP BY a.STAFFS_ID, a.BEGINDT||' - '||a.ENDDT
  )
  GROUP BY STAFFS_ID
) j ON a.STAFFS_ID=j.STAFFS_ID
WHERE a.TZISRECEIVED=1 AND TO_NUMBER(TO_CHAR(TO_DATE(a.BEGINDT,'YYYY-MM-DD'),'YYYY'))=" + yr+gazarid + @" 
ORDER BY f.SORT, d.SORT, g.SORT, a.STAFFS_ID
) a");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

        //*****staffsdataadd.aspx*****//
        public string StaffsdataaddTab1Datatable(string fromdate, string todate, string decision, string type)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            if (decision != "") decision = " AND b.SHAGNALDECISION_ID=" + decision;
            if (type != "") type = " AND b.SHAGNALTYPE_ID=" + type;
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet(@"SELECT
  a.SHAGNAL_ID, 
  a.STAFFLISTID, 
  b.NAME, 
  b.DT, 
  b.SHAGNALTYPE_ID, 
  c.NAME as SHAGNALTYPE_NAME, 
  b.SHAGNALDECISION_ID, 
  d.NAME as SHAGNALDECISION_NAME, 
  b.ORGDESCRIPTION, 
  b.PRICE, 
  b.GROUND, 
  b.TUSHAALNO, 
  b.TUSHAALDT, 
  b.FILENAME
FROM (
  SELECT a.SHAGNAL_ID, RTRIM(xmlagg (xmlelement (e, a.STAFFS_ID || ',')).extract('//text()'), ',') as STAFFLISTID 
  FROM ST_SHAGNAL_STAFFS a 
  INNER JOIN ST_SHAGNAL b ON a.SHAGNAL_ID=b.ID
  WHERE TO_DATE(b.DT,'YYYY-MM-DD') BETWEEN TO_DATE('" + fromdate + @"','YYYY-MM-DD') AND TO_DATE('" + todate + @"','YYYY-MM-DD')" + decision + type + @"
  GROUP BY a.SHAGNAL_ID
) a 
INNER JOIN ST_SHAGNAL b ON a.SHAGNAL_ID=b.ID
INNER JOIN STN_SHAGNALTYPE c ON b.SHAGNALTYPE_ID=c.ID
INNER JOIN STN_SHAGNALDECISION d ON b.SHAGNALDECISION_ID=d.ID
ORDER BY b.DT");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void SHAGNAL_INSERT(string P_NAME, string P_DT, string P_SHAGNALTYPE_ID, string P_SHAGNALDECISION_ID, string P_ORGDESCRIPTION, string P_PRICE, string P_GROUND, string P_TUSHAALNO, string P_TUSHAALDT, string P_FILENAME, string P_STAFFLIST, string P_STAFFID)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string[] ParamName = new string[12], ParamValue = new string[12];
                ParamName[0] = "P_NAME";
                ParamName[1] = "P_DT";
                ParamName[2] = "P_SHAGNALTYPE_ID";
                ParamName[3] = "P_SHAGNALDECISION_ID";
                ParamName[4] = "P_ORGDESCRIPTION";
                ParamName[5] = "P_PRICE";
                ParamName[6] = "P_GROUND";
                ParamName[7] = "P_TUSHAALNO";
                ParamName[8] = "P_TUSHAALDT";
                ParamName[9] = "P_FILENAME";
                ParamName[10] = "P_STAFFLIST";
                ParamName[11] = "P_STAFFID";
                ParamValue[0] = P_NAME;
                ParamValue[1] = P_DT;
                ParamValue[2] = P_SHAGNALTYPE_ID;
                ParamValue[3] = P_SHAGNALDECISION_ID;
                ParamValue[4] = P_ORGDESCRIPTION;
                ParamValue[5] = P_PRICE;
                ParamValue[6] = P_GROUND;
                ParamValue[7] = P_TUSHAALNO;
                ParamValue[8] = P_TUSHAALDT;
                ParamValue[9] = P_FILENAME;
                ParamValue[10] = P_STAFFLIST;
                ParamValue[11] = P_STAFFID;
                myObj.SP_OracleExecuteNonQuery("SHAGNAL_INSERT", ParamName, ParamValue);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void SHAGNAL_UPDATE(string P_ID, string P_NAME, string P_DT, string P_SHAGNALTYPE_ID, string P_SHAGNALDECISION_ID, string P_ORGDESCRIPTION, string P_PRICE, string P_GROUND, string P_TUSHAALNO, string P_TUSHAALDT, string P_FILENAME, string P_STAFFLIST, string P_STAFFID)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string[] ParamName = new string[13], ParamValue = new string[13];
                ParamName[0] = "P_ID";
                ParamName[1] = "P_NAME";
                ParamName[2] = "P_DT";
                ParamName[3] = "P_SHAGNALTYPE_ID";
                ParamName[4] = "P_SHAGNALDECISION_ID";
                ParamName[5] = "P_ORGDESCRIPTION";
                ParamName[6] = "P_PRICE";
                ParamName[7] = "P_GROUND";
                ParamName[8] = "P_TUSHAALNO";
                ParamName[9] = "P_TUSHAALDT";
                ParamName[10] = "P_FILENAME";
                ParamName[11] = "P_STAFFLIST";
                ParamName[12] = "P_STAFFID";
                ParamValue[0] = P_ID;
                ParamValue[1] = P_NAME;
                ParamValue[2] = P_DT;
                ParamValue[3] = P_SHAGNALTYPE_ID;
                ParamValue[4] = P_SHAGNALDECISION_ID;
                ParamValue[5] = P_ORGDESCRIPTION;
                ParamValue[6] = P_PRICE;
                ParamValue[7] = P_GROUND;
                ParamValue[8] = P_TUSHAALNO;
                ParamValue[9] = P_TUSHAALDT;
                ParamValue[10] = P_FILENAME;
                ParamValue[11] = P_STAFFLIST;
                ParamValue[12] = P_STAFFID;
                myObj.SP_OracleExecuteNonQuery("SHAGNAL_UPDATE", ParamName, ParamValue);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void SHAGNAL_DELETE(string P_ID)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string[] ParamName = new string[1], ParamValue = new string[1];
                ParamName[0] = "P_ID";
                ParamValue[0] = P_ID;
                myObj.SP_OracleExecuteNonQuery("SHAGNAL_DELETE", ParamName, ParamValue);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string StaffsdataaddTab2Datatable(string fromdate, string todate, string posdegreedtl, string rankposdegree)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            if (posdegreedtl != "") posdegreedtl = " AND b.POSDEGREEDTL_ID=" + posdegreedtl;
            if (rankposdegree != "") rankposdegree = " AND b.RANKPOSDEGREE_ID=" + rankposdegree;
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet(@"SELECT
  a.ZEREGDEV_ID, 
  a.STAFFLISTID, 
  b.POSDEGREEDTL_ID, 
  c.NAME as POSDEGREEDTL_NAME, 
  b.RANKPOSDEGREE_ID, 
  d.NAME as RANKPOSDEGREE_NAME, 
  b.DECISIONDESC, 
  b.DT, 
  b.CERTIFICATENO, 
  b.UPPER, 
  b.FILENAME
FROM (
  SELECT a.ZEREGDEV_ID, RTRIM(xmlagg (xmlelement (e, a.STAFFS_ID || ',')).extract('//text()'), ',') as STAFFLISTID 
  FROM ST_ZEREGDEV_STAFFS a 
  INNER JOIN ST_ZEREGDEV b ON a.ZEREGDEV_ID=b.ID
  WHERE TO_DATE(b.DT,'YYYY-MM-DD') BETWEEN TO_DATE('"+fromdate+"','YYYY-MM-DD') AND TO_DATE('"+todate+"','YYYY-MM-DD')"+posdegreedtl+rankposdegree+@"
  GROUP BY a.ZEREGDEV_ID
) a 
INNER JOIN ST_ZEREGDEV b ON a.ZEREGDEV_ID=b.ID
INNER JOIN STN_POSDEGREEDTL c ON b.POSDEGREEDTL_ID=c.ID
INNER JOIN STN_RANKPOSDEGREE d ON b.RANKPOSDEGREE_ID=d.ID
ORDER BY b.DT");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void ZEREGDEV_INSERT(string P_POSDEGREEDTL_ID, string P_RANKPOSDEGREE_ID, string P_DECISIONDESC, string P_DT, string P_CERTIFICATENO, string P_UPPER, string P_FILENAME, string P_STAFFLIST, string P_STAFFID)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string[] ParamName = new string[9], ParamValue = new string[9];
                ParamName[0] = "P_POSDEGREEDTL_ID";
                ParamName[1] = "P_RANKPOSDEGREE_ID";
                ParamName[2] = "P_DECISIONDESC";
                ParamName[3] = "P_DT";
                ParamName[4] = "P_CERTIFICATENO";
                ParamName[5] = "P_UPPER";
                ParamName[6] = "P_FILENAME";
                ParamName[7] = "P_STAFFLIST";
                ParamName[8] = "P_STAFFID";
                ParamValue[0] = P_POSDEGREEDTL_ID;
                ParamValue[1] = P_RANKPOSDEGREE_ID;
                ParamValue[2] = P_DECISIONDESC;
                ParamValue[3] = P_DT;
                ParamValue[4] = P_CERTIFICATENO;
                ParamValue[5] = P_UPPER;
                ParamValue[6] = P_FILENAME;
                ParamValue[7] = P_STAFFLIST;
                ParamValue[8] = P_STAFFID;
                myObj.SP_OracleExecuteNonQuery("ZEREGDEV_INSERT", ParamName, ParamValue);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void ZEREGDEV_UPDATE(string P_ID, string P_POSDEGREEDTL_ID, string P_RANKPOSDEGREE_ID, string P_DECISIONDESC, string P_DT, string P_CERTIFICATENO, string P_UPPER, string P_FILENAME, string P_STAFFLIST, string P_STAFFID)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string[] ParamName = new string[10], ParamValue = new string[10];
                ParamName[0] = "P_ID";
                ParamName[1] = "P_POSDEGREEDTL_ID";
                ParamName[2] = "P_RANKPOSDEGREE_ID";
                ParamName[3] = "P_DECISIONDESC";
                ParamName[4] = "P_DT";
                ParamName[5] = "P_CERTIFICATENO";
                ParamName[6] = "P_UPPER";
                ParamName[7] = "P_FILENAME";
                ParamName[8] = "P_STAFFLIST";
                ParamName[9] = "P_STAFFID";
                ParamValue[0] = P_ID;
                ParamValue[1] = P_POSDEGREEDTL_ID;
                ParamValue[2] = P_RANKPOSDEGREE_ID;
                ParamValue[3] = P_DECISIONDESC;
                ParamValue[4] = P_DT;
                ParamValue[5] = P_CERTIFICATENO;
                ParamValue[6] = P_UPPER;
                ParamValue[7] = P_FILENAME;
                ParamValue[8] = P_STAFFLIST;
                ParamValue[9] = P_STAFFID;
                myObj.SP_OracleExecuteNonQuery("ZEREGDEV_UPDATE", ParamName, ParamValue);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public void ZEREGDEV_DELETE(string P_ID)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string[] ParamName = new string[1], ParamValue = new string[1];
                ParamName[0] = "P_ID";
                ParamValue[0] = P_ID;
                myObj.SP_OracleExecuteNonQuery("ZEREGDEV_DELETE", ParamName, ParamValue);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

        //*****tomilolt.aspx*****//
        public string tomiloltTab1ModalSelectstafflistForSelect2(string selectedList)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                //DataSet ds = myObj.OracleExecuteDataSet("SELECT f.ID as GAZAR_ID, f.INITNAME, a.ID as STAFF_ID, SUBSTR(a.LNAME,0,1)||'.'||SUBSTR(a.FNAME,1,1)||LOWER(SUBSTR(a.FNAME,2))||' | '||g.NAME as STAFF_NAME, NVL2(h.ST_ID,' selected=\"selected\"',null) as ISSELECTED FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID = b.STAFFS_ID INNER JOIN STN_MOVE c ON b.MOVE_ID = c.ID INNER JOIN ST_BRANCH d ON b.BRANCH_ID = d.ID INNER JOIN ST_BRANCH f ON d.FATHER_ID = f.ID INNER JOIN STN_POS g ON b.POS_ID = g.ID LEFT JOIN( SELECT TO_NUMBER(ST_ID) as ST_ID FROM( select regexp_substr('"+selectedList+"', '[^,]+', 1, level) AS ST_ID from dual connect by regexp_substr('"+selectedList+"', '[^,]+', 1, level) is not null ) a ) h ON a.ID = h.ST_ID WHERE b.ISACTIVE = 1 AND c.ACTIVE = 1 ORDER BY f.SORT, g.SORT, a.LNAME, a.FNAME");
                DataSet ds = myObj.OracleExecuteDataSet("SELECT f.ID as GAZAR_ID, f.INITNAME, a.ID as STAFF_ID, SUBSTR(a.LNAME,0,1)||'.'||SUBSTR(a.FNAME,1,1)||LOWER(SUBSTR(a.FNAME,2))||' | '||g.NAME as STAFF_NAME, NVL2(h.ST_ID,' selected=\"selected\"',null) as ISSELECTED FROM ST_STAFFS a INNER JOIN ST_STBR b ON a.ID = b.STAFFS_ID INNER JOIN STN_MOVE c ON b.MOVE_ID = c.ID INNER JOIN ST_BRANCH d ON b.BRANCH_ID = d.ID INNER JOIN ST_BRANCH f ON d.FATHER_ID = f.ID INNER JOIN STN_POS g ON b.POS_ID = g.ID LEFT JOIN( SELECT TO_NUMBER(ST_ID) as ST_ID FROM( select regexp_substr('" + selectedList + "', '[^,]+', 1, level) AS ST_ID from dual connect by regexp_substr('" + selectedList + "', '[^,]+', 1, level) is not null ) a ) h ON a.ID = h.ST_ID WHERE b.ISACTIVE = 1 ORDER BY f.SORT, g.SORT, a.LNAME, a.FNAME");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

        //*****profile.aspx*****//
        public string ProfileTab1t9t1Datatable(string staffid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                DataSet ds = myObj.OracleExecuteDataSet(@"SELECT ID, NAME, DT, ISEDIT
FROM (
  SELECT a.STAFFS_ID as ID, b.NAME, b.DT, 0 as ISEDIT
  FROM ST_SHAGNAL_STAFFS a
  INNER JOIN ST_SHAGNAL b ON a.SHAGNAL_ID=b.ID
  WHERE a.STAFFS_ID="+ staffid + @"
  UNION ALL
  SELECT ID, NAME, DT, 1 as ISEDIT
  FROM ST_BONUS 
  WHERE STAFFS_ID=" + staffid + @"
)
ORDER BY DT DESC");
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

        //*****workingtime.aspx*****//
        public string WorkingtimeTab2Datatable(string yr, string month, string month2, string gazar, string heltes, string stid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            string yr1 = yr, strMonthList = "";
            for (int i = Int32.Parse(month); i <= Int32.Parse(month2); i++)
            {
                if (strMonthList == "") strMonthList += i.ToString();
                else strMonthList += "," + i.ToString();
            }
            if (gazar != "") gazar = " AND c.FATHER_ID=" + gazar;
            if (heltes != "") heltes = " AND c.ID=" + heltes;
            if (stid != "") stid = " AND b.STAFFS_ID=" + stid;
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string strQry = "";
                strQry = @"SELECT ROWNUM as ROWNO, a.* 
FROM (
  SELECT 
    a.ST_ID, 
    CASE WHEN d.ID = d.FATHER_ID THEN d.INITNAME ELSE f.INITNAME || '-' || d.INITNAME END as NEGJ, 
    g.NAME as POS_NAME, 
    SUBSTR(h.LNAME, 0, 1) || '.' || SUBSTR(h.FNAME, 1, 1) || LOWER(SUBSTR(h.FNAME, 2)) as STAFFNAME, 
    a.WORKDAY, 
    a.CHOLOODAYSUM, 
    a.UWCHTEIDAYSUM, 
    a.AMRALTDAYSUM, 
    a.TOMILOLTDAYSUM, 
    ROUND((a.WORKDAY - (a.CHOLOODAYSUM + a.UWCHTEIDAYSUM + a.AMRALTDAYSUM + a.TOMILOLTDAYSUM + a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)), 2) as WORKEDAY, 
    a.TASALSANDAYSUM, 
    ROUND(a.HOTSORSONMINUTESUM, 1) as HOTSORSONMINUTESUM, 
    ROUND(a.HOTSORSONDAYSUM,1) as HOTSORSONDAYSUM, 
    ROUND(a.ERTMINUTESUM, 1) as ERTMINUTESUM, 
    ROUND(a.OROIMINUTESUM, 1) as OROIMINUTESUM, 
    ROUND((a.WORKDAY - (a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)), 2) as EVALWORKEDDAY, 
    ROUND(((a.WORKDAY - (a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)) / a.WORKDAY) * 100, 2) as PER
  FROM(
    SELECT ST_ID,
      SUM(ISWORK) as WORKDAY,
      SUM(CHOLOODAY) as CHOLOODAYSUM,
      SUM(UWCHTEIDAY) as UWCHTEIDAYSUM,
      SUM(AMRALTDAY) as AMRALTDAYSUM,
      SUM(TOMILOLTDAY) as TOMILOLTDAYSUM,
      SUM(ISTASALSAN) as TASALSANDAYSUM,
      SUM(HOTSORSONMINUTE) as HOTSORSONMINUTESUM,
      SUM(HOTSORSONDAY) as HOTSORSONDAYSUM,
      SUM(ERTMINUTE) as ERTMINUTESUM,
      SUM(ERTDAY) as ERTDAYSUM,
      SUM(OROIMINUTE) as OROIMINUTESUM,
      SUM(OROIDAY) as OROIDAYSUM
    FROM(
      SELECT ST_ID, TP, DT, MINTM, MAXTM, ISWORK,
        CASE WHEN TP = 11 AND ISWORK = 1 OR TP = 12 THEN 1 ELSE 0 END as CHOLOODAY,
        CASE WHEN TP = 13 AND ISWORK = 1 THEN 1 ELSE 0 END as UWCHTEIDAY,
        CASE WHEN TP = 21 AND ISWORK = 1 THEN 1 ELSE 0 END as AMRALTDAY,
        CASE WHEN TP = 31 AND ISWORK = 1 THEN 1 ELSE 0 END as TOMILOLTDAY,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM = MAXTM THEN 1 ELSE 0 END as ISTASALSAN,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'hotsorson', 'minute') ELSE 0 END as HOTSORSONMINUTE,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'hotsorson', 'day') ELSE 0 END as HOTSORSONDAY,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'ert', 'minute') ELSE 0 END as ERTMINUTE,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'ert', 'day') ELSE 0 END as ERTDAY,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'oroi', 'minute') ELSE 0 END as OROIMINUTE,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'oroi', 'day') ELSE 0 END as OROIDAY
      FROM(
        SELECT a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END as ISWORK
        FROM(
          SELECT a.ST_ID, NVL(b.TP, 0) as TP, a.DT, a.MINTM, a.MAXTM, a.ISWORK
          FROM(
            SELECT a.ST_ID, a.DT, NVL(b.TP, 0) as TP, NVL(b.MINTM, '00:00:00') as MINTM, NVL(b.MAXTM, '00:00:00') as MAXTM, a.ISWORK
            FROM(
              SELECT a.STAFFS_ID as ST_ID, a.DT, CASE WHEN b.DT IS NULL THEN a.ISWORK ELSE 1 END as ISWORK
              FROM ( 
                SELECT a.ID, b.STAFFS_ID, c.FATHER_ID as GAZAR_ID, c.ID as HELTES_ID, a.DT, CASE WHEN(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 6 OR(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 7 THEN 0 ELSE 1 END as ISWORK
                FROM (
                  SELECT a.ID, a.BEGINDT, a.ENDDT, b.DT
                  FROM (
                    SELECT a.ID, 
                      CASE 
                        WHEN TO_DATE(a.DT,'YYYY-MM-DD')>TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD') 
                        THEN a.DT 
                        ELSE '" + yr + "-" + month + @"-01' 
                      END as BEGINDT, 
                      CASE 
                        --WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        --THEN CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                        WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        THEN CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                    FROM ST_STBR a
                    INNER JOIN ST_BRANCH b ON a.BRANCH_ID=b.ID AND b.ISACTIVE=1
                    INNER JOIN STN_MOVE c ON a.MOVE_ID=c.ID
                    WHERE a.POS_ID!=2020102 AND c.ACTIVE=1
                    AND ((
                        TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD') 
                        BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                        OR 
                        LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                      ) OR 
                        (
                          TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')<TO_DATE(a.DT,'YYYY-MM-DD') AND LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD'))>TO_DATE(a.DT,'YYYY-MM-DD')
                        ))
                  ) a, (
                    SELECT DT
                    FROM(
                      SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                      FROM DUAL
                      CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                    )
                    WHERE DT BETWEEN TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(DT, 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
                  ) b
                ) a
                INNER JOIN ST_STBR b ON a.ID=b.ID
                INNER JOIN ST_BRANCH c ON b.BRANCH_ID=c.ID AND c.ISACTIVE=1
                WHERE a.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD') AND b.POS_ID!=2020102" + gazar + heltes + stid + @"
              ) a
              LEFT JOIN(
                SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT FROM ST_HOLIDAYISWORK WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + strMonthList + @")
              ) b ON a.DT = b.DT  
            ) a
            LEFT JOIN(
              SELECT a.MONTH, a.INOUT as TP, b.ID as ST_ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') as DT, TO_CHAR(MIN(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MINTM, TO_CHAR(MAX(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MAXTM
              FROM hr_mof.STN_TRCDLOG a
              INNER JOIN ST_STAFFS b ON a.ENO = b.FINGERID
              INNER JOIN ST_STBR c ON b.ID = c.STAFFS_ID
              INNER JOIN STN_MOVE d ON c.MOVE_ID = d.ID
              INNER JOIN ST_BRANCH f ON c.BRANCH_ID = f.ID AND f.ISACTIVE = 1
              WHERE c.POS_ID!=2020102 AND a.INOUT = 0 AND a.YEAR = " + yr + @" AND a.MONTH IN(" + strMonthList + @") AND TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') BETWEEN TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd'), 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
              GROUP BY a.MONTH, a.INOUT, b.ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd')
            ) b ON a.ST_ID = b.ST_ID AND a.DT = b.DT  
          ) a
          LEFT JOIN(
            SELECT DT, STAFFS_ID, MAX(TP) as TP
            FROM(
              SELECT b.DT, a.STAFFS_ID, 11 as TP
              FROM ST_CHULUUDAYF3 a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 12 as TP
              FROM ST_CHULUUDAYT2 a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                a.ISRECEIVED = 1 AND
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 13 as TP
              FROM ST_CHULUUSICK a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 21 as TP
              FROM ST_AMRALT a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                a.TZISRECEIVED = 1 AND
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 31 as TP
              FROM ST_TOMILOLT_STAFFS a
              INNER JOIN ST_TOMILOLT c ON a.TOMILOLT_ID = c.ID,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(c.FROMDATE, 'YYYY-MM-DD') AND TO_DATE(c.TODATE, 'YYYY-MM-DD')
            )
            GROUP BY DT, STAFFS_ID
          ) b ON a.DT = b.DT AND a.ST_ID = b.STAFFS_ID
        ) a
        LEFT JOIN(
          SELECT TO_DATE('" + yr + @"-' || HOLMONTH || '-' || HOLDAY, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYOFFICIAL
          WHERE HOLMONTH IN(" + strMonthList + @")
          UNION ALL
          SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYUNOFFICIAL
          WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + strMonthList + @")
        ) b ON a.DT = b.DT
        GROUP BY a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END
      )
    )
    GROUP BY ST_ID
  ) a
  INNER JOIN ST_STBR c ON a.ST_ID = c.STAFFS_ID AND c.ISACTIVE = 1
  INNER JOIN ST_BRANCH d ON c.BRANCH_ID = d.ID AND d.ISACTIVE = 1
  INNER JOIN ST_BRANCH f ON d.FATHER_ID = f.ID AND f.ISACTIVE = 1
  INNER JOIN STN_POS g ON c.POS_ID = g.ID
  INNER JOIN ST_STAFFS h ON a.ST_ID = h.ID
  WHERE a.WORKDAY>0
  ORDER BY f.SORT, d.SORT, g.SORT, a.ST_ID ) a";
                DataSet ds = myObj.OracleExecuteDataSet(strQry);
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string WorkingtimeTab3Datatable(string yr, string month, string month2, string gazar, string heltes, string stid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            string strMonthList = "";
            for (int i = Int32.Parse(month); i <= Int32.Parse(month2); i++)
            {
                if (strMonthList == "") strMonthList += i.ToString();
                else strMonthList += "," + i.ToString();
            }
            if (gazar != "") gazar = " AND c.FATHER_ID=" + gazar;
            if (heltes != "") heltes = " AND c.ID=" + heltes;
            if (stid != "") stid = " AND b.STAFFS_ID=" + stid;
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string strQry = "";
                strQry = @"SELECT ROWNUM as ROWNO, a.* 
FROM (
  SELECT 
    a.ST_ID, 
    CASE WHEN d.ID = d.FATHER_ID THEN d.INITNAME ELSE f.INITNAME || '-' || d.INITNAME END as NEGJ, 
    g.NAME as POS_NAME, 
    SUBSTR(h.LNAME, 0, 1) || '.' || SUBSTR(h.FNAME, 1, 1) || LOWER(SUBSTR(h.FNAME, 2)) as STAFFNAME, 
    a.WORKDAY, 
    a.CHOLOODAYSUM, 
    a.UWCHTEIDAYSUM, 
    a.AMRALTDAYSUM, 
    a.TOMILOLTDAYSUM, 
    ROUND((a.WORKDAY - (a.CHOLOODAYSUM + a.UWCHTEIDAYSUM + a.AMRALTDAYSUM + a.TOMILOLTDAYSUM + a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)), 2) as WORKEDAY, 
    a.TASALSANDAYSUM, 
    ROUND(a.HOTSORSONMINUTESUM, 1) as HOTSORSONMINUTESUM, 
    ROUND(a.HOTSORSONDAYSUM,1) as HOTSORSONDAYSUM, 
    ROUND(a.ERTMINUTESUM, 1) as ERTMINUTESUM, 
    ROUND(a.OROIMINUTESUM, 1) as OROIMINUTESUM, 
    ROUND((a.WORKDAY - (a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)), 2) as EVALWORKEDDAY, 
    ROUND(((a.WORKDAY - (a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)) / a.WORKDAY) * 100, 2) as PER
  FROM(
    SELECT ST_ID,
      SUM(ISWORK) as WORKDAY,
      SUM(CHOLOODAY) as CHOLOODAYSUM,
      SUM(UWCHTEIDAY) as UWCHTEIDAYSUM,
      SUM(AMRALTDAY) as AMRALTDAYSUM,
      SUM(TOMILOLTDAY) as TOMILOLTDAYSUM,
      SUM(ISTASALSAN) as TASALSANDAYSUM,
      SUM(HOTSORSONMINUTE) as HOTSORSONMINUTESUM,
      SUM(HOTSORSONDAY) as HOTSORSONDAYSUM,
      SUM(ERTMINUTE) as ERTMINUTESUM,
      SUM(ERTDAY) as ERTDAYSUM,
      SUM(OROIMINUTE) as OROIMINUTESUM,
      SUM(OROIDAY) as OROIDAYSUM
    FROM(
      SELECT ST_ID, TP, DT, MINTM, MAXTM, ISWORK,
        CASE WHEN TP = 11 AND ISWORK = 1 OR TP = 12 THEN 1 ELSE 0 END as CHOLOODAY,
        CASE WHEN TP = 13 AND ISWORK = 1 THEN 1 ELSE 0 END as UWCHTEIDAY,
        CASE WHEN TP = 21 AND ISWORK = 1 THEN 1 ELSE 0 END as AMRALTDAY,
        CASE WHEN TP = 31 AND ISWORK = 1 THEN 1 ELSE 0 END as TOMILOLTDAY,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM = MAXTM THEN 1 ELSE 0 END as ISTASALSAN,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'hotsorson', 'minute') ELSE 0 END as HOTSORSONMINUTE,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'hotsorson', 'day') ELSE 0 END as HOTSORSONDAY,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'ert', 'minute') ELSE 0 END as ERTMINUTE,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'ert', 'day') ELSE 0 END as ERTDAY,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'oroi', 'minute') ELSE 0 END as OROIMINUTE,
        CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'oroi', 'day') ELSE 0 END as OROIDAY
      FROM(
        SELECT a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END as ISWORK
        FROM(
          SELECT a.ST_ID, NVL(b.TP, 0) as TP, a.DT, a.MINTM, a.MAXTM, a.ISWORK
          FROM(
            SELECT a.ST_ID, a.DT, NVL(b.TP, 0) as TP, NVL(b.MINTM, '00:00:00') as MINTM, NVL(b.MAXTM, '00:00:00') as MAXTM, a.ISWORK
            FROM(
              SELECT a.STAFFS_ID as ST_ID, a.DT, CASE WHEN b.DT IS NULL THEN a.ISWORK ELSE 1 END as ISWORK
              FROM ( 
                SELECT a.ID, b.STAFFS_ID, c.FATHER_ID as GAZAR_ID, c.ID as HELTES_ID, a.DT, CASE WHEN(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 6 OR(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 7 THEN 0 ELSE 1 END as ISWORK
                FROM (
                  SELECT a.ID, a.BEGINDT, a.ENDDT, b.DT
                  FROM (
                    SELECT a.ID, 
                      CASE 
                        WHEN TO_DATE(a.DT,'YYYY-MM-DD')>TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD') 
                        THEN a.DT 
                        ELSE '" + yr + "-" + month + @"-01' 
                      END as BEGINDT, 
                      CASE 
                        --WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        --THEN CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                        WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        THEN CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                    FROM ST_STBR a
                    INNER JOIN ST_BRANCH b ON a.BRANCH_ID=b.ID AND b.ISACTIVE=1
                    INNER JOIN STN_MOVE c ON a.MOVE_ID=c.ID
                    WHERE a.POS_ID!=2020102 AND c.ACTIVE=1
                    AND ((
                        TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD') 
                        BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                        OR 
                        LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                      ) OR 
                        (
                          TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')<TO_DATE(a.DT,'YYYY-MM-DD') AND LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD'))>TO_DATE(a.DT,'YYYY-MM-DD')
                        ))
                  ) a, (
                    SELECT DT
                    FROM(
                      SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                      FROM DUAL
                      CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                    )
                    WHERE DT BETWEEN TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(DT, 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
                  ) b
                ) a
                INNER JOIN ST_STBR b ON a.ID=b.ID
                INNER JOIN ST_BRANCH c ON b.BRANCH_ID=c.ID AND c.ISACTIVE=1
                WHERE a.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD') AND b.POS_ID!=2020102" + gazar + heltes + stid + @"
              ) a
              LEFT JOIN(
                SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT FROM ST_HOLIDAYISWORK WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + strMonthList + @")
              ) b ON a.DT = b.DT  
            ) a
            LEFT JOIN(
              SELECT a.MONTH, a.INOUT as TP, b.ID as ST_ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') as DT, TO_CHAR(MIN(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MINTM, TO_CHAR(MAX(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MAXTM
              FROM hr_mof.STN_TRCDLOG a
              INNER JOIN ST_STAFFS b ON a.ENO = b.FINGERID
              INNER JOIN ST_STBR c ON b.ID = c.STAFFS_ID
              INNER JOIN STN_MOVE d ON c.MOVE_ID = d.ID
              INNER JOIN ST_BRANCH f ON c.BRANCH_ID = f.ID AND f.ISACTIVE = 1
              WHERE c.POS_ID!=2020102 AND a.INOUT = 0 AND a.YEAR = " + yr + @" AND a.MONTH IN(" + strMonthList + @") AND TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') BETWEEN TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd'), 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
              GROUP BY a.MONTH, a.INOUT, b.ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd')
            ) b ON a.ST_ID = b.ST_ID AND a.DT = b.DT  
          ) a
          LEFT JOIN(
            SELECT DT, STAFFS_ID, MAX(TP) as TP
            FROM(
              SELECT b.DT, a.STAFFS_ID, 11 as TP
              FROM ST_CHULUUDAYF3 a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 12 as TP
              FROM ST_CHULUUDAYT2 a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                a.ISRECEIVED = 1 AND
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 13 as TP
              FROM ST_CHULUUSICK a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 21 as TP
              FROM ST_AMRALT a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                a.TZISRECEIVED = 1 AND
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 31 as TP
              FROM ST_TOMILOLT_STAFFS a
              INNER JOIN ST_TOMILOLT c ON a.TOMILOLT_ID = c.ID,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(c.FROMDATE, 'YYYY-MM-DD') AND TO_DATE(c.TODATE, 'YYYY-MM-DD')
            )
            GROUP BY DT, STAFFS_ID
          ) b ON a.DT = b.DT AND a.ST_ID = b.STAFFS_ID
        ) a
        LEFT JOIN(
          SELECT TO_DATE('" + yr + @"-' || HOLMONTH || '-' || HOLDAY, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYOFFICIAL
          WHERE HOLMONTH IN(" + strMonthList + @")
          UNION ALL
          SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYUNOFFICIAL
          WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + strMonthList + @")
        ) b ON a.DT = b.DT
        GROUP BY a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END
      )
    )
    GROUP BY ST_ID
  ) a
  INNER JOIN ST_STBR c ON a.ST_ID = c.STAFFS_ID AND c.ISACTIVE = 1
  INNER JOIN ST_BRANCH d ON c.BRANCH_ID = d.ID AND d.ISACTIVE = 1
  INNER JOIN ST_BRANCH f ON d.FATHER_ID = f.ID AND f.ISACTIVE = 1
  INNER JOIN STN_POS g ON c.POS_ID = g.ID
  INNER JOIN ST_STAFFS h ON a.ST_ID = h.ID
  WHERE a.WORKDAY>0
  ORDER BY f.SORT, d.SORT, g.SORT, a.ST_ID 
) a";
                DataSet ds = myObj.OracleExecuteDataSet(strQry);
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string WorkingtimeTab3Desc(string year, string month, string month2, string stid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            string strMonthList = "";
            for (int i = Int32.Parse(month); i <= Int32.Parse(month2); i++)
            {
                if (strMonthList == "") strMonthList += i.ToString();
                else strMonthList += "," + i.ToString();
            }
            string[] date = new string[3];
            string[] time1 = new string[3];
            string[] time2 = new string[3];
            string strQry = @"SELECT  a.ST_ID, b.NEGJ, b.POSNAME, b.STNAME, a.TP, TO_CHAR(a.DT, 'YYYY-MM-DD') as DT, a.MINTM, a.MAXTM, a.ISWORK, 
      ROUND(IS_CHECK_TSAG(TO_DATE(TO_CHAR(a.DT,'YYYY-MM-DD')||' '||a.MINTM,'YYYY-MM-DD HH24:MI;SS'), TO_DATE(TO_CHAR(a.DT,'YYYY-MM-DD')||' '||a.MAXTM,'YYYY-MM-DD HH24:MI;SS'), 'hotsorson', 'minute'),1) as HOTSORSONMINUTE, 
      ROUND(IS_CHECK_TSAG(TO_DATE(TO_CHAR(a.DT,'YYYY-MM-DD')||' '||a.MINTM,'YYYY-MM-DD HH24:MI;SS'), TO_DATE(TO_CHAR(a.DT,'YYYY-MM-DD')||' '||a.MAXTM,'YYYY-MM-DD HH24:MI;SS'), 'ert', 'minute'),1) as ERTMINUTE
    FROM (
      SELECT a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END as ISWORK
        FROM(
          SELECT a.ST_ID, NVL(b.TP, 0) as TP, a.DT, a.MINTM, a.MAXTM, a.ISWORK
          FROM(
            SELECT a.ST_ID, a.DT, NVL(b.TP, 0) as TP, NVL(b.MINTM, '00:00:00') as MINTM, NVL(b.MAXTM, '00:00:00') as MAXTM, a.ISWORK
            FROM(
              SELECT a.STAFFS_ID as ST_ID, a.DT, CASE WHEN b.DT IS NULL THEN a.ISWORK ELSE 1 END as ISWORK
              FROM ( 
                SELECT a.ID, b.STAFFS_ID, c.FATHER_ID as GAZAR_ID, c.ID as HELTES_ID, a.DT, CASE WHEN(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 6 OR(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 7 THEN 0 ELSE 1 END as ISWORK
                FROM (
                  SELECT a.ID, a.STAFFS_ID, a.BEGINDT, a.ENDDT, b.DT
                  FROM (
                    SELECT a.ID, a.STAFFS_ID, 
                      CASE 
                        WHEN TO_DATE(a.DT,'YYYY-MM-DD')>TO_DATE('" + year + "-" + month + @"-01','YYYY-MM-DD') 
                        THEN a.DT 
                        ELSE '" + year + "-" + month + @"-01' 
                      END as BEGINDT, 
                      CASE 
                        --WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        --THEN CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                        WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        THEN CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                    FROM ST_STBR a
                    INNER JOIN ST_BRANCH b ON a.BRANCH_ID=b.ID AND b.ISACTIVE=1
                    INNER JOIN STN_MOVE c ON a.MOVE_ID=c.ID
                    WHERE a.POS_ID!=2020102 AND c.ACTIVE=1 AND a.STAFFS_ID=" + stid + @"
                    AND ((
                        TO_DATE('" + year + "-" + month + @"-01','YYYY-MM-DD') 
                        BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                        OR 
                        LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                      ) OR 
                        (
                          TO_DATE('" + year + "-" + month + @"-01','YYYY-MM-DD')<TO_DATE(a.DT,'YYYY-MM-DD') AND LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01','YYYY-MM-DD'))>TO_DATE(a.DT,'YYYY-MM-DD')
                        ))
                  ) a, (
                    SELECT DT
                    FROM(
                      SELECT(TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                      FROM DUAL
                      CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd')
                    )
                    WHERE DT BETWEEN TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(DT, 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
                  ) b
                ) a
                INNER JOIN ST_STBR b ON a.ID=b.ID
                INNER JOIN ST_BRANCH c ON b.BRANCH_ID=c.ID AND c.ISACTIVE=1
                WHERE a.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD') AND b.POS_ID!=2020102
              ) a
              LEFT JOIN(
                SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT FROM ST_HOLIDAYISWORK WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + year + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + strMonthList + @")
              ) b ON a.DT = b.DT  
            ) a
            LEFT JOIN(
              SELECT a.MONTH, a.INOUT as TP, b.ID as ST_ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') as DT, TO_CHAR(MIN(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MINTM, TO_CHAR(MAX(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MAXTM
              FROM hr_mof.STN_TRCDLOG a
              INNER JOIN ST_STAFFS b ON a.ENO = b.FINGERID
              INNER JOIN ST_STBR c ON b.ID = c.STAFFS_ID
              INNER JOIN STN_MOVE d ON c.MOVE_ID = d.ID
              INNER JOIN ST_BRANCH f ON c.BRANCH_ID = f.ID AND f.ISACTIVE = 1
              WHERE c.POS_ID!=2020102 AND a.INOUT = 0 AND a.YEAR = " + year + @" AND a.MONTH IN(" + strMonthList + @") AND TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') BETWEEN TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd'), 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
              GROUP BY a.MONTH, a.INOUT, b.ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd')
            ) b ON a.ST_ID = b.ST_ID AND a.DT = b.DT  
          ) a
          LEFT JOIN(
            SELECT DT, STAFFS_ID, MAX(TP) as TP
            FROM(
              SELECT b.DT, a.STAFFS_ID, 11 as TP
              FROM ST_CHULUUDAYF3 a,
              (
                SELECT(TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 12 as TP
              FROM ST_CHULUUDAYT2 a,
              (
                SELECT(TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                a.ISRECEIVED = 1 AND
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 13 as TP
              FROM ST_CHULUUSICK a,
              (
                SELECT(TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 21 as TP
              FROM ST_AMRALT a,
              (
                SELECT(TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                a.TZISRECEIVED = 1 AND
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 31 as TP
              FROM ST_TOMILOLT_STAFFS a
              INNER JOIN ST_TOMILOLT c ON a.TOMILOLT_ID = c.ID,
              (
                SELECT(TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + year + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + year + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(c.FROMDATE, 'YYYY-MM-DD') AND TO_DATE(c.TODATE, 'YYYY-MM-DD')
            )
            GROUP BY DT, STAFFS_ID
          ) b ON a.DT = b.DT AND a.ST_ID = b.STAFFS_ID
        ) a
        LEFT JOIN(
          SELECT TO_DATE('" + year + @"-' || HOLMONTH || '-' || HOLDAY, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYOFFICIAL
          WHERE HOLMONTH IN(" + strMonthList + @")
          UNION ALL
          SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYUNOFFICIAL
          WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + year + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + strMonthList + @")
        ) b ON a.DT = b.DT
        GROUP BY a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END
      ORDER BY a.DT
    ) a
    LEFT JOIN (
      SELECT 
        a.ID, 
        CASE WHEN d.ID=d.FATHER_ID THEN d.INITNAME ELSE f.INITNAME||'-'||d.INITNAME END as NEGJ, 
        g.NAME as POSNAME, 
        SUBSTR(a.LNAME, 0, 1) || '.' || SUBSTR(a.FNAME, 1, 1) || LOWER(SUBSTR(a.FNAME, 2)) as STNAME, 
        d.SORT as NEGJSORT,
        g.SORT as POSSORT
      FROM ST_STAFFS a 
      INNER JOIN ST_STBR b ON a.ID=b.STAFFS_ID AND b.ISACTIVE=1 
      INNER JOIN STN_MOVE c ON b.MOVE_ID=c.ID AND c.ACTIVE=1 
      INNER JOIN ST_BRANCH d ON b.BRANCH_ID=d.ID 
      INNER JOIN ST_BRANCH f ON d.FATHER_ID=f.ID 
      INNER JOIN STN_POS g ON b.POS_ID=g.ID 
    ) b ON a.ST_ID=b.ID
    ORDER BY b.NEGJSORT, b.POSSORT";
            try
            {
                DataSet ds = myObj.OracleExecuteDataSet(strQry);
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string WorkingtimeTab4Datatable(string yr, string month)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string strQry = "";
                strQry = @"SELECT ROWNUM as ROWNO, a.*
FROM (
    SELECT 
    b.NAME as BR_NAME, a.WORKDAY, ROUND(a.WORKDAY-(a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM),2) as EVALWORKEDDAY, ROUND(((a.WORKDAY - (a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)) / a.WORKDAY) * 100, 2) as PER, ROUND((ROUND(((a.WORKDAY - (a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)) / a.WORKDAY) * 100, 2) * 0.1),2) as PNT, b.SORT
    FROM (  
        SELECT FATHER_ID as GAZAR_ID,
          SUM(ISWORK) as WORKDAY,
          SUM(CHOLOODAY) as CHOLOODAYSUM,
          SUM(UWCHTEIDAY) as UWCHTEIDAYSUM,
          SUM(AMRALTDAY) as AMRALTDAYSUM,
          SUM(TOMILOLTDAY) as TOMILOLTDAYSUM,
          SUM(ISTASALSAN) as TASALSANDAYSUM,
          SUM(HOTSORSONMINUTE) as HOTSORSONMINUTESUM,
          SUM(HOTSORSONDAY) as HOTSORSONDAYSUM,
          SUM(ERTMINUTE) as ERTMINUTESUM,
          SUM(ERTDAY) as ERTDAYSUM,
          SUM(OROIMINUTE) as OROIMINUTESUM,
          SUM(OROIDAY) as OROIDAYSUM
        FROM(
          SELECT FATHER_ID, ST_ID, TP, DT, MINTM, MAXTM, ISWORK,
            CASE WHEN TP = 11 AND ISWORK = 1 OR TP = 12 THEN 1 ELSE 0 END as CHOLOODAY,
            CASE WHEN TP = 13 AND ISWORK = 1 THEN 1 ELSE 0 END as UWCHTEIDAY,
            CASE WHEN TP = 21 AND ISWORK = 1 THEN 1 ELSE 0 END as AMRALTDAY,
            CASE WHEN TP = 31 AND ISWORK = 1 THEN 1 ELSE 0 END as TOMILOLTDAY,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM = MAXTM THEN 1 ELSE 0 END as ISTASALSAN,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'hotsorson', 'minute') ELSE 0 END as HOTSORSONMINUTE,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'hotsorson', 'day') ELSE 0 END as HOTSORSONDAY,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'ert', 'minute') ELSE 0 END as ERTMINUTE,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'ert', 'day') ELSE 0 END as ERTDAY,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'oroi', 'minute') ELSE 0 END as OROIMINUTE,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'oroi', 'day') ELSE 0 END as OROIDAY
          FROM(
            SELECT a.FATHER_ID, a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END as ISWORK
            FROM(
              SELECT a.FATHER_ID, a.ST_ID, NVL(b.TP, 0) as TP, a.DT, a.MINTM, a.MAXTM, a.ISWORK
              FROM(
                SELECT a.FATHER_ID, a.ST_ID, a.DT, NVL(b.TP, 0) as TP, NVL(b.MINTM, '00:00:00') as MINTM, NVL(b.MAXTM, '00:00:00') as MAXTM, a.ISWORK
                FROM(
                  SELECT a.FATHER_ID, a.STAFFS_ID as ST_ID, a.DT, CASE WHEN b.DT IS NULL THEN a.ISWORK ELSE 1 END as ISWORK
                  FROM ( 
                    SELECT a.ID, c.FATHER_ID, b.STAFFS_ID, c.FATHER_ID as GAZAR_ID, c.ID as HELTES_ID, a.DT, CASE WHEN(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 6 OR(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 7 THEN 0 ELSE 1 END as ISWORK
                    FROM (
                      SELECT a.ID, a.BEGINDT, a.ENDDT, b.DT
                      FROM (
                        SELECT a.ID, 
                          CASE 
                            WHEN TO_DATE(a.DT,'YYYY-MM-DD')>TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD') 
                            THEN a.DT 
                            ELSE '" + yr + "-" + month + @"-01' 
                          END as BEGINDT, 
                          CASE 
                            --WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')) 
                            --THEN CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                            WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')) 
                            THEN CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                        FROM ST_STBR a
                        INNER JOIN ST_BRANCH b ON a.BRANCH_ID=b.ID AND b.ISACTIVE=1
                        INNER JOIN STN_MOVE c ON a.MOVE_ID=c.ID
                        WHERE a.POS_ID!=2020102 AND c.ACTIVE=1
                        AND ((
                            TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD') 
                            BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                            OR 
                            LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')) 
                            BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                          ) OR 
                            (
                              TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')<TO_DATE(a.DT,'YYYY-MM-DD') AND LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD'))>TO_DATE(a.DT,'YYYY-MM-DD')
                            ))
                      ) a, (
                        SELECT DT
                        FROM(
                          SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                          FROM DUAL
                          CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                        )
                        WHERE DT BETWEEN TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(DT, 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
                      ) b
                    ) a
                    INNER JOIN ST_STBR b ON a.ID=b.ID
                    INNER JOIN ST_BRANCH c ON b.BRANCH_ID=c.ID AND c.ISACTIVE=1
                    WHERE a.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD') AND b.POS_ID!=2020102
                  ) a
                  LEFT JOIN(
                    SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT FROM ST_HOLIDAYISWORK WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + month + @")
                  ) b ON a.DT = b.DT  
                ) a
                LEFT JOIN(
                  SELECT a.MONTH, a.INOUT as TP, b.ID as ST_ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') as DT, TO_CHAR(MIN(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MINTM, TO_CHAR(MAX(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MAXTM
                  FROM hr_mof.STN_TRCDLOG a
                  INNER JOIN ST_STAFFS b ON a.ENO = b.FINGERID
                  INNER JOIN ST_STBR c ON b.ID = c.STAFFS_ID
                  INNER JOIN STN_MOVE d ON c.MOVE_ID = d.ID
                  INNER JOIN ST_BRANCH f ON c.BRANCH_ID = f.ID AND f.ISACTIVE = 1
                  WHERE c.POS_ID!=2020102 AND a.INOUT = 0 AND a.YEAR = " + yr + @" AND a.MONTH IN(" + month + @") AND TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') BETWEEN TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd'), 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
                  GROUP BY a.MONTH, a.INOUT, b.ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd')
                ) b ON a.ST_ID = b.ST_ID AND a.DT = b.DT  
              ) a
              LEFT JOIN(
                SELECT DT, STAFFS_ID, MAX(TP) as TP
                FROM(
                  SELECT b.DT, a.STAFFS_ID, 11 as TP
                  FROM ST_CHULUUDAYF3 a,
                  (
                    SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
                  UNION ALL
                  SELECT b.DT, a.STAFFS_ID, 12 as TP
                  FROM ST_CHULUUDAYT2 a,
                  (
                    SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    a.ISRECEIVED = 1 AND
                    b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
                  UNION ALL
                  SELECT b.DT, a.STAFFS_ID, 13 as TP
                  FROM ST_CHULUUSICK a,
                  (
                    SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
                  UNION ALL
                  SELECT b.DT, a.STAFFS_ID, 21 as TP
                  FROM ST_AMRALT a,
                  (
                    SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    a.TZISRECEIVED = 1 AND
                    b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
                  UNION ALL
                  SELECT b.DT, a.STAFFS_ID, 31 as TP
                  FROM ST_TOMILOLT_STAFFS a
                  INNER JOIN ST_TOMILOLT c ON a.TOMILOLT_ID = c.ID,
                  (
                    SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    b.DT BETWEEN TO_DATE(c.FROMDATE, 'YYYY-MM-DD') AND TO_DATE(c.TODATE, 'YYYY-MM-DD')
                )
                GROUP BY DT, STAFFS_ID
              ) b ON a.DT = b.DT AND a.ST_ID = b.STAFFS_ID
            ) a
            LEFT JOIN(
              SELECT TO_DATE('" + yr + @"-' || HOLMONTH || '-' || HOLDAY, 'YYYY-MM-DD') as DT
              FROM ST_HOLIDAYOFFICIAL
              WHERE HOLMONTH IN(" + month + @")
              UNION ALL
              SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT
              FROM ST_HOLIDAYUNOFFICIAL
              WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + month + @")
            ) b ON a.DT = b.DT
            GROUP BY a.FATHER_ID, a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END
          )
        )
        GROUP BY FATHER_ID
    ) a
    INNER JOIN ST_BRANCH b ON a.GAZAR_ID=b.ID AND b.ISACTIVE=1
    ORDER BY b.SORT
) a";
                DataSet ds = myObj.OracleExecuteDataSet(strQry);
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string WorkingtimeTab4t2Datatable(string yr, string month, string month2, string gazar, string heltes, string stid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            string strMonthList = "";
            for (int i = Int32.Parse(month); i <= Int32.Parse(month2); i++)
            {
                if (strMonthList == "") strMonthList += i.ToString();
                else strMonthList += "," + i.ToString();
            }
            if (gazar != "") gazar = " AND c.FATHER_ID=" + gazar;
            if (heltes != "") heltes = " AND c.ID=" + heltes;
            if (stid != "") stid = " AND b.STAFFS_ID=" + stid;
            string strTableVal = "";
            string strQry = "";
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string strColor1 = "\"background:#fcf8e3;\"";
                strQry = @"SELECT a.ST_ID
        , CASE WHEN b.ID=b.FATHER_ID THEN b.INITNAME ELSE c.INITNAME||'-'||b.INITNAME END as NEGJ
        , d.NAME as POSNAME
        , SUBSTR(f.LNAME,0,1)||'.'||SUBSTR(f.FNAME,1,1)||LOWER(SUBSTR(f.FNAME,2)) as STNAME
        , a.TP
        , TO_CHAR(a.DT,'YYYY-MM-DD') as DT
        , a.MINTM
        , a.MAXTM
        , CASE WHEN a.ISWORK=0 THEN 'style=" + strColor1 + @"' ELSE null END as ISWORK
      FROM(
        SELECT a.BRANCH_ID, a.POS_ID, a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END as ISWORK
        FROM(
          SELECT a.BRANCH_ID, a.POS_ID, a.ST_ID, NVL(b.TP, 0) as TP, a.DT, a.MINTM, a.MAXTM, a.ISWORK
          FROM(
            SELECT a.BRANCH_ID, a.POS_ID, a.ST_ID, a.DT, NVL(b.TP, 0) as TP, NVL(b.MINTM, '00:00:00') as MINTM, NVL(b.MAXTM, '00:00:00') as MAXTM, a.ISWORK
            FROM(
              SELECT a.BRANCH_ID, a.POS_ID, a.STAFFS_ID as ST_ID, a.DT, CASE WHEN b.DT IS NULL THEN a.ISWORK ELSE 1 END as ISWORK
              FROM(
                SELECT a.ID, b.BRANCH_ID, b.POS_ID, b.STAFFS_ID, c.FATHER_ID as GAZAR_ID, c.ID as HELTES_ID, a.DT, CASE WHEN(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 6 OR(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 7 THEN 0 ELSE 1 END as ISWORK
                FROM(
                  SELECT a.ID, a.BEGINDT, a.ENDDT, b.DT
                  FROM(
                    SELECT a.ID,
                      CASE
                        WHEN TO_DATE(a.DT, 'YYYY-MM-DD') > TO_DATE('" + yr + "-" + month + @"-01', 'YYYY-MM-DD')
                        THEN a.DT
                        ELSE '" + yr + "-" + month + @"-01'
                      END as BEGINDT,
                      CASE
                        --WHEN TO_DATE(CASE WHEN c.ACTIVE = 0 THEN NVL(a.ENDDT, a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE, 'YYYY-MM-DD')) END, 'YYYY-MM-DD') < LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'YYYY-MM-DD'))
                        --THEN CASE WHEN c.ACTIVE = 0 THEN NVL(a.ENDDT, a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE, 'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'YYYY-MM-DD')), 'YYYY-MM-DD') END as ENDDT
                        WHEN TO_DATE(CASE WHEN c.ACTIVE = 0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT, a.DT), 'YYYY-MM-DD') - 1), 'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT, 'YYYY-MM-DD') - 1), 'YYYY-MM-DD'), TO_CHAR(SYSDATE, 'YYYY-MM-DD')) END, 'YYYY-MM-DD') < LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'YYYY-MM-DD'))
                        THEN CASE WHEN c.ACTIVE = 0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT, a.DT), 'YYYY-MM-DD') - 1), 'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT, 'YYYY-MM-DD') - 1), 'YYYY-MM-DD'), TO_CHAR(SYSDATE, 'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'YYYY-MM-DD')), 'YYYY-MM-DD') END as ENDDT
                    FROM ST_STBR a
                    INNER JOIN ST_BRANCH b ON a.BRANCH_ID = b.ID AND b.ISACTIVE = 1
                    INNER JOIN STN_MOVE c ON a.MOVE_ID = c.ID
                    WHERE a.POS_ID != 2020102 AND c.ACTIVE=1
                    AND ((
                        TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD') 
                        BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                        OR 
                        LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD')) 
                        BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                      ) OR 
                        (
                          TO_DATE('" + yr + "-" + month + @"-01','YYYY-MM-DD')<TO_DATE(a.DT,'YYYY-MM-DD') AND LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01','YYYY-MM-DD'))>TO_DATE(a.DT,'YYYY-MM-DD')
                        ))
                  ) a, (
                    SELECT DT
                    FROM(
                      SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                      FROM DUAL
                      CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
                    )
                    WHERE DT BETWEEN TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(DT, 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
                  ) b
                ) a
                INNER JOIN ST_STBR b ON a.ID = b.ID
                INNER JOIN ST_BRANCH c ON b.BRANCH_ID = c.ID AND c.ISACTIVE = 1
                WHERE a.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD') AND b.POS_ID != 2020102" + gazar + heltes + stid + @"
              ) a
              LEFT JOIN(
                SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT FROM ST_HOLIDAYISWORK WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + strMonthList + @")
              ) b ON a.DT = b.DT
            ) a
            LEFT JOIN(
              SELECT a.MONTH, a.INOUT as TP, b.ID as ST_ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') as DT, TO_CHAR(MIN(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MINTM, TO_CHAR(MAX(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MAXTM
              FROM hr_mof.STN_TRCDLOG a
              INNER JOIN ST_STAFFS b ON a.ENO = b.FINGERID
              INNER JOIN ST_STBR c ON b.ID = c.STAFFS_ID
              INNER JOIN STN_MOVE d ON c.MOVE_ID = d.ID
              INNER JOIN ST_BRANCH f ON c.BRANCH_ID = f.ID AND f.ISACTIVE = 1
              WHERE c.POS_ID != 2020102 AND a.INOUT = 0 AND a.YEAR = " + yr + @" AND a.MONTH IN(" + strMonthList + @") AND TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') BETWEEN TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd'), 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
              GROUP BY a.MONTH, a.INOUT, b.ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd')
            ) b ON a.ST_ID = b.ST_ID AND a.DT = b.DT
          ) a
          LEFT JOIN(
            SELECT DT, STAFFS_ID, MAX(TP) as TP
            FROM(
              SELECT b.DT, a.STAFFS_ID, 11 as TP
              FROM ST_CHULUUDAYF3 a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 12 as TP
              FROM ST_CHULUUDAYT2 a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                a.ISRECEIVED = 1 AND
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 13 as TP
              FROM ST_CHULUUSICK a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 21 as TP
              FROM ST_AMRALT a,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                a.TZISRECEIVED = 1 AND
                b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
              UNION ALL
              SELECT b.DT, a.STAFFS_ID, 31 as TP
              FROM ST_TOMILOLT_STAFFS a
              INNER JOIN ST_TOMILOLT c ON a.TOMILOLT_ID = c.ID,
              (
                SELECT(TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                FROM DUAL
                CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + "-" + month2 + @"-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + "-" + month + @"-01', 'yyyy-mm-dd')
              ) b
              WHERE
                b.DT BETWEEN TO_DATE(c.FROMDATE, 'YYYY-MM-DD') AND TO_DATE(c.TODATE, 'YYYY-MM-DD')
            )
            GROUP BY DT, STAFFS_ID
          ) b ON a.DT = b.DT AND a.ST_ID = b.STAFFS_ID
        ) a
        LEFT JOIN(
          SELECT TO_DATE('" + yr + @"-' || HOLMONTH || '-' || HOLDAY, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYOFFICIAL
          WHERE HOLMONTH IN(" + strMonthList + @")
          UNION ALL
          SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT
          FROM ST_HOLIDAYUNOFFICIAL
          WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(" + strMonthList + @")
        ) b ON a.DT = b.DT
        GROUP BY a.BRANCH_ID, a.POS_ID, a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END
      ) a
      INNER JOIN ST_BRANCH b ON a.BRANCH_ID = b.ID AND b.ISACTIVE = 1
      INNER JOIN ST_BRANCH c ON b.FATHER_ID = c.ID AND c.ISACTIVE = 1
      INNER JOIN STN_POS d ON a.POS_ID = d.ID
      INNER JOIN ST_STAFFS f ON a.ST_ID = f.ID
      ORDER BY c.SORT, b.SORT, d.SORT, a.ST_ID, a.DT";
                DataSet ds = myObj.OracleExecuteDataSet(strQry);
                strTableVal += "<table style=\"border: 1px solid #000; border-collapse: collapse; font: 11pt arial, sans-serif; width: 100%;\">";
                strTableVal += "<thead style=\"background-color:C6D9F1;\">";
                strTableVal += "<tr>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Нэгж</th>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Нэр</th>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Албан тушаал</th>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Огноо</th>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Төлөв</th>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Ирсэн</th>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Явсан</th>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Хоцорсон<br>(минут)</th>";
                strTableVal += "<th style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">Эрт явсан<br>(минут)</th>";
                strTableVal += "</tr>";
                strTableVal += "</thead>";
                strTableVal += "<tbody>";
                string[] date = new string[3];
                string[] time1 = new string[3];
                string[] time2 = new string[3];
                if (ds.Tables[0].Rows.Count != 0)
                {
                    foreach (DataRow dr in ds.Tables[0].Rows)
                    {
                        Array.Clear(date, 0, date.Length);
                        date = new string[3];
                        date = Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd").Split('-');

                        strTableVal += "<tr " + dr["ISWORK"].ToString() + ">";
                        if (dr["ISWORK"].ToString() == "style=\"background:#fcf8e3;\"")
                        {
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-blueDark\">Амралтын өдөр</span></td>";
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + dr["MINTM"].ToString().Replace("00:00:00", "--:--:--") + "</td>";
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + dr["MAXTM"].ToString().Replace("00:00:00", "--:--:--") + "</td>";
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                        }
                        else {
                            if (dr["TP"].ToString() == "0")
                            {
                                Array.Clear(time1, 0, time1.Length);
                                time1 = new string[3];
                                time1 = dr["MINTM"].ToString().Split(':');
                                Array.Clear(time2, 0, time2.Length);
                                time2 = new string[3];
                                time2 = dr["MAXTM"].ToString().Split(':');
                                DateTime come = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2]), Int32.Parse(time1[0]), Int32.Parse(time1[1]), Int32.Parse(time1[2]));
                                DateTime go = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2]), Int32.Parse(time2[0]), Int32.Parse(time2[1]), Int32.Parse(time2[2]));
                                DateTime late = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2]), 08, 40, 00);
                                DateTime early = new DateTime(Int32.Parse(date[0]), Int32.Parse(date[1]), Int32.Parse(date[2]), 17, 30, 00);
                                if (dr["MINTM"].ToString() != dr["MAXTM"].ToString())
                                {
                                    var lateTimeSpanHour = TimeSpan.FromHours(Convert.ToDouble(come.Subtract(late).Hours));
                                    var earlyTimeSpanHour = TimeSpan.FromHours(Convert.ToDouble(early.Subtract(go).Hours));
                                    var lateTimeSpanMin = TimeSpan.FromMinutes(Convert.ToDouble(come.Subtract(late).Minutes));
                                    var earlyTimeSpanMin = TimeSpan.FromMinutes(Convert.ToDouble(early.Subtract(go).Minutes));
                                    var lateTimeSpanSec = TimeSpan.FromSeconds(Convert.ToDouble(come.Subtract(late).Seconds));
                                    var earlyTimeSpanSec = TimeSpan.FromSeconds(Convert.ToDouble(early.Subtract(go).Seconds));
                                    TimeSpan lateTimeSpan = lateTimeSpanHour + lateTimeSpanMin + lateTimeSpanSec;
                                    TimeSpan earlyTimeSpan = earlyTimeSpanHour + earlyTimeSpanMin + earlyTimeSpanSec;

                                    if (Convert.ToDouble(come.Subtract(late).Seconds) > 0)
                                    {
                                        if (Convert.ToDouble(come.Subtract(early).Seconds) > 0)
                                        {
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-yellow\">Хоцорсон</span></td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + come.ToString("HH:mm:ss") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + go.ToString("HH:mm:ss") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + lateTimeSpan.ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + earlyTimeSpan.ToString() + "</td>";
                                        }
                                        else
                                        {
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-yellow\">Хоцорсон</span></td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + come.ToString("HH:mm:ss") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + go.ToString("HH:mm:ss") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + lateTimeSpan.ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                        }
                                    }
                                    else
                                    {
                                        if (Convert.ToDouble(come.Subtract(early).Seconds) > 0)
                                        {
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-blueLight\">Цагтаа ирсэн</span></td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + come.ToString("HH:mm:ss") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + go.ToString("HH:mm:ss") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + earlyTimeSpan.ToString() + "</td>";
                                        }
                                        else
                                        {
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-blueLight\">Цагтаа ирсэн</span></td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + come.ToString("HH:mm:ss") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + go.ToString("HH:mm:ss") + "</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                            strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                        }
                                    }
                                }
                                else
                                {
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-redLight\">Тасалсан</span></td>";
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">" + come.ToString("HH:mm:ss").Replace("00:00:00", "--:--:--") + "</td>";
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                    strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                }
                            }
                            else if (dr["TP"].ToString() == "13")
                            {
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-pink\">Өвчтэй</span></td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                            }
                            else if (dr["TP"].ToString() == "21")
                            {
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-greenDark\">Ээлжийн амралттай</span></td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                            }
                            else if (dr["TP"].ToString() == "11" || dr["TP"].ToString() == "12")
                            {
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-orangeDark\">Чөлөөтэй</span></td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                            }
                            else if (dr["TP"].ToString() == "31")
                            {
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-blue\">Албан томилолт</span></td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                            }
                            else if (dr["TP"].ToString() == "91")
                            {
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["NEGJ"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["STNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + dr["POSNAME"].ToString() + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\">" + Convert.ToDateTime(dr["DT"]).ToString("yyyy-MM-dd") + "</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:center;\"><span class=\"label bg-color-lighten\">Тэмдэглэлт өдөр</span></td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                                strTableVal += "<td style=\"border: 1px solid #DDD; padding:5px; text-align:right;\">--:--:--</td>";
                            }
                        }
                        strTableVal += "</tr>";
                    }
                }
                strTableVal += "</tbody>";
                strTableVal += "</table>";
                return strTableVal;
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }
        public string WorkingtimeTab4t3Datatable(string yr)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string strQry = @"SELECT * FROM (
    SELECT 
      b.NAME as GAZAR_NAME, a.MNTH, ROUND(((a.WORKDAY - (a.TASALSANDAYSUM + a.HOTSORSONDAYSUM + a.ERTDAYSUM)) / a.WORKDAY) * 100, 2) as PER, b.SORT
    FROM (
      SELECT FATHER_ID as GAZAR_ID, TO_NUMBER(TO_CHAR(DT,'MM')) as MNTH, 
          SUM(ISWORK) as WORKDAY,
          SUM(CHOLOODAY) as CHOLOODAYSUM,
          SUM(UWCHTEIDAY) as UWCHTEIDAYSUM,
          SUM(AMRALTDAY) as AMRALTDAYSUM,
          SUM(TOMILOLTDAY) as TOMILOLTDAYSUM,
          SUM(ISTASALSAN) as TASALSANDAYSUM,
          SUM(HOTSORSONMINUTE) as HOTSORSONMINUTESUM,
          SUM(HOTSORSONDAY) as HOTSORSONDAYSUM,
          SUM(ERTMINUTE) as ERTMINUTESUM,
          SUM(ERTDAY) as ERTDAYSUM,
          SUM(OROIMINUTE) as OROIMINUTESUM,
          SUM(OROIDAY) as OROIDAYSUM
        FROM(
          SELECT FATHER_ID, ST_ID, TP, DT, MINTM, MAXTM, ISWORK,
            CASE WHEN TP = 11 AND ISWORK = 1 OR TP = 12 THEN 1 ELSE 0 END as CHOLOODAY,
            CASE WHEN TP = 13 AND ISWORK = 1 THEN 1 ELSE 0 END as UWCHTEIDAY,
            CASE WHEN TP = 21 AND ISWORK = 1 THEN 1 ELSE 0 END as AMRALTDAY,
            CASE WHEN TP = 31 AND ISWORK = 1 THEN 1 ELSE 0 END as TOMILOLTDAY,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM = MAXTM THEN 1 ELSE 0 END as ISTASALSAN,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'hotsorson', 'minute') ELSE 0 END as HOTSORSONMINUTE,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'hotsorson', 'day') ELSE 0 END as HOTSORSONDAY,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'ert', 'minute') ELSE 0 END as ERTMINUTE,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'ert', 'day') ELSE 0 END as ERTDAY,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'oroi', 'minute') ELSE 0 END as OROIMINUTE,
            CASE WHEN TP = 0 AND ISWORK = 1 AND MINTM != MAXTM THEN IS_CHECK_TSAG(TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MINTM, 'yyyy-mm-dd hh24:mi:ss'), TO_DATE(TO_CHAR(DT, 'yyyy-mm-dd') || ' ' || MAXTM, 'yyyy-mm-dd hh24:mi:ss'), 'oroi', 'day') ELSE 0 END as OROIDAY
          FROM(
            SELECT a.FATHER_ID, a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END as ISWORK
            FROM(
              SELECT a.FATHER_ID, a.ST_ID, NVL(b.TP, 0) as TP, a.DT, a.MINTM, a.MAXTM, a.ISWORK
              FROM(
                SELECT a.FATHER_ID, a.ST_ID, a.DT, NVL(b.TP, 0) as TP, NVL(b.MINTM, '00:00:00') as MINTM, NVL(b.MAXTM, '00:00:00') as MAXTM, a.ISWORK
                FROM(
                  SELECT a.FATHER_ID, a.STAFFS_ID as ST_ID, a.DT, CASE WHEN b.DT IS NULL THEN a.ISWORK ELSE 1 END as ISWORK
                  FROM ( 
                    SELECT a.ID, c.FATHER_ID, b.STAFFS_ID, c.FATHER_ID as GAZAR_ID, c.ID as HELTES_ID, a.DT, CASE WHEN(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 6 OR(MOD(TO_CHAR(a.DT, 'J'), 7) + 1) = 7 THEN 0 ELSE 1 END as ISWORK
                    FROM (
                      SELECT a.ID, a.BEGINDT, a.ENDDT, b.DT
                      FROM (
                        SELECT a.ID, 
                          CASE 
                            WHEN TO_DATE(a.DT,'YYYY-MM-DD')>TO_DATE('" + yr + @"-01-01','YYYY-MM-DD') 
                            THEN a.DT 
                            ELSE '" + yr + @"-01-01' 
                          END as BEGINDT, 
                          CASE 
                            --WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + yr + @"-12-01','YYYY-MM-DD')) 
                            --THEN CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + @"-12-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                            WHEN TO_DATE(CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')<LAST_DAY(TO_DATE('" + yr + @"-12-01','YYYY-MM-DD')) 
                            THEN CASE WHEN c.ACTIVE=0 THEN TO_CHAR((TO_DATE(NVL(a.ENDDT,a.DT),'YYYY-MM-DD')-1),'YYYY-MM-DD') ELSE NVL(TO_CHAR((TO_DATE(a.ENDDT,'YYYY-MM-DD')-1),'YYYY-MM-DD'), TO_CHAR(SYSDATE,'YYYY-MM-DD')) END ELSE TO_CHAR(LAST_DAY(TO_DATE('" + yr + @"-12-01','YYYY-MM-DD')),'YYYY-MM-DD') END as ENDDT
                        FROM ST_STBR a
                        INNER JOIN ST_BRANCH b ON a.BRANCH_ID=b.ID AND b.ISACTIVE=1
                        INNER JOIN STN_MOVE c ON a.MOVE_ID=c.ID
                        WHERE a.POS_ID!=2020102 AND c.ACTIVE=1
                        AND ((
                            TO_DATE('" + yr + @"-01-01','YYYY-MM-DD') 
                            BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                            OR 
                            LAST_DAY(TO_DATE('" + yr + @"-12-01','YYYY-MM-DD')) 
                            BETWEEN TO_DATE(a.DT,'YYYY-MM-DD') AND TO_DATE(CASE WHEN c.ACTIVE=0 THEN NVL(a.ENDDT,a.DT) ELSE NVL(a.ENDDT, TO_CHAR(SYSDATE,'YYYY-MM-DD')) END,'YYYY-MM-DD')
                          ) OR 
                            (
                              TO_DATE('" + yr + @"-01-01','YYYY-MM-DD')<TO_DATE(a.DT,'YYYY-MM-DD') AND LAST_DAY(TO_DATE('" + yr + @"-12-01','YYYY-MM-DD'))>TO_DATE(a.DT,'YYYY-MM-DD')
                            ))
                      ) a, (
                        SELECT DT
                        FROM(
                          SELECT(TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                          FROM DUAL
                          CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + @"-12-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd')
                        )
                        WHERE DT BETWEEN TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(DT, 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
                      ) b
                    ) a
                    INNER JOIN ST_STBR b ON a.ID=b.ID
                    INNER JOIN ST_BRANCH c ON b.BRANCH_ID=c.ID AND c.ISACTIVE=1
                    WHERE a.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD') AND b.POS_ID!=2020102
                  ) a
                  LEFT JOIN(
                    SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT FROM ST_HOLIDAYISWORK WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(1,2,3,4,5,6,7,8,9,10,11,12)
                  ) b ON a.DT = b.DT  
                ) a
                LEFT JOIN(
                  SELECT a.MONTH, a.INOUT as TP, b.ID as ST_ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') as DT, TO_CHAR(MIN(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MINTM, TO_CHAR(MAX(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY || ' ' || a.HOUR || ':' || a.MINUTE || ':' || a.SECOND, 'yyyy-mm-dd hh24:mi:ss')), 'hh24:mi:ss') as MAXTM
                  FROM hr_mof.STN_TRCDLOG a
                  INNER JOIN ST_STAFFS b ON a.ENO = b.FINGERID
                  INNER JOIN ST_STBR c ON b.ID = c.STAFFS_ID
                  INNER JOIN STN_MOVE d ON c.MOVE_ID = d.ID
                  INNER JOIN ST_BRANCH f ON c.BRANCH_ID = f.ID AND f.ISACTIVE = 1
                  WHERE c.POS_ID!=2020102 AND a.INOUT = 0 AND a.YEAR = " + yr + @" AND a.MONTH IN(1,2,3,4,5,6,7,8,9,10,11,12) AND TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd') BETWEEN TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') AND SYSDATE AND MOD(TO_CHAR(TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd'), 'J'), 7) + 1 IN(1, 2, 3, 4, 5, 6, 7)
                  GROUP BY a.MONTH, a.INOUT, b.ID, TO_DATE(a.YEAR || '-' || a.MONTH || '-' || a.DAY, 'yyyy-mm-dd')
                ) b ON a.ST_ID = b.ST_ID AND a.DT = b.DT  
              ) a
              LEFT JOIN(
                SELECT DT, STAFFS_ID, MAX(TP) as TP
                FROM(
                  SELECT b.DT, a.STAFFS_ID, 11 as TP
                  FROM ST_CHULUUDAYF3 a,
                  (
                    SELECT(TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + @"-12-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
                  UNION ALL
                  SELECT b.DT, a.STAFFS_ID, 12 as TP
                  FROM ST_CHULUUDAYT2 a,
                  (
                    SELECT(TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + @"-12-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    a.ISRECEIVED = 1 AND
                    b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
                  UNION ALL
                  SELECT b.DT, a.STAFFS_ID, 13 as TP
                  FROM ST_CHULUUSICK a,
                  (
                    SELECT(TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + @"-12-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
                  UNION ALL
                  SELECT b.DT, a.STAFFS_ID, 21 as TP
                  FROM ST_AMRALT a,
                  (
                    SELECT(TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + @"-12-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    a.TZISRECEIVED = 1 AND
                    b.DT BETWEEN TO_DATE(a.BEGINDT, 'YYYY-MM-DD') AND TO_DATE(a.ENDDT, 'YYYY-MM-DD')
                  UNION ALL
                  SELECT b.DT, a.STAFFS_ID, 31 as TP
                  FROM ST_TOMILOLT_STAFFS a
                  INNER JOIN ST_TOMILOLT c ON a.TOMILOLT_ID = c.ID,
                  (
                    SELECT(TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd') - 1) + ROWNUM DT
                    FROM DUAL
                    CONNECT BY LEVEL <= (LAST_DAY(TO_DATE('" + yr + @"-12-01', 'yyyy-mm-dd')) + 1) - TO_DATE('" + yr + @"-01-01', 'yyyy-mm-dd')
                  ) b
                  WHERE
                    b.DT BETWEEN TO_DATE(c.FROMDATE, 'YYYY-MM-DD') AND TO_DATE(c.TODATE, 'YYYY-MM-DD')
                )
                GROUP BY DT, STAFFS_ID
              ) b ON a.DT = b.DT AND a.ST_ID = b.STAFFS_ID
            ) a
            LEFT JOIN(
              SELECT TO_DATE('" + yr + @"-' || HOLMONTH || '-' || HOLDAY, 'YYYY-MM-DD') as DT
              FROM ST_HOLIDAYOFFICIAL
              WHERE HOLMONTH IN(1,2,3,4,5,6,7,8,9,10,11,12)
              UNION ALL
              SELECT TO_DATE(HOLDATE, 'YYYY-MM-DD') as DT
              FROM ST_HOLIDAYUNOFFICIAL
              WHERE TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'YYYY')) = " + yr + @" AND TO_NUMBER(TO_CHAR(TO_DATE(HOLDATE, 'YYYY-MM-DD'), 'MM')) IN(1,2,3,4,5,6,7,8,9,10,11,12)
            ) b ON a.DT = b.DT
            GROUP BY a.FATHER_ID, a.ST_ID, a.TP, a.DT, a.MINTM, a.MAXTM, CASE WHEN b.DT is null THEN a.ISWORK ELSE 0 END
          )
        )
        GROUP BY FATHER_ID, TO_NUMBER(TO_CHAR(DT,'MM'))
    ) a
    INNER JOIN ST_BRANCH b ON a.GAZAR_ID=b.ID AND b.ISACTIVE=1
  )
  pivot(
    AVG(PER) ";
                strQry += "FOR MNTH IN (1 as \"Jan\",2 as \"Feb\",3 as \"Mar\",4 as \"Apr\",5 as \"May\",6 as \"Jun\",7 as \"Jul\",8 as \"Aug\",9 as \"Sep\",10 as \"Oct\",11 as \"Nov\",12 as \"Dec\") ) ORDER BY SORT";
                DataSet ds = myObj.OracleExecuteDataSet(strQry);
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

        //*****rprt1.aspx*****//
        public string Rprt1Tab1Datatable(string gazar, string heltes, string stid)
        {
            ModifyDB myObj = new ModifyDB();
            GetTableData myObjGetTableData = new GetTableData();
            if (gazar != "") gazar = " AND f.FATHER_ID=" + gazar;
            if (heltes != "") heltes = " AND f.ID=" + heltes;
            if (stid != "") stid = " AND a.STAFFS_ID=" + stid;
            try
            {
                if (!myObj.checkUserSession()) throw new cs.HRMISException("SessionDied");
                string strQry = "";
                strQry = @"SELECT ROWNUM as ROWNO, a.* 
FROM (
  SELECT
  a.STAFFS_ID, 
  SUBSTR(c.LNAME,1,1)||LOWER(SUBSTR(c.LNAME,2))||' '||SUBSTR(c.FNAME,1,1)||LOWER(SUBSTR(c.FNAME,2)) as STAFFNAME,
  CASE WHEN f.ID=f.FATHER_ID THEN f.INITNAME ELSE g.INITNAME||' - '||f.INITNAME END as NEGJ,
  h.NAME as POSNAME,
  a.NAME, 
  a.SHAGNALDECISION_NAME, 
  a.DT, 
  a.TUSHAALNO, 
  a.PRICE, 
  a.CNT
FROM (
  SELECT
    a.STAFFS_ID, 
    a.NAME, 
    a.SHAGNALDECISION_NAME, 
    a.DT, 
    a.TUSHAALNO,
    a.PRICE, 
    b.CNT
  FROM (
    SELECT 
      b.STAFFS_ID, 
      a.NAME, 
      c.NAME as SHAGNALDECISION_NAME, 
      a.DT, 
      a.TUSHAALNO, 
      a.PRICE
    FROM ST_SHAGNAL a 
    INNER JOIN ST_SHAGNAL_STAFFS b ON a.ID=b.SHAGNAL_ID
    INNER JOIN STN_SHAGNALDECISION c ON a.SHAGNALDECISION_ID=c.ID
    UNION ALL
    SELECT 
      STAFFS_ID, NAME, null as SHAGNALDECISION_NAME, DT, null as TUSHAALNO, null as PRICE
    FROM ST_BONUS
  ) a
  LEFT JOIN (
    SELECT STAFFS_ID, SUM(CNT) as CNT
    FROM (
      SELECT 
        STAFFS_ID, COUNT(SHAGNAL_ID) as CNT
      FROM ST_SHAGNAL_STAFFS
      GROUP BY STAFFS_ID
      UNION ALL
      SELECT STAFFS_ID, COUNT(ID) as CNT
      FROM ST_BONUS
      GROUP BY STAFFS_ID
    ) 
    GROUP BY STAFFS_ID
  ) b ON a.STAFFS_ID=b.STAFFS_ID
) a
INNER JOIN ST_STAFFS c ON a.STAFFS_ID=c.ID
INNER JOIN ST_STBR d ON c.ID=d.STAFFS_ID AND d.ISACTIVE=1 
INNER JOIN STN_MOVE i ON d.MOVE_ID=i.ID AND i.ACTIVE=1
INNER JOIN ST_BRANCH f ON d.BRANCH_ID=f.ID AND d.ISACTIVE=1 
INNER JOIN ST_BRANCH g ON f.FATHER_ID=g.ID AND g.ISACTIVE=1 
LEFT JOIN STN_POS h ON d.POS_ID=h.ID 
WHERE 1=1" + gazar+heltes+stid+@"
ORDER BY g.SORT, f.SORT, h.SORT, c.ID, a.DT 
) a";
                DataSet ds = myObj.OracleExecuteDataSet(strQry);
                return myObjGetTableData.DataTableToJson(ds.Tables[0]);
            }
            catch (cs.HRMISException ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
            catch (Exception ex)
            {
                myObjGetTableData.exeptionMethod(ex);
                throw ex;
            }
        }

    }
}
