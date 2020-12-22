using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI.WebControls;
using System.Web.UI.HtmlControls;
using QSPForm.Common.DataDef;
using dataDef = QSPForm.Common.DataDef.EmailEntityTable;
using QSP.OrderExpress.Web.Code;

namespace QSP.OrderExpress.Web.UserControls {
    /// <summary>
    ///		Summary description for Phone Number List.
    /// </summary>
    public partial class EmailAddressList : BaseWebUserControl {
        protected dataDef dtEmailAddress = new dataDef();
        private int c_ParentID;
        private int c_EmailID;
        private int c_ParentType;
        protected System.Web.UI.WebControls.Button btnAddNew;
        protected System.Data.DataTable tblTypeEmailAddress = new DataTable();
        protected System.Data.DataView dvTypeEmailAddress = new DataView();
        private QSPForm.Business.EmailAddressSystem emailSys = new QSPForm.Business.EmailAddressSystem();

        protected void Page_Load(object sender, System.EventArgs e) {
            // Put user code to initialize the page here					
            lblMessage.Text = "";
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
            this.dtgEmailAddress.DeleteCommand += new System.Web.UI.WebControls.DataGridCommandEventHandler(this.dtgEmailAddress_DeleteCommand);
            this.DataBinding += new System.EventHandler(this.Page_DataBinding);

        }
        #endregion

        protected void Page_DataBinding(object sender, System.EventArgs e) {
            try {
                //retreive data detail item for db
                //Init DataList				
                BindGrid();
            }
            catch (Exception ex) {
                lblMessage.Text = ex.Message;
            }
        }

