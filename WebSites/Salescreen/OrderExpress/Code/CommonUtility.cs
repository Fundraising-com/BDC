using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.SessionState;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using System.Data.SqlClient;
using System.Text;
using System.Collections.Specialized;
using System.Web.Mail;
using System.Diagnostics;
using QSPForm.Common.DataDef;
using System.Text.RegularExpressions;
using System.Configuration;

namespace QSP.OrderExpress.Web.Code {
    /// <summary>
    /// Summary description for CommonUtility.
    /// </summary>
    public class CommonUtility {
        public CommonUtility() {
            //
            // TODO: Add constructor logic here
            //
        }

        public static void GenerateMouseBehavior(System.Web.UI.WebControls.WebControl wctl, string strPath, string strImageOn, string strImageOff) {
            wctl.Attributes.Add("onMouseOver", "this.src='" + strPath + strImageOn + "'");
            wctl.Attributes.Add("onMouseOut", "this.src='" + strPath + strImageOff + "'");
        }

        public static object iif(bool val, object valIfTrue, object valIfFalse) {
            if (val)
                return valIfTrue;
            return valIfFalse;
        }

        //public static void SetInitialFocus(Control control) 
        //{
        //    if (control.Page == null) 
        //    {
        //        throw new ArgumentException(
        //            "The Control must be added to a Page before you can set the IntialFocus to it.");
        //    }
        //    if (control.Page.Request.Browser.JavaScript == true) 
        //    {
        //        // Create JavaScript
        //        StringBuilder s = new StringBuilder();
        //        s.Append("\n<SCRIPT LANGUAGE='JavaScript'>\n");
        //        s.Append("<!--\n");
        //        s.Append("function SetInitialFocus()\n");
        //        s.Append("{\n");
        //        s.Append("   document.");

        //        // Find the Form
        //        Control p = control.Parent;
        //        while (!(p is System.Web.UI.HtmlControls.HtmlForm))
        //            p = p.Parent;
        //        s.Append(p.ClientID);

        //        s.Append("['");
        //        s.Append(control.UniqueID);

        //        // Set Focus on the selected item of a RadioButtonList
        //        RadioButtonList rbl = control as RadioButtonList;
        //        if (rbl != null) 
        //        {
        //            string suffix = "_0";
        //            int t = 0;
        //            foreach (ListItem li in rbl.Items) 
        //            {
        //                if (li.Selected) 
        //                {
        //                    suffix = "_" + t.ToString();
        //                    break;
        //                }
        //                t++;
        //            }
        //            s.Append(suffix);
        //        }

        //        // Set Focus on the first item of a CheckBoxList
        //        if (control is CheckBoxList) 
        //        {
        //            s.Append("_0");
        //        }

        //        s.Append("'].focus();\n");
        //        s.Append("}\n");

        //        if (control.Page.SmartNavigation)
        //            s.Append("window.setTimeout(SetInitialFocus, 500);\n");
        //        else
        //            s.Append("window.onload = SetInitialFocus;\n");

        //        s.Append("// -->\n");
        //        s.Append("</SCRIPT>");

        //        // Register Client Script
        //        control.Page.RegisterClientScriptBlock("InitialFocus", s.ToString());
        //    }
        //}

        public static int getInt(String st) {
            int temp = 0;
            try {
                temp = Convert.ToInt32(st);
            }
            catch { }
            return temp;
        }

        public static decimal getDecimal(String st) {
            decimal temp = 0;
            try {
                temp = Decimal.Parse(st);
            }
            catch { }
            return temp;
        }

        public static Boolean ConvertBoolean(int num) {

            Boolean temp = false;

            if (num == 1) { temp = true; }
            else temp = false;
            return temp;
        }

        public static Boolean ConvertBoolean(String num) {
            Boolean temp = false;
            if ((getInt(num)) == 1) { temp = true; }
            else temp = false;
            return temp;
        }

        public static String getString(String st) {
            if (st.Equals("null")) { return " "; }
            else
                return st;
        }

        public static String _getWebConfigValue(String strKey) {
            String temp = " ";
            try {
                System.Configuration.AppSettingsReader configurationAppSettings = new System.Configuration.AppSettingsReader();
                temp = ((string)(configurationAppSettings.GetValue(strKey, typeof(string))));
                //"ImportPath"
            }
            catch { }
            return temp;
        }

        public static NameValueCollection CreateNVCollection(String strCollection) {
            NameValueCollection strToCreate = new NameValueCollection();
            char chLineSep = ';';
            char chColSep = '=';

            String[] straColl_Line;
            straColl_Line = strCollection.Split(chLineSep);

            for (int i = 0; i < straColl_Line.Length; i++) {
                String[] straColl_Col;
                straColl_Col = straColl_Line[i].Split(chColSep);
                if (straColl_Col.Length > 1) {
                    strToCreate.Add(straColl_Col[0], straColl_Col[1]);
                }
                else {
                    strToCreate.Add(straColl_Col[0], "");
                }

            }
            return strToCreate;
        }

        public static void sendMailToSupport(System.Exception err, String ReqPath, String MoreInfo) {
            System.Exception err1;
            if (err.InnerException == null) {
                err1 = err;
            }
            else {
                err1 = err.InnerException;
            }
            String strBodyMessage = "\n\nURL:\n http://localhost/" + ReqPath + "\n\nMESSAGE:\n " + err1.Message + "\n\nSTACK TRACE:\n" + err1.StackTrace + MoreInfo;

            System.Web.Mail.MailMessage oMessage = new MailMessage();
            oMessage.From = CommonUtility._getWebConfigValue("FromCustomerServiceEmail");
            oMessage.To = CommonUtility._getWebConfigValue("ToCustomerServiceEmail");
            oMessage.Subject = "Application Error from QSPForm_Web";
            oMessage.Body = strBodyMessage;
            SmtpMail.SmtpServer = CommonUtility._getWebConfigValue("SMTP");
            System.Web.Mail.SmtpMail.Send(oMessage);
        }

        public static void WriteInEventViewerLog(System.Exception err, String ReqPath, String MoreInfo) {
            System.Exception err1;
            if (err.InnerException == null) {
                err1 = err;
            }
            else {
                err1 = err.InnerException;
            }
            String strMessage = "\n\nURL:\n http://localhost/" + ReqPath + "\n\nMESSAGE:\n " + err1.Message + "\n\nSTACK TRACE:\n" + err1.StackTrace + MoreInfo;

            //Create event log if it does not exist

            String LogName = "Application";
            String LogSource = "QSPForm_Web Web";
            if (!EventLog.SourceExists(LogName)) {
                EventLog.CreateEventSource(LogSource, LogName);
            }

            // Insert into event log
            EventLog Log = new EventLog();
            Log.Source = LogSource;
            Log.WriteEntry(strMessage, EventLogEntryType.Error);
        }

        public String setJavaScriptFctForMenuCmd(String sFctName, String sErrorMsg, String sNameHiddenToSet) {

            StringBuilder strBuild = new StringBuilder();
            if (sErrorMsg.Length == 0) {
                sErrorMsg = "Modification have been made, do you want to save it ??";
            }

            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("	function " + sFctName + "(Menu) {\n");
            strBuild.Append("		var oCtl				\n");
            strBuild.Append("		var find				\n");
            strBuild.Append("		var Resp				\n");
            strBuild.Append("		var IsChange			\n");
            strBuild.Append("		var IsValid				\n");
            strBuild.Append("		IsValid = false;		\n");
            strBuild.Append("		IsChange =false;		\n");
            strBuild.Append("								\n");
            strBuild.Append("		for (i=1; i < document.forms(0).elements.length; i++){	\n");
            strBuild.Append("			if (document.forms(0).elements[i].id.match(/" + sNameHiddenToSet + "/)) {\n");
            strBuild.Append("				oCtl = document.forms(0).elements[i];\n");
            strBuild.Append("				break;			\n");
            strBuild.Append("			}					\n");
            strBuild.Append("		}						\n");
            strBuild.Append("		//Assign MenuItem Value	\n");
            strBuild.Append("		if (oCtl != null) {\n");
            //strBuild.Append("			if (oCtl.value != Menu) {				\n");
            strBuild.Append("													\n");
            strBuild.Append("				if (document.forms(0) != 'undefined') {	\n");
            strBuild.Append("					if (document.forms[0].hidChange != null) {\n");
            strBuild.Append("						if (document.forms(0).hidChange.value == '1') {\n");
            strBuild.Append("							IsChange =true;\n");
            strBuild.Append("						}						\n");
            strBuild.Append("					}							\n");
            strBuild.Append("												\n");
            strBuild.Append("					if (IsChange) {				\n");
            strBuild.Append("						IsChange = confirm('" + sErrorMsg + "');\n");
            strBuild.Append("					}					\n");
            strBuild.Append("					if (IsChange) {		\n");

            strBuild.Append("						try {			\n");
            strBuild.Append("							Page_ClientValidate();	\n");
            strBuild.Append("							if (Page_IsValid) {		\n");
            strBuild.Append("								IsValid = true;		\n");
            strBuild.Append("							} else {				\n");
            strBuild.Append("								IsValid = false;	\n");
            strBuild.Append("							}						\n");
            strBuild.Append("						} catch (e) {				\n");
            strBuild.Append("							IsValid = true;\n");
            strBuild.Append("						}							\n");
            strBuild.Append("					} else {						\n");
            strBuild.Append("						if (document.forms(0).hidChange != null) {							\n");
            strBuild.Append("							document.forms(0).hidChange.value = '0';\n");
            strBuild.Append("						}							\n");
            strBuild.Append("						IsValid = true;				\n");
            strBuild.Append("					}								\n");
            strBuild.Append("					if (IsValid) {					\n");
            strBuild.Append("						oCtl.value = Menu;				\n");
            strBuild.Append("						document.forms(0).submit();		\n");
            strBuild.Append("					}									\n");

            strBuild.Append("				}										\n");
            //			strBuild.Append("			}											\n");
            strBuild.Append("		}												\n");
            strBuild.Append("														\n");
            strBuild.Append("	}													\n");

            strBuild.Append("</script>	\n");

            return strBuild.ToString();
        }

