using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.BusinessRuleTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CustomerInfo.
    /// </summary>
    public partial class BusinessRuleForm : BaseWebUserControl {
        protected dataDef dtBusinessRule = new dataDef();
        private int c_ParentID = 0;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private QSPForm.Business.BusinessRuleSystem bizSys = new QSPForm.Business.BusinessRuleSystem();
        protected System.Data.DataTable tblBizField = new DataTable();
        protected System.Data.DataTable tblOperator = new DataTable();
        protected DataTable tblFormSectionType = new DataTable();
        protected DataView DVFormSectionType;
        protected System.Web.UI.HtmlControls.HtmlTable tblAddButton;
        bool c_IsReadOnly = false;
        bool c_HideButton = false;
        protected DataView DVBizRule;
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
            this.imgBtnAddNew.Click += new System.Web.UI.ImageClickEventHandler(this.imgBtnAddNew_Click);
            this.dtLstBizRule.ItemCreated += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtLstBizRule_ItemCreated);
            this.dtLstBizRule.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtLstBizRule_DeleteCommand);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList								
                BindForm();
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        protected void Page_PreRender(object sender, System.EventArgs e) {
            try {
                imgBtnAddNew.Visible = !c_HideButton;
            }
            catch (Exception ex) {
                throw ex;
            }
        }

        public void LoadDataSet() {
            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.

            // Attempt to fill the temporary dataset.
            //dtBusinessRule = bizSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        public void BindForm() {
            FillDataTableForDropDownList();
            DVBizRule = new DataView(dtBusinessRule);
            DVBizRule.RowFilter = "ISNULL(" + dataDef.FLD_IS_FORM_PROPERTY + ",FALSE) = FALSE";
            dtLstBizRule.DataBind();
            BindPreDefBizRules();
        }

        public void BindPreDefBizRules() {
            //Account Sales History
            txtAccountSalesHistory_NbDayInterval.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.ACCOUNT_HISTORY_INTERVAL_NB_DAY);
            txtAccountSalesHistory_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.ACCOUNT_HISTORY_INTERVAL_MIN_TOTAL_AMOUNT);
            //Common Carrier Name
            txtCommonCarrierName.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.COMMON_CARRIER_NAME);

            //----------------------Product Section --------------------
            //Min Line Item Quantity
            txtProductSection_MinLineItemQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0);
            //Min total Quantity
            txtProductSection_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0);
            //Max total Quantity
            txtProductSection_MaxTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MAX_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0);
            //Min total Amount 
            txtProductSection_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0);
            //Max total Amount 
            txtProductSection_MaxTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MAX_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0);
            //Min Day of Lead Time 
            txtProductSection_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0);

            //----------------------Product Section 1
            //Min Line Item Quantity
            txtProductSection1_MinLineItemQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 1);
            //Min total Quantity
            txtProductSection1_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 1);
            //Min total Amount 
            txtProductSection1_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 1);
            //Min Day of Lead Time 
            txtProductSection1_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 1);

            //-----------------------Product Section 2
            //Min Line Item Quantity
            txtProductSection2_MinLineItemQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 2);
            //Min total Quantity
            txtProductSection2_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 2);
            //Min total Amount 
            txtProductSection2_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 2);
            //Min Day of Lead Time 
            txtProductSection2_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 2);

            //Product Section 3
            //Min Line Item Quantity
            txtProductSection3_MinLineItemQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 3);
            //Min total Quantity
            txtProductSection3_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 3);
            //Min total Amount 
            txtProductSection3_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 3);
            //Min Day of Lead Time 
            txtProductSection3_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 3);

            //----------------------Other Section --------------------
            //Min total Quantity
            txtOtherSection_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 0);
            //Min total Amount 
            txtOtherSection_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 0);
            //Min Day of Lead Time 
            txtOtherSection_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 0);

            //----------------------Other Section 1
            //Min total Quantity
            txtOtherSection1_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 1);
            //Min total Amount 
            txtOtherSection1_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 1);
            //Min Day of Lead Time 
            txtOtherSection1_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 1);

            //-----------------------Other Section 2
            //Min total Quantity
            txtOtherSection2_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 2);
            //Min total Amount 
            txtOtherSection2_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 2);
            //Min Day of Lead Time 
            txtOtherSection2_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 2);

            //-----------------------Other Section 3
            //Min total Quantity
            txtOtherSection3_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 3);
            //Min total Amount 
            txtOtherSection3_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 3);
            //Min Day of Lead Time 
            txtOtherSection3_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 3);

            //----------------------Supply Section --------------------
            //Min total Quantity
            txtSupplySection_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 0);
            //Min total Amount 
            txtSupplySection_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 0);
            //Min Day of Lead Time 
            txtSupplySection_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 0);

            //----------------------Supply Section 1
            //Min total Quantity
            txtSupplySection1_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 1);
            //Min total Amount 
            txtSupplySection1_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 1);
            //Min Day of Lead Time 
            txtSupplySection1_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 1);

            //-----------------------Supply Section 2
            //Min total Quantity
            txtSupplySection2_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 2);
            //Min total Amount 
            txtSupplySection2_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 2);
            //Min Day of Lead Time 
            txtSupplySection2_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 2);

            //-----------------------Supply Section 3
            //Min total Quantity
            txtSupplySection3_MinTotalQuantity.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 3);
            //Min total Amount 
            txtSupplySection3_MinTotalAmount.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 3);
            //Min Day of Lead Time 
            txtSupplySection3_MinNbDayLeadTime.Text = dtBusinessRule.GetFormPropertyValueString(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 3);
        }

        public int Count {
            get {
                return this.dtLstBizRule.Items.Count;
            }
        }

        public BusinessRuleTable DataSource {
            get {
                return dtBusinessRule;
            }
            set {
                dtBusinessRule = value;
            }
        }

        public RepeatDirection RepeatDirection {
            get {
                return dtLstBizRule.RepeatDirection;
            }
            set {
                dtLstBizRule.RepeatDirection = value;
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

        public int EntityTypeID {
            get {
                return c_EntityTypeID;
            }
            set {
                c_EntityTypeID = value;
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

        public bool HideButton {
            get {
                return c_HideButton;
            }
            set {
                c_HideButton = value;
            }
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    int iCount = 0;
                    foreach (DataRow row in dt.Rows) {
                        if (sValue == row[0].ToString()) {
                            iIndex = iCount;
                            break;
                        }
                        iCount++;
                    }
                }
            }
            catch (Exception ex) {
                this.Page.SetPageError(ex);
            }
            return iIndex;
        }

        private void FillDataTableForDropDownList() {
            try {
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //Logical Operator				
                tblOperator = comSys.SelectAllLogicalOperator();
                //Biz Field
                QSPForm.Business.BusinessFieldSystem bizFldSys = new QSPForm.Business.BusinessFieldSystem();
                if (c_EntityTypeID > 0)
                    tblBizField = bizFldSys.SelectAllByEntityTypeID(c_EntityTypeID);
                else
                    tblBizField = bizFldSys.SelectAll();

                //Section Type
                tblFormSectionType = comSys.SelectAllFormSectionType();
                DataRow newRow = tblFormSectionType.NewRow();
                newRow[0] = 0;
                newRow[1] = "---SELECT---";
                tblFormSectionType.Rows.InsertAt(newRow, 0);
                DVFormSectionType = new DataView(tblFormSectionType);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                //Predefined Business rules
                UpdatePreDefBizRules();
                int iCounter = 0;
                //'We save everything that is possible				
                for (iCounter = 0; iCounter <= dtLstBizRule.Items.Count - 1; iCounter++) {
                    DataListItem dlstItem;
                    dlstItem = dtLstBizRule.Items[iCounter];
                    DataView dv = new DataView(dtBusinessRule);
                    int ID = Convert.ToInt32(dtLstBizRule.DataKeys[iCounter]);
                    dv.Sort = dataDef.FLD_PKID;
                    int iIndex = dv.Find(ID);
                    if (iIndex != -1) {
                        DataRow row = dv[iIndex].Row;
                        if (row.RowState != DataRowState.Deleted) {
                            CommonUtility clsUtil = new CommonUtility();
                            //'Table Mapping                      
                            row[dataDef.FLD_FORM_ID] = c_ParentID;
                            row[dataDef.FLD_NAME] = ((TextBox)dlstItem.FindControl("txtName")).Text;
                            row[dataDef.FLD_LOGICAL_OPERATOR_ID] = ((DropDownList)dlstItem.FindControl("ddlOperator")).SelectedValue;
                            row[dataDef.FLD_FIELD_ID] = ((DropDownList)dlstItem.FindControl("ddlBizField")).SelectedValue;
                            row[dataDef.FLD_VALUE_TO_COMPARE] = ((TextBox)dlstItem.FindControl("txtValueToCompare")).Text;
                            //row[dataDef.FLD_MESSAGE] = ((TextBox) dlstItem.FindControl("txtMessage")).Text;
                            string sValue = ""; ;
                            //Form Section Type
                            DropDownList ddl = ((DropDownList)dlstItem.FindControl("ddlFormSectionType"));
                            if (ddl.SelectedIndex > 0)
                                sValue = ddl.SelectedValue;
                            else
                                sValue = "";
                            clsUtil.UpdateRow(row, dataDef.FLD_FORM_SECTION_TYPE_ID, sValue);

                            //Form Section Number
                            TextBox txt;
                            txt = ((TextBox)dlstItem.FindControl("txtSectionNumber"));
                            sValue = txt.Text;
                            clsUtil.UpdateRow(row, dataDef.FLD_FORM_SECTION_NUMBER, sValue);

                            if (row.RowState == DataRowState.Added)
                                row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                            else
                                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                        }
                    }
                }

                blnValid = true;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        public void UpdatePreDefBizRules() {
            //Account Sales History
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.ACCOUNT_HISTORY_INTERVAL_NB_DAY, txtAccountSalesHistory_NbDayInterval.Text, this.Page.UserID);
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.ACCOUNT_HISTORY_INTERVAL_MIN_TOTAL_AMOUNT, txtAccountSalesHistory_MinTotalAmount.Text, this.Page.UserID);
            //Common Carrier Name
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.COMMON_CARRIER_NAME, txtCommonCarrierName.Text, this.Page.UserID);

            //----------------------Product Section --------------------
            //Min Line Item Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0, txtProductSection_MinLineItemQuantity.Text, this.Page.UserID);
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0, txtProductSection_MinTotalQuantity.Text, this.Page.UserID);
            //Max total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MAX_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0, txtProductSection_MaxTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0, txtProductSection_MinTotalAmount.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MAX_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0, txtProductSection_MaxTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 0, txtProductSection_MinNbDayLeadTime.Text, this.Page.UserID);

            //----------------------Product Section 1
            //Min Line Item Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 1, txtProductSection1_MinLineItemQuantity.Text, this.Page.UserID);
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 1, txtProductSection1_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 1, txtProductSection1_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 1, txtProductSection1_MinNbDayLeadTime.Text, this.Page.UserID);

            //-----------------------Product Section 2
            //Min Line Item Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 2, txtProductSection2_MinLineItemQuantity.Text, this.Page.UserID);
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 2, txtProductSection2_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 2, txtProductSection2_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 2, txtProductSection2_MinNbDayLeadTime.Text, this.Page.UserID);

            //-----------------------Product Section 3
            //Min Line Item Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_LINE_ITEM_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 3, txtProductSection3_MinLineItemQuantity.Text, this.Page.UserID);
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 3, txtProductSection3_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 3, txtProductSection3_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.STANDARD_PRODUCT, 3, txtProductSection3_MinNbDayLeadTime.Text, this.Page.UserID);

            //----------------------Other Section --------------------
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 0, txtOtherSection_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 0, txtOtherSection_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 0, txtOtherSection_MinNbDayLeadTime.Text, this.Page.UserID);

            //----------------------Other Section 1
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 1, txtOtherSection1_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 1, txtOtherSection1_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 1, txtOtherSection1_MinNbDayLeadTime.Text, this.Page.UserID);

            //-----------------------Other Section 2
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 2, txtOtherSection2_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 2, txtOtherSection2_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 2, txtOtherSection2_MinNbDayLeadTime.Text, this.Page.UserID);

            //-----------------------Other Section 3
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 3, txtOtherSection3_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 3, txtOtherSection3_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.OTHER_PRODUCT, 3, txtOtherSection3_MinNbDayLeadTime.Text, this.Page.UserID);

            //----------------------Supply Section --------------------
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 0, txtSupplySection_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 0, txtSupplySection_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 0, txtSupplySection_MinNbDayLeadTime.Text, this.Page.UserID);

            //----------------------Supply Section 1
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 1, txtSupplySection1_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 1, txtSupplySection1_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 1, txtSupplySection1_MinNbDayLeadTime.Text, this.Page.UserID);

            //-----------------------Supply Section 2
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 2, txtSupplySection2_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 2, txtSupplySection2_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 2, txtSupplySection2_MinNbDayLeadTime.Text, this.Page.UserID);

            //-----------------------Supply Section 3
            //Min total Quantity
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_QUANTITY, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 3, txtSupplySection3_MinTotalQuantity.Text, this.Page.UserID);
            //Min total Amount 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_TOTAL_AMOUNT, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 3, txtSupplySection3_MinTotalAmount.Text, this.Page.UserID);
            //Min Day of Lead Time 
            dtBusinessRule.SetFormProperty(QSPForm.Common.FormProperty.MIN_NB_DAY_LEAD_TIME, QSPForm.Common.FormSectionType.SUPPLY_PRODUCT, 3, txtSupplySection3_MinNbDayLeadTime.Text, this.Page.UserID);
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            AddNew();
        }

        private void dtLstBizRule_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e) {
            Delete(e.Item.ItemIndex);
        }

        public void Delete(int iItemIndex) {
            UpdateDataSource();
            DataView dv = new DataView(dtBusinessRule);
            int ID = Convert.ToInt32(dtLstBizRule.DataKeys[iItemIndex]);
            dv.Sort = dataDef.FLD_PKID;
            int iIndex = dv.Find(ID);
            if (iIndex != -1) {
                DataRow row = dv[iIndex].Row;
                if (row.RowState != DataRowState.Deleted) {
                    if (row.RowState != DataRowState.Added)
                        row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;

                    row.Delete();

                }
            }
            BindForm();
        }

        public void AddNew() {
            UpdateDataSource();
            dtBusinessRule.Rows.Add(dtBusinessRule.NewRow());
            BindForm();
        }

        private void dtLstBizRule_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {

                ImageButton imgBtnDelete = (ImageButton)e.Item.FindControl("imgBtnDelete");
                if (imgBtnDelete != null) {
                    imgBtnDelete.Visible = (!c_HideButton);
                }
            }
        }
    }
}