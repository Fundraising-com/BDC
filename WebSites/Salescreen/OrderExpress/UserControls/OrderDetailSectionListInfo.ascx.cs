using System;
using System.Collections;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrderDetailTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class OrderDetailSectionListInfo : BaseOrderDetailSectionList//BaseWebUserControl
    {
        protected dataDef dTblOrderDetail = new dataDef();
        protected FormSectionTable dTblFormSection = new FormSectionTable();
        protected DataView dvFormSection = new DataView();
        private OrderData dtsOrder = new OrderData();
        private int c_ParentID;
        private int c_FormSectionTypeID = 0;
        private int c_FormSectionNumber = 0;
        protected CatalogItemDetailTable tblCatalogItemDetail = new CatalogItemDetailTable();
        private int c_FormID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {

            // Put user code to initialize the page here				
            //			if (IsFirstLoad)
            //			{

            //			}
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
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public override void BindForm() {
            try {
                //retreive data detail item for db
                //Init DataList
                //BindRateInformation();
                QSPForm.Business.FormSystem frmSys = new QSPForm.Business.FormSystem();
                dTblFormSection = frmSys.SelectAllFormSectionByFormID(c_FormID, c_FormSectionTypeID);
                BindSectionList();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        //private void BindRateInformation()
        //{
        //    float profit = 0;
        //    DataRow ordRow = dtsOrder.OrderHeader.Rows[0];
        //    if (!ordRow.IsNull(OrderHeaderTable.FLD_PROFIT_RATE))
        //        profit = Convert.ToSingle(ordRow[OrderHeaderTable.FLD_PROFIT_RATE]);
        //    if (profit > 0)
        //    {
        //        lblProfitPercentage.Text = profit.ToString("P2");
        //    }
        //    else
        //    {
        //        lblProfitPercentage.Text = "N/A";
        //    }
        //    tblProfitRate.Visible = (profit > 0);
        //}

        protected void Page_PreRender(object sender, System.EventArgs e) {
        }

        public override int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public int FormSectionTypeID {
            get {
                return c_FormSectionTypeID;
            }
            set {
                c_FormSectionTypeID = value;
            }
        }

        public int FormSectionNumber {
            get {
                return c_FormSectionNumber;
            }
            set {
                c_FormSectionNumber = value;
            }
        }

        //public decimal ProfitRate
        //{
        //    get { return profitRate; }
        //    set { profitRate = value; }
        //}

        public override OrderData DataSource {
            get {
                return dtsOrder;
            }
            set {
                dtsOrder = value;
                dTblOrderDetail = dtsOrder.OrderDetail;
                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_FORM_ID))
                    c_FormID = Convert.ToInt32(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_FORM_ID]);
            }
        }

        private void BindSectionList() {
            dvFormSection.Table = dTblFormSection;
            string sFilter = "";
            string sFilterType = "";
            sFilterType = FormSectionTable.FLD_FORM_SECTION_TYPE_ID + " = " + c_FormSectionTypeID.ToString();
            dvFormSection.RowFilter = sFilterType;
            //Build Section
            //The section 1 is always visible
            bool IsSection1 = true;// dTblFormSection.IsContainFormSectionType(c_FormSectionTypeID, 1);
            tblSection1.Visible = IsSection1;
            if (IsSection1) {
                string sTitle = "Section 1:";
                sFilter = sFilterType + " AND ISNULL(" + FormSectionTable.FLD_FORM_SECTION_NUMBER + ", 0) <= 1";
                dvFormSection.RowFilter = sFilter;
                if (dvFormSection.Count > 0) {
                    DataRow frmSecRow = dvFormSection[0].Row;
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_FORM_SECTION_TITLE)) {
                        sTitle = frmSecRow[FormSectionTable.FLD_FORM_SECTION_TITLE].ToString();
                    }
                }
                OrderDetailListInfo_Section1.SectionTitle = sTitle;
                OrderDetailListInfo_Section1.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailListInfo_Section1.FormSectionNumber = 1;
                OrderDetailListInfo_Section1.DataSource = dTblOrderDetail;
                //OrderDetailListInfo_Section1.ProfitRate = this.ProfitRate;
                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_PROFIT_RATE)) {
                    OrderDetailListInfo_Section1.ProfitRate = Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE].ToString());
                }
                OrderDetailListInfo_Section1.BindForm();
            }
            bool IsSection2 = dTblFormSection.IsContainFormSectionType(c_FormSectionTypeID, 2);
            tblSection2.Visible = IsSection2;
            if (IsSection2) {
                string sTitle = "Section 2:";
                sFilter = sFilterType + " AND " + FormSectionTable.FLD_FORM_SECTION_NUMBER + " = 2";
                dvFormSection.RowFilter = sFilter;
                if (dvFormSection.Count > 0) {
                    DataRow frmSecRow = dvFormSection[0].Row;
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_FORM_SECTION_TITLE)) {
                        sTitle = frmSecRow[FormSectionTable.FLD_FORM_SECTION_TITLE].ToString();
                    }
                }
                OrderDetailListInfo_Section2.SectionTitle = sTitle;
                OrderDetailListInfo_Section2.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailListInfo_Section2.FormSectionNumber = 2;
                OrderDetailListInfo_Section2.DataSource = dTblOrderDetail;
                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_PROFIT_RATE)) {
                    OrderDetailListInfo_Section2.ProfitRate = Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE].ToString());
                }
                OrderDetailListInfo_Section2.BindForm();
            }
            bool IsSection3 = dTblFormSection.IsContainFormSectionType(c_FormSectionTypeID, 3);
            tblSection3.Visible = IsSection3;
            if (IsSection3) {
                string sTitle = "";
                sFilter = sFilterType + " AND " + FormSectionTable.FLD_FORM_SECTION_NUMBER + " = 3";
                dvFormSection.RowFilter = sFilter;
                if (dvFormSection.Count > 0) {
                    DataRow frmSecRow = dvFormSection[0].Row;
                    if (!frmSecRow.IsNull(FormSectionTable.FLD_FORM_SECTION_TITLE)) {
                        sTitle = frmSecRow[FormSectionTable.FLD_FORM_SECTION_TITLE].ToString();
                    }
                }
                OrderDetailListInfo_Section2.SectionTitle = sTitle;
                OrderDetailListInfo_Section3.FormSectionTypeID = c_FormSectionTypeID;
                OrderDetailListInfo_Section3.FormSectionNumber = 3;
                OrderDetailListInfo_Section3.DataSource = dTblOrderDetail;
                if (!dtsOrder.OrderHeader.Rows[0].IsNull(OrderHeaderTable.FLD_PROFIT_RATE)) {
                    OrderDetailListInfo_Section3.ProfitRate = Convert.ToDecimal(dtsOrder.OrderHeader.Rows[0][OrderHeaderTable.FLD_PROFIT_RATE].ToString());
                }
                OrderDetailListInfo_Section3.BindForm();
            }
            lblTotalQuantity.Text = dTblOrderDetail.GetTotalQuantity(c_FormSectionTypeID, c_FormSectionNumber).ToString();
            lblTotalAmount.Text = dTblOrderDetail.GetTotalAmount(c_FormSectionTypeID, c_FormSectionNumber).ToString("C");
        }

        public override int FormID {
            get {
                return c_FormID;
            }
            set {
                c_FormID = value;
            }
        }
    }
}