        public int EmailID {
            get {
                return c_EmailID;
            }
            set {
                c_EmailID = value;
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

        public EmailEntityTable DataSource {
            get {
                return dtEmailAddress;

            }
            set {
                dtEmailAddress = value;
            }
        }

        public void LoadDataSource() {

            // Create a new dataset to hold the records returned from the call to FillDataSet.
            // A temporary dataset is used because filling the existing dataset would
            // require the databindings to be rebound.
            //dtEmailAddress = emailSys.SelectAllByEntityID(c_ParentType, c_ParentID);
        }

        private void BindGrid() {
            FillDataTableForDropDownList();
            this.dtgEmailAddress.DataBind();
        }

        private void imgBtnAddNew_Click(object sender, System.Web.UI.ImageClickEventArgs e) {
            UpdateDataSource();
            DataRow NewRow = dtEmailAddress.NewRow();
            dtEmailAddress.Rows.InsertAt(NewRow, 0);
            BindGrid();
        }

        private void FillDataTableForDropDownList() {
            try {
                QSPForm.Business.CommonSystem comSys = new QSPForm.Business.CommonSystem();
                //Type Address				
                tblTypeEmailAddress = comSys.SelectAllEmailType();
                dvTypeEmailAddress = new DataView(tblTypeEmailAddress);
                dvTypeEmailAddress.Sort = tblTypeEmailAddress.Columns[0].ColumnName;
            }
            catch (Exception ex) {
                lblMessage.Text = ex.Message;
            }
        }

        protected int getSelectedIndex(DataView dv, String sValue) {
            int iIndex = -1;
            try {
                if (sValue != "") {
                    iIndex = dv.Find(sValue);
                }
            }
            catch (Exception ex) {
                lblMessage.Text = ex.Message;
            }
            return iIndex;
        }

        private void dtgEmailAddress_DeleteCommand(object source, System.Web.UI.WebControls.DataGridCommandEventArgs e) {
            UpdateDataSource();
            int pkID = Convert.ToInt32(dtgEmailAddress.DataKeys[e.Item.ItemIndex]);
            DataRow row = dtEmailAddress.Rows.Find(pkID);
            //Don't need to update a row recently added who doesn't exist
            //really in the database
            if (row.RowState != DataRowState.Added)
                row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
            row.Delete();
            BindGrid();
        }

        public bool UpdateDataSource() {
            bool blnValid = false;

            try {
                CommonUtility clsUtil = new CommonUtility();
                int iCounter = 0;
                //'We save everything that is possible
                //Invalid 	
                for (iCounter = 0; iCounter <= dtgEmailAddress.Items.Count - 1; iCounter++) {
                    DataGridItem dgItem;
                    dgItem = dtgEmailAddress.Items[iCounter];
                    int pkID = Convert.ToInt32(dtgEmailAddress.DataKeys[iCounter]);
                    DataRow row = dtEmailAddress.Rows.Find(pkID);

                    //'Table Mapping                 
                    clsUtil.UpdateRow(row, dataDef.FLD_ENTITY_ID, c_ParentID.ToString());
                    clsUtil.UpdateRow(row, dataDef.FLD_ENTITY_TYPE_ID, c_ParentType.ToString());
                    clsUtil.UpdateRow(row, dataDef.FLD_TYPE, ((DropDownList)dgItem.FindControl("ddlType")).SelectedValue);
                    clsUtil.UpdateRow(row, dataDef.FLD_TYPE_NAME, ((DropDownList)dgItem.FindControl("ddlType")).SelectedItem.Text);
                    clsUtil.UpdateRow(row, dataDef.FLD_EMAIL_ADDRESS, ((TextBox)dgItem.FindControl("txtEmailAddress")).Text);
                    clsUtil.UpdateRow(row, dataDef.FLD_RECIPIENT_NAME, ((TextBox)dgItem.FindControl("txtRecipientName")).Text);
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

        private bool Update(DataGridItem dgItem) {
            bool IsSuccess = false;

            c_EmailID = Convert.ToInt32(dtgEmailAddress.DataKeys[dgItem.ItemIndex]);

            // get edited row values in grid
            DataRow row;
            if (c_EmailID <= 0) {
                row = dtEmailAddress.NewRow();
                row[dataDef.FLD_ENTITY_ID] = c_ParentID;
                row[dataDef.FLD_ENTITY_TYPE_ID] = c_ParentType;
                row[dataDef.FLD_TYPE] = ((DropDownList)dgItem.FindControl("ddlType")).SelectedValue;
                row[dataDef.FLD_TYPE_NAME] = ((DropDownList)dgItem.FindControl("ddlType")).SelectedItem.Text;
                row[dataDef.FLD_EMAIL_ADDRESS] = ((TextBox)dgItem.FindControl("txtEmailAddress")).Text;
                row[dataDef.FLD_RECIPIENT_NAME] = ((TextBox)dgItem.FindControl("txtRecipientName")).Text;

                row[dataDef.FLD_CREATE_USER_ID] = Page.UserID;
                dtEmailAddress.Rows.Add(row);
                IsSuccess = emailSys.Insert(dtEmailAddress);

            }
            else {
                dtEmailAddress = emailSys.SelectOne(c_ParentType, c_EmailID);
                if (dtEmailAddress.Rows.Count > 0) {
                    row = dtEmailAddress.Rows[0];
                    row[dataDef.FLD_ENTITY_ID] = c_ParentID;
                    row[dataDef.FLD_ENTITY_TYPE_ID] = c_ParentType;
                    row[dataDef.FLD_TYPE] = ((DropDownList)dgItem.FindControl("ddlType")).SelectedValue;
                    row[dataDef.FLD_TYPE_NAME] = ((DropDownList)dgItem.FindControl("ddlType")).SelectedItem.Text;
                    row[dataDef.FLD_EMAIL_ADDRESS] = ((TextBox)dgItem.FindControl("txtEmailAddress")).Text;
                    row[dataDef.FLD_RECIPIENT_NAME] = ((TextBox)dgItem.FindControl("txtRecipientName")).Text;

                    row[dataDef.FLD_UPDATE_USER_ID] = Page.UserID;
                    IsSuccess = emailSys.Update(dtEmailAddress);
                }
                else {
                    //If we can't find it is probably because someone delete it before
                    IsSuccess = true;
                }
            }

            return IsSuccess;
        }
    }
}