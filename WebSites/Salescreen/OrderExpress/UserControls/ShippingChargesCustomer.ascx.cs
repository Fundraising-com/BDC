using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.OrderData;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for FormCatalogGroupList.
    /// </summary>
    public partial class ShippingChargesCustomer : BaseWebUserControl {
        protected ShipmentGroupTable dTblShipGrp = new ShipmentGroupTable();
        private int c_ParentID = 0;
        bool c_IsReadOnly = false;
        protected DataView DVPaymentAssignmentType;
        protected int c_EntityTypeID = 0;

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here				
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
                //BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            try {
                if (this.Visible) {
                    if (this.c_IsReadOnly) {
                        lblPaymentAssignmentType.Visible = true;
                        ddlPaymentAssignmentType.Visible = false;
                    }
                    else {
                        lblPaymentAssignmentType.Visible = false;
                        ddlPaymentAssignmentType.Visible = true;
                    }

                    #region Old code

                    // As of 2008-10-06
                    // This code ensures that only field support is able to edit this field
                    // This is no longer the case since, fsms can now choose qsp to pay

                    //if (this.c_IsReadOnly) 
                    //{
                    //    // noop
                    //}
                    //else 
                    //{
                    //    if (this.Page.Role < QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT)
                    //    {
                    //        this.c_IsReadOnly = true;
                    //    }
                    //    else 
                    //    {
                    //        this.c_IsReadOnly = false;
                    //    }
                    //}

                    //lblPaymentAssignmentType.Visible = c_IsReadOnly;
                    //ddlPaymentAssignmentType.Visible = !c_IsReadOnly;

                    #endregion
                }
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void BindForm() {
            FillDataTableForDropDownList();

            if (dTblShipGrp.Rows.Count > 0) {
                DataRow shipRow = dTblShipGrp.Rows[0];

                if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID)) {
                    ListItem lstItem = ddlPaymentAssignmentType.Items.FindByValue(shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID].ToString());
                    if (lstItem != null) {
                        ddlPaymentAssignmentType.ClearSelection();
                        lstItem.Selected = true;
                        lblPaymentAssignmentType.Text = ddlPaymentAssignmentType.SelectedItem.Text;
                    }
                }

                if (lblPaymentAssignmentType.Text.Trim().Length == 0) {
                    lblPaymentAssignmentType.Text = "None";
                }
            }

            //if (dTblShipGrp.Rows.Count > 0)
            //{
            //    DataRow shipRow = dTblShipGrp.Rows[0];
            //    if (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT)
            //    {
            //        if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID))
            //        {
            //            ListItem lstItem = ddlPaymentAssignmentType.Items.FindByValue(shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID].ToString());
            //            if (lstItem != null)
            //            {
            //                ddlPaymentAssignmentType.ClearSelection();
            //                lstItem.Selected = true;
            //                lblPaymentAssignmentType.Text = ddlPaymentAssignmentType.SelectedItem.Text;
            //            }                       
            //        }
            //    }                
            //    else
            //    {//When the user is an FSM -- If the field is empty 
            //        int PayAssignType = 0;
            //        if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID))
            //            PayAssignType = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID]);

            //        if (PayAssignType == QSPForm.Common.PaymentAssignmentType.NONE)
            //        {
            //            PayAssignType = QSPForm.Common.PaymentAssignmentType.PAY_BY_FSM;
            //        }
            //        ListItem lstItem = ddlPaymentAssignmentType.Items.FindByValue(PayAssignType.ToString());
            //        if (lstItem != null)
            //        {
            //            ddlPaymentAssignmentType.ClearSelection();
            //            lstItem.Selected = true;
            //            lblPaymentAssignmentType.Text = ddlPaymentAssignmentType.SelectedItem.Text;
            //        }  
            //    }
            //    if (lblPaymentAssignmentType.Text.Trim().Length == 0)
            //    {   
            //        lblPaymentAssignmentType.Text = "None";
            //    }
            //}

            #region Old code

            //FillDataTableForDropDownList();
            //if (dTblShipGrp.Rows.Count > 0)
            //{
            //    DataRow shipRow = dTblShipGrp.Rows[0];
            //    if (this.Page.Role >= QSPForm.Business.AuthSystem.ROLE_FIELD_SUPPORT)
            //    {
            //        if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID))
            //        {
            //            ListItem lstItem = ddlPaymentAssignmentType.Items.FindByValue(shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID].ToString());
            //            if (lstItem != null)
            //            {
            //                ddlPaymentAssignmentType.ClearSelection();
            //                lstItem.Selected = true;
            //                lblPaymentAssignmentType.Text = ddlPaymentAssignmentType.SelectedItem.Text;
            //            }                       
            //        }
            //    }                
            //    else
            //    {//When the user is an FSM -- If the field is empty 
            //        int PayAssignType = 0;
            //        if (!shipRow.IsNull(ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID))
            //            PayAssignType = Convert.ToInt32(shipRow[ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID]);

            //        if (PayAssignType == QSPForm.Common.PaymentAssignmentType.NONE)
            //        {
            //            PayAssignType = QSPForm.Common.PaymentAssignmentType.PAY_BY_FSM;
            //        }
            //        ListItem lstItem = ddlPaymentAssignmentType.Items.FindByValue(PayAssignType.ToString());
            //        if (lstItem != null)
            //        {
            //            ddlPaymentAssignmentType.ClearSelection();
            //            lstItem.Selected = true;
            //            lblPaymentAssignmentType.Text = ddlPaymentAssignmentType.SelectedItem.Text;
            //        }  

            //    }
            //    if (lblPaymentAssignmentType.Text.Trim().Length == 0)
            //    {   
            //        lblPaymentAssignmentType.Text = "None";
            //    }
            //}

            #endregion
        }

        public ShipmentGroupTable DataSource {
            get {
                return dTblShipGrp;

            }
            set {
                dTblShipGrp = value;
            }
        }

        public int ParentID {
            get {
                return c_ParentID;
            }
            set {
                c_ParentID = value;
            }
        }

        public bool IsReadOnly {
            get {
                return c_IsReadOnly;
            }
            set {
                c_IsReadOnly = value;
            }
        }

        private void FillDataTableForDropDownList() {
            try {
                //Customer
                CommonUtility clsUtil = new CommonUtility();
                //clsUtil.SetPaymentAssignmentTypeDropDownList(ddlPaymentAssignmentType);
                clsUtil.SetPaymentAssignmentTypeDropDownList(ddlPaymentAssignmentType, Page.Role);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                //if (!c_IsReadOnly)
                //{
                CommonUtility clsUtil = new CommonUtility();
                DataRow shipRow = dTblShipGrp.Rows[0];
                clsUtil.UpdateRow(shipRow, ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_ID, ddlPaymentAssignmentType.SelectedValue);

                string sPaymentAssignmentTypeName = "";
                if (ddlPaymentAssignmentType.SelectedValue != "") {
                    sPaymentAssignmentTypeName = ddlPaymentAssignmentType.SelectedItem.Text;
                }

                clsUtil.UpdateRow(shipRow, ShipmentGroupTable.FLD_SHIPPING_EXPEDITED_FREIGHT_CHARGES_PAYMENT_ASSIGNMENT_TYPE_NAME, sPaymentAssignmentTypeName);
                //}

                blnValid = true;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        public bool ValidateForm() {

            if (this.Visible) {
                CompVal_PaymentAssignmentType.Validate();
                return CompVal_PaymentAssignmentType.IsValid;
            }

            //if everything have been ok
            return true;
        }
    }
}