        public string GetPageUrl(QSPForm.Business.AppItem NoMenu, string paramName, string paramValue) {
            StringBuilder strBuild = new StringBuilder();
            string pageUrl = "";
            pageUrl = GetPageUrl(NoMenu);
            strBuild.Append(pageUrl);

            if (pageUrl.IndexOf("?") == -1) {
                strBuild.Append("?");
                strBuild.Append(paramName);
                strBuild.Append("=");
                strBuild.Append(paramValue);
            }
            else {
                strBuild.Append("&");
                strBuild.Append(paramName);
                strBuild.Append("=");
                strBuild.Append(paramValue);
            }

            return strBuild.ToString();
        }

        public string GetPageUrl(QSPForm.Business.AppItem NoMenu) {
            string pageUrl = "";
            if ((NoMenu == QSPForm.Business.AppItem.Calendar) || (NoMenu == QSPForm.Business.AppItem.BusinessCalendar)) {
                pageUrl = "BusinessCalendar.aspx";
            }
            else {
                QSPForm.Business.ContentManagerSystem CMSys = new QSPForm.Business.ContentManagerSystem();
                pageUrl = CMSys.GetAppItemToGo(NoMenu);
            }
            return pageUrl;
        }

        public void SetJScriptForOpenDetail(System.Web.UI.WebControls.WebControl webCtl, QSPForm.Business.AppItem NoMenu, string paramName, string paramValue, int width, int height) {
            SetJScriptForOpenDetail(webCtl, NoMenu, paramName, paramValue, width, height, "OnClick");
        }

        public void SetJScriptForOpenDetail(System.Web.UI.WebControls.WebControl webCtl, QSPForm.Business.AppItem NoMenu, string paramName, string paramValue, int width, int height, string eventName) {
            SetJScriptForOpenDetail(webCtl, NoMenu, paramName, paramValue, width, height, eventName, "");
        }

        public void SetJScriptForOpenDetail(System.Web.UI.WebControls.WebControl webCtl, QSPForm.Business.AppItem NoMenu, string paramName, string paramValue, int width, int height, string eventName, string additionalQuery) {
            string url = GetPageUrl(NoMenu, paramName, paramValue);
            if (additionalQuery.Length > 0)
                url = url + additionalQuery;

            if (width == 0) {
                width = 850;
            }
            if (height == 0) {
                height = 750;
            }

            if (webCtl != null) {
                webCtl.Attributes.Add(eventName, "window.open('" + url + "','','toobars=yes,scrollbars=yes,status=yes,width=" + width.ToString() + ",height=" + height.ToString() + ",resizable=yes');return false;");
            }
        }

        public void SetJScriptForOpenDetailNoCMS(System.Web.UI.WebControls.WebControl webCtl, string url, string paramName, string paramValue, int width, int height) {
            SetJScriptForOpenDetailNoCMS(webCtl, url, paramName, paramValue, width, height, "OnClick", "");
        }

        public void SetJScriptForOpenDetailNoCMS(System.Web.UI.WebControls.WebControl webCtl, string url, string paramName, string paramValue, int width, int height, string eventName) {
            SetJScriptForOpenDetailNoCMS(webCtl, url, paramName, paramValue, width, height, eventName, "");
        }

        /// <summary>
        /// New Method without CMS to open the link in new page
        /// </summary>
        /// <param name="webCtl"></param>
        /// <param name="url"></param>
        /// <param name="paramName"></param>
        /// <param name="paramValue"></param>
        /// <param name="width"></param>
        /// <param name="height"></param>
        /// <param name="eventName"></param>
        /// <param name="additionalQuery"></param>
        public void SetJScriptForOpenDetailNoCMS(System.Web.UI.WebControls.WebControl webCtl, string url, string paramName, string paramValue, int width, int height, string eventName, string additionalQuery) {
            string urlActual = url + "&" + paramName + "=" + paramValue;
            if (additionalQuery.Length > 0)
                urlActual = urlActual + additionalQuery;

            if (width == 0) {
                width = 850;
            }
            if (height == 0) {
                height = 750;
            }

            if (webCtl != null) {
                webCtl.Attributes.Add(eventName, "window.open('" + urlActual + "','','toobars=yes,scrollbars=yes,status=yes,width=" + width.ToString() + ",height=" + height.ToString() + ",resizable=yes');return false;");
            }
        }

        public void SetJScriptForOpenCalendar(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, System.Web.UI.WebControls.WebControl webCtlText, bool IsBizCal, System.Web.UI.WebControls.WebControl webCtlOrdDate, System.Web.UI.WebControls.WebControl webCtlWarehouse, int NbDayLeadTime, int formID) {
            if (IsBizCal)
                SetJScriptForOpenCalendar(webCtl, webCtlValue, webCtlText, QSPForm.Business.AppItem.BusinessCalendar, webCtlOrdDate, webCtlWarehouse, NbDayLeadTime, formID);
            else
                SetJScriptForOpenCalendar(webCtl, webCtlValue, webCtlText, QSPForm.Business.AppItem.Calendar, null, null, 0, 0);
        }

        public void SetJScriptForOpenCalendar(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, System.Web.UI.WebControls.WebControl webCtlText, bool IsBizCal, System.Web.UI.WebControls.WebControl webCtlOrdDate, System.Web.UI.WebControls.WebControl webCtlWarehouse, int NbDayLeadTime) {
            if (IsBizCal)
                SetJScriptForOpenCalendar(webCtl, webCtlValue, webCtlText, QSPForm.Business.AppItem.BusinessCalendar, webCtlOrdDate, webCtlWarehouse, NbDayLeadTime, 0);
            else
                SetJScriptForOpenCalendar(webCtl, webCtlValue, webCtlText, QSPForm.Business.AppItem.Calendar, null, null, 0, 0);
        }

        public void SetJScriptForOpenCalendar(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue) {
            SetJScriptForOpenCalendar(webCtl, webCtlValue, null, false, null, null, 0, 0);

        }

        public void SetJScriptForOpenCalendar(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, System.Web.UI.WebControls.WebControl webCtlText) {
            SetJScriptForOpenCalendar(webCtl, webCtlValue, webCtlText, QSPForm.Business.AppItem.BusinessCalendar, null, null, 0, 0);

        }

