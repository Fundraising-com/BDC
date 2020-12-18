//===========================================================================
// This file was modified as part of an ASP.NET 2.0 Web project conversion.
// The class name was changed and the class modified to inherit from the abstract base class 
// in file 'App_Code\Migrated\Stub_BusinessCalendar_aspx_cs.cs'.
// During runtime, this allows other classes in your web application to bind and access 
// the code-behind page using the abstract base class.
// The associated content page 'BusinessCalendar.aspx' was also modified to refer to the new class name.
// For more information on this code pattern, please refer to http://go.microsoft.com/fwlink/?LinkId=46995 
//===========================================================================
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
using QSPForm.Common.DataDef;
using QSPForm.Business;
using dataDef = QSPForm.Common.DataDef.BusinessCalendarTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web {
    /// <summary>
    /// Summary description for Business Calendar.
    /// </summary>
    public partial class BusinessCalendar : BaseBusinessCalendar {
        private String IDRefCtrl = "";
        private String IDRefCtrl1 = "";
        private DateTime OrderDate = DateTime.Today;
        private bool IsBizCal = false;
        private DateTime CalDate = DateTime.Today;
        private int WarehouseID = 0;
        private int MinNbDayLeadTime = 0;
        private int formID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            GetQueyParameter();
        }

        #region Web Form Designer generated code
        override protected void OnInit(EventArgs e) {
            //
            // CODEGEN: This call is required by the ASP.NET Web Form Designer.
            //
            InitializeComponent();
            base.OnInit(e);
        }

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {

        }
        #endregion

        protected void Page_PreRender(object sender, System.EventArgs e) {

        }

        //		public void GetQueyParameter()
        override public void GetQueyParameter() {
            if (Request["IDRefCtrl"] != null) {
                IDRefCtrl = Request["IDRefCtrl"].ToString();
                BizCalendar_Ctrl.IDRefCtrl = IDRefCtrl;
            }

            if (Request["NameRefCtrl"] != null) {
                IDRefCtrl1 = Request["NameRefCtrl"].ToString();
                BizCalendar_Ctrl.IDRefCtrl1 = IDRefCtrl1;
            }


            try {
                if (Request["formID"] != null) {
                    formID = Convert.ToInt32(Request["formID"]);
                }
            }
            catch (Exception ex) {
                formID = 0;
            }
            BizCalendar_Ctrl.FormID = formID;

            //Is Business Calendar
            try {

                if (Request["IsBizCal"] != null) {
                    IsBizCal = Convert.ToBoolean(Request["IsBizCal"]);
                }
            }
            catch (Exception ex) {
                IsBizCal = false;
            }
            BizCalendar_Ctrl.IsBizCal = IsBizCal;

            //Order Date
            try {
                if (Request["OrdDate"] != null) {
                    OrderDate = Convert.ToDateTime(Request["OrdDate"]);
                }
            }
            catch (Exception ex) {
                OrderDate = DateTime.Today;
            }
            BizCalendar_Ctrl.OrderDate = OrderDate;

            //WarehouseID
            try {
                if (Request["WarehouseID"] != null) {
                    WarehouseID = Convert.ToInt32(Request["WarehouseID"]);
                }
            }
            catch (Exception ex) {
                WarehouseID = 0;
            }
            BizCalendar_Ctrl.WarehouseID = WarehouseID;


            //MinNbDayLeadTime
            try {
                if (Request["MinNbDayLeadTime"] != null) {
                    MinNbDayLeadTime = Convert.ToInt32(Request["MinNbDayLeadTime"]);
                }
            }
            catch (Exception ex) {
                MinNbDayLeadTime = 0;
            }
            BizCalendar_Ctrl.MinNbDayLeadTime = MinNbDayLeadTime;

            try {
                if (!IsPostBack) {
                    DateTime calDate = DateTime.Today;

                    if (Request["CalDate"] != null) {
                        if (Request["CalDate"].ToString().Length > 0) {
                            calDate = Convert.ToDateTime(Request["CalDate"]);
                            BizCalendar_Ctrl.SelectedDate = calDate;
                        }
                    }


                }
            }
            catch (Exception ex) {
            }
        }
    }
}