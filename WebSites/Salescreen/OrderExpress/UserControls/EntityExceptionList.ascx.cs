using System;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataRef = QSPForm.Common.DataDef.EntityExceptionTable;

using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Entity Exception List.
    /// </summary>
    public partial class EntityExceptionList : BaseWebUserControl {
        private bool isCHR = false;
        private bool isCHRExpeditedFreightNeeded = false;
        private int requiredLeadTime = 0;
        private int selectedLeadTime = 0;
        private int c_EntityID = 0;
        private int c_EntityTypeID = 0;
        protected DataSet dts = new DataSet();
        protected AccountData dtsAccount = new AccountData();
        protected dataRef dTblEntityException = new dataRef();
        protected DataView dvException = new DataView();
        protected DataView dvNote = new DataView();
        private QSPForm.Business.EntityExceptionSystem orderExceptionSys = new QSPForm.Business.EntityExceptionSystem();
        private bool c_AdvancedColumnVisible = false;
        private bool c_IsReadOnly = false;

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
            this.dtgEntityException.ItemCreated += new System.Web.UI.WebControls.DataGridItemEventHandler(this.dtgEntityException_ItemCreated);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here					

            if (!IsPostBack) {
                //				if (!c_DisplayApprovableException)
                //				{
                //					//Put Invisible the Approval columns when displaying note
                //					dtgEntityException.Columns[6].Visible = false;
                //					dtgEntityException.Columns[7].Visible = false;
                //				}
            }
        }

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList			
                BindForm();
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            try {
                dtgEntityException.Columns[6].Visible = c_AdvancedColumnVisible;
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public void BindForm() {
            try {
                //retreive data detail item for db
                //Init DataList

                BindGrid();
                ShippingChargesCustomerDetail.Visible = false;

                if (c_EntityTypeID == QSPForm.Common.EntityType.TYPE_ORDER_BILLING) {
                    if (IsCHR) {
                        if (IsCHRExpeditedFreightNeeded) {
                            ShippingChargesCustomerDetail.Visible = true;
                            ShippingChargesCustomerDetail.BindForm();
                        }
                    }
                    else {
                        //ShippingChargesCustomer
                        if (dTblEntityException.IsContainExceptionType((int)QSPForm.Common.BusinessExceptionType.Expedited_Freight_Charges)) {
                            ShippingChargesCustomerDetail.Visible = true;
                            ShippingChargesCustomerDetail.BindForm();
                        }
                    }
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
        }

        public bool ValidateForm() {
            return ShippingChargesCustomerDetail.ValidateForm();

        }

        public bool UpdateDataSource() {
            bool IsSuccess = true;

            if (!c_IsReadOnly) {
                if ((this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT) && (dtgEntityException.Items.Count > 0)) {
                    CommonUtility clsUtil = new CommonUtility();

                    // get edited row values in grid
                    //Re init
                    IsSuccess = false;
                    DataView dvFind = new DataView(dTblEntityException);
                    dvFind.Sort = dataRef.FLD_PKID;
                    for (int iCount = 0; iCount < dtgEntityException.Items.Count; iCount++) {
                        DataGridItem dgItem = dtgEntityException.Items[iCount];
                        int excID = Convert.ToInt32(dtgEntityException.DataKeys[iCount]);
                        int iIndex = -1;
                        iIndex = dvFind.Find(excID);
                        if (iIndex != -1) {
                            DataRow row = dvFind[iIndex].Row;
                            CheckBox chkApproved = ((CheckBox)dgItem.FindControl("chkApproved"));
                            bool approved = false;
                            if (chkApproved != null) {
                                approved = chkApproved.Checked;
                            }
                            clsUtil.UpdateRow(row, dataRef.FLD_APPROVED, approved.ToString());

                            if (row.RowState != DataRowState.Unchanged) {
                                if (row.RowState == DataRowState.Added) {
                                    row[dataRef.FLD_CREATE_USER_ID] = Page.UserID;
                                }
                                else {
                                    clsUtil.UpdateRow(row, dataRef.FLD_UPDATE_USER_ID, Page.UserID.ToString());
                                }
                            }
                        }

                        //Operation Sucessful
                        IsSuccess = true;
                    }
                    //if (c_EntityTypeID == QSPForm.Common.EntityType.TYPE_ORDER_BILLING)
                    //{
                    //    ShippingChargesCustomerDetail.UpdateDataSource();
                    //}
                }

                // CHR CODE
                if (c_EntityTypeID == QSPForm.Common.EntityType.TYPE_ORDER_BILLING) {
                    ShippingChargesCustomerDetail.UpdateDataSource();
                }
            }

            return IsSuccess;
        }

        private void BindGrid() {
            dvException = new DataView(dTblEntityException);
            dvException.RowFilter = dataRef.FLD_ENTITY_TYPE_ID + " = " + c_EntityTypeID.ToString()
                                    + " AND " + dataRef.FLD_ENTITY_ID + " = " + c_EntityID.ToString();

            #region CHR code

            // CHR CODE
            // TODO: Hardcoded logic for chr
            //try
            //{
            //    if (this.IsCHR)
            //    {
            //        foreach (DataRow row in dTblEntityException.Rows)
            //        {
            //            if (((string)row[4]).Trim() == "Minimum Day Lead Time Exception - Product")
            //            {
            //                if (this.IsCHRExpeditedFreightNeeded)
            //                {
            //                    // Modify the text
            //                    row[7] = "For Product Orders, <u>LESS</u> than " + this.RequiredLeadTime.ToString() + " Business Days cannot be guaranteed.";
            //                }
            //                else
            //                {
            //                    // Remove the exception
            //                    dTblEntityException.Rows.Remove(row);
            //                }
            //            }
            //        }
            //    }
            //}
            //catch (Exception ex)
            //{
            //}

            #endregion

            if (dvException.Count == 0) {
                string sEntityTypeName = "";
                if (c_EntityTypeID == QSPForm.Common.EntityType.TYPE_ACCOUNT) {
                    sEntityTypeName = "Account";
                }
                else if (c_EntityTypeID == QSPForm.Common.EntityType.TYPE_ORDER_BILLING) {
                    sEntityTypeName = "Order";
                }
                else if (c_EntityTypeID == QSPForm.Common.EntityType.TYPE_PROGRAM_AGREEMENT) {
                    sEntityTypeName = "Program Agreement";
                }
                string msg = sEntityTypeName + " has been validated.";

                lblBusinessRule.Text = msg;

                lblBusinessRule.ForeColor = Color.Blue;
                trResultListEmpty.Visible = true;
                trResultList.Visible = false;
            }
            else {
                trResultListEmpty.Visible = false;
                trResultList.Visible = true;
                this.dtgEntityException.DataBind();
            }
        }

        private void dtgEntityException_ItemCreated(object sender, DataGridItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                CheckBox chkApproved = (CheckBox)e.Item.FindControl("chkApproved");
                if (chkApproved != null) {
                    bool IsEnabled = false;
                    int EntityExceptionID = Convert.ToInt32(dtgEntityException.DataKeys[e.Item.ItemIndex]);
                    int ExceptionTypeID = 0;
                    DataView dvFind = new DataView(dTblEntityException);
                    dvFind.Sort = dataRef.FLD_PKID;
                    int iIndex = dvFind.Find(EntityExceptionID);
                    if (iIndex != -1) {
                        ExceptionTypeID = Convert.ToInt32(dvFind[iIndex][dataRef.FLD_EXCEPTION_TYPE_ID]);
                    }

                    if (ExceptionTypeID >= (int)QSPForm.Common.BusinessExceptionType.Approved_Exception &&
                        ExceptionTypeID < (int)QSPForm.Common.BusinessExceptionType.Standard_Exception) {
                        if (!c_IsReadOnly) {
                            IsEnabled = (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT);
                            chkApproved.Enabled = IsEnabled;
                        }
                    }
                    else {
                        chkApproved.Visible = false;
                        Label lblNonApp = (Label)e.Item.FindControl("lblNonApplicable");
                        lblNonApp.Visible = true;
                    }
                }
            }
        }

        public bool IsCHR {
            get {
                return this.isCHR;
            }
            set {
                this.isCHR = value;
            }
        }

        public bool IsCHRExpeditedFreightNeeded {
            get {
                return this.isCHRExpeditedFreightNeeded;
            }
            set {
                this.isCHRExpeditedFreightNeeded = value;
            }
        }

        public int RequiredLeadTime {
            get {
                return this.requiredLeadTime;
            }
            set {
                this.requiredLeadTime = value;
            }
        }

        public int SelectedLeadTime {
            get {
                return this.selectedLeadTime;
            }
            set {
                this.selectedLeadTime = value;
            }
        }

        public int EntityID {
            get {
                return c_EntityID;
            }
            set {
                c_EntityID = value;
            }
        }

        public int EntityTypeID {
            get {
                return c_EntityTypeID;
            }
            set {
                c_EntityTypeID = value;
            }
        }

        public bool AdvancedColumnVisible {
            get {
                return c_AdvancedColumnVisible;
            }
            set {
                c_AdvancedColumnVisible = value;
            }
        }

        public bool IsReadOnly {
            get {
                return c_IsReadOnly;
            }
            set {
                c_IsReadOnly = value;
                ShippingChargesCustomerDetail.IsReadOnly = value;
            }
        }

        public dataRef DataSource {
            get {
                return dTblEntityException;
            }
            set {
                dTblEntityException = value;
            }
        }

        public ShipmentGroupTable ShipmentGroup_DataSource {
            get {
                return ShippingChargesCustomerDetail.DataSource;
            }
            set {
                ShippingChargesCustomerDetail.DataSource = value;
            }
        }
    }
}