        public string SetJScriptForOpenCalendar(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, System.Web.UI.WebControls.WebControl webCtlText, QSPForm.Business.AppItem NoMenu, System.Web.UI.WebControls.WebControl webCtlOrdDate, System.Web.UI.WebControls.WebControl webCtlWarehouse, int MinNbDayLeadTime, int formID) {
            StringBuilder strBuild = new StringBuilder();
            //'Default
            int width = 210;
            int height = 190;

            string namePage = NoMenu.ToString();

            string url = GetPageUrl(NoMenu);

            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");

            //Add positioning feature
            strBuild.Append("	function findoffsetPosX(obj)\n");
            strBuild.Append("	{\n");
            strBuild.Append("		var curleft = 0;\n");
            strBuild.Append("		if (obj.offsetParent)\n");
            strBuild.Append("		{\n");
            strBuild.Append("			while (obj.offsetParent)\n");
            strBuild.Append("		{\n");
            strBuild.Append("				curleft += obj.offsetLeft\n");
            strBuild.Append("				obj = obj.offsetParent;\n");
            strBuild.Append("			}\n");
            strBuild.Append("		}\n");
            strBuild.Append("		else if (obj.x)\n");
            strBuild.Append("			curleft += obj.x;\n");
            strBuild.Append("		return curleft;\n");
            strBuild.Append("	}\n");

            strBuild.Append("	function findoffsetPosY(obj)		\n");
            strBuild.Append("	{		\n");
            strBuild.Append("		var curtop = 0;		\n");
            strBuild.Append("		if (obj.offsetParent)		\n");
            strBuild.Append("		{		\n");
            strBuild.Append("			while (obj.offsetParent)		\n");
            strBuild.Append("			{		\n");
            strBuild.Append("				curtop += obj.offsetTop		\n");
            strBuild.Append("				obj = obj.offsetParent;		\n");
            strBuild.Append("			}		\n");
            strBuild.Append("		}		\n");
            strBuild.Append("		else if (obj.y)		\n");
            strBuild.Append("			curtop += obj.y;		\n");
            strBuild.Append("		return curtop;		\n");
            strBuild.Append("	}		\n");

            strBuild.Append("	var " + namePage + ";\n");
            if (webCtlOrdDate != null) {
                if (webCtlWarehouse != null) {
                    if (MinNbDayLeadTime > 0)
                        strBuild.Append("	function Open" + namePage + " (IDRefCtrl, NameRefCtrl, OrdDateRefCtrl, WarehouseRefCtrl, MinNbDayLeadTime) {\n");
                    else
                        strBuild.Append("	function Open" + namePage + " (IDRefCtrl, NameRefCtrl, OrdDateRefCtrl, WarehouseRefCtrl) {\n");
                }
                else {
                    if (MinNbDayLeadTime > 0)
                        strBuild.Append("	function Open" + namePage + " (IDRefCtrl, NameRefCtrl, OrdDateRefCtrl, MinNbDayLeadTime, formID) {\n");
                    else
                    {
                        strBuild.Append("	function Open" + namePage + " (IDRefCtrl, NameRefCtrl, OrdDateRefCtrl) {\n");
                        //strBuild.Append("	function Open" + namePage + " (IDRefCtrl, NameRefCtrl, OrdDateRefCtrl, MinNbDayLeadTime, formID) {\n");
                    }
                }
            }
            else
                strBuild.Append("	function Open" + namePage + " (IDRefCtrl, NameRefCtrl) {\n");
            strBuild.Append("try {\n");
            strBuild.Append("		if (" + namePage + " != null) {\n");
            strBuild.Append("			" + namePage + ".close();\n");
            strBuild.Append("		}\n");
            strBuild.Append("		var urlstr;\n");
            strBuild.Append("		\n");

            if (url.IndexOf("?") == -1) {
                strBuild.Append("		urlstr = '" + url + "?IDRefCtrl=' + IDRefCtrl + '&NameRefCtrl=' + NameRefCtrl \n");
            }
            else {
                strBuild.Append("		urlstr = '" + url + "&IDRefCtrl=' + IDRefCtrl + '&NameRefCtrl=' + NameRefCtrl \n");
            }
            if (NoMenu == QSPForm.Business.AppItem.BusinessCalendar) {
                strBuild.Append("		urlstr = urlstr + '&IsBizCal=true' \n");
                strBuild.Append("		var orderDate = window.document.getElementById(OrdDateRefCtrl);    \n");
                strBuild.Append("  		if (orderDate != null)		\n");
                strBuild.Append("  			urlstr = urlstr + '&OrdDate=' + orderDate.innerHTML;		\n");
                //				strBuild.Append("  			urlstr = urlstr + '&OrdDate=' + " + DateTime.Now.ToShortDateString()+";		\n");

                //				strBuild.Append("		var objWarehouse;	\n");
                //				strBuild.Append("		var objWarehouse = window.document.getElementById(WarehouseRefCtrl);    \n");
                //				strBuild.Append("  		if (objWarehouse != null)	{	\n");
                //				strBuild.Append("  			if (objWarehouse.value.length > 0)	\n");
                //				strBuild.Append("  				urlstr = urlstr + '&WarehouseID=' + objWarehouse.value;		\n");
                //				strBuild.Append("  		}	\n");
            }
            if (MinNbDayLeadTime > 0) {
                strBuild.Append("  		urlstr = urlstr + '&MinNbDayLeadTime=' + MinNbDayLeadTime;		\n");
            }
            if (formID > 0) {
                strBuild.Append("  		urlstr = urlstr + '&formID=' + formID;		\n");
            }

            //strBuild.Append("		alert('step 1');			\n");
            //strBuild.Append("		var value;			\n");
            strBuild.Append("		var PosX = 0;    \n");
            strBuild.Append("		var PosY = 0 ;    \n");
            strBuild.Append("		var objValue = window.document.getElementById(IDRefCtrl);    \n");
            strBuild.Append("  		if (objValue != null) {		\n");
            strBuild.Append("  			urlstr = urlstr + '&CalDate=' + objValue.value;		\n");
            //strBuild.Append("			alert('step 2');			\n");
            strBuild.Append("			PosX = window.screenLeft + findoffsetPosX(objValue);\n");
            //strBuild.Append("			alert('step 3');			\n");
            //			strBuild.Append("			alert('scrollTop:' + document.body.scrollTop)	\n");
            //			strBuild.Append("			alert('window.screenTop:' + window.screenTop)	\n");
            //			strBuild.Append("			alert('objValue.offsetHeight:' + objValue.offsetHeight)	\n");
            //			strBuild.Append("			alert('objValue.offsetHeight:' + objValue.offsetHeight)	\n");
            strBuild.Append("			PosY = window.screenTop - document.documentElement.scrollTop + findoffsetPosY(objValue) + objValue.offsetHeight; \n");
            //strBuild.Append("			alert(document.body.scrollTop);			\n");
            //strBuild.Append("			PosY = window.screenTop - document.body.scrollTop + findoffsetPosY(objValue) + objValue.offsetHeight; \n");
            //strBuild.Append("			alert(document.documentElement.scrollTop);			\n");
            //strBuild.Append("			alert('PosX=' + PosX + ' PosY=' + PosY);			\n");
            //strBuild.Append("			alert(urlstr);			\n");
            strBuild.Append("}		\n");
            //strBuild.Append("		alert('step 4');			\n");
            strBuild.Append("		" + namePage + " = window.open(urlstr,'','toolbars=yes,status=yes,scrollbars=no,width=" + width.ToString() + ",height=" + height.ToString() + ",left='+ PosX + ',top=' + PosY +',resizable=no',false);		\n");
            //strBuild.Append("		document.all[IDRefCtrl].value = window.showModalDialog(urlstr,document.all[IDRefCtrl].value, 'dialogLeft:500px;dialogTop:500px;dialogHeight:" + height.ToString() + "px;dialogWidth:" + width.ToString() + "px;center:No;help:No;scroll:Yes;resizable:Yes;status:Yes;');");
            strBuild.Append("		} catch(e) {\n");
            strBuild.Append("			alert(e.message);\n");
            strBuild.Append("}		\n");
            strBuild.Append("	}\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");

            string sfctname = "Open" + namePage;

            if (!webCtl.Page.IsClientScriptBlockRegistered(sfctname)) {

                webCtl.Page.RegisterClientScriptBlock(sfctname, strBuild.ToString());
            }

            string sCtlTextClientID;
            string sCtlValueClientID;

            if (webCtl != null) {
                sCtlValueClientID = webCtlValue.ClientID;

                if (webCtlText != null) {
                    sCtlTextClientID = webCtlText.ClientID;
                    if (webCtlOrdDate != null) {
                        if (webCtlWarehouse != null) {
                            if (MinNbDayLeadTime > 0)
                                webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "', '" + sCtlTextClientID + "', '" + webCtlOrdDate.ClientID + "', '" + webCtlWarehouse.ClientID + "', '" + MinNbDayLeadTime + "');return false;");
                            else
                                webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "', '" + sCtlTextClientID + "', '" + webCtlOrdDate.ClientID + "', '" + webCtlWarehouse.ClientID + "');return false;");
                        }
                        else {
                            if (MinNbDayLeadTime > 0)
                                webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "', '" + sCtlTextClientID + "', '" + webCtlOrdDate.ClientID + "', '" + MinNbDayLeadTime + "', '" + formID + "');return false;");
                            else
                                webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "', '" + sCtlTextClientID + "', '" + webCtlOrdDate.ClientID + "');return false;");
                        }
                    }
                    else {
                        webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "', '" + sCtlTextClientID + "');return false;");
                    }
                }
                else {
                    webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "');return false;");

                }
            }

            return sfctname;
        }

        public string SetJScriptForOpenSelector(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, System.Web.UI.WebControls.WebControl webCtlText, QSPForm.Business.AppItem NoMenu, int width, int height, string additionalQuery) {
            StringBuilder strBuild = new StringBuilder();
            //'Default
            if (width == 0) {
                width = 650;
            }
            if (height == 0) {
                height = 700;
            }
            string namePage = NoMenu.ToString();

            string url = GetPageUrl(NoMenu);

            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("	var " + namePage + ";\n");
            strBuild.Append("	function Open" + namePage + " (IDRefCtrl, NameRefCtrl) {\n");
            strBuild.Append("		if (" + namePage + " != null) {\n");
            strBuild.Append("			" + namePage + ".close();\n");
            strBuild.Append("		}\n");
            strBuild.Append("		var urlstr;\n");
            strBuild.Append("		\n");

            if (url.IndexOf("?") == -1) {
                strBuild.Append("		urlstr = '" + url + "?IDRefCtrl=' + IDRefCtrl + '&NameRefCtrl=' + NameRefCtrl \n");
            }
            else {
                strBuild.Append("		urlstr = '" + url + "&IDRefCtrl=' + IDRefCtrl + '&NameRefCtrl=' + NameRefCtrl \n");
            }

            /* optional queryString */
            if ((additionalQuery != null) && (additionalQuery.Length > 0)) {
                strBuild.Append(" urlstr += \"" + additionalQuery + "\";\n");
            }

            strBuild.Append("		" + namePage + " = window.open(urlstr,'','toolbars=yes,status=yes,scrollbars=yes,width=" + width.ToString() + ",height=" + height.ToString() + ",resizable=yes',false);		\n");
            //strBuild.Append("		document.all[IDRefCtrl].value = window.showModalDialog(urlstr,document.all[IDRefCtrl].value, 'dialogLeft:500px;dialogTop:500px;dialogHeight:" + height.ToString() + "px;dialogWidth:" + width.ToString() + "px;center:No;help:No;scroll:Yes;resizable:Yes;status:Yes;');");
            strBuild.Append("	}\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");

            string sfctname = "Open" + namePage;

            if (!webCtl.Page.IsClientScriptBlockRegistered(sfctname)) {

                webCtl.Page.RegisterClientScriptBlock(sfctname, strBuild.ToString());
            }

            string sCtlTextClientID;
            string sCtlValueClientID;

            if (webCtl != null) {
                sCtlValueClientID = webCtlValue.ClientID;

                if (webCtlText != null) {
                    sCtlTextClientID = webCtlText.ClientID;
                    webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "', '" + sCtlTextClientID + "');return false;");
                }
                else {
                    webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "');return false;");
                }
            }

            return sfctname;
        }

        public string SetJScriptForOpenSelector(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, System.Web.UI.WebControls.WebControl webCtlText, string url1, string fmName, int width, int height, string additionalQuery) {
            StringBuilder strBuild = new StringBuilder();
            //'Default
            if (width == 0) {
                width = 650;
            }
            if (height == 0) {
                height = 700;
            }
            string namePage = fmName;

            string url = url1;

            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("	var " + namePage + ";\n");
            strBuild.Append("	function Open" + namePage + " (IDRefCtrl, NameRefCtrl) {\n");
            strBuild.Append("		if (" + namePage + " != null) {\n");
            strBuild.Append("			" + namePage + ".close();\n");
            strBuild.Append("		}\n");
            strBuild.Append("		var urlstr;\n");
            strBuild.Append("		\n");

            if (url.IndexOf("?") == -1) {
                strBuild.Append("		urlstr = '" + url + "?IDRefCtrl=' + IDRefCtrl + '&NameRefCtrl=' + NameRefCtrl \n");
            }
            else {
                strBuild.Append("		urlstr = '" + url + "&IDRefCtrl=' + IDRefCtrl + '&NameRefCtrl=' + NameRefCtrl \n");
            }

            /* optional queryString */
            if ((additionalQuery != null) && (additionalQuery.Length > 0)) {
                strBuild.Append(" urlstr += \"" + additionalQuery + "\";\n");
            }

            strBuild.Append("		" + namePage + " = window.open(urlstr,'','toolbars=yes,status=yes,scrollbars=yes,width=" + width.ToString() + ",height=" + height.ToString() + ",resizable=yes',false);		\n");
            //strBuild.Append("		document.all[IDRefCtrl].value = window.showModalDialog(urlstr,document.all[IDRefCtrl].value, 'dialogLeft:500px;dialogTop:500px;dialogHeight:" + height.ToString() + "px;dialogWidth:" + width.ToString() + "px;center:No;help:No;scroll:Yes;resizable:Yes;status:Yes;');");
            strBuild.Append("	}\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");

            string sfctname = "Open" + namePage;

            if (!webCtl.Page.IsClientScriptBlockRegistered(sfctname)) {

                webCtl.Page.RegisterClientScriptBlock(sfctname, strBuild.ToString());
            }

            string sCtlTextClientID;
            string sCtlValueClientID;

            if (webCtl != null) {
                sCtlValueClientID = webCtlValue.ClientID;

                if (webCtlText != null) {
                    sCtlTextClientID = webCtlText.ClientID;
                    webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "', '" + sCtlTextClientID + "');return false;");
                }
                else {
                    webCtl.Attributes.Add("OnClick", sfctname + "('" + sCtlValueClientID + "');return false;");
                }

            }

            return sfctname;
        }

        public string SetJScriptForOpenSelector(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, System.Web.UI.WebControls.WebControl webCtlText, QSPForm.Business.AppItem NoMenu, int width, int height) {
            return SetJScriptForOpenSelector(webCtl, webCtlValue, webCtlText, NoMenu, width, height, null);
        }

        public string SetJScriptForOpenSelector(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, QSPForm.Business.AppItem NoMenu, int width, int height, string AdditionQuery) {
            return SetJScriptForOpenSelector(webCtl, webCtlValue, null, NoMenu, width, height, AdditionQuery);
        }

        public string SetJScriptForOpenSelector(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlValue, QSPForm.Business.AppItem NoMenu, int width, int height) {
            return SetJScriptForOpenSelector(webCtl, webCtlValue, null, NoMenu, width, height, null);
        }

        public string SetJScriptForCloseSelector(System.Web.UI.WebControls.WebControl webCtl, string sValue, string sText, string IDRefCtrl, string NameRefCtrl) {
            return SetJScriptForCloseSelector(webCtl, sValue, sText, IDRefCtrl, NameRefCtrl, "OnClick");
        }

        public string GetJScriptForCloseSelector(string iID, string sName, string IDRefCtrl, string NameRefCtrl) {
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<script language=javascript>\n");
            //strBuild.Append("<!--			\n");
            //strBuild.Append("function SetInfo(iID, sName){\n");
            //strBuild.Append("   alert('SetInfo' + '' + CustID + '' + CustName);\n");
            strBuild.Append("  var iID = \"" + iID + "\";\n");
            strBuild.Append("  var sName = \"" + sName + "\";\n");
            strBuild.Append("   if (iID !=  null) {\n");
            strBuild.Append("		var objValue = opener.document.getElementById('" + IDRefCtrl + "');    \n");
            strBuild.Append("  		if (objValue != null)		\n");
            strBuild.Append("  			objValue.value = iID;		\n");
            if (NameRefCtrl.Length > 0) {
                strBuild.Append("		var objText = opener.document.getElementById('" + NameRefCtrl + "');    \n");
                strBuild.Append("  		if (objText != null)		\n");
                strBuild.Append("  			objText.value = sName;		\n");
            }
            strBuild.Append("			window.close();		\n");
            strBuild.Append("		} else {\n");
            strBuild.Append("			alert('Insufficient Information !');\n");
            strBuild.Append("		}\n");
            //strBuild.Append("	}\n");
            //strBuild.Append("//-->\n");
            strBuild.Append("</script>");

            return strBuild.ToString();
        }

        public string SetJScriptForCloseSelector(System.Web.UI.WebControls.WebControl webCtl, string sValue, string sText, string IDRefCtrl, string NameRefCtrl, string eventName) {
            StringBuilder strBuild = new StringBuilder();
            //'Default
            sText = sText.Replace("'", "\\'");
            String sJScript = "";
            if (NameRefCtrl.Length > 0)
                sJScript = "SetInfo(\"" + sValue + "\", \"" + sText.Trim() + "\");return false;";
            else
                sJScript = "SetInfo(\"" + sValue + "\");return false;";

            webCtl.Attributes.Add(eventName, sJScript);

            //strBuild.Append(GetJScriptForCloseSelector();//iID,sName,IDRefCtrl,NameRefCtrl));

            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("function SetInfo(iID, sName){\n");
            //strBuild.Append("   alert('SetInfo' + '' + CustID + '' + CustName);\n");
            strBuild.Append("   if (iID !=  null) {\n");
            strBuild.Append("		var objValue = opener.document.getElementById('" + IDRefCtrl + "');    \n");
            strBuild.Append("  		if (objValue != null)		\n");
            strBuild.Append("  			objValue.value = iID;		\n");
            if (NameRefCtrl.Length > 0) {
                strBuild.Append("		var objText = opener.document.getElementById('" + NameRefCtrl + "');    \n");
                strBuild.Append("  		if (objText != null)		\n");
                strBuild.Append("  			objText.value = sName;		\n");
            }
            strBuild.Append("			window.close();		\n");
            strBuild.Append("		} else {\n");
            strBuild.Append("			alert('Insufficient Information !');\n");
            strBuild.Append("		}\n");
            strBuild.Append("	}\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");

            string sfctname = "SetInfo";
            if (!webCtl.Page.IsClientScriptBlockRegistered(sfctname)) {

                webCtl.Page.RegisterClientScriptBlock(sfctname, strBuild.ToString());
            }

            return sfctname;
        }

        public string SetJScriptForCloseCalendar(System.Web.UI.WebControls.WebControl webCtl, string sValue, string IDRefCtrl, string eventName) {
            return SetJScriptForCloseCalendar(webCtl, sValue, "", IDRefCtrl, "", eventName);
        }

        public string SetJScriptForCloseCalendar(System.Web.UI.WebControls.WebControl webCtl, string sValue, string IDRefCtrl) {
            return SetJScriptForCloseCalendar(webCtl, sValue, IDRefCtrl, "OnClick");
        }

        public string SetJScriptForCloseCalendar(System.Web.UI.WebControls.WebControl webCtl, string sValue, string sText, string IDRefCtrl, string IDRefCtrl1) {
            return SetJScriptForCloseCalendar(webCtl, sValue, sText, IDRefCtrl, IDRefCtrl1, "OnClick");
        }

        public string SetJScriptForCloseCalendar(System.Web.UI.WebControls.WebControl webCtl, string sValue, string sText, string IDRefCtrl, string IDRefCtrl1, string eventName) {
            StringBuilder strBuild = new StringBuilder();
            //'Default
            sText = sText.Replace("'", "\\'");
            String sJScript = "";
            //			if (bAlert)
            //				sJScript = "if (confirm('This date doesn't respect the minimum required, Do you want to proceed anyway'));";
            if (IDRefCtrl1.Length > 0)
                sJScript = sJScript + "SetInfo(\"" + sValue + "\", \"" + sText.Trim() + "\");return false;";
            else
                sJScript = sJScript + "SetInfo(\"" + sValue + "\");return false;";

            webCtl.Attributes.Add(eventName, sJScript);

            //			strBuild.Append("<script language=javascript>\n");
            //			strBuild.Append("<!--			\n");
            //			strBuild.Append("function SetInfo(iID, sName){\n");
            //			//strBuild.Append("   alert('SetInfo' + '' + CustID + '' + CustName);\n");
            //			strBuild.Append("   if ((iID !=  null) && (opener !=  null)) {\n");
            //			strBuild.Append("		var objValue = opener.document.getElementById('" + IDRefCtrl + "');    \n");
            //			strBuild.Append("  		if (objValue != null)		\n");
            //			strBuild.Append("  			objValue.value = iID;		\n");
            //			if (IDRefCtrl1.Length >0)
            //			{
            //				strBuild.Append("		var objText = opener.document.getElementById('" + IDRefCtrl1 + "');    \n");
            //				strBuild.Append("  		if (objText != null)		\n");
            //				strBuild.Append("  			objText.innerHTML = sName;		\n");
            //			}
            //			strBuild.Append("			window.close();		\n");
            //			strBuild.Append("		} else {\n");
            //			strBuild.Append("			alert('Insufficient Information !');\n");
            //			strBuild.Append("		}\n");
            //			strBuild.Append("	}\n");
            //			strBuild.Append("//-->\n");
            //			strBuild.Append("</script>");
            //
            string sfctname = "SetInfo";
            //			if (!page.IsClientScriptBlockRegistered(sfctname))
            //			{
            //				
            //				page.RegisterClientScriptBlock(sfctname, strBuild.ToString());				
            //			}

            return sfctname;
        }

        public string SetJScriptForCloseSelector(System.Web.UI.WebControls.WebControl webCtl, string sValue, string IDRefCtrl, string eventName) {
            return SetJScriptForCloseSelector(webCtl, sValue, "", IDRefCtrl, "", eventName);
        }

        public string SetJScriptForCloseSelector(System.Web.UI.WebControls.WebControl webCtl, string sValue, string IDRefCtrl) {
            return SetJScriptForCloseSelector(webCtl, sValue, "", IDRefCtrl, "", "OnClick");
        }

        public void RenderStartUpScroll(int height, Page page) {
            System.Text.StringBuilder sJScript = new System.Text.StringBuilder();
            sJScript.Append("<script language=javascript>\n");

            sJScript.Append("function scrollToSection() {\n");
            sJScript.Append("       window.scrollTo(0," + height.ToString() + ");\n");
            sJScript.Append("}\n");

            if (page.SmartNavigation) {
                sJScript.Append("window.setTimeout(scrollToSection, 1000);\n");
            }
            else {
                sJScript.Append("window.onload = scrollToSection;\n");
            }

            sJScript.Append("</SCRIPT>");

            // Register Client Script
            page.RegisterClientScriptBlock("ScrollToSection", sJScript.ToString());
        }

        public void RenderStartUpScroll(System.Web.UI.WebControls.WebControl webCtl) {
            System.Text.StringBuilder sJScript = new System.Text.StringBuilder();
            sJScript.Append("<script language=javascript>\n");

            sJScript.Append("function scrollToSection() {\n");
            sJScript.Append("   var objTable = window.document.getElementById('" + webCtl.ClientID + "');\n");
            sJScript.Append("   var toScroll = 0;\n");
            sJScript.Append("	var curtop = 0;\n");
            sJScript.Append("   if (objTable != null) {\n");
            sJScript.Append("	    if (objTable.offsetParent)\n");
            sJScript.Append("	    {\n");
            sJScript.Append("	    	while (objTable.offsetParent)\n");
            sJScript.Append("	    	{\n");
            sJScript.Append("	    		curtop += objTable.offsetTop\n");
            sJScript.Append("	    		objTable = objTable.offsetParent;\n");
            sJScript.Append("	    	}\n");
            sJScript.Append("	    }\n");
            sJScript.Append("	    else if (objTable.y)\n");
            sJScript.Append("	    	curtop += objTable.y;\n");
            sJScript.Append("	    toScroll = curtop;\n");
            sJScript.Append("       window.scrollTo(0,toScroll);\n");
            sJScript.Append("   }\n");
            sJScript.Append("}\n");

            sJScript.Append("window.onload = scrollToSection;\n");

            sJScript.Append("</SCRIPT>");

            // Register Client Script
            webCtl.Page.RegisterClientScriptBlock("ScrollToSection", sJScript.ToString());
            //webCtl.Page.SetFocus(webCtl);
        }

        public void SetJScriptForCopyValueFromCtrlToCtrl(System.Web.UI.WebControls.WebControl webCtrlFrom, System.Web.UI.WebControls.WebControl webCtrlTo) {
            string sFunctionName = "CopyValueFromCtrlToCtrl";
            System.Text.StringBuilder sJScript = new System.Text.StringBuilder();
            sJScript.Append("<script language=javascript>\n");

            sJScript.Append("	function " + sFunctionName + "(RefCtrlFrom,RefCtrlTo) {\n");
            sJScript.Append("		var objFrom = window.document.getElementById(RefCtrlFrom); \n");
            sJScript.Append("		var objTo = window.document.getElementById(RefCtrlTo); \n");
            sJScript.Append("		objTo.value = objFrom.value; \n");
            sJScript.Append("	} \n");
            sJScript.Append("</script>");

            webCtrlFrom.Attributes.Add("OnChange", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom.Attributes.Add("OnKeyPress", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom.Attributes.Add("onkeydown", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom.Attributes.Add("onkeyup", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlTo.ClientID + "');");

            if (!webCtrlFrom.Page.IsClientScriptBlockRegistered(sFunctionName)) {
                webCtrlFrom.Page.RegisterClientScriptBlock(sFunctionName, sJScript.ToString());
            }
        }

        public void SetJScriptForCopyValueFromCtrlToCtrl(System.Web.UI.WebControls.WebControl webCtrlFrom, System.Web.UI.WebControls.WebControl webCtrlFrom1, System.Web.UI.WebControls.WebControl webCtrlTo) {
            string sFunctionName = "CopyValueFromCtrlsToCtrl";
            System.Text.StringBuilder sJScript = new System.Text.StringBuilder();
            sJScript.Append("<script language=javascript>\n");

            sJScript.Append("	function " + sFunctionName + "(RefCtrlFrom,RefCtrlFrom1,RefCtrlTo) {\n");
            sJScript.Append("		var objFrom = window.document.getElementById(RefCtrlFrom); \n");
            sJScript.Append("		var objFrom1 = window.document.getElementById(RefCtrlFrom1); \n");
            sJScript.Append("		var objTo = window.document.getElementById(RefCtrlTo); \n");
            sJScript.Append("		objTo.value = objFrom.value + ' ' + objFrom1.value; \n");
            sJScript.Append("	} \n");
            sJScript.Append("</script>");

            webCtrlFrom.Attributes.Add("OnChange", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlFrom1.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom.Attributes.Add("OnKeyPress", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlFrom1.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom.Attributes.Add("onkeydown", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlFrom1.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom.Attributes.Add("onkeyup", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlFrom1.ClientID + "', '" + webCtrlTo.ClientID + "');");

            webCtrlFrom1.Attributes.Add("OnChange", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlFrom1.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom1.Attributes.Add("OnKeyPress", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlFrom1.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom1.Attributes.Add("onkeydown", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlFrom1.ClientID + "', '" + webCtrlTo.ClientID + "');");
            webCtrlFrom1.Attributes.Add("onkeyup", sFunctionName + "('" + webCtrlFrom.ClientID + "', '" + webCtrlFrom1.ClientID + "', '" + webCtrlTo.ClientID + "');");

            if (!webCtrlFrom.Page.IsClientScriptBlockRegistered(sFunctionName)) {
                webCtrlFrom.Page.RegisterClientScriptBlock(sFunctionName, sJScript.ToString());
            }
        }

        public void SetJScriptForWaitMsg(System.Web.UI.WebControls.WebControl webCtl, System.Web.UI.WebControls.WebControl webCtlButton, string sWaitMsg) {
            System.Text.StringBuilder sJScript = new System.Text.StringBuilder();
            sJScript.Append("<script language=javascript>\n");
            sJScript.Append("	var WaitMsgTimeOutID\n");
            sJScript.Append("	function WaitMsg(controlID)  {\n");
            sJScript.Append("	   var objSpan = window.document.getElementById(controlID);\n");
            sJScript.Append("	   eval('objSpan.style.color = \\\"red\\\"');\n");
            sJScript.Append("		if(objSpan.innerText == '.......')\n");
            sJScript.Append("		{\n");
            sJScript.Append("			eval('objSpan.innerText = \\\"" + sWaitMsg + "...\\\"');\n");
            sJScript.Append("		}\n");
            sJScript.Append("		else\n");
            sJScript.Append("		{\n");
            sJScript.Append("			eval('objSpan.innerText = \\\".......\\\"');\n");
            sJScript.Append("		}\n");
            sJScript.Append("		var fctToRun = 'WaitMsg(\\\"' + controlID + '\\\")'; \n");
            sJScript.Append("		WaitMsgTimeOutID = window.setTimeout(fctToRun, 100);\n");
            sJScript.Append("	}\n");
            sJScript.Append("</SCRIPT>");

            // Register Client Script
            webCtl.Page.RegisterClientScriptBlock("WaitMsg", sJScript.ToString());
            if (webCtlButton.Attributes["OnClick"] != null) {
                string sAttr = webCtlButton.Attributes["OnClick"].ToString() + "WaitMsg('" + webCtl.ClientID + "');";
                webCtlButton.Attributes.Add("OnClick", sAttr);
            }
            else {
                webCtlButton.Attributes.Add("OnClick", "WaitMsg('" + webCtl.ClientID + "');");
            }
        }

        public void SetJScriptForClearWaitMsg(System.Web.UI.WebControls.WebControl webCtl) {
            System.Text.StringBuilder sJScript = new System.Text.StringBuilder();
            sJScript.Append("<script language=javascript>\n");
            sJScript.Append("	window.clearTimeout(WaitMsgTimeOutID);\n");

            sJScript.Append("</SCRIPT>");

            // Register Client Script
            webCtl.Page.RegisterStartupScript("ClearWaitMsg", sJScript.ToString());

        }
        //		function waitMsg() 
        //		{
        //			eval('lblMessage.style.color = "red"');
        //			if(lblMessage.innerText == ".......")
        //			{
        //				eval('lblMessage.innerText = "Please Wait..."');
        //			}
        //			else
        //			{
        //				eval('lblMessage.innerText = "......."');
        //			}
        //			setTimeout("waitMsg()", 100);
        //		} 	

        public void SetFiscalYearDropDownList(DropDownList ddlFiscalYear, bool IncludeAll) {
            QSPForm.Business.BusinessCalendarSystem calSys = new QSPForm.Business.BusinessCalendarSystem();
            int curFiscalYear = calSys.GetFiscalYear(DateTime.Today);
            DataTable tbl = new DataTable();
            tbl.Columns.Add("Value", typeof(System.Int32));
            tbl.Columns.Add("Text", typeof(System.String));
            for (int countYear = 1999; countYear <= curFiscalYear; countYear++) {
                tbl.Rows.Add(new object[] { countYear, countYear.ToString() });
            }
            if (IncludeAll) {
                AddRowForIncludeAll(tbl);
            }
            ddlFiscalYear.DataValueField = tbl.Columns[0].ColumnName;
            ddlFiscalYear.DataTextField = tbl.Columns[1].ColumnName;
            ddlFiscalYear.DataSource = tbl;
            ddlFiscalYear.DataBind();
            ddlFiscalYear.SelectedIndex = (ddlFiscalYear.Items.Count - 1);
        }

        public void SetOrganizationTypeDropDownList(DropDownList ddlOrgType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllOrganizationType();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlOrgType.DataValueField = tbl.Columns[0].ColumnName;
            ddlOrgType.DataTextField = tbl.Columns[1].ColumnName;
            ddlOrgType.DataSource = tbl;
            ddlOrgType.DataBind();
            ddlOrgType.SelectedIndex = 0;
        }

        public void SetOrganizationLevelDropDownList(DropDownList ddlOrgType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllOrganizationLevel();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlOrgType.DataValueField = tbl.Columns[0].ColumnName;
            ddlOrgType.DataTextField = tbl.Columns[1].ColumnName;
            ddlOrgType.DataSource = tbl;
            ddlOrgType.DataBind();
            ddlOrgType.SelectedIndex = 0;
        }

        public void SetTradeClassDropDownList(DropDownList ddlOrgType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllTradeClass();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---None---");
            }
            ddlOrgType.DataValueField = tbl.Columns[0].ColumnName;
            ddlOrgType.DataTextField = tbl.Columns[1].ColumnName;
            ddlOrgType.DataSource = tbl;
            ddlOrgType.DataBind();
            ddlOrgType.SelectedIndex = 0;
        }

        public void SetAccountTypeDropDownList(DropDownList ddlAccountType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllAccountType();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlAccountType.DataValueField = tbl.Columns[0].ColumnName;
            ddlAccountType.DataTextField = tbl.Columns[1].ColumnName;
            ddlAccountType.DataSource = tbl;
            ddlAccountType.DataBind();
            ddlAccountType.SelectedIndex = 0;
        }

        public void SetProgramTypeDropDownList(DropDownList ddlProgramType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllProgramType();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlProgramType.DataValueField = tbl.Columns[0].ColumnName;
            ddlProgramType.DataTextField = tbl.Columns[1].ColumnName;
            ddlProgramType.DataSource = tbl;
            ddlProgramType.DataBind();
            ddlProgramType.SelectedIndex = 0;
        }

        public void SetProgramTypeDropDownList(DropDownList ddlProgramType, int EntityTypeID, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllProgramTypeByEntityTypeID(EntityTypeID);
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlProgramType.DataValueField = tbl.Columns[0].ColumnName;
            ddlProgramType.DataTextField = tbl.Columns[1].ColumnName;
            ddlProgramType.DataSource = tbl;
            ddlProgramType.DataBind();
            ddlProgramType.SelectedIndex = 0;
        }

        public void SetSourceDropDownList(DropDownList ddlSource, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllSource();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlSource.DataValueField = tbl.Columns[0].ColumnName;
            ddlSource.DataTextField = tbl.Columns[1].ColumnName;
            ddlSource.DataSource = tbl;
            ddlSource.DataBind();
            ddlSource.SelectedIndex = 0;
        }

        public void SetOrderTypeDropDownList(DropDownList ddlOrderType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllOrderType();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlOrderType.DataValueField = tbl.Columns[0].ColumnName;
            ddlOrderType.DataTextField = tbl.Columns[1].ColumnName;
            ddlOrderType.DataSource = tbl;
            ddlOrderType.DataBind();
            ddlOrderType.SelectedIndex = 0;
        }

        public void SetOrderTypeDropDownList(DropDownList ddl, bool IncludeAll, int FormID) {
            QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
            FormOrderTypeTable tbl = frmSys.SelectAllOrderTypeByFormID(FormID);
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---", FormOrderTypeTable.FLD_ORDER_TYPE_ID, FormOrderTypeTable.FLD_ORDER_TYPE_NAME);
            }
            ddl.DataValueField = tbl.Columns[FormOrderTypeTable.FLD_ORDER_TYPE_ID].ColumnName;
            ddl.DataTextField = tbl.Columns[FormOrderTypeTable.FLD_ORDER_TYPE_NAME].ColumnName;
            ddl.DataSource = tbl;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        public void SetDeliveryMethodDropDownList(DropDownList ddl, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllDeliveryMethod();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddl.DataValueField = tbl.Columns[0].ColumnName;
            ddl.DataTextField = tbl.Columns[1].ColumnName;
            ddl.DataSource = tbl;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        public void SetDeliveryMethodDropDownList(DropDownList ddl, bool IncludeAll, int FormID) {
            QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
            FormDeliveryMethodTable tbl = frmSys.SelectAllDeliveryMethodByFormID(FormID);
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---", FormDeliveryMethodTable.FLD_DELIVERY_METHOD_ID, FormDeliveryMethodTable.FLD_DELIVERY_METHOD_NAME);
            }
            ddl.DataValueField = tbl.Columns[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_ID].ColumnName;
            ddl.DataTextField = tbl.Columns[FormDeliveryMethodTable.FLD_DELIVERY_METHOD_NAME].ColumnName;
            ddl.DataSource = tbl;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        public void SetOrderStatusDropDownList(DropDownList ddlOrderStatus, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllOrderStatus();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlOrderStatus.DataValueField = tbl.Columns[0].ColumnName;
            ddlOrderStatus.DataTextField = tbl.Columns[1].ColumnName;
            ddlOrderStatus.DataSource = tbl;
            ddlOrderStatus.DataBind();
            ddlOrderStatus.SelectedIndex = 0;
        }

        public void SetStatusReasonDropDownList(DropDownList ddlStatusReason, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllStatusReason();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlStatusReason.DataValueField = tbl.Columns[0].ColumnName;
            ddlStatusReason.DataTextField = tbl.Columns[1].ColumnName;
            ddlStatusReason.DataSource = tbl;
            ddlStatusReason.DataBind();
            ddlStatusReason.SelectedIndex = 0;
        }

        public void SetStatusCategoryDropDownList(DropDownList ddlStatusCategory, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllStatusCategory();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlStatusCategory.DataValueField = tbl.Columns[0].ColumnName;
            ddlStatusCategory.DataTextField = tbl.Columns[1].ColumnName;
            ddlStatusCategory.DataSource = tbl;
            ddlStatusCategory.DataBind();
            ddlStatusCategory.SelectedIndex = 0;
        }

        public void SetImageCategoryDropDownList(DropDownList ddlImageCategory, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllImageCategory();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlImageCategory.DataValueField = tbl.Columns[0].ColumnName;
            ddlImageCategory.DataTextField = tbl.Columns[1].ColumnName;
            ddlImageCategory.DataSource = tbl;
            ddlImageCategory.DataBind();
            ddlImageCategory.SelectedIndex = 0;
        }

        public void SetAccountStatusCategoryDropDownList(DropDownList ddlStatusCategory, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllStatusCategory(QSPForm.Common.EntityType.TYPE_ACCOUNT);
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlStatusCategory.DataValueField = tbl.Columns[0].ColumnName;
            ddlStatusCategory.DataTextField = tbl.Columns[1].ColumnName;
            ddlStatusCategory.DataSource = tbl;
            ddlStatusCategory.DataBind();
            ddlStatusCategory.SelectedIndex = 0;
        }

        public void SetProgramAgreementStatusCategoryDropDownList(DropDownList ddlStatusCategory, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllStatusCategory(QSPForm.Common.EntityType.TYPE_PROGRAM_AGREEMENT);
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlStatusCategory.DataValueField = tbl.Columns[0].ColumnName;
            ddlStatusCategory.DataTextField = tbl.Columns[1].ColumnName;
            ddlStatusCategory.DataSource = tbl;
            ddlStatusCategory.DataBind();
            ddlStatusCategory.SelectedIndex = 0;
        }

        public void SetOrderStatusCategoryDropDownList(DropDownList ddlStatusCategory, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllStatusCategory(QSPForm.Common.EntityType.TYPE_ORDER_BILLING);
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlStatusCategory.DataValueField = tbl.Columns[0].ColumnName;
            ddlStatusCategory.DataTextField = tbl.Columns[1].ColumnName;
            ddlStatusCategory.DataSource = tbl;
            ddlStatusCategory.DataBind();
            ddlStatusCategory.SelectedIndex = 0;
        }

        public void SetWarehouseStatusCategoryDropDownList(DropDownList ddlStatusCategory, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllStatusCategory(QSPForm.Common.EntityType.TYPE_WAREHOUSE);
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlStatusCategory.DataValueField = tbl.Columns[0].ColumnName;
            ddlStatusCategory.DataTextField = tbl.Columns[1].ColumnName;
            ddlStatusCategory.DataSource = tbl;
            ddlStatusCategory.DataBind();
            ddlStatusCategory.SelectedIndex = 0;
        }

        public void SetDocumentTypeDropDownList(DropDownList ddlDocumentType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllDocumentType();
            if (IncludeAll) {
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlDocumentType.DataValueField = tbl.Columns[0].ColumnName;
            ddlDocumentType.DataTextField = tbl.Columns[1].ColumnName;
            ddlDocumentType.DataSource = tbl;
            ddlDocumentType.DataBind();
            ddlDocumentType.SelectedIndex = 0;
        }

        public void SetUSStateDropDownList(DropDownList ddlState, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllUSState();
            if (IncludeAll) {
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlState.DataValueField = tbl.Columns[0].ColumnName;
            ddlState.DataTextField = tbl.Columns[1].ColumnName;
            ddlState.DataSource = tbl;
            ddlState.DataBind();
            ddlState.SelectedIndex = 0;
        }

        public void SetRegionDropDownList(DropDownList ddl, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllRegion();
            if (IncludeAll) {
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddl.DataValueField = tbl.Columns[0].ColumnName;
            ddl.DataTextField = tbl.Columns[1].ColumnName;
            ddl.DataSource = tbl;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        public void SetUSSubdivisionDropDownList(DropDownList ddl, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllUSState();
            if (IncludeAll) {
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddl.DataValueField = tbl.Columns[0].ColumnName;
            ddl.DataTextField = tbl.Columns[1].ColumnName;
            ddl.DataSource = tbl;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        public void SetCultureDropDownList(DropDownList ddlCulture, bool IncludeAll) {
            if (IncludeAll) {
                ddlCulture.Items.Add(new ListItem("---SELECT---", ""));
            }
            ddlCulture.Items.Add(new ListItem("en-US", "en-US"));
            ddlCulture.Items.Add(new ListItem("en-CA", "en-CA"));
            ddlCulture.Items.Add(new ListItem("fr-CA", "fr-CA"));

            ddlCulture.SelectedIndex = 0;
        }

        public void SetFormDropDownList(DropDownList ddlForm, bool IncludeAll) {
            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
            FormTable tbl = formSys.SelectAll();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlForm.DataValueField = tbl.Columns[FormTable.FLD_PKID].ColumnName;
            ddlForm.DataTextField = tbl.Columns[FormTable.FLD_FORM_NAME].ColumnName;
            ddlForm.DataSource = tbl;
            ddlForm.DataBind();
            ddlForm.SelectedIndex = 0;
        }

        public void SetFormDropDownList(DropDownList ddlForm, int EntityType, bool IncludeAll) {
            QSPForm.Business.FormSystem formSys = new QSPForm.Business.FormSystem();
            FormTable tbl = formSys.SelectByEntityType(EntityType);
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            tbl.Columns.Add("DescLong", typeof(System.String), "ISNULL(" + FormTable.FLD_FORM_CODE + ",'') + ' - ' + " + "ISNULL(" + FormTable.FLD_FORM_NAME + ",'')");
            ddlForm.DataValueField = FormTable.FLD_PKID;
            ddlForm.DataTextField = "DescLong";
            ddlForm.DataSource = tbl;
            ddlForm.DataBind();
            ddlForm.SelectedIndex = 0;
        }

        public void SetBusinessDivisionDropDownList(DropDownList ddl, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllBusinessDivision();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddl.DataValueField = "business_division_id";
            ddl.DataTextField = "business_division_name";
            ddl.DataSource = tbl;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        public void SetProductTypeDropDownList(DropDownList ddlProductType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllProductType();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlProductType.DataValueField = tbl.Columns[0].ColumnName;
            ddlProductType.DataTextField = tbl.Columns[1].ColumnName;
            ddlProductType.DataSource = tbl;
            ddlProductType.DataBind();
            ddlProductType.SelectedIndex = 0;
        }

        public void SetVendorDropDownList(DropDownList ddlVendor, bool IncludeAll) {
            QSPForm.Business.VendorSystem vendorSys = new QSPForm.Business.VendorSystem();
            VendorTable tbl = vendorSys.SelectAll();
            if (IncludeAll) {
                DataRow r = tbl.NewRow();
                r[VendorTable.FLD_PKID] = 0;
                r[VendorTable.FLD_NAME] = "---SELECT---";
                tbl.Rows.InsertAt(r, 0);
            }
            ddlVendor.DataValueField = tbl.Columns[VendorTable.FLD_PKID].ColumnName;
            ddlVendor.DataTextField = tbl.Columns[VendorTable.FLD_NAME].ColumnName;
            ddlVendor.DataSource = tbl;
            ddlVendor.DataBind();
            ddlVendor.SelectedIndex = 0;
        }

        public void SetTaskTypeDropDownList(DropDownList ddlTaskType, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllTaskType();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlTaskType.DataValueField = tbl.Columns[0].ColumnName;
            ddlTaskType.DataTextField = tbl.Columns[1].ColumnName;
            ddlTaskType.DataSource = tbl;
            ddlTaskType.DataBind();
            ddlTaskType.SelectedIndex = 0;
        }

        public void SetFormSectionTypeDropDownList(DropDownList ddl, bool IncludeAll) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllFormSectionType();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddl.DataValueField = tbl.Columns[0].ColumnName;
            ddl.DataTextField = tbl.Columns[1].ColumnName;
            ddl.DataSource = tbl;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        public void SetTemplateEmailDropDownList(DropDownList ddlTmplEmail, bool IncludeAll) {

            QSPForm.Business.TemplateEmailSystem tmplEmailSys = new QSPForm.Business.TemplateEmailSystem();
            TemplateEmailTable tbl = tmplEmailSys.SelectAll();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddlTmplEmail.DataValueField = tbl.Columns[TemplateEmailTable.FLD_PKID].ColumnName;
            ddlTmplEmail.DataTextField = tbl.Columns[TemplateEmailTable.FLD_TEMPLATE_EMAIL_NAME].ColumnName;
            ddlTmplEmail.DataSource = tbl;
            ddlTmplEmail.DataBind();
            ddlTmplEmail.SelectedIndex = 0;
        }

        public void SetBizNotificationTypeDropDownList(DropDownList ddl, bool IncludeAll) {

            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllBusinessNotificationType();
            if (IncludeAll) {
                //AddRowForIncludeAll(tbl);
                AddRowForIncludeAll(tbl, "---SELECT---");
            }
            ddl.DataValueField = tbl.Columns[0].ColumnName;
            ddl.DataTextField = tbl.Columns[1].ColumnName;
            ddl.DataSource = tbl;
            ddl.DataBind();
            ddl.SelectedIndex = 0;
        }

        public void SetPaymentAssignmentTypeDropDownList(DropDownList ddlPaymentAssignmentType) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllPaymentAssignmentType();

            ddlPaymentAssignmentType.DataValueField = tbl.Columns[0].ColumnName;
            ddlPaymentAssignmentType.DataTextField = tbl.Columns[1].ColumnName;
            ddlPaymentAssignmentType.DataSource = tbl;
            ddlPaymentAssignmentType.DataBind();
            ddlPaymentAssignmentType.SelectedIndex = 0;
        }

        public void SetPaymentAssignmentTypeDropDownList(DropDownList ddlPaymentAssignmentType, int role) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllPaymentAssignmentType();

            DataView dv;
            if (role == 1) {
                dv = new DataView(tbl, "payment_assignment_type_id > 0", "payment_assignment_type_id", DataViewRowState.CurrentRows);
            }
            else {
                dv = tbl.DefaultView;
            }

            ddlPaymentAssignmentType.DataValueField = dv.Table.Columns[0].ColumnName;
            ddlPaymentAssignmentType.DataTextField = dv.Table.Columns[1].ColumnName;
            ddlPaymentAssignmentType.DataSource = dv;
            ddlPaymentAssignmentType.DataBind();
            ddlPaymentAssignmentType.SelectedIndex = 0;
        }

        public void SetProgramAgreementCatalogs(CheckBoxList chkProgramAgreementCatalog, int formId, DateTime programAgreementDate) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            DataTable tbl = comSys.SelectAllProgramAgreementCatalogs(formId, programAgreementDate);
            chkProgramAgreementCatalog.DataTextField = tbl.Columns[0].ColumnName;
            chkProgramAgreementCatalog.DataValueField = tbl.Columns[1].ColumnName;
            chkProgramAgreementCatalog.DataSource = tbl;
            chkProgramAgreementCatalog.DataBind();
        }

        private void AddRowForIncludeAll(DataTable tbl) {
            AddRowForIncludeAll(tbl, "--SELECT--");
        }

        private void AddRowForIncludeAll(DataTable tbl, string ValueColumnName, string TextColumnName) {
            AddRowForIncludeAll(tbl, "--SELECT--", ValueColumnName, TextColumnName);
        }

        private void AddRowForIncludeAll(DataTable tbl, string TestForAll) {
            AddRowForIncludeAll(tbl, TestForAll, "", "");
        }

        private void AddRowForIncludeAll(DataTable tbl, string TestForAll, string ValueColumnName, string TextColumnName) {
            DataRow row = tbl.NewRow();
            DataColumn colValue;
            DataColumn colText;
            if (ValueColumnName.Length > 0)
                colValue = tbl.Columns[ValueColumnName];
            else
                colValue = tbl.Columns[0];

            if (TextColumnName.Length > 0)
                colText = tbl.Columns[TextColumnName];
            else
                colText = tbl.Columns[1];

            if (colValue.DataType == System.Type.GetType("System.Int32"))
                row[colValue.ColumnName] = 0;
            else
                row[colValue.ColumnName] = "";

            row[colText.ColumnName] = TestForAll;
            tbl.Rows.InsertAt(row, 0);
        }

        public bool UpdateRow(DataRow row, string colFieldName, string sValue) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            return comSys.UpdateRow(row, colFieldName, sValue);
        }

        public bool UpdateRow(DataRow row, string colFieldName, int sValue) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            if (sValue == 1)
                return comSys.UpdateRow(row, colFieldName, "true");
            else
                return comSys.UpdateRow(row, colFieldName, "false");
        }

        public bool UpdateRow(DataRow row, string colFieldName, DataRow rowToCopy) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            return comSys.UpdateRow(row, colFieldName, rowToCopy);
        }

        public string FormatPhoneNumber(string phoneNumber) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            return comSys.FormatPhoneNumber(phoneNumber);
        }

        public string FormatZipCode(string zipCode) {
            QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
            return comSys.FormatZipCode(zipCode);
        }

        public void SetFormBusinessMessage(Label lblBusinessMessage, QSPForm.Business.AppItem appItem, int FormID) {
            SetFormBusinessMessage(lblBusinessMessage, appItem, FormID, 0);
        }

        public void SetFormBusinessMessage(Label lblBusinessMessage, QSPForm.Business.AppItem appItem, int FormID, int FormSectionTypeID) {
            SetFormBusinessMessage(lblBusinessMessage, appItem, FormID, FormSectionTypeID, 0);
        }

        public void SetFormBusinessMessage(Label lblBusinessMessage, QSPForm.Business.AppItem appItem, int FormID, int FormSectionTypeID, int FormSectionNumber) {
            QSPForm.Business.BusinessExceptionSystem excSys = new QSPForm.Business.BusinessExceptionSystem();
            QSPForm.Common.DataDef.BusinessExceptionTable dTblExc = excSys.SelectAllByNoAppItem(appItem, FormID, FormSectionTypeID, FormSectionNumber);
            lblBusinessMessage.Text = "";
            foreach (DataRow excRow in dTblExc.Rows) {
                string msg = excRow[BusinessExceptionTable.FLD_WARNING_MESSAGE].ToString().Trim();

                if (msg.Length > 0) {
                    if (lblBusinessMessage.Text.Length > 0)
                        lblBusinessMessage.Text = lblBusinessMessage.Text + "<BR><BR>";

                    lblBusinessMessage.Text = lblBusinessMessage.Text + msg;
                }
            }
        }

        public string[] GetFormTypesForCalendar() {
            try {
                string c_FormTypes = "";

                if (ConfigurationManager.AppSettings["QSPOrderExpress.FormTypeName"] != null)
                    c_FormTypes = ConfigurationManager.AppSettings["QSPOrderExpress.FormTypeName"].ToString();

                if (c_FormTypes.Length > 0) {
                    return c_FormTypes.Split(',');
                }
                else
                    return null;
            }
            catch (Exception e) {
                return null;
            }
        }

        public string GetShutDownForm(int FormID, ref int ShutdownFormID) {
            string[] FormTypes = GetFormTypesForCalendar();
            string shutdownform = "QSPOrderExpress.";
            string[] shutDownFormTypeID = null;
            string shutDownFormTypeIds = string.Empty;
            for (int i = 0; i < FormTypes.Length; i++) {
                shutdownform = "QSPOrderExpress." + FormTypes[i];
                if (ConfigurationManager.AppSettings[shutdownform] != null)
                    shutDownFormTypeIds = ConfigurationManager.AppSettings[shutdownform].ToString();

                if (shutDownFormTypeIds.Length > 0) {
                    shutDownFormTypeID = shutDownFormTypeIds.Split(',');
                }

                if (shutDownFormTypeID != null) {
                    if (shutDownFormTypeID.Length > 0) {
                        for (int x = 0; x < shutDownFormTypeID.Length; x++) {
                            if (Convert.ToInt32(shutDownFormTypeID[x]) == FormID) {
                                // ShutdownStartDateString = shutdownform + ".StartDate";
                                // ShutdownEndDateString = shutdownform + ".EndDate";
                                ShutdownFormID = Convert.ToInt32(shutDownFormTypeID[x]);
                                return shutdownform;
                            }
                        }
                    }
                }
            }
            return null;
        }

        public string[] GetShutDownStartDate(string ShutdownFormStartDate) {
            string c_ShutDownStartDates = "";
            ShutdownFormStartDate = ShutdownFormStartDate + ".StartDate";
            if (ConfigurationManager.AppSettings[ShutdownFormStartDate] != null)
                c_ShutDownStartDates = ConfigurationManager.AppSettings[ShutdownFormStartDate].ToString();

            if (c_ShutDownStartDates.Length > 0) {
                return c_ShutDownStartDates.Split(',');
            }
            else
                return null;
        }

        public string[] GetShutDownEndDate(string ShutdownEndDateString) {
            string c_ShutDownEndDates = "";
            ShutdownEndDateString = ShutdownEndDateString + ".EndDate";
            if (ConfigurationManager.AppSettings[ShutdownEndDateString] != null)
                c_ShutDownEndDates = ConfigurationManager.AppSettings[ShutdownEndDateString].ToString();

            if (c_ShutDownEndDates.Length > 0) {
                return c_ShutDownEndDates.Split(',');
            }
            else
                return null;
        }
    }
}