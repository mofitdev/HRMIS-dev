using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace HRWebApp.webservice
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the interface name "IServiceMain" in both code and config file together.
    [ServiceContract]
    public interface IServiceMain
    {
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string GazarListForDDL();
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string HeltesListForDDL(string gazarId);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string StaffListForDDL(string gazarId, string heltesId);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string OracleExecuteScalar(string qry);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void OracleExecuteNonQuery(string qry);

        //*****property.aspx*****//
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string PropertyTab1Datatable(string staffid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string PropertyTab1PropertyTypeListForDDL();
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string PropertyTab1UnitListForDDL();
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void PROPERTYUSE_INSERT(string P_RECEIVEDDT, string P_CODE, string P_PROPERTYTYPELIST_ID, string P_UNITLIST_ID, string P_UNITPRICE, string P_DESCRIPTION, string P_STAFFS_ID, string P_MOD_STAFFS_ID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void PROPERTYUSE_UPDATE(string P_PROPERTYLIST_ID, string P_RECEIVEDDT, string P_CODE, string P_PROPERTYTYPELIST_ID, string P_UNITLIST_ID, string P_UNITPRICE, string P_DESCRIPTION, string P_STAFFS_ID, string P_MOD_STAFFS_ID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string PropertyTab2Datatable();

        //*****srv.aspx*****//
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string SrvTab1Datatable(string tp, string staffid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string SrvTab1AnswerList(string questionid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string StaffListForSelect2(string selectedList);

        //*****amralt.aspx*****//
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string AmraltTab2t1Datatable(string yr, string gazarid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string AmraltTab1GetAmraltDays(string yr, string staffid);

        //*****staffsdataadd.aspx*****//
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string StaffsdataaddTab1Datatable(string fromdate, string todate, string decision, string type);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void SHAGNAL_INSERT(string P_NAME, string P_DT, string P_SHAGNALTYPE_ID, string P_SHAGNALDECISION_ID, string P_ORGDESCRIPTION, string P_PRICE, string P_GROUND, string P_TUSHAALNO, string P_TUSHAALDT, string P_FILENAME, string P_STAFFLIST, string P_STAFFID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void SHAGNAL_UPDATE(string P_ID, string P_NAME, string P_DT, string P_SHAGNALTYPE_ID, string P_SHAGNALDECISION_ID, string P_ORGDESCRIPTION, string P_PRICE, string P_GROUND, string P_TUSHAALNO, string P_TUSHAALDT, string P_FILENAME, string P_STAFFLIST, string P_STAFFID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void SHAGNAL_DELETE(string P_ID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string StaffsdataaddTab2Datatable(string fromdate, string todate, string posdegreedtl, string rankposdegree);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void ZEREGDEV_INSERT(string P_POSDEGREEDTL_ID, string P_RANKPOSDEGREE_ID, string P_DECISIONDESC, string P_DT, string P_CERTIFICATENO, string P_UPPER, string P_FILENAME, string P_STAFFLIST, string P_STAFFID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void ZEREGDEV_UPDATE(string P_ID, string P_POSDEGREEDTL_ID, string P_RANKPOSDEGREE_ID, string P_DECISIONDESC, string P_DT, string P_CERTIFICATENO, string P_UPPER, string P_FILENAME, string P_STAFFLIST, string P_STAFFID);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        void ZEREGDEV_DELETE(string P_ID);

        //*****tomilolt.aspx*****//
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string tomiloltTab1ModalSelectstafflistForSelect2(string selectedList);

        //*****profile.aspx*****//
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string ProfileTab1t9t1Datatable(string staffid);

        //*****workingtime.aspx*****//
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string WorkingtimeTab2Datatable(string yr, string month, string month2, string gazar, string heltes, string stid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string WorkingtimeTab3Datatable(string yr, string month, string month2, string gazar, string heltes, string stid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string WorkingtimeTab3Desc(string year, string month, string month2, string stid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string WorkingtimeTab4Datatable(string yr, string month);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string WorkingtimeTab4t2Datatable(string yr, string month, string month2, string gazar, string heltes, string stid);
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string WorkingtimeTab4t3Datatable(string yr);

        //*****rprt1.aspx*****//
        [OperationContract]
        [WebInvoke(Method = "POST", ResponseFormat = WebMessageFormat.Json)]
        string Rprt1Tab1Datatable(string gazar, string heltes, string stid);
    }
}
