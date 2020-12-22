using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.BusinessCalendarTable;
using System.Text;
using System.Configuration;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for BusinessCalendarControlForm.
    /// </summary>
    public partial class BusinessCalendarControlForm : BaseWebUserControl {
        private BusinessCalendarSystem calSys = new BusinessCalendarSystem();
        protected dataDef dTblBusinessCal = new dataDef();
        protected dataDef dTblWarehouseCal = new dataDef();
        private const string DATASOURCE = "Data_Source";
        private DataView dvCalendar;
        private String c_IDRefCtrl = "";
        private String c_IDRefCtrl1 = "";
        private DateTime c_OrderDate = DateTime.Today;
        private bool c_IsBizCal = false;
        private int c_WarehouseID = 0;
        private int c_MinNbDayLeadTime = 0;
        private int formID = 0;
        private int shutdownformID = 0;
        CommonUtility clsUtil = new CommonUtility();
        private bool c_IsSelector = true;
        public System.EventHandler SelectionChanged;
        private DateTime c_ShutDownStartDate = DateTime.Today;
        private DateTime c_ShutDownEndDate = DateTime.Today;
        private string[] ShutDownStartDates = null;
        private string[] ShutDownEndDates = null;
        private string ShutdownString = String.Empty;
        //private string ShutdownStartDateString = String.Empty;
        //private string ShutdownEndDateString = String.Empty;
        private int ShutdownFormID = 0;

        protected void Page_Load(object sender, EventArgs e) {
            // Put user code to initialize the page here
            SetJavascript();

            string minDateAllowed = "2/2/1753"; //Minumum date Calendar can show
            DateTime MinValue = Convert.ToDateTime(minDateAllowed);
            if (!IsPostBack) {
                if (calBusiness.SelectedDate >= MinValue)
                    calBusiness.VisibleDate = calBusiness.SelectedDate;
                else
                    calBusiness.VisibleDate = DateTime.Today;

                if (c_IsBizCal) {
                    calBusiness.WeekendDayStyle.Font.Strikeout = true;
                    calBusiness.DayStyle.Font.Underline = false;
                }
            }
            LoadDataSource();
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        ///		Required method for Designer support - do not modify
        ///		the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            this.calBusiness.DayRender += new System.Web.UI.WebControls.DayRenderEventHandler(this.calBusiness_DayRender);
            //This event is not necessary
            this.calBusiness.VisibleMonthChanged += new System.Web.UI.WebControls.MonthChangedEventHandler(this.calBusiness_VisibleMonthChanged);

        }
        #endregion


        public dataDef DataSource {
            get {
                return dTblWarehouseCal;
            }
            set {
                dTblWarehouseCal = value;
            }
        }

        public bool IsSelector {
            get {
                return c_IsSelector;
            }
            set {
                c_IsSelector = value;
            }
        }

        public bool IsBizCal {
            get {
                return c_IsBizCal;
            }
            set {
                c_IsBizCal = value;
            }
        }

        public int WarehouseID {
            get {
                return c_WarehouseID;
            }
            set {
                c_WarehouseID = value;
            }
        }

        public int MinNbDayLeadTime {
            get {
                return c_MinNbDayLeadTime;
            }
            set {
                c_MinNbDayLeadTime = value;
            }
        }

        public string IDRefCtrl {
            get {
                return c_IDRefCtrl;
            }
            set {
                c_IDRefCtrl = value;
            }
        }

        public string IDRefCtrl1 {
            get {
                return c_IDRefCtrl1;
            }
            set {
                c_IDRefCtrl1 = value;
            }
        }

        public int FormID {
            get {
                return formID;
            }
            set {
                formID = value;
            }
        }

        public DateTime OrderDate {
            get {
                return c_OrderDate;
            }
            set {
                c_OrderDate = value;
            }
        }

        public DateTime SelectedDate {
            get {
                return calBusiness.SelectedDate;
            }
            set {
                calBusiness.SelectedDate = value;
            }
        }
        // /// <summary>
        // /// WFC shutdown Start Dates
        // /// </summary>
        //private string[] ShutDownStartDates
        // {
        //     get
        //     {
        //         string c_ShutDownStartDates = "";
        //         if (ConfigurationManager.AppSettings[ShutdownStartDateString] != null)
        //             c_ShutDownStartDates = ConfigurationManager.AppSettings[ShutdownStartDateString].ToString();

        //         if (c_ShutDownStartDates.Length > 0)
        //         {
        //             return c_ShutDownStartDates.Split(',');
        //         }
        //         else
        //             return null;
        //     }
        // }

        // /// <summary>
        // /// WFC shutdown End Dates
        // /// </summary>
        // private string[] ShutDownEndDates
        // {
        //     get
        //     {
        //         string c_ShutDownEndDates = "";
        //         if (ConfigurationManager.AppSettings[ShutdownEndDateString] != null)
        //             c_ShutDownEndDates = ConfigurationManager.AppSettings[ShutdownEndDateString].ToString();

        //         if (c_ShutDownEndDates.Length > 0)
        //         {
        //             return c_ShutDownEndDates.Split(',');
        //         }
        //         else
        //             return null;
        //     }
        // }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            if (c_IsBizCal) {
                this.ViewState[DATASOURCE] = dTblBusinessCal;
            }
            //			else
            //			{
            //				if (c_IsSelector)
            //				{
            //					//Set all to white to show that all are available to select
            //					calBusiness.WeekendDayStyle.BackColor = calBusiness.DayStyle.BackColor;
            //				}
            //			}
        }

        public void LoadDataSource() {
            if (c_IsBizCal) {
                GetDataSource();
                dvCalendar = new DataView(dTblBusinessCal);
                dvCalendar.Sort = BusinessCalendarTable.FLD_PKID;
            }
        }

        public void GetDataSource() {
            //We get at each time 3 month of data to handled the other month show in the calendar

            DateTime StartDate = new DateTime(calBusiness.VisibleDate.AddMonths(-1).Year, calBusiness.VisibleDate.AddMonths(-1).Month, 1);
            DateTime EndDate = StartDate.AddMonths(3).AddDays(-1);
            ShutdownString = clsUtil.GetShutDownForm(FormID, ref ShutdownFormID);
            if (ShutdownString != null) {
                ShutDownStartDates = clsUtil.GetShutDownStartDate(ShutdownString);
                ShutDownEndDates = clsUtil.GetShutDownEndDate(ShutdownString);
            }

            if (FormID > 0)
                dTblBusinessCal = calSys.SelectAll_Search(StartDate, EndDate, OrderDate, ShutDownStartDates, ShutDownEndDates, WarehouseID, FormID, ShutdownFormID);
            else if (WarehouseID > 0)
                dTblBusinessCal = calSys.SelectAll_Search(StartDate, EndDate, OrderDate, c_WarehouseID);
            else
                dTblBusinessCal = calSys.SelectAll_Search(StartDate, EndDate, OrderDate);
        }

        //protected string GetShutDownForm(int FormID)
        //{
        //    string[] FormTypes = clsUtil.GetFormTypesForCalendar();
        //    string shutdownform = "QSPOrderExpress.";
        //    string[] shutDownFormTypeID = null;
        //    string shutDownFormTypeIds = string.Empty;
        //    for(int i=0; i < FormTypes.Length; i++)
        //    {
        //        shutdownform = "QSPOrderExpress." + FormTypes[i];
        //        if (ConfigurationManager.AppSettings[shutdownform] != null)
        //            shutDownFormTypeIds = ConfigurationManager.AppSettings[shutdownform].ToString();

        //        if (shutDownFormTypeIds.Length > 0)
        //        {
        //            shutDownFormTypeID = shutDownFormTypeIds.Split(',');
        //        }

        //        if (shutDownFormTypeID.Length > 0)
        //        {
        //            for (int x = 0; x < shutDownFormTypeID.Length; x++)
        //            {
        //                if (Convert.ToInt32(shutDownFormTypeID[x]) == FormID)
        //                {
        //                    ShutdownStartDateString = shutdownform + ".StartDate";
        //                    ShutdownEndDateString = shutdownform + ".EndDate";
        //                    ShutdownFormID = Convert.ToInt32(shutDownFormTypeID[x]);
        //                    return shutdownform;
        //                }
        //            }
        //        }
        //    }
        //    return null;
        //}
        public void BindCalendar() {

        }

        private void calBusiness_DayRender(object sender, System.Web.UI.WebControls.DayRenderEventArgs e) {
            if (c_IsBizCal) {
                //if (FormID == ShutdownFormID && e.Day.Date >= ShutDownStartDate && e.Day.Date <= ShutDownEndDate)
                //{
                //    e.Day.IsSelectable = false;
                //    e.Cell.BackColor = calBusiness.WeekendDayStyle.BackColor;
                //    e.Cell.Font.Strikeout = calBusiness.WeekendDayStyle.Font.Strikeout;
                //    e.Cell.Font.Underline = false; 
                //}else
                if ((!e.Day.IsWeekend) && (e.Day.Date >= DateTime.Today)) {
                    int iIndex = dvCalendar.Find(e.Day.Date);
                    if (iIndex != -1) {
                        DataRow row = dvCalendar[iIndex].Row;

                        if (!row.IsNull(dataDef.FLD_IS_HOLIDAY)) {
                            bool IsHoliday = Convert.ToBoolean(row[dataDef.FLD_IS_HOLIDAY]);

                            if (IsHoliday) {
                                e.Day.IsSelectable = false;
                                e.Cell.BackColor = calBusiness.WeekendDayStyle.BackColor;
                                e.Cell.Font.Strikeout = calBusiness.WeekendDayStyle.Font.Strikeout;
                                e.Cell.Font.Underline = false;
                            }
                            else {
                                if (!row.IsNull(dataDef.FLD_NB_DAY_LEAD_TIME)) {
                                    int DayLeadTime = Convert.ToInt32(row[dataDef.FLD_NB_DAY_LEAD_TIME]);
                                    if (DayLeadTime <= -1) {
                                        e.Day.IsSelectable = false;
                                        e.Cell.BackColor = calBusiness.WeekendDayStyle.BackColor;
                                        e.Cell.Font.Strikeout = calBusiness.WeekendDayStyle.Font.Strikeout;
                                        e.Cell.Font.Underline = false;
                                    }
                                    else {
                                        //Apply javascript for closing window										
                                        if (c_IsSelector) {
                                            clsUtil.SetJScriptForCloseCalendar(e.Cell, e.Day.Date.ToShortDateString(), DayLeadTime.ToString(), IDRefCtrl, IDRefCtrl1);

                                            if (DayLeadTime < c_MinNbDayLeadTime) {
                                                e.Cell.BackColor = Color.Pink;
                                                e.Cell.Font.Underline = false;
                                            }
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                else {
                    e.Day.IsSelectable = false;
                    e.Cell.BackColor = calBusiness.WeekendDayStyle.BackColor;
                    e.Cell.Font.Strikeout = calBusiness.WeekendDayStyle.Font.Strikeout;
                    e.Cell.Font.Underline = false;
                }
            }
            else {
                //Apply javascript for closing window
                if (c_IsSelector) {
                    //					WebControl webCtl = (WebControl) e.Cell.Controls[0];					
                    clsUtil.SetJScriptForCloseCalendar(e.Cell, e.Day.Date.ToShortDateString(), IDRefCtrl);
                }
            }
        }

        private void calBusiness_VisibleMonthChanged(object sender, System.Web.UI.WebControls.MonthChangedEventArgs e) {
            if (c_IsBizCal) {
                LoadDataSource();
            }
        }

        protected void calBusiness_SelectionChanged(object sender, EventArgs e) {
            if (SelectionChanged != null) {
                SelectionChanged(sender, e);
            }
        }

        protected void calBusiness_Render(object sender, System.EventArgs e) {
        }

        private void SetJavascript() {
            string sMessage = "";
            sMessage = "This date does not meet minimum requirement.  Please modify.";
            StringBuilder strBuild = new StringBuilder();
            strBuild.Append("<script language=javascript>\n");
            strBuild.Append("<!--			\n");
            strBuild.Append("function SetInfo(iID, sName){\n");
            strBuild.Append("   if ((iID !=  null) && (opener !=  null)) {\n");
            strBuild.Append("		var bIsValid = true;   \n");
            strBuild.Append("		if (sName < " + c_MinNbDayLeadTime.ToString() + ") {\n");
            strBuild.Append("			bIsValid = window.confirm('" + sMessage + "'); \n");
            strBuild.Append("		}\n");
            strBuild.Append("		if (bIsValid) {\n");
            strBuild.Append("			var objValue = opener.document.getElementById('" + c_IDRefCtrl + "');    \n");
            strBuild.Append("  			if (objValue != null)		\n");
            strBuild.Append("  				objValue.value = iID;		\n");
            if (c_IDRefCtrl1.Length > 0) {
                strBuild.Append("			var objText = opener.document.getElementById('" + c_IDRefCtrl1 + "');    \n");
                strBuild.Append("  			if (objText != null)		\n");
                strBuild.Append("  				objText.innerHTML = (parseInt(sName) + 1);		\n");
                //strBuild.Append("  			alert(sName);		\n");
            }
            if (c_IDRefCtrl.Length > 0) {
                strBuild.Append("			var objValue1 = opener.document.getElementById('" + c_IDRefCtrl.Replace("StartDate", "EndDate") + "');    \n");
                strBuild.Append("  			if (objValue1 != null) {		\n");
                strBuild.Append("  				if (objValue1.value == '') 		\n");
                strBuild.Append("  					objValue1.value = iID;		\n");
                strBuild.Append("  			}		\n");

            }
            strBuild.Append("				window.close();		\n");
            strBuild.Append("		} else {\n");
            strBuild.Append("			alert('sName = ' + sName);alert(sName<3);alert('bIsValid = ' + bIsValid);   \n");
            strBuild.Append("		}\n");

            strBuild.Append("	} else {\n");
            strBuild.Append("		alert('Insufficient Information !');\n");
            strBuild.Append("	}\n");
            strBuild.Append("}\n");
            strBuild.Append("//-->\n");
            strBuild.Append("</script>");

            string sfctname = "SetInfo";
            if (!this.Page.IsClientScriptBlockRegistered(sfctname)) {
                this.Page.RegisterClientScriptBlock(sfctname, strBuild.ToString());
            }
        }
    }
}