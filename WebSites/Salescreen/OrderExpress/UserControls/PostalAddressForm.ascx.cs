using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.PostalAddressEntityTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for CustomerInfo.
    /// </summary>
    public partial class PostalAddressForm : BaseWebUserControl {
        protected dataDef dtAddress = new dataDef();
        private int c_ParentID = 0;
        private int c_AddressID = 0;
        private int c_ParentType = 0;
        protected System.Web.UI.WebControls.ValidationSummary ValSum;
        private QSPForm.Business.PostalAddressSystem addrSys = new QSPForm.Business.PostalAddressSystem();
        protected System.Data.DataTable tblState = new DataTable();
        protected DataView DVState;
        protected System.Data.DataTable tblTypeAddress = new DataTable();
        protected DataView DVTypeAddress;
        protected System.Web.UI.HtmlControls.HtmlTable tblAddButton;
        bool c_IsReadOnly = false;
        bool c_HideButton = false;
        bool c_HideTypeAddress = false;
        bool c_HideTitleAddress = true;
        int c_FilterTypeAddress = 0;
        protected DataView DVAddress;

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
            this.dtLstAddress.DeleteCommand += new System.Web.UI.WebControls.DataListCommandEventHandler(this.dtLstAddress_DeleteCommand);
            this.dtLstAddress.ItemCreated += new System.Web.UI.WebControls.DataListItemEventHandler(this.dtLstAddress_ItemCreated);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);
        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList								
                BindDataList();
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
            //dtAddress = addrSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        private void BindDataList() {
            FillDataTableForDropDownList();
            DVAddress = new DataView(dtAddress);
            if (c_FilterTypeAddress > 0)
                DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + c_FilterTypeAddress.ToString();
            dtLstAddress.DataBind();
        }

        public int Count {
            get {
                return this.dtLstAddress.Items.Count;
            }
        }

        public PostalAddressEntityTable DataSource {
            get {
                return dtAddress;
            }
            set {
                dtAddress = value;
            }
        }

        public int AddressID {
            get {
                return c_AddressID;
            }
            set {
                c_AddressID = value;
            }
        }

        public RepeatDirection RepeatDirection {
            get {
                return dtLstAddress.RepeatDirection;
            }
            set {
                dtLstAddress.RepeatDirection = value;
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

        public int ParentType {
            //Identify on wich we have to do our operation
            //0= Nothing (direct to the postal address table)
            //1= Organization
            //2= Account
            //3= Campaign
            //4= Order
            get {
                return c_ParentType;
            }
            set {
                c_ParentType = value;
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

        public bool HideTitleAddress {
            get {
                return c_HideTitleAddress;
            }
            set {
                c_HideTitleAddress = value;
            }
        }

        public bool HideTypeAddress {
            get {
                return c_HideTypeAddress;
            }
            set {
                c_HideTypeAddress = value;
            }
        }

        public int FilterTypeAddress {
            get {
                return c_FilterTypeAddress;
            }
            set {
                c_FilterTypeAddress = value;
            }
        }

        protected int getSelectedIndex(DataTable dt, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    int iCounter = 0;
                    foreach (DataRow row in dt.Rows) {
                        if (sValue == row[0].ToString()) {
                            iIndex = iCounter;
                            break;
                        }
                        iCounter++;
                    }
                }
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
            return iIndex;
        }

        private void FillDataTableForDropDownList() {
            try {
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //State
                tblState = comSys.SelectAllUSState();
                DVState = new DataView(tblState);
                //Type Address				
                tblTypeAddress = comSys.SelectAllPostalAddressType();
                DVTypeAddress = new DataView(tblTypeAddress);
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                CommonUtility clsUtil = new CommonUtility();
                int iCounter = 0;
                //'We save everything that is possible
                DVAddress = new DataView(dtAddress);
                if (c_FilterTypeAddress > 0)
                    DVAddress.RowFilter = PostalAddressEntityTable.FLD_TYPE + " = " + c_FilterTypeAddress.ToString();

                for (iCounter = 0; iCounter <= dtLstAddress.Items.Count - 1; iCounter++) {
                    DataListItem dlstItem;
                    dlstItem = dtLstAddress.Items[iCounter];
                    int pkID = Convert.ToInt32(dtLstAddress.DataKeys[iCounter]);
                    DataRow row = dtAddress.Rows.Find(pkID);
                    //Table Mapping    
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_ID, c_ParentID.ToString());
                    //Entity Type
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ENTITY_TYPE_ID, c_ParentType.ToString());
                    //Address Type 
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_TYPE, ((DropDownList)dlstItem.FindControl("ddlType")).SelectedValue.Trim());
                    //Contact First Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_FIRST_NAME, ((TextBox)dlstItem.FindControl("txtFirstName")).Text.Trim());
                    //Contact First Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_LAST_NAME, ((TextBox)dlstItem.FindControl("txtLastName")).Text.Trim());
                    //Address Line 1
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ADDRESS1, ((TextBox)dlstItem.FindControl("txtAddressLine1")).Text.Trim());
                    //Address Line 2
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ADDRESS2, ((TextBox)dlstItem.FindControl("txtAddressLine2")).Text.Trim());
                    //City
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_CITY, ((TextBox)dlstItem.FindControl("txtCity")).Text.Trim());
                    //County
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_COUNTY, ((TextBox)dlstItem.FindControl("txtCounty")).Text.Trim());
                    //State
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_SUBDIVISION_CODE, ((DropDownList)dlstItem.FindControl("ddlState")).SelectedItem.Value.Trim());
                    //State Name
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_SUBDIVISION_NAME_1, ((DropDownList)dlstItem.FindControl("ddlState")).SelectedItem.Text.Trim());
                    //Zip Code
                    clsUtil.UpdateRow(row, PostalAddressEntityTable.FLD_ZIP, ((TextBox)dlstItem.FindControl("txtZip")).Text.Trim());

                    if (row.RowState != DataRowState.Unchanged) {
                        if (row.RowState == DataRowState.Added)
                            row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                        else
                            row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                    }
                }

                blnValid = true;
            }
            catch (Exception ex) {
                Page.SetPageError(ex);
            }

            return blnValid;
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            AddNew();
        }

        private void dtLstAddress_DeleteCommand(object source, System.Web.UI.WebControls.DataListCommandEventArgs e) {
            Delete(e.Item.ItemIndex);
        }

        public void Delete(int iIndex) {
            UpdateDataSource();
            int pkID = Convert.ToInt32(dtLstAddress.DataKeys[iIndex]);
            DataRow row = dtAddress.Rows.Find(pkID);

            if (row.RowState != DataRowState.Added)
                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
            row.Delete();
            BindDataList();
        }

        public void AddNew() {
            UpdateDataSource();
            dtAddress.Rows.Add(dtAddress.NewRow());
            BindDataList();
        }

        private void dtLstAddress_ItemCreated(object sender, System.Web.UI.WebControls.DataListItemEventArgs e) {
            if ((e.Item.ItemType == ListItemType.Item) || (e.Item.ItemType == ListItemType.AlternatingItem)) {
                ImageButton imgBtnDelete = (ImageButton)e.Item.FindControl("imgBtnDelete");
                if (imgBtnDelete != null) {
                    imgBtnDelete.Visible = (!c_HideButton);
                }

                HtmlTableRow htmlTblRowTypeAddress = (HtmlTableRow)e.Item.FindControl("htmlTblRowTypeAddress");
                if (htmlTblRowTypeAddress != null) {
                    htmlTblRowTypeAddress.Visible = !c_HideTypeAddress;
                }

                HtmlTableRow htmlTblRowTitleAddress = (HtmlTableRow)e.Item.FindControl("htmlTblRowTitleAddress");
                if (htmlTblRowTitleAddress != null) {
                    htmlTblRowTitleAddress.Visible = !c_HideTitleAddress;
                }
            }
        }
    